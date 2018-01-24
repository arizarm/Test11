using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;

public class DisbursementCotrol
{     
    int disbID;    

    //GET DISBURSEMENT LIST TO DISPLAY
    public List<DisbursementListItems> gvDisbursementPopulate()
    {
        List<DisbursementListItems> disbursementListItemsList = new List<DisbursementListItems>();

        //get all disbursement data
        List<Disbursement> disbursement = EFBroker_Disbursement.GetAllDisbursementList();   

        //get dep detail for each disbursement 
        foreach(Disbursement d in disbursement)
        {
            int disbId = d.DisbursementID;
            string depCode = d.DeptCode;
            string depName = d.Department.DeptName;
            string collectionDate = d.CollectionDate.Value.ToLongDateString();
            string collectionTime = d.CollectionTime.ToString();
            string collectionPoint = EFBroker_DeptEmployee.GetCollectionPointbyDeptCode(depCode);

            //put all data to display class
            DisbursementListItems disbursementListItems = new DisbursementListItems(disbId, depCode, depName, collectionDate, collectionTime, collectionPoint);

            //add display data to list
            disbursementListItemsList.Add(disbursementListItems);
        }        
        return disbursementListItemsList;
    }

    //GET DISBURSEMENT DETAIL OBJECT TO DISPLAY
    public DisbursementListItems DisbursementListItemsObj(int disbId)
    {
        disbID = disbId;

        Disbursement disb = EFBroker_Disbursement.GetDisbursmentbyDisbID(disbID);
        
        string depCode = disb.DeptCode;
        string depName = disb.Department.DeptName;
        string collectionDate = disb.CollectionDate.Value.ToLongDateString();
        string collectionTime = disb.CollectionTime.ToString();
        string collectionPoint = EFBroker_DeptEmployee.GetCollectionPointbyDeptCode(depCode);

        //put all data to display class
        DisbursementListItems disbursementListItems = new DisbursementListItems(disbId, depCode, depName, collectionDate, collectionTime, collectionPoint);
                
        return disbursementListItems;
    }

    //GET DISBURSEMENT DETAIL LIST TO DISPLAY
    public List<DisbursementDetailListItems> gvDisbursementDetailPopulate()
    {
        List<DisbursementDetailListItems> disbursementDetailListItemsList = new List<DisbursementDetailListItems>();
        
        List<Disbursement_Item> disbursementDetail = new List<Disbursement_Item>();

        disbursementDetail = EFBroker_Disbursement.GetDisbursement_ItemsbyDisbID(disbID);        

        foreach (Disbursement_Item disbDetails in disbursementDetail)
        {
            string itemCode = disbDetails.ItemCode;
            string itemDesc = disbDetails.Item.Description;
            int actualQty = (int)disbDetails.ActualQty;
            string remarks = disbDetails.Remarks;
            int reqQty = (int)disbDetails.TotalRequestedQty;

            DisbursementDetailListItems disbursementDetailListItems = new DisbursementDetailListItems(itemCode, itemDesc, reqQty, actualQty, remarks);

            disbursementDetailListItemsList.Add(disbursementDetailListItems);
        }
        return disbursementDetailListItemsList;
    }

    //VERIFY ACCESS CODE
    public bool checkAccessCode(int disbId, string accessCode)
    {
        disbID = disbId;

        if (EFBroker_Disbursement.GetAccessCodebyDisbID(disbId).Equals(accessCode))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //Get earliest date for regenerate requisition
    public DateTime getRegenrateDate()
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
    public Employee getDepRep(string depName)
    {
        return EFBroker_DeptEmployee.GetDeptRepByDeptCode(depName);
    }


    //ADD REQUISITION ITEM
    public void addItemToRequisition(string code, int qty, int id)
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
    public Disbursement GetCurrentDisbursement()
    {
        int disbIDInt = Convert.ToInt32(disbID);
        return EFBroker_Disbursement.GetDisbursmentbyDisbID(disbIDInt);
    }


    //update Disbursement final actual quantity
    public void UpdateDisbursementActualQty(List<int> actualQty)
    {
        int disbIDInt = Convert.ToInt32(disbID);
        EFBroker_Disbursement.UpdateDisbursementActualQty(disbIDInt, actualQty);
    }

    //update Disbursement Status
    public void UpdateDisbursementStatus()
    {
        int disbIDInt = Convert.ToInt32(disbID);
        EFBroker_Disbursement.UpdateDisbursementStatus(disbIDInt);
    }

    //Add disbursement transaction to Stockcard 
    public void AddStockCardTransaction()
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