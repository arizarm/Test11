using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Windows.Forms;
using System.Transactions;


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
        return EFBroker_Disbursement.GetPendingAndProgressRetrievalList();
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
    public List<RetrievalListDetailItem> DisplayRetrievalListDetail(int rId)
    {
        List<Retrieval> retrievalList = new List<Retrieval>();

        List<string> itemCodeList = new List<string>();

        List<Disbursement> disbursementList = EFBroker_Disbursement.GetDisbursmentListbyRetrievalID(rId);

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
                            r.TotalRequestedQty += EFBroker_Disbursement.GetRequestedQtybyDisbursementIDItemCode(dI.DisbursementID, s);
                        }
                    }
                    if (add)
                    {
                        RetrievalListDetailItem ri1 = CreateRetrievalListDetailItemList(itemCode);
                        itemCodeList.Add(itemCode);
                        RetrievalListDetailItemList.Add(ri1);
                    }
                }
                else
                {
                    RetrievalListDetailItem ri2 = CreateRetrievalListDetailItemList(itemCode);
                    itemCodeList.Add(itemCode);
                    RetrievalListDetailItemList.Add(ri2);
                }
            }
        }
        return RetrievalListDetailItemList;
    }

    //Create new RetrievalListDetailItem
    public RetrievalListDetailItem CreateRetrievalListDetailItemList(string itemCode)
    {
        RetrievalListDetailItem r;
        Item item = EFBroker_Item.GetItembyItemCode(itemCode);
        string bin = item.Bin;
        string description = item.Description;
        //what item are you getting>
        int totalRequestedQty = (int)(context.Disbursement_Item.Where(x => x.ItemCode.ToString().Equals(itemCode)).Select(x => x.TotalRequestedQty).First());
        int retrievedQty = (int)(context.Disbursement_Item.Where(x => x.ItemCode.ToString().Equals(itemCode)).Select(x => x.ActualQty).First());

        r = new RetrievalListDetailItem(bin, description, totalRequestedQty, itemCode, retrievedQty);

        return r;
    }


    //update actual qty for non-shortfall disbursement items when generate disbursement button clicked
    public List<RetrievalShortfallItem> UpdateRetrieval(int rId, Dictionary<string, int> retrievedData)
    {
        //update retrieval status
        EFBroker_Disbursement.UpdateRetrievalStatus(rId, "InProgress");

        List<Disbursement> disbList = EFBroker_Disbursement.GetDisbursmentListbyRetrievalID(rId);

        List<Disbursement> disbursementList = EFBroker_Disbursement.GetDisbursmentListbyRetrievalID(rId);

        foreach (KeyValuePair<string, int> kvp in retrievedData)
        {
            string iCode = kvp.Key;

            foreach (Disbursement d in disbList)
            {
                foreach (Disbursement_Item dI in d.Disbursement_Item)
                {
                    if (dI.ItemCode.Equals(iCode))
                    {
                        dI.ActualQty = kvp.Value;
                        EFBroker_Disbursement.UpdateDisbursementItem(dI);
                        ////to update item balance
                        //Item item = EFBroker_Item.GetItembyItemCode(dI.ItemCode);
                        //item.BalanceQty -= kvp.Value;//////////////////////////////////////////
                        //EFBroker_Item.UpdateItem(item);
                    }
                }
            }
        }


        return CheckShortfall(rId, retrievedData);///// to call after testing
    }

    //Generate short fall item list when Genereate Disbursement is clicked
    public List<RetrievalShortfallItem> CheckShortfall(int rId, Dictionary<string, int> retrievedData)
    {
        RetrievalListDetailItemList = DisplayRetrievalListDetail(rId);

        List<RetrievalShortfallItem> RetrievalShortfallItemList = new List<RetrievalShortfallItem>();

        foreach (KeyValuePair<string, int> kvp in retrievedData)
        {
            string iCode = kvp.Key;
            int retrievedQty = kvp.Value;

            Item item = EFBroker_Item.GetItembyItemCode(iCode);
            item.BalanceQty -= retrievedQty;//////////////////////////////////////////
            EFBroker_Item.UpdateItem(item);

            foreach (RetrievalListDetailItem retListD in RetrievalListDetailItemList)
            {
                if (retListD.ItemCode == iCode)
                {
                    if (retrievedQty < retListD.TotalRequestedQty)
                    {
                        RetrievalShortfallItem r = new RetrievalShortfallItem(retListD.Description, retrievedQty, retListD.ItemCode);
                        RetrievalShortfallItemList.Add(r);
                    }
                    ////////////////////////
                    //if (kvp.Value == retListD.TotalRequestedQty)  // if no shortfall
                    //{
                        
                    //}
                    ////////////////////////
                }
            }
        }
        return RetrievalShortfallItemList;
    }

    //populate shortfall data for sub gridview
    public List<RetrievalShortfallItemSub> DisplayRetrievalShortfallSub(int rId, string shortfallItemCode)
    {
        List<RetrievalShortfallItemSub> RetrievalShortfallItemSubList = new List<RetrievalShortfallItemSub>();

        List<Disbursement> disbursementList = EFBroker_Disbursement.GetDisbursmentListbyRetrievalID(rId);

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



    public void SaveActualQtyBreakdownByDepartment(int rId, List<RetrievalShortfallItemSub> retrievalShortfallItemSubListOfList)
    {
        List<Disbursement> disbursementList = EFBroker_Disbursement.GetDisbursmentListbyRetrievalID(rId);

        resetActualQty(retrievalShortfallItemSubListOfList, disbursementList);

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
                            di.ActualQty += rsub.ActualQty;
                            EFBroker_Disbursement.UpdateDisbursementItem(di);
                            //////to update item balance  in ActualQtyBreakdownByDepartment
                            //Item item = EFBroker_Item.GetItembyItemCode(di.ItemCode);
                            //item.BalanceQty -= rsub.ActualQty;//////////////////////////////////////////
                            //EFBroker_Item.UpdateItem(item);
                            ///////
                            //EFBroker_Disbursement.UpdateDisbursementItem(di);
                        }
                    }
                }
            }
        }
        return;
    }

    //Reset Actual Quantity to zero 
    public void resetActualQty(List<RetrievalShortfallItemSub> retrievalShortfallItemSubListOfList, List<Disbursement> disbursementList)
    {
        foreach (RetrievalShortfallItemSub shortfallList in retrievalShortfallItemSubListOfList)
        {
            foreach (Disbursement d in disbursementList)
            {
                foreach (Disbursement_Item di in d.Disbursement_Item)
                {
                    if (shortfallList.ItemCode == di.ItemCode)
                    {
                        di.ActualQty = 0;
                    }
                }
            }
        }
    }

    public CollectionPointItem CreateCollectionPointItemList(Disbursement d)
    {
        CollectionPoint cp1 = EFBroker_DeptEmployee.GetCollectionPointbyDeptCode(d.Department.DeptCode);
        string collectionPoint = cp1.CollectionPoint1;
        string defaultCollectionTime = cp1.DefaultCollectionTime;
        CollectionPointItem c = new CollectionPointItem(collectionPoint, defaultCollectionTime);
        return c;
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
                    CollectionLocationIDList.Add((int)d.Department.CollectionLocationID);
                    CollectionPointItem c1 = CreateCollectionPointItemList(d);
                    collectionPointItemList.Add(c1);
                }
            }
            else
            {
                CollectionLocationIDList.Add((int)d.Department.CollectionLocationID);
                CollectionPointItem c2 = CreateCollectionPointItemList(d);
                collectionPointItemList.Add(c2);
            }
        }
        return collectionPointItemList;
    }



    public void SaveCollectionTimeAndDateToDisbursement(int rId, string collectionPoint, DateTime date, string time)
    {
        EFBroker_Disbursement.UpdateRetrievalStatus(rId, "Closed");

        List<Disbursement> disbursementList = EFBroker_Disbursement.GetDisbursmentListbyRetrievalID(rId);

        Random r = new Random();

        foreach (Disbursement d in disbursementList)
        {
            if (d.Department.CollectionPoint.CollectionPoint1 == collectionPoint)
            {
                d.CollectionDate = date;
                d.CollectionTime = time;
                int value = r.Next(1000, 9999);
                d.AccessCode = value.ToString();
                d.Status = "Ready";
                EFBroker_Disbursement.UpdateDisbursement(d);
            }
        }
    }


    public int AddRetrieval()
    {
        Retrieval r = new Retrieval();
        r.RetrievedBy = 1001;       //base on user session
        r.RetrievedDate = DateTime.Today;
        r.RetrievalStatus = "Pending";
        //context.Retrievals.Add(r);
        //context.SaveChanges();
        //retrievalId = r.RetrievalID; // get auto increasement data after SaveChanges        
        //return retrievalId;
        return EFBroker_Disbursement.AddNewRetrieval(1001);
    }

    public void AddDisbursement(List<int> requNo)//////////////////////////////////InProgress doesn't work
    {
        List<int> disbursementID = new List<int>();
        Disbursement d = new Disbursement();
        List<int> requestedBy = new List<int>(); //EmpID
        List<string> deptCodeList = new List<string>();


        foreach (int i in requNo)
        {
            requestedBy.Add((int)(EFBroker_Requisition.GetRequisitionByID(i).RequestedBy));
        }

        //foreach requestedBy get depcode
        foreach (int i in requestedBy)
        {
            //string dC = context.Employees.Where(x => x.EmpID.Equals(i)).Select(x => x.DeptCode).First().ToString();
            string dC = EFBroker_DeptEmployee.GetDepartByEmpID(i).DeptCode.ToString();

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
            int disbID = EFBroker_Disbursement.AddNewDisbursment(d);

            disbursementID.Add(disbID);//auto increasement disbursementID after SaveChanges
        }
        foreach (int i in disbursementID)
        {
//            string disbDep = context.Disbursements.Where(x => x.DisbursementID == i).Select(x => x.DeptCode).First();
            string disbDep = EFBroker_Disbursement.GetDisbursmentbyDisbID(i).DeptCode;

            foreach (int no in requNo)
            {
                //update requisition table 
                Requisition r = new Requisition();
                r = EFBroker_Requisition.GetRequisitionByID(no);

 //             string dep = context.Employees.Where(x => x.EmpID == r.RequestedBy).Select(x => x.DeptCode).First();
                string dep = EFBroker_DeptEmployee.GetDepartByEmpID(r.RequestedBy??0).DeptCode;

                if (dep == disbDep)
                {
                    //r.Status = "InProgress";
                    //r.DisbursementID = i;
                    //context.Entry(r).State = System.Data.Entity.EntityState.Modified;
                    //context.SaveChanges();
                    r.Status = "InProgress";/////////////////////////////////////////////
                    r.DisbursementID = i;
                    EFBroker_Requisition.UpdateRequisition(r);
                }
            }
            AddDisbursemen_Item(i);
        }
    }

    public void AddDisbursemen_Item(int disbursementID)
    {
        List<Requisition_Item> Requisition_ItemListOfList = new List<Requisition_Item>();

        List<int> requisitionIDList = new List<int>();
        requisitionIDList = EFBroker_Requisition.GetRequisitionIDListbyDisbID(disbursementID);
        foreach (int rL in requisitionIDList)
        {
            List<Requisition_Item> Requisition_ItemList = EFBroker_Requisition.GetRequisitionItemListbyReqID(rL);

            //foreach (Requisition_Item r in Requisition_ItemListOfList)
            foreach (Requisition_Item r in Requisition_ItemList)
            {
                if (Disbursement_ItemList.Count != 0)
                {
                    bool add = true;

                    foreach (Disbursement_Item i in Disbursement_ItemList)
                    {
                        if (i.ItemCode == r.ItemCode && i.DisbursementID == r.Requisition.DisbursementID)
                        {
                            add = false;
                            i.TotalRequestedQty += r.RequestedQty;
                        }
                    }
                    if (add)
                    {
                        Disbursement_Item dItem1 = CreateDisbursementItemList(disbursementID, r);
                        EFBroker_Disbursement.AddNewDisbursementItem(dItem1);
                        Disbursement_ItemList.Add(dItem1);
                    }
                }
                else
                {
                    Disbursement_Item dItem2 =CreateDisbursementItemList(disbursementID, r);
                    EFBroker_Disbursement.AddNewDisbursementItem(dItem2);
                    Disbursement_ItemList.Add(dItem2);
                }
            }
        }
    }

    public Disbursement_Item CreateDisbursementItemList(int disbursementID, Requisition_Item r)
    {
        Disbursement_Item disbursement_Item = new Disbursement_Item();
        disbursement_Item.DisbursementID = disbursementID;
        disbursement_Item.ItemCode = r.ItemCode;
        disbursement_Item.TotalRequestedQty = r.RequestedQty;
        disbursement_Item.ActualQty = 0;///////////////////////////////////////////////////////
        return disbursement_Item;
    }
}
