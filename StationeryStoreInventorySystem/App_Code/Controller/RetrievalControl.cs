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

    List<RetrievalListDetailItem> RetrievalListDetailItemList = new List<RetrievalListDetailItem>();

    List<Disbursement_Item> Disbursement_ItemList = new List<Disbursement_Item>();

    int retrievalId;

    //Get all Retrieval List
    public List<Retrieval> DisplayRetrievalList()
    {
        return EFBroker_Disbursement.GetAllRetrievalList();
    }

    //Display retrieval by search keyword
    public List<Retrieval> DisplaySearch(string searchWord)
    {
        List<Retrieval> retrievalList = EFBroker_Disbursement.GetAllRetrievalList();

        List<Retrieval> searchList = new List<Retrieval>();

        foreach (Retrieval i in retrievalList)
        {
            searchList = retrievalList.Where(x => x.RetrievedDate.Value.ToLongDateString().ToLower().Contains(searchWord.ToLower()) || x.RetrievalID.ToString().Contains(searchWord) || x.RetrievedBy.ToString().Contains(searchWord)).ToList();
        }
        return searchList;
    }

    //Get Retrieval Detail By RetrievalID
    //public List<RetrievalListDetailItem> DisplayRetrievalListDetail(int rId)
    //{
    //    List<Retrieval> retrievalList = new List<Retrieval>();

    //    List<string> itemCodeList = new List<string>();

    //    List<Disbursement> disbursementList = context.Disbursements.Include("Retrieval").Include("Department").Include("Disbursement_Item").Where(x => x.RetrievalID == rId).ToList();

    //    RetrievalListDetailItem r;

    //    foreach (Disbursement d in disbursementList)
    //    {
    //        foreach (Disbursement_Item dI in d.Disbursement_Item)
    //        {
    //            string itemCode = dI.ItemCode;

    //            if (itemCodeList.Count() != 0)
    //            {
    //                bool add = true;

    //                foreach (string s in itemCodeList)
    //                {
    //                    if (s == itemCode)
    //                    {
    //                        add = false;
    //                        r = RetrievalListDetailItemList.Where(x => x.ItemCode.Equals(s)).First();
    //                        r.TotalRequestedQty += (int)(context.Disbursement_Item.Where(x => x.DisbursementID == dI.DisbursementID && x.ItemCode.ToString().Equals(s)).Select(x => x.TotalRequestedQty).First());
    //                    }
    //                }
    //                if (add)
    //                {
    //                    CreateRetrievalListDetailItemList(itemCode, itemCodeList);
    //                }
    //            }
    //            else
    //            {
    //                CreateRetrievalListDetailItemList(itemCode, itemCodeList);
    //            }
    //        }
    //    }
    //    return RetrievalListDetailItemList;
    //}

    ////Create new RetrievalListDetailItem
    //public void CreateRetrievalListDetailItemList(string itemCode, List<string> itemCodeList)
    //{
    //    RetrievalListDetailItem r;

    //    string bin = context.Items.Where(x => x.ItemCode == itemCode).Select(x => x.Bin).First().ToString();
    //    string description = context.Items.Where(x => x.ItemCode == itemCode).Select(x => x.Description).First().ToString();
    //    int totalRequestedQty = (int)(context.Disbursement_Item.Where(x => x.ItemCode.ToString().Equals(itemCode)).Select(x => x.TotalRequestedQty).First());

    //    r = new RetrievalListDetailItem(bin, description, totalRequestedQty, itemCode);
    //    RetrievalListDetailItemList.Add(r);
    //    itemCodeList.Add(itemCode);
    //}

    //-- test Code--
    //Get Retrieval Detail By RetrievalID
    public List<RetrievalListDetailItem> DisplayRetrievalListDetail(int rId)
    {
        List<Disbursement_Item> disbursementItemList = new List<Disbursement_Item>();
        List<RetrievalListDetailItem> retrievalItemList = new List<RetrievalListDetailItem>();
        HashSet<Item> itemSet = new HashSet<Item>();
        Dictionary<string, int> qtyCounter = new Dictionary<string, int>();
        using (StationeryEntities context = new StationeryEntities())
        {
            disbursementItemList = context.Disbursement_Item.Include("Item").Where(di => di.Disbursement.RetrievalID == rId).ToList();
        }
        foreach (Disbursement_Item di in disbursementItemList)
        {
            int qty;

            if (!qtyCounter.TryGetValue(di.ItemCode, out qty))
            {
                qty = 0;
                itemSet.Add(di.Item);
                qtyCounter.Add(di.ItemCode, qty);
            }
            qty += di.TotalRequestedQty ?? 0;
            qtyCounter[di.ItemCode] = qty;
        }
        foreach (Item item in itemSet)
        {
            RetrievalListDetailItem retrievalItem = new RetrievalListDetailItem(item.Bin, item.Description, qtyCounter[item.ItemCode], item.ItemCode);
            retrievalItemList.Add(retrievalItem);
        }
        return retrievalItemList;
    }

    //update actual qty for non-shortfall disbursement items when generate disbursement button clicked
    public void UpdateDisbursementNonShortfallItemActualQty(int rId, List<int> ActualQty, List<RetrievalListDetailItem> retDetailList)
    {
        RetrievalListDetailItemList = retDetailList;

        List<Disbursement> disbursementList = context.Disbursements.Include("Retrieval").Include("Department").Include("Disbursement_Item").Where(x => x.RetrievalID == rId).ToList();

        int i = 0;
        foreach (RetrievalListDetailItem r in RetrievalListDetailItemList)
        {
            if (r.TotalRequestedQty == ActualQty[i])
            {
                foreach (Disbursement d in disbursementList)
                {
                    foreach (Disbursement_Item di in d.Disbursement_Item)
                    {
                        if (di.TotalRequestedQty == ActualQty[i] && di.ItemCode == r.ItemCode)
                        {
                            di.ActualQty = ActualQty[i];
                            //don't use this, it makes the retrievalstatus ="Retrieved" even if one itemCode is processed.
                            //di.Disbursement.Retrieval.RetrievalStatus = "Retrieved";
                            //Item item = EFBroker_Item.GetItembyItemCode(di.ItemCode);
                            //item.BalanceQty -= ActualQty[i];//////////////////////////////////////////
                            //EFBroker_Item.UpdateItem(item);
                            UpdateItemRetrieval(di.ItemCode, ActualQty[i]);
                        }
                    }
                }
            }
            i++;
        }
        EFBroker_Disbursement.UpdateRetrievalStatus(rId);
        context.SaveChanges();
    }
    //// for testCode in RetrievalListDetail  FinalizeDisbursmentList_Click method
    //public RetrievalShortfallItem CreateShortfallItemActualQty( int actualQty, RetrievalListDetailItem detailItem)
    //{
    //    RetrievalShortfallItem item = new RetrievalShortfallItem(detailItem.Description, actualQty, detailItem.ItemCode);
    //    return item;
    //}
    public void UpdateItemRetrieval(string itemCode, int actualQty)
    {
        Item item = EFBroker_Item.GetItembyItemCode(itemCode);
        item.BalanceQty = item.BalanceQty - actualQty;
        EFBroker_Item.UpdateItem(item);
        return;
    }
    //Generate short fall item list when Genereate Disbursement is clicked
    public List<RetrievalShortfallItem> CheckShortfall(List<int> ActualQty)
    {
        List<RetrievalShortfallItem> RetrievalShortfallItemList = new List<RetrievalShortfallItem>();
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

    //populate shortfall data for sub gridview
    public List<RetrievalShortfallItemSub> DisplayRetrievalShortfallSub(int rId, string shortfallItemCode)
    {
        List<RetrievalShortfallItemSub> RetrievalShortfallItemSubList = new List<RetrievalShortfallItemSub>();

        List<Disbursement> disbursementList = context.Disbursements.Include("Retrieval").Include("Department").Include("Disbursement_Item").Where(x => x.RetrievalID == rId).ToList();

        int i = 0;

        foreach (Disbursement d in disbursementList)
        {
            foreach (Requisition r in d.Requisitions)
            {
                //if only one deptName
                string deptName = d.Department.DeptName.ToString();
                string deptCode = d.Department.DeptCode.ToString();
                try
                {
                    //int requestedQty = (int)context.Requisition_Item.Where(x => x.RequisitionID == r.RequisitionID && x.ItemCode.Equals(shortfallItemCode)).Select(x => x.RequestedQty).First();
                    int requestedQty = EFBroker_Requisition.FindReqItemsByReqIDItemID(r.RequisitionID, shortfallItemCode).RequestedQty ?? 0;

                    //actual Qty bind with avialable Qty
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

    public void GenerateAccessCode(int rId)
    {
        //List<Disbursement> disbursementList = context.Disbursements.Include("Retrieval").Include("Department").Include("Disbursement_Item").Where(x => x.RetrievalID == rId).ToList();
        List<Disbursement> disbursementList = EFBroker_Disbursement.GetDisbursmentListbyRetrievalID(rId);
        Random r = new Random();

        foreach (Disbursement d in disbursementList)
        {
            int value = r.Next(1000, 9999);
            d.AccessCode = value.ToString();
            d.Status = "Ready";
            EFBroker_Disbursement.UpdateDisbursement(d);
            //context.SaveChanges();
        }
        return;
    }

    public void SaveActualQtyBreakdownByDepartment(int rId, List<RetrievalShortfallItemSub> retrievalShortfallItemSubListOfList)
    {
        List<Disbursement> disbursementList = context.Disbursements.Include("Retrieval").Include("Department").Include("Disbursement_Item").Where(x => x.RetrievalID == rId).ToList();

        foreach (RetrievalShortfallItemSub rsub in retrievalShortfallItemSubListOfList)
        {
            foreach (Disbursement d in disbursementList)
            {
                if (rsub.DeptCode == d.DeptCode)
                {
                    foreach (Disbursement_Item di in d.Disbursement_Item)
                    {
                        if (rsub.ItemCode == di.ItemCode)
                        {
                            //find the correct Disbursement_Item to save
                            di.ActualQty = rsub.ActualQty;
                            context.SaveChanges();
                            //EFBroker_Disbursement.UpdateDisbursementItem(di);
                        }
                    }
                }
            }
        }
        return;
    }

    public void CreateCollectionPointItemList(Disbursement d, List<CollectionPointItem> collectionPointItemList, List<int> CollectionLocationIDList)
    {
        CollectionLocationIDList.Add((int)d.Department.CollectionLocationID);
        string collectionPoint = context.CollectionPoints.Where(x => x.CollectionLocationID == d.Department.CollectionLocationID).Select(x => x.CollectionPoint1.ToString()).First();
        string defaultCollectionTime = context.CollectionPoints.Where(x => x.CollectionLocationID == d.Department.CollectionLocationID).Select(x => x.DefaultCollectionTime.ToString()).First();
        CollectionPointItem c = new CollectionPointItem(collectionPoint, defaultCollectionTime);
        collectionPointItemList.Add(c);
    }
    public List<CollectionPointItem> DisplayCollectionPoint(int rId)
    {
        List<Disbursement> disbursementList = EFBroker_Disbursement.GetDisbursmentListbyRetrievalID(rId);

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

    public void SaveCollectionTimeAndDateToDisbursement(int rId, string collectionPoint, DateTime date, string time)
    {
        List<Disbursement> disbursementList = context.Disbursements.Include("Retrieval").Include("Department").Include("Disbursement_Item").Where(x => x.RetrievalID == rId).ToList();

        foreach (Disbursement d in disbursementList)
        {
            if (d.Department.CollectionPoint.CollectionPoint1 == collectionPoint)
            {
                d.CollectionDate = date;
                d.CollectionTime = time;
            }
        }
        context.SaveChanges();
    }

    public int AddRetrieval(int empID)
    {
        return EFBroker_Disbursement.AddNewRetrieval(empID);
    }

    //public void AddDisbursement(List<int> requNo)
    //{
    //    List<int> disbursementID = new List<int>();
    //    Disbursement d = new Disbursement();
    //    List<int> requestedBy = new List<int>(); //EmpID
    //    List<string> deptCodeList = new List<string>();

    //    foreach (int i in requNo)
    //    {
    //        requestedBy.Add((int)(context.Requisitions.Where(x => x.RequisitionID.Equals(i)).Select(x => x.RequestedBy).First()));
    //    }

    //    //foreach requestedBy get depcode
    //    foreach (int i in requestedBy)
    //    {
    //        string dC = context.Employees.Where(x => x.EmpID.Equals(i)).Select(x => x.DeptCode).First().ToString();

    //        if (deptCodeList.Count() != 0)
    //        {
    //            bool add = true;

    //            foreach (string s in deptCodeList)
    //            {
    //                if (s == dC)
    //                {
    //                    add = false;
    //                }
    //            }
    //            if (add) deptCodeList.Add(dC);
    //        }
    //        else
    //        {
    //            deptCodeList.Add(dC);
    //        }
    //    }

    //    //foreach depcode add disbursement + disbDetail
    //    foreach (string i in deptCodeList)
    //    {
    //        //add Disbursement
    //        d.RetrievalID = retrievalId;
    //        d.DeptCode = i;
    //        d.Status = "Pending";
    //        context.Disbursements.Add(d);
    //        context.SaveChanges();

    //        disbursementID.Add(d.DisbursementID);//auto increasement disbursementID after SaveChanges
    //    }
    //    foreach (int i in disbursementID)
    //    {
    //        string disbDep = context.Disbursements.Where(x => x.DisbursementID == i).Select(x => x.DeptCode).First();

    //        foreach (int no in requNo)
    //        {
    //            //update requisition table 
    //            Requisition r = new Requisition();
    //            r = context.Requisitions.Where(x => x.RequisitionID.Equals(no)).First();

    //            string dep = context.Employees.Where(x => x.EmpID == r.RequestedBy).Select(x => x.DeptCode).First();

    //            if (dep == disbDep)
    //            {
    //                r.Status = "InProgress";/////////////////////////////////////////////
    //                r.DisbursementID = i;
    //                context.SaveChanges();
    //            }
    //        }
    //        AddDisbursemen_Item(i);
    //    }
    //}

    //public void AddDisbursemen_Item(int disbursementID)
    //{
    //    List<Requisition_Item> Requisition_ItemListOfList = new List<Requisition_Item>();

    //    List<int> requisitionIDList = new List<int>();
    //    requisitionIDList = context.Requisitions.Where(x => x.DisbursementID == disbursementID).Select(x => x.RequisitionID).ToList();
    //    foreach (int rL in requisitionIDList)
    //    {
    //        List<Requisition_Item> Requisition_ItemList = context.Requisition_Item.Where(x => x.RequisitionID == rL).ToList();

    //        //foreach (Requisition_Item r in Requisition_ItemListOfList)
    //        foreach (Requisition_Item r in Requisition_ItemList)
    //        {
    //            if (Disbursement_ItemList.Count != 0)
    //            {
    //                bool add = true;

    //                foreach (Disbursement_Item i in Disbursement_ItemList)
    //                {
    //                    if (i.ItemCode == r.ItemCode)
    //                    {
    //                        add = false;
    //                        i.TotalRequestedQty += r.RequestedQty;
    //                    }
    //                }
    //                if (add)
    //                {
    //                    CreateDisbursementItemList(disbursementID, r);
    //                }
    //            }
    //            else
    //            {
    //                CreateDisbursementItemList(disbursementID, r);
    //            }
    //            context.SaveChanges();
    //        }
    //    }
    //}

    //public void CreateDisbursementItemList(int disbursementID, Requisition_Item r)
    //{
    //    Disbursement_Item disbursement_Item = new Disbursement_Item();
    //    disbursement_Item.DisbursementID = disbursementID;
    //    disbursement_Item.ItemCode = r.ItemCode;
    //    disbursement_Item.TotalRequestedQty = r.RequestedQty;
    //    context.Disbursement_Item.Add(disbursement_Item);
    //    Disbursement_ItemList.Add(disbursement_Item);
    //}

    ////TEST CODE


    public void AddDisbursement(List<int> requNos, int retrievalID)
    {
        DepReqDictionary depReqDic = GetSelectedRequisitionDepartmentList(requNos);
        foreach (string depCode in depReqDic.keys)
        {
            int disbursementID = CreateNewDisbursment(depCode, retrievalID);
            List<int> requisitionNos = depReqDic.dictionary[depCode];
            DepReqDictionary multicounter = GetRequisition_quantities(requisitionNos, depCode);
            CreateNewDisbursementItems(multicounter.keys, disbursementID, depCode, multicounter.accumulator);
            foreach (int i in requNos)
            {
                EFBroker_Requisition.UpdateRequisitionStatus(i, "InProgress");
            }
        }
    }

    public void CreateNewDisbursementItems(HashSet<string> itemCodes, int disbursementID, string depCode, Dictionary<string, int> accumulator)
    {
        List<Disbursement_Item> diList = new List<Disbursement_Item>();
        foreach (string itemCode in itemCodes)
        {
            Disbursement_Item disbursement_Item = CreateNewDisbursement_Item(disbursementID, itemCode, accumulator[itemCode]);
            diList.Add(disbursement_Item);
        }
        EFBroker_Disbursement.AddNewDisbursementItemList(diList);
        return;
    }
    public static DepReqDictionary GetRequisition_quantities(List<int> requisitionNos, string depCode)
    {
        DepReqDictionary multiCounter = new DepReqDictionary();
        HashSet<string> itemCodeKeys = new HashSet<string>();
        Dictionary<string, int> quantities = new Dictionary<string, int>();
        List<Requisition_Item> rList;
        foreach (int i in requisitionNos)
        {
            rList = EFBroker_Requisition.GetRequisitionItemListbyReqID(i);
            foreach (Requisition_Item ri in rList)
            {
                int qty;
                itemCodeKeys.Add(ri.ItemCode);
                if (!quantities.TryGetValue(ri.ItemCode, out qty))
                {
                    qty = 0;
                    quantities.Add(ri.ItemCode, qty);

                }
                qty = qty + ri.RequestedQty ?? 0;
                quantities[ri.ItemCode] = qty;
            }

            multiCounter.keys = itemCodeKeys;
            multiCounter.accumulator = quantities;
        }
        return multiCounter;
    }
    public static DepReqDictionary GetSelectedRequisitionDepartmentList(List<int> requisitionNos)
    {
        DepReqDictionary complete = new DepReqDictionary();
        Dictionary<string, List<int>> depCodeDic = new Dictionary<string, List<int>>();
        HashSet<string> keys = new HashSet<string>();

        foreach (int i in requisitionNos)
        {
            List<int> list = new List<int>(); ;
            string d = EFBroker_Requisition.GetDepartmentCodebyRequisitionID(i);
            if (depCodeDic.TryGetValue(d, out list))
            {
                list.Add(i);
                depCodeDic[d] = list;
            }
            else
            {
                list = new List<int>();
                list.Add(i);
                depCodeDic.Add(d, list);
                keys.Add(d);
            }
        }
        complete.dictionary = depCodeDic;
        complete.keys = keys;
        return complete;
    }
    public static int CreateNewDisbursment(string depCode, int retrievalID)
    {
        Disbursement d = new Disbursement();
        d.RetrievalID = retrievalID;
        d.Status = "Pending";
        d.DeptCode = depCode;
        //save and get disbursementID
        return EFBroker_Disbursement.AddNewDisbursment(d);
    }
    public static Disbursement_Item CreateNewDisbursement_Item(int disbursementID, string itemCode, int totalReqQty)
    {
        Disbursement_Item disbursement_Item = new Disbursement_Item();
        disbursement_Item.DisbursementID = disbursementID;
        disbursement_Item.ItemCode = itemCode;
        disbursement_Item.TotalRequestedQty = totalReqQty;
        return disbursement_Item;
    }
}
