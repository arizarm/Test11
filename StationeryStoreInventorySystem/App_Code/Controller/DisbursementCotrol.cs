using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;

public class DisbursementCotrol
{
    //GET DISBURSEMENT LIST TO DISPLAY
    public List<DisbursementListItems> GvDisbursementPopulate()
    {
        List<DisbursementListItems> disbursementListItemsList = new List<DisbursementListItems>();

        //get all disbursement data
        List<Disbursement> disbursement = EFBroker_Disbursement.GetAllDisbursementList();

        //get dep detail for each disbursement 
        foreach (Disbursement d in disbursement)
        {
            //Set DisbursementListItem Details
            DisbursementListItems disbursementListItems = CreateDisbursementListItem(d);
            //add display data to list
            disbursementListItemsList.Add(disbursementListItems);
        }
        return disbursementListItemsList;
    }

    //GET DISBURSEMENT DETAIL OBJECT TO DISPLAY
    public DisbursementListItems DisbursementListItemsObj(int disbId)
    {
        Disbursement disb = EFBroker_Disbursement.GetDisbursmentbyDisbID(disbId);

        return CreateDisbursementListItem(disb);
    }
    ////Set DisbursementListItem Details
    public DisbursementListItems CreateDisbursementListItem(Disbursement disb)
    {
        string depCode = disb.DeptCode;
        string depName = disb.Department.DeptName;
        string collectionDate = disb.CollectionDate.Value.ToLongDateString();
        string collectionTime = disb.CollectionTime.ToString();
        string collectionPoint = EFBroker_DeptEmployee.GetCollectionPointbyDeptCode(disb.DeptCode).CollectionPoint1;
        DisbursementListItems disbursementListItems = new DisbursementListItems(disb.DisbursementID, collectionDate, collectionTime, depCode, depName, collectionPoint);

        return disbursementListItems;
    }
    //GET DISBURSEMENT DETAIL LIST TO DISPLAY
    public List<DisbursementDetailListItems> GvDisbursementDetailPopulate(int disbId)
    {
        List<DisbursementDetailListItems> disbursementDetailListItemsList = new List<DisbursementDetailListItems>();

        List<Disbursement_Item> disbursementDetail = new List<Disbursement_Item>();

        disbursementDetail = EFBroker_Disbursement.GetDisbursement_ItemsbyDisbID(disbId);

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
    public bool CheckAccessCode(int disbId, string accessCode)
    {
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
    public DateTime GetRegenrateDate(int disbId)
    {
        return (DateTime)EFBroker_Requisition.GetEarliestReqDateTimebyDisbID(disbId);
    }


    //ADD REQUISITION ITEM
    public void AddItemToRequisition(string code, int qty, int id)
    {
        Requisition_Item ri = new Requisition_Item();
        ri.RequisitionID = id;
        ri.ItemCode = code;
        ri.RequestedQty = qty;
        EFBroker_Requisition.AddItemToRequisition(ri);
    }

    //Get Current Disbursement
    public Disbursement GetCurrentDisbursement(int disbId)
    {
        return EFBroker_Disbursement.GetDisbursmentbyDisbID(disbId);
    }


    //update Disbursement final actual quantity
    public void UpdateDisbursement(int disbId, List<int> actualQty, List<string> disbRemark)
    {
        //Update actual disbursement quantity
        EFBroker_Disbursement.UpdateDisbursementActualQty(disbId, actualQty, disbRemark);

        //Update Disbursement statu to "Closed"
        UpdateDisbursementStatus(disbId);

        //Add Disbursement transaction to StockCard
        AddStockCardTransaction(disbId);
    }

    //update Disbursement Status
    public void UpdateDisbursementStatus(int disbId)
    {
        EFBroker_Disbursement.UpdateDisbursementStatus(disbId);
        List<Requisition> requisitionList = EFBroker_Requisition.GetRequisitionListByDisbursementID(disbId);
        requisitionList.ForEach(r => r.Status = "Closed");
        EFBroker_Requisition.UpdateRequisitionList(requisitionList);
    }

    //Add disbursement transaction to Stockcard 
    public void AddStockCardTransaction(int disbId)
    {
        string transactionType = "Disbursement";
        List<Disbursement_Item> d = EFBroker_Disbursement.GetDisbursement_ItemsbyDisbID(disbId);

        string itemCode;
        int Qty;
        int balance;

        foreach (Disbursement_Item dI in d)
        {
            itemCode = dI.ItemCode;
            Qty = (int)dI.ActualQty;
            balance = (int)dI.Item.BalanceQty - Qty;

            StockCard sc = new StockCard();
            sc.ItemCode = itemCode;
            sc.TransactionType = transactionType;
            sc.Qty = Qty;
            sc.Balance = balance;
            sc.TransactionDetailID = disbId;
            EFBroker_StockCard.AddStockTransaction(sc);
        }
    }
}