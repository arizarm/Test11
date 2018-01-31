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

    public List<RetrievalListDetailItem> DisplayRetrievalListDetail(int requisitionId)
    {
        List<RetrievalListDetailItem> retrievalListDetailItemDisplayList = new List<RetrievalListDetailItem>();

        //get retrievalStatus by requisitionId
        string retrievalStatus = context.Retrievals.Where(x => x.RetrievalID == requisitionId).Select(x => x.RetrievalStatus).First().ToString();

        // dictionary with itemcode + totalrequestedQty
        Dictionary<string, int> itemcodeAndTotalRequestedQtyDictionary = new Dictionary<string, int>(); 

        HashSet<String> uniqueItemcodeHashSet = new HashSet<string>();

        List<Disbursement> disbursementList = EFBroker_Disbursement.GetDisbursmentListbyRetrievalID(requisitionId);

        // remove repeated itemcode in disbursementList
        foreach (Disbursement d in disbursementList)
        {
            foreach (Disbursement_Item dI in d.Disbursement_Item)
            {
                uniqueItemcodeHashSet.Add(dI.ItemCode);
            }
        }

        // accumulate totalRequestedQty if there is same itemCode in Disbursement, then 
        foreach (string i in uniqueItemcodeHashSet)
        {
            string itemCode = i;
            int totalRequestedQty = 0;
            foreach (Disbursement d in disbursementList)
            {
                foreach (Disbursement_Item dI in d.Disbursement_Item)
                {
                    if (dI.ItemCode == itemCode)
                    {
                        totalRequestedQty += (int)dI.TotalRequestedQty;
                    }
                }
            }
            itemcodeAndTotalRequestedQtyDictionary.Add(itemCode, totalRequestedQty);
        }

        //create RetrievalListDetailItem to display in DisplayRetrievalListDetail base on retrievalStatus
        foreach (KeyValuePair<string, int> kvp in itemcodeAndTotalRequestedQtyDictionary)
        {
            int retrievedQty = 0;
            if (retrievalStatus == "Pending")
            {
                //default retrievedQty is same as totalRequestedQty
                retrievedQty = kvp.Value;
            }
            if (retrievalStatus == "InProgress")
            {
                //retrievedQty is same as value which inputted in warehouse 
                retrievedQty = (int)(context.Disbursement_Item.Include("Disbursement").Where(x => x.Disbursement.RetrievalID == requisitionId && x.ItemCode.Equals(kvp.Key)).Select(x => x.ActualQty).First());
            }

            Item item = EFBroker_Item.GetItembyItemCode(kvp.Key);
            string bin = item.Bin;
            string description = item.Description;

            RetrievalListDetailItem retDetail = new RetrievalListDetailItem(bin, description, kvp.Value, kvp.Key, retrievedQty);
            retrievalListDetailItemDisplayList.Add(retDetail);
        }
        return retrievalListDetailItemDisplayList;
    }

    //in android
    //update actual qty for non-shortfall disbursement items when generate disbursement button clicked in RetrievalListDetail page at werehouse 
    public List<RetrievalShortfallItem> UpdateRetrieval(int requisitionId, Dictionary<string, int> retrievedData)
    {
        //update retrieval status
        EFBroker_Disbursement.UpdateRetrievalStatus(requisitionId, "InProgress");

        List<Disbursement> disbursementList = EFBroker_Disbursement.GetDisbursmentListbyRetrievalID(requisitionId);

        foreach (KeyValuePair<string, int> kvp in retrievedData)
        {
            string itemCode = kvp.Key;

            foreach (Disbursement d in disbursementList)
            {
                foreach (Disbursement_Item dI in d.Disbursement_Item)
                {
                    if (dI.ItemCode.Equals(itemCode))
                    {
                        dI.ActualQty = kvp.Value;
                        EFBroker_Disbursement.UpdateDisbursementItem(dI);
                    }
                }
            }
        }
        //call CheckShortfall method after button generate disbursement clicked in RetrievalListDetail page at werehouse 
        return CheckShortfall(requisitionId, retrievedData);
    }

    //Generate short fall item list when Genereate Disbursement is clicked
    public List<RetrievalShortfallItem> CheckShortfall(int requisitionId, Dictionary<string, int> retrievedData)
    {
        RetrievalListDetailItemList = DisplayRetrievalListDetail(requisitionId);

        List<RetrievalShortfallItem> RetrievalShortfallItemList = new List<RetrievalShortfallItem>();

        foreach (KeyValuePair<string, int> kvp in retrievedData)
        {
            string itemCode = kvp.Key;
            int retrievedQty = kvp.Value;

            //Update Item Balance 
            Item item = EFBroker_Item.GetItembyItemCode(itemCode);
            item.BalanceQty -= retrievedQty;
            EFBroker_Item.UpdateItem(item);

            foreach (RetrievalListDetailItem retListD in RetrievalListDetailItemList)
            {
                if (retListD.ItemCode == itemCode)
                {
                    if (retrievedQty < retListD.TotalRequestedQty)
                    {
                        RetrievalShortfallItem r = new RetrievalShortfallItem(retListD.Description, retrievedQty, retListD.ItemCode);
                        RetrievalShortfallItemList.Add(r);
                    }
                }
            }
        }
        return RetrievalShortfallItemList;
    }

    //populate shortfall data for sub gridview
    public List<RetrievalShortfallItemSub> DisplayRetrievalShortfallSubGridView(int requisitionId, string shortfallItemCode)
    {
        List<RetrievalShortfallItemSub> RetrievalShortfallItemSubGridViewList = new List<RetrievalShortfallItemSub>();

        List<Disbursement> disbursementList = EFBroker_Disbursement.GetDisbursmentListbyRetrievalID(requisitionId);

        int i = 0;
        foreach (Disbursement d in disbursementList)
        {
            foreach (Requisition r in EFBroker_Requisition.GetRequisitionListByDisbursementID(d.DisbursementID))
            {
                //if only one departmentName
                string departmentName = d.Department.DeptName.ToString();
                string departmentCode = d.Department.DeptCode.ToString();
                try
                {
                    int requestedQty = EFBroker_Requisition.FindReqItemsByReqIDItemID(r.RequisitionID, shortfallItemCode).RequestedQty ?? 0; //if RequestedQty is null, assign to 0

                    //actualQty(0) bind with avialableQty(retrievedQty)
                    RetrievalShortfallItemSub rsfs = new RetrievalShortfallItemSub((DateTime)r.RequestDate, departmentName, departmentCode, requestedQty, 0, shortfallItemCode);
                    RetrievalShortfallItemSubGridViewList.Add(rsfs);
                    i++;
                }
                catch (Exception e)
                {
                    continue;
                }
            }
        }

        return RetrievalShortfallItemSubGridViewList;
    }


    // need to reset ActualQty To Zero before += rsub.ActualQty;, because same item code from different department has same ActualQty now
    public void SaveActualQtyBreakdownByDepartment(int requisitionId, List<RetrievalShortfallItemSub> retrievalShortfallItemSubListOfList)
    {
        List<Disbursement> disbursementList = EFBroker_Disbursement.GetDisbursmentListbyRetrievalID(requisitionId);

        resetActualQtyToZero(retrievalShortfallItemSubListOfList, disbursementList);

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
                        }
                    }
                }
            }
        }
        return;
    }

    //Reset Actual Quantity to zero 
    public void resetActualQtyToZero(List<RetrievalShortfallItemSub> retrievalShortfallItemSubListOfList, List<Disbursement> disbursementList)
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

    // filter out different Department which has same collect point 
    public List<CollectionPointItem> DisplayCollectionPoint(int requisitionId)
    {
        List<Disbursement> disbursementList = EFBroker_Disbursement.GetDisbursmentListbyRetrievalID(requisitionId);

        List<CollectionPointItem> collectionPointItemList = new List<CollectionPointItem>();

        List<int> CollectionLocationIDList = new List<int>();

        foreach (Disbursement d in disbursementList)
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

    public void SaveCollectionTimeAndDateToDisbursement(int requisitionId, string collectionPoint, DateTime date, string time)
    {
        EFBroker_Disbursement.UpdateRetrievalStatus(requisitionId, "Closed");

        List<Disbursement> disbursementList = EFBroker_Disbursement.GetDisbursmentListbyRetrievalID(requisitionId);

        Random r = new Random();

        foreach (Disbursement d in disbursementList)
        {
            string depCode = d.Department.DeptCode;            
            if (EFBroker_DeptEmployee.GetCollectionPointbyDeptCode(depCode).CollectionPoint1 == collectionPoint)////////////////The ObjectContext instance has been disposed and can no longer be used for operations that require a connection."}
            {
                d.CollectionDate = date;
                d.CollectionTime = time;
                int value = r.Next(1000, 9999);
                d.AccessCode = value.ToString();
                d.Status = "Ready";
                EFBroker_Disbursement.UpdateDisbursement(d);

                //
                string departmentRepresentativeEmail = EFBroker_DeptEmployee.GetDRepresentativeEmailByDeptCode(depCode);
                string departmentRepresentativeName = EFBroker_DeptEmployee.GetDRepresentativeNameByDeptCode(depCode);
                Utility.sendMail(departmentRepresentativeEmail, "New Collection Notification " + DateTime.Now.ToString(),"Dear "+ departmentRepresentativeName +", "+ Environment.NewLine + Environment.NewLine  + "Disbursement for items from your department is ready for collection. Your access code is " + d.AccessCode + ". Please go to "+ collectionPoint + " on " + ((DateTime)d.CollectionDate).ToShortDateString() +" at "+ d.CollectionTime +". Thank you.");
                //
            }
        }
    }


    public void AddDisbursement(int requisitionId, List<int> requisitionNo)
    {
        List<int> disbursementIDList = new List<int>();
        Disbursement d = new Disbursement();
        List<int> requestedByList = new List<int>(); //EmpID
        List<string> departmentCodeList = new List<string>();

        foreach (int i in requisitionNo)
        {
            requestedByList.Add((int)(EFBroker_Requisition.GetRequisitionByID(i).RequestedBy));
        }

        //foreach requestedByList get depcode
        foreach (int i in requestedByList)
        {
            //string departmentCode = context.Employees.Where(x => x.EmpID.Equals(i)).Select(x => x.DeptCode).First().ToString();
            string departmentCode = EFBroker_DeptEmployee.GetDepartByEmpID(i).DeptCode.ToString();

            if (departmentCodeList.Count() != 0)
            {
                bool add = true;

                foreach (string s in departmentCodeList)
                {
                    if (s == departmentCode)
                    {
                        add = false;
                    }
                }
                if (add) departmentCodeList.Add(departmentCode);
            }
            else
            {
                departmentCodeList.Add(departmentCode);
            }
        }

        //foreach depcode add disbursement + disbDetail
        foreach (string i in departmentCodeList)
        {
            //add Disbursement
            d.RetrievalID = requisitionId;
            d.DeptCode = i;
            d.Status = "Pending";
            int disbursementId = EFBroker_Disbursement.AddNewDisbursment(d);

            disbursementIDList.Add(disbursementId);//auto increasement disbursementIDList after SaveChanges
        }
        foreach (int i in disbursementIDList)
        {
            string departmentCode = EFBroker_Disbursement.GetDisbursmentbyDisbID(i).DeptCode;

            foreach (int no in requisitionNo)
            {
                //update requisition table 
                Requisition r = new Requisition();
                r = EFBroker_Requisition.GetRequisitionByID(no);

                string dep = EFBroker_DeptEmployee.GetDepartByEmpID(r.RequestedBy ?? 0).DeptCode;//if null, ==0

                if (dep == departmentCode)
                {
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
        List<int> requisitionIDList = new List<int>();
        requisitionIDList = EFBroker_Requisition.GetRequisitionIDListbyDisbID(disbursementID);
        foreach (int rL in requisitionIDList)
        {
            List<Requisition_Item> Requisition_ItemList = EFBroker_Requisition.GetRequisitionItemListbyReqID(rL);

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
                            EFBroker_Disbursement.UpdateDisbursementItem(i);
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
                    Disbursement_Item dItem2 = CreateDisbursementItemList(disbursementID, r);
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
        disbursement_Item.ActualQty = 0;///////////////////////////////////////////////////////in order to do += ActualQty
        return disbursement_Item;
    }

    //discard invalid disbursement and set requisitioin status to PRORITY if all qty zero
    public bool CheckInvalidDisbursement(int rId)
    {
        List<Disbursement> disbList = EFBroker_Disbursement.GetDisbursmentListbyRetrievalID(rId);

        Dictionary<Disbursement, bool> chkDisbStatus = new Dictionary<Disbursement, bool>();

        bool valid = false;

        foreach(Disbursement d in disbList)
        {
            foreach(Disbursement_Item di in d.Disbursement_Item)
            {
                if(di.ActualQty != 0)
                {
                    valid = true;
                }
            }
            chkDisbStatus.Add(d, valid);
        }

        if (!valid)
        {
            List<Requisition> reqList = new List<Requisition>();

            foreach (Disbursement d in disbList)
            {
                Requisition r = EFBroker_Requisition.GetRequisitionByDisbID(d.DisbursementID);
                r.Status = "Priority";
                r.DisbursementID = null;
                EFBroker_Requisition.UpdateRequisition(r);

                d.Status = "Invalid";
                EFBroker_Disbursement.UpdateDisbursement(d);
            }
        }

        valid = false;

        foreach (KeyValuePair<Disbursement,bool> kvp in chkDisbStatus)
        {
            //if 1 of the disbursement is true => valid is true
            if(kvp.Value)
            {
                valid = true;
            }
        }

        if(!valid)  //   valid = false;
        {
            EFBroker_Disbursement.UpdateRetrievalStatus(rId, "Invalid");
        }

        return valid;
    }
}
