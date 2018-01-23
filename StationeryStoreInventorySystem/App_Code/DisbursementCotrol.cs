using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;

public class DisbursementCotrol
{
    static StationeryEntities context = new StationeryEntities();

    static string disbID;

    static List<Disbursement> disbursement = new List<Disbursement>();

    static DisbursementListItems disbursementListItems;
    static List<DisbursementListItems> disbursementListItemsList;

    static List<Disbursement_Item> disbursementDetail = new List<Disbursement_Item>();

    static DisbursementDetailListItems disbursementDetailListItems;
    static List<DisbursementDetailListItems> disbursementDetailListItemsList;


    //GET DISBURSEMENT LIST TO DISPLAY
    public static List<DisbursementListItems> gvDisbursementPopulate()
    {
        //create new list
        disbursementListItemsList = new List<DisbursementListItems>();

        //get all disbursement data
        disbursement = context.Disbursements.Include("Department").ToList();

        //attributes to display
        string disbId;
        string depName;
        string collectionPoint;
        string collectionDate;
        string collectionTime;
        string depCode;

        //for each disbursement 
        for (int i = 0; i < disbursement.Count; i++)
        {
            //get disbursement id
            disbId = disbursement[i].DisbursementID.ToString();

            //get deparment name
            depCode = disbursement[i].DeptCode;
            depName = disbursement[i].Department.DeptName;

            //get formatted collection date
            collectionDate = disbursement[i].CollectionDate.Value.ToLongDateString();

            //get collection time
            collectionTime = disbursement[i].CollectionTime.ToString();

            //get collection point
            collectionPoint = context.Departments.Include("CollectionPoint").Where(x => x.DeptCode.Equals(depCode)).Select(x => x.CollectionPoint.CollectionPoint1).First().ToString();

            //put all data to display class
            disbursementListItems = new DisbursementListItems(disbId, collectionDate, collectionTime, depName, collectionPoint);

            //add display data to list
            disbursementListItemsList.Add(disbursementListItems);
        }

        //return list to display
        return disbursementListItemsList;
    }

    //GET DISBURSEMENT DETAIL OBJECT TO DISPLAY
    public static DisbursementListItems DisbursementListItemsObj(string disbId)
    {
        disbID = disbId;

        //get selected disbursement data
        DisbursementListItems disbItem = null;
        if (disbursementListItems.DisbId == disbID)
        {
            disbItem = disbursementListItems;
        }
        return disbItem;
    }

    //GET DISBURSEMENT DETAIL LIST TO DISPLAY
    public static List<DisbursementDetailListItems> gvDisbursementDetailPopulate()
    {
        disbursementDetailListItemsList = new List<DisbursementDetailListItems>();

        disbursementDetail = context.Disbursement_Item.Include("Item").Where(x => x.DisbursementID.ToString().Equals(disbID)).ToList();

        string itemDesc;
        int reqQty;
        int actualQty;
        string remarks;
        string itemCode;

        foreach (Disbursement_Item disbDetails in disbursementDetail)
        {
            itemCode = disbDetails.ItemCode;
            itemDesc = disbDetails.Item.Description;
            actualQty = (int)disbDetails.ActualQty;
            remarks = disbDetails.Remarks;
            reqQty = (int)disbDetails.TotalRequestedQty;

            disbursementDetailListItems = new DisbursementDetailListItems(itemCode, itemDesc, reqQty, actualQty, remarks);

            disbursementDetailListItemsList.Add(disbursementDetailListItems);
        }
        return disbursementDetailListItemsList;
    }

    //VERIFY ACCESS CODE
    public static bool checkAccessCode(string accessCode)
    {
        if ((context.Disbursements.Where(x => x.DisbursementID.ToString().Equals(disbID)).Select(x => x.AccessCode).First().ToString()).Equals(accessCode))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //Get earliest date for regenerate requisition
    public static DateTime getRegenrateDate()
    {
        List<string> dateList = new List<string>();

        dateList = context.Requisitions.Where(x => x.DisbursementID.ToString().Equals(disbID)).OrderBy(x=> x.RequestDate).Select(x => x.RequestDate.ToString()).ToList();

        DateTime inputDate = new DateTime();
        DateTime earliestDate = new DateTime();

        foreach (string dateString in dateList)
        {
            inputDate = DateTime.Parse(dateString);

            if (earliestDate.ToString().Equals("1/1/0001 12:00:00 AM"))
            {
                earliestDate = DateTime.Parse(dateString);
            }
            else
            {
                if (inputDate.CompareTo(earliestDate) < 0)
                {
                    earliestDate = inputDate;
                }
            }
        }
        return earliestDate;
    }


    //get Department Representative Name by Department Name
    public static string getDepRep(string depName)
    {
        return context.Employees.Include("Department").Where(x => x.Department.DeptName.Equals(depName) && x.Role.Equals("Representative")).Select(x => x.EmpName).First().ToString();
    }

    //get Employee ID by Employee Name
    public static int getEmpIdbyEmpName(string empName)
    {
        return context.Employees.Where(x => x.EmpName.Equals(empName)).Select(x => x.EmpID).First();
    }

    //ADD REQUISITION ITEM
    public static void addItemToRequisition(string code, int qty, int id)
    {
        using (StationeryEntities context = new StationeryEntities())
        {
            Requisition_Item ri = new Requisition_Item();
            ri.RequisitionID = id;
            ri.ItemCode = code;
            ri.RequestedQty = qty;
            context.Requisition_Item.Add(ri);
            context.SaveChanges();
        }
    }

    //Get Current Disbursement
    public static Disbursement GetCurrentDisbursement()
    {
        return context.Disbursements.Where(x => x.DisbursementID.ToString().Equals(disbID)).First();
    }


    //update Disbursement final actual quantity
    public static void UpdateDisbursementActualQty(List<int> ActualQty)
    {
        Disbursement d = GetCurrentDisbursement();

        int i = 0;
        foreach (Disbursement_Item di in d.Disbursement_Item)
        {
            di.ActualQty = ActualQty[i];
            i++;        }
        context.SaveChanges();
    }

    //update Disbursement Status
    public static void UpdateDisbursementStatus()
    {
        Disbursement d = GetCurrentDisbursement();
        d.Status = "Completed";
        context.SaveChanges();
    }

    //Add disbursement transaction to Stockcard 
    public static void AddStockCardTransaction()
    {
        Disbursement d = GetCurrentDisbursement();
        string transactionType = "Disbursement";
        int transId = Convert.ToInt32(disbID);

        string itemCode;
        int Qty;
        int balance;

        foreach (Disbursement_Item dI in d.Disbursement_Item)
        {
            itemCode = dI.ItemCode;
            Qty = (int) dI.ActualQty;
            balance = (int)context.Items.Where(x => x.ItemCode.Equals(itemCode)).Select(x => x.BalanceQty).First() - Qty;

            StockCard sc = new StockCard();
            sc.ItemCode = itemCode;
            sc.TransactionType = transactionType;
            sc.Qty = Qty;
            sc.Balance = balance;
            sc.TransactionDetailID = transId;
            context.StockCards.Add(sc);
            context.SaveChanges();
        }        
    }
}