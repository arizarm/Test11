using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for RetrievalControl
/// </summary>
public class RetrievalControl
{
    static StationeryEntities context = new StationeryEntities();

    static List<Disbursement> disbList;
    static List<Retrieval> rList;
    static string searchWord;
    static List<Retrieval> searchList;
    static ReqisitionListItem item;


    public static List<Retrieval> DisplayRetrievalList()
    {
        return context.Retrievals.ToList();
    }

    public static List<Retrieval> DisplaySearch(string searchWord)
    {
        rList = context.Retrievals.ToList();
        foreach (Retrieval i in rList)
        {
            searchList = rList.Where(x => x.RetrievedDate.Value.ToLongDateString().ToLower().Contains(searchWord.ToLower()) || x.RetrievalID.ToString().Contains(searchWord) || x.RetrievedBy.ToString().Contains(searchWord)).ToList();
        }
        return searchList;
    }


    public static void CreateRetrievalListDetailItemList(string itemCode, List<string> itemCodeList)
    {
        RetrievalListDetailItem r;
        string bin = context.Items.Where(x => x.ItemCode == itemCode).Select(x => x.Bin).First().ToString();
        string description = context.Items.Where(x => x.ItemCode == itemCode).Select(x => x.Description).First().ToString();
        int totalRequestedQty = (int)(context.Disbursement_Item.Where(x => x.ItemCode.ToString().Equals(itemCode)).Select(x => x.TotalRequestedQty).First());
        r = new RetrievalListDetailItem(bin, description, totalRequestedQty, itemCode);
        RetrievalListDetailItemList.Add(r);
        itemCodeList.Add(itemCode);
    }


    static List<RetrievalListDetailItem> RetrievalListDetailItemList;
    static List<Retrieval> rlist;
    public static List<RetrievalListDetailItem> DisplayRetrievalListDetail(string retrievalId)
    {

        rlist = new List<Retrieval>();

        List<string> itemCodeList = new List<string>();
        RetrievalListDetailItemList = new List<RetrievalListDetailItem>();

        disbList = context.Disbursements.Include("Department").Include("Disbursement_Item").Where(x => x.RetrievalID.ToString().Equals(retrievalId)).ToList();

        RetrievalListDetailItem r;

        foreach (Disbursement d in disbList)
        {
            foreach (Disbursement_Item dI in d.Disbursement_Item)
            {
                string itemCode = dI.ItemCode;

                if (itemCodeList.Count() != 0)
                {
                    bool add = true;

                    foreach (string s in itemCodeList)
                    {
                        if (s == itemCode)
                        {
                            add = false;
                            r = RetrievalListDetailItemList.Where(x => x.ItemCode.Equals(s)).First();
                            r.TotalRequestedQty += (int)(context.Disbursement_Item.Where(x => x.DisbursementID == dI.DisbursementID && x.ItemCode.ToString().Equals(s)).Select(x => x.TotalRequestedQty).First());
                        }
                    }
                    if (add)
                    {
                        CreateRetrievalListDetailItemList(itemCode, itemCodeList);
                    }
                }
                else
                {
                    CreateRetrievalListDetailItemList(itemCode, itemCodeList);
                }
            }
        }

        return RetrievalListDetailItemList;
    }



    
    public static void SaveRetrieved(List<int> ActualQty)//////////////////////////////////////////////
    {
        Disbursement_Item di = new Disbursement_Item();
        List<Disbursement_Item> diList = new List<Disbursement_Item>();


        foreach (Disbursement d in disbList)
        {
            foreach (Disbursement_Item dI in d.Disbursement_Item)
            {
                foreach (int i in ActualQty)
                {
                    di.ActualQty = i;
                }
            }
        }

        //foreach (int i in ActualQty)
        //{
        //    di.ActualQty = i;
        //}
        // context.Disbursement_Item.Add(di);
        context.SaveChanges();
    }



    static int retrievalId;
    public static void AddRetrieval()
    {
        Retrieval r = new Retrieval();
        r.RetrievedBy = 1001;       //base on user session
        r.RetrievedDate = DateTime.Today;
        r.RetrievalStatus = "Pending";

        context.Retrievals.Add(r);
        context.SaveChanges();

        retrievalId = r.RetrievalID; // get auto increasement data after SaveChanges
    }

    static List<int> disbursementID = new List<int>();
    public static void AddDisbursement(List<int> requNo)
    {
        Disbursement d = new Disbursement();

        List<int> requestedBy = new List<int>(); //EmpID
        List<string> deptCode = new List<string>();

        foreach (int i in requNo)
        {
            requestedBy.Add((int)(context.Requisitions.Where(x => x.RequisitionID.Equals(i)).Select(x => x.RequestedBy).First()));
        }


        //foreach requestedBy get depcode
        foreach (int i in requestedBy)
        {
            string dC = context.Employees.Where(x => x.EmpID.Equals(i)).Select(x => x.DeptCode).First().ToString();

            if (deptCode.Count() != 0)
            {
                bool add = true;

                foreach (string s in deptCode)
                {
                    if (s == dC)
                    {
                        add = false;
                    }
                }
                if (add) deptCode.Add(dC);
            }
            else
            {
                deptCode.Add(dC);
            }
        }

        //foreach depcode add disbursement + disbDetail
        foreach (string i in deptCode)
        {
            //d.RetrievalID = context.Retrievals.Select(x => x.RetrievalID).LastOrDefault();
            d.RetrievalID = retrievalId;
            d.DeptCode = i;
            d.Status = "Pending";
            context.Disbursements.Add(d);
            context.SaveChanges();

            //add disbursement id into requisition table
            foreach (int j in requestedBy)
            {
                string unfilterDepCode = context.Employees.Where(x => x.EmpID.Equals(j)).Select(x => x.DeptCode).First().ToString();
                if (unfilterDepCode == i)
                {
                    int requisitionID = context.Requisitions.Where(x => x.RequestedBy == j).Select(x => x.RequisitionID).First();
                    Requisition r = context.Requisitions.Where(x => x.RequisitionID.Equals(requisitionID)).First();
                    r.DisbursementID = d.DisbursementID;
                    context.Requisitions.Add(r);
                }
            }
            disbursementID.Add(d.DisbursementID);////////////////////auto increasement disbursementID after SaveChanges
        }


        foreach (int i in disbursementID)
        {
            AddDisbursemen_Item(i);
        }


        //  int CollectionLocationID;
        //CollectionLocationID = context.Departments.Where(x => x.DeptCode.Equals(d.DeptCode)).Select(x => x.CollectionLocationID).First().ToString();

        //context.CollectionPoints.Where(x => x.CollectionLocationID.Equals(CollectionLocationID)).Select(x => x.DefaultCollectionTime).First().ToString();

        ////d.CollectionTime =  context.CollectionPoints.Where(x => x.CollectionLocationID.Equals(CollectionLocationID)).Select(x => x.DefaultCollectionTime).First().ToString
        //d.CollectionDate = DateTime.Today;

        //d.AccessCode =
    }

    public static void AddDisbursemen_Item(int disbursementID)
    //public static void AddDisbursemen_Item(List<int> requNo)
    {
        Disbursement_Item di;
        List<Disbursement_Item> diList = new List<Disbursement_Item>();

        List<int> requisitionIDList = new List<int>();
        requisitionIDList = context.Requisitions.Where(x => x.DisbursementID == disbursementID).Select(x => x.RequisitionID).ToList();
        List<Requisition_Item> Requisition_Item = new List<Requisition_Item>();

        foreach (int i in requisitionIDList)
        {
            Requisition_Item = context.Requisition_Item.Where(x => x.RequisitionID == i).ToList();
        }

        foreach (Requisition_Item r in Requisition_Item)
        {
            if (diList.Count != 0)
            {
                foreach (Disbursement_Item i in diList)
                {
                    if (i.ItemCode == r.ItemCode)
                    {
                        i.TotalRequestedQty += r.RequestedQty;
                    }
                }
            }
            else
            {
                di = new Disbursement_Item();
                di.DisbursementID = disbursementID;
                di.ItemCode = r.ItemCode;
                di.TotalRequestedQty = r.RequestedQty;
                context.Disbursement_Item.Add(di);
            }
            //di.DisbursementID = context.Disbursements.Select(x => x.DisbursementID).Last();
            //di.ItemCode = context.Requisition_Item.Where(x => x.RequisitionID == i).Select(x => x.ItemCode).First();
            context.SaveChanges();
        }

        //di.ActualQty =
        //di.Remarks =

    }


}