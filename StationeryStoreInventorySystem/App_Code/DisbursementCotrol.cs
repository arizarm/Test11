using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;

public class DisbursementCotrol
{
    //static StationeryEntities context = new StationeryEntities();

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
        disbursement = EFBroker_Disbursement.GetAllDisbursementList();

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
            collectionPoint = EFBroker_DeptEmployee.GetCollectionPointbyDeptCode(depCode);

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

        int disbIDInt = Convert.ToInt32(disbID);
        disbursementDetail = EFBroker_Disbursement.GetDisbursement_ItemsbyDisbID(disbIDInt);

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
        int disbIDInt = Convert.ToInt32(disbID);
        if (EFBroker_Disbursement.GetAccessCodebyDisbID(disbIDInt).Equals(accessCode))
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
        int disbIDInt = Convert.ToInt32(disbID);
        List<DateTime?> dates = new List<DateTime?>();
        List<string> dateList = new List<string>();
        dates = EFBroker_Requisition.GetDateTimeListbyDisbID(disbIDInt);
        foreach(DateTime d in dates)
        {
            if (d != null)
            {
                dateList.Add(d.ToLongDateString());
            }
        }
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
    public static Employee getDepRep(string depName)
    {
        return EFBroker_DeptEmployee.GetDeptRepByDeptCode(depName);
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
        int disbIDInt = Convert.ToInt32(disbID);
        return EFBroker_Disbursement.GetDisbursmentbyDisbID(disbIDInt);
    }


    //update Disbursement final actual quantity
    public static void UpdateDisbursementActualQty(List<int> actualQty)
    {
        int disbIDInt = Convert.ToInt32(disbID);
        EFBroker_Disbursement.UpdateDisbursementActualQty(disbIDInt, actualQty);
    }

    //update Disbursement Status
    public static void UpdateDisbursementStatus()
    {
        int disbIDInt = Convert.ToInt32(disbID);
        EFBroker_Disbursement.UpdateDisbursementStatus(disbIDInt);
    }

    //Add disbursement transaction to Stockcard 
    public static void AddStockCardTransaction()
    {
        string transactionType = "Disbursement";
        int transId = Convert.ToInt32(disbID);
        List<Disbursement_Item> d = EFBroker_Disbursement.GetDisbursement_ItemsbyDisbID(transId);

        string itemCode;
        int Qty;
        int balance;

        foreach (Disbursement_Item dI in d)
        {
            itemCode = dI.ItemCode;
            Qty = (int) dI.ActualQty;
            balance = (int)dI.Item.BalanceQty - Qty;
            

            StockCard sc = new StockCard();
            sc.ItemCode = itemCode;
            sc.TransactionType = transactionType;
            sc.Qty = Qty;
            sc.Balance = balance;
            sc.TransactionDetailID = transId;
            EFBroker_StockCard.AddStockTransaction(sc);
        }        
    }
}