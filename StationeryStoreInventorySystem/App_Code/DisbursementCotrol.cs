using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DisbursementCotrol
/// </summary>
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


    public static List<DisbursementListItems> gvDisbursementPopulate()
    {
        //create new list
        disbursementListItemsList = new List<DisbursementListItems>();

        //get all disbursement data
        disbursement = context.Disbursements.ToList();

        //attributes to display
        string disbId;
        string depName;
        string collectionPoint;
        string collectionDate;
        string collectionTime;
        string depCode;
        int colId;

        //for each disbursement 
        for (int i = 0; i < disbursement.Count; i++)
        {
            //get disbursement id
            disbId = disbursement[i].DisbursementID.ToString();

            //get deparment name
            depCode = disbursement[i].DeptCode;
            depName = context.Departments.Where(x => x.DeptCode.Equals(depCode)).Select(x => x.DeptName).First().ToString();

            //get formatted collection date
            collectionDate = disbursement[i].CollectionDate.Value.ToLongDateString();

            //get collection time
            collectionTime = disbursement[i].CollectionTime.ToString();

            //get collection point
            colId = Convert.ToInt32(context.Departments.Where(x => x.DeptCode.Equals(depCode)).Select(x => x.CollectionLocationID).First().ToString());
            collectionPoint = context.CollectionPoints.Where(x => x.CollectionLocationID.Equals(colId)).Select(x => x.CollectionPoint1).First().ToString();

            //put all data to display class
            disbursementListItems = new DisbursementListItems(disbId, collectionDate, collectionTime, depName, collectionPoint);

            //add display data to list
            disbursementListItemsList.Add(disbursementListItems);
        }

        //return list to display
        return disbursementListItemsList;
    }

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
    
    public static List<DisbursementDetailListItems> gvDisbursementDetailPopulate()
    {
        //create new list
        disbursementDetailListItemsList = new List<DisbursementDetailListItems>();

        //get all disbursement detail data 
        disbursementDetail = context.Disbursement_Item.Where(x => x.DisbursementID.ToString().Equals(disbID)).ToList();

        //get list of requisition form inlucded in current disbursement
        List<int> reqId = new List<int>();
        reqId = context.Requisitions.Where(x => x.DisbursementID.ToString().Equals(disbID)).Select(x => x.RequisitionID).ToList();

        //attributes to display
        string itemDesc;
        int reqQty;
        int actualQty;
        string remarks;
        string itemCode;

        //for each disbursement detail items 
        foreach (Disbursement_Item disbDetails in disbursementDetail)
        {
            //get Item description
            itemCode = disbDetails.ItemCode;
            itemDesc = context.Items.Where(x => x.ItemCode.Equals(itemCode)).Select(x => x.Description).First().ToString();

            //get actual quantity
            actualQty = (int) disbDetails.ActualQty;

            //get remark
            remarks = disbDetails.Remarks;

            //get total requested quantity
            reqQty = (int)disbDetails.TotalRequestedQty;

            //put all data to display class
            disbursementDetailListItems = new DisbursementDetailListItems(itemDesc, reqQty, actualQty, remarks);

            //add display data to list
            disbursementDetailListItemsList.Add(disbursementDetailListItems);
        }        
        return disbursementDetailListItemsList;
    }

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
}