using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Windows.Forms;


/// <summary>
/// Summary description for RetrievalControl
/// </summary>
public class RetrievalControl
{
    static StationeryEntities context = new StationeryEntities();

    static List<Disbursement> disbursementList;
    static List<Retrieval> retrievalList;
    static string searchWord;
    static List<Retrieval> searchList;

    static List<Requisition> requisitionList;

    static List<RetrievalListDetailItem> RetrievalListDetailItemList;

    static List<RetrievalShortfallItem> RetrievalShortfallItemList;

    static int retrievalId;


    public static void GenerateAccessCode()
    {
        Random r = new Random();

        foreach (Disbursement d in disbursementList)
        {
            int value = r.Next(1000, 9999);
            d.AccessCode = value.ToString();
            d.Status = "Ready";
            EFBroker_Disbursement.UpdateDisbursement(d);
        }
        return;
    }

    public static void SaveActualQtyBreakdownByDepartment(List<RetrievalShortfallItemSub> retrievalShortfallItemSubListOfList)
    {
        foreach (RetrievalShortfallItemSub rsub in retrievalShortfallItemSubListOfList)////////////////////////////
        {
            foreach (Disbursement d in disbursementList)/////////////////////
            {
                if (rsub.DeptCode == d.DeptCode)
                {
                    foreach (Disbursement_Item di in d.Disbursement_Item)//////////////////
                    {
                        if (rsub.ItemCode == di.ItemCode)
                        {
                            //find the correct Disbursement_Item to save
                            di.ActualQty = rsub.ActualQty;
                            EFBroker_Disbursement.UpdateDisbursementItem(di);
                        }
                    }
                }

            }
        }
        return;
    }

    static List<RetrievalShortfallItemSub> RetrievalShortfallItemSubList;
    public static List<RetrievalShortfallItemSub> DisplayRetrievalShortfallSub(string shortfallItemCode)
    {
       // List<string> shortfallItemCodeList = new List<string>();
        RetrievalShortfallItemSubList = new List<RetrievalShortfallItemSub>();

        int i = 0;
        foreach (Disbursement d in disbursementList)/////////////////////////////////////////////////
        {
            foreach (Requisition r in d.Requisitions)
            {
                ///////////////////////if only one deptName
                string deptName = d.Department.DeptName.ToString();
                string deptCode = d.Department.DeptCode.ToString();
                try///////////////////////////////
                {
                    int requestedQty = (int)context.Requisition_Item.Where(x => x.RequisitionID == r.RequisitionID && x.ItemCode.Equals(shortfallItemCode)).Select(x => x.RequestedQty).First();

                    //////////////////////actual Qty bind with avialable Qty
                    RetrievalShortfallItemSub rsfs = new RetrievalShortfallItemSub((DateTime)r.RequestDate, deptName, deptCode, requestedQty, 0, shortfallItemCode);
                    RetrievalShortfallItemSubList.Add(rsfs);
                    i++;
                }
                catch (Exception e)
                {
                    continue;
                }
            }
        }

        return RetrievalShortfallItemSubList;
    }



    public static void CreateCollectionPointItemList(Disbursement d, List<CollectionPointItem> collectionPointItemList, List<int> CollectionLocationIDList)
    {
        CollectionLocationIDList.Add((int)d.Department.CollectionLocationID);
        string collectionPoint = context.CollectionPoints.Where(x => x.CollectionLocationID == d.Department.CollectionLocationID).Select(x => x.CollectionPoint1.ToString()).First();
        string defaultCollectionTime = context.CollectionPoints.Where(x => x.CollectionLocationID == d.Department.CollectionLocationID).Select(x => x.DefaultCollectionTime.ToString()).First();
        CollectionPointItem c = new CollectionPointItem(collectionPoint, defaultCollectionTime);
        collectionPointItemList.Add(c);
    }
    public static List<CollectionPointItem> DisplayCollectionPoint(int retrievalId)
    {
        disbursementList = context.Disbursements.Include("Retrieval").Include("Department").Include("Disbursement_Item").Where(x => x.RetrievalID == retrievalId).ToList();

        List<CollectionPointItem> collectionPointItemList = new List<CollectionPointItem>();
        List<int> CollectionLocationIDList = new List<int>();

        foreach (Disbursement d in disbursementList)///same collect point from different DeptCode
        {
            if (CollectionLocationIDList.Count != 0)
            {
                bool add = true;
                foreach (int cID in CollectionLocationIDList)
                {
                    if (d.Department.CollectionLocationID == cID)
                    {
                        add = false;
                    }
                }
                if (add)
                {
                    CreateCollectionPointItemList(d, collectionPointItemList, CollectionLocationIDList);
                }
            }
            else
            {
                CreateCollectionPointItemList(d, collectionPointItemList, CollectionLocationIDList);
            }
        }
        return collectionPointItemList;
    }

    public static void SaveCollectionTimeAndDateToDisbursement(List<DateTime> dateList, List<string> timeList)//
    {
        int i = 0;
        foreach (Disbursement d in disbursementList)
        {
            if (d.DeptCode[i] == d.Department.DeptCode[i])
            {
                d.CollectionDate = dateList[i];
                d.CollectionTime = timeList[i];
            }
            i++;
        }
        context.SaveChanges();
    }

    public static List<RetrievalShortfallItem> CheckShortfall(List<int> ActualQty)///////////////////////////////////////////
    {
        RetrievalShortfallItemList = new List<RetrievalShortfallItem>();//////////////////////////////////////////////

        for (int i = 0; i < RetrievalListDetailItemList.Count; i++)
        {
            if (ActualQty[i] < RetrievalListDetailItemList[i].TotalRequestedQty)
            {
                RetrievalShortfallItem r = new RetrievalShortfallItem(RetrievalListDetailItemList[i].Description, ActualQty[i], RetrievalListDetailItemList[i].ItemCode);
                RetrievalShortfallItemList.Add(r);
            }
        }

        return RetrievalShortfallItemList;
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


    public static List<RetrievalListDetailItem> DisplayRetrievalListDetail(int rId)
    {

        retrievalId = rId;
        retrievalList = new List<Retrieval>();

        List<string> itemCodeList = new List<string>();
        RetrievalListDetailItemList = new List<RetrievalListDetailItem>();

        disbursementList = context.Disbursements.Include("Retrieval").Include("Department").Include("Disbursement_Item").Where(x => x.RetrievalID == retrievalId).ToList();

        RetrievalListDetailItem r;

        foreach (Disbursement d in disbursementList)
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


    public static void UpdateDisbursementNonShortfallItemActualQty(List<int> ActualQty)
    {
        int i = 0;
        foreach (RetrievalListDetailItem r in RetrievalListDetailItemList)
        {
            if (r.TotalRequestedQty == ActualQty[i])
            {
                foreach (Disbursement d in disbursementList)/////////////////////
                {
                    foreach (Disbursement_Item di in d.Disbursement_Item)//////////////////
                    {
                        if (di.TotalRequestedQty == ActualQty[i] && di.ItemCode == r.ItemCode)
                        {
                            //find the correct Disbursement_Item to save
                            di.ActualQty = ActualQty[i];
                            di.Disbursement.Retrieval.RetrievalStatus = "Retrieved";//////////////////////////
                        }
                    }
                }
            }
            i++;
        }
        context.SaveChanges();
    }

    public static List<Retrieval> DisplayRetrievalList()
    {
        return context.Retrievals.ToList();
    }

    public static List<Retrieval> DisplaySearch(string searchWord)
    {
        retrievalList = context.Retrievals.ToList();
        foreach (Retrieval i in retrievalList)
        {
            searchList = retrievalList.Where(x => x.RetrievedDate.Value.ToLongDateString().ToLower().Contains(searchWord.ToLower()) || x.RetrievalID.ToString().Contains(searchWord) || x.RetrievedBy.ToString().Contains(searchWord)).ToList();
        }
        return searchList;
    }

    public static int AddRetrieval()
    {
        Retrieval r = new Retrieval();
        r.RetrievedBy = 1001;       //base on user session
        r.RetrievedDate = DateTime.Today;
        r.RetrievalStatus = "Pending";

        context.Retrievals.Add(r);
        context.SaveChanges();

        retrievalId = r.RetrievalID; // get auto increasement data after SaveChanges
        return retrievalId;
    }


    public static void AddDisbursement(List<int> requNo)
    {
        List<int> disbursementID = new List<int>();
        Disbursement d = new Disbursement();
        List<int> requestedBy = new List<int>(); //EmpID
        List<string> deptCodeList = new List<string>();

        foreach (int i in requNo)
        {
            requestedBy.Add((int)(context.Requisitions.Where(x => x.RequisitionID.Equals(i)).Select(x => x.RequestedBy).First()));
        }

        //foreach requestedBy get depcode
        foreach (int i in requestedBy)
        {
            string dC = context.Employees.Where(x => x.EmpID.Equals(i)).Select(x => x.DeptCode).First().ToString();

            if (deptCodeList.Count() != 0)
            {
                bool add = true;

                foreach (string s in deptCodeList)
                {
                    if (s == dC)
                    {
                        add = false;
                    }
                }
                if (add) deptCodeList.Add(dC);
            }
            else
            {
                deptCodeList.Add(dC);
            }
        }

        //foreach depcode add disbursement + disbDetail
        foreach (string i in deptCodeList)
        {
            //add Disbursement
            d.RetrievalID = retrievalId;
            d.DeptCode = i;
            d.Status = "Pending";
            context.Disbursements.Add(d);
            context.SaveChanges();

            disbursementID.Add(d.DisbursementID);////////////////////auto increasement disbursementID after SaveChanges
        }
        foreach (int i in disbursementID)
        {
            string disbDep = context.Disbursements.Where(x => x.DisbursementID == i).Select(x => x.DeptCode).First();

            foreach (int no in requNo)
            {
                //update requisition table 
                Requisition r = new Requisition();
                r = context.Requisitions.Where(x => x.RequisitionID.Equals(no)).First();

                string dep = context.Employees.Where(x => x.EmpID == r.RequestedBy).Select(x => x.DeptCode).First();

                if (dep == disbDep)
                {
                    r.DisbursementID = i;
                    context.SaveChanges();
                }
            }
            AddDisbursemen_Item(i);
        }
    }


    static List<Requisition_Item> Requisition_ItemList;
    static Disbursement_Item disbursement_Item;
    static List<Disbursement_Item> Disbursement_ItemList;
    public static void AddDisbursemen_Item(int disbursementID)
    {
        List<Requisition_Item> Requisition_ItemListOfList = new List<Requisition_Item>();

        List<int> requisitionIDList = new List<int>();
        requisitionIDList = context.Requisitions.Where(x => x.DisbursementID == disbursementID).Select(x => x.RequisitionID).ToList();
        Disbursement_ItemList = new List<Disbursement_Item>();
        foreach (int rL in requisitionIDList)   ////////////////
        {
            Requisition_ItemList = context.Requisition_Item.Where(x => x.RequisitionID == rL).ToList();

            //foreach (Requisition_Item r in Requisition_ItemListOfList)
            foreach (Requisition_Item r in Requisition_ItemList)/////////////////////////////
            {
                if (Disbursement_ItemList.Count != 0)
                {
                    bool add = true;

                    foreach (Disbursement_Item i in Disbursement_ItemList)
                    {
                        if (i.ItemCode == r.ItemCode)
                        {
                            add = false;
                            i.TotalRequestedQty += r.RequestedQty;
                        }
                    }
                    if (add)
                    {
                        CreateDisbursementItemList(disbursementID, r);
                    }
                }
                else
                {
                    CreateDisbursementItemList(disbursementID, r);
                }
                context.SaveChanges();
            }
        }
    }

    public static void CreateDisbursementItemList(int disbursementID, Requisition_Item r)
    {
        disbursement_Item = new Disbursement_Item();
        disbursement_Item.DisbursementID = disbursementID;
        disbursement_Item.ItemCode = r.ItemCode;
        disbursement_Item.TotalRequestedQty = r.RequestedQty;
        context.Disbursement_Item.Add(disbursement_Item);
        Disbursement_ItemList.Add(disbursement_Item);
    }
}
