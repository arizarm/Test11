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

    static List<RetrievalShortfallItem> RetrievalShortfallItemList = new List<RetrievalShortfallItem>();




    public static void GenerateDisbursementList()
    {
        Random r = new Random();

        foreach (Disbursement d in disbursementList)
        {
            int value = r.Next(1000, 9999);
            d.AccessCode = value.ToString();
            d.Status = "Processing";
        }

        context.SaveChanges();
    }

    public static List<RetrievalShortfallItem> DisplayRetrievalShortfall(List<int> txtRetrievedList)
    {
        int i = 0;
        foreach (RetrievalListDetailItem r in RetrievalListDetailItemList)
        {
            string description = r.Description;
            RetrievalShortfallItem rsf = new RetrievalShortfallItem(description, txtRetrievedList[i]);//, deptNameList[i], requestedQtyList[i]
            RetrievalShortfallItemList.Add(rsf);
            i++;
        }
        return RetrievalShortfallItemList;
    }

    static List<RetrievalShortfallItemSub> RetrievalShortfallItemSubList = new List<RetrievalShortfallItemSub>();
    static List<RetrievalShortfallItemSub> RetrievalShortfallItemSubListList = new List<RetrievalShortfallItemSub>();

    public static List<RetrievalShortfallItemSub> DisplayRetrievalShortfallSub()
    {
        List<string> deptNameList = new List<string>();
        List<int> requestedQtyList = new List<int>();


        // if itemcode in requestion == left, show dept name and requested number

        int i = 0;
        foreach (Disbursement d in disbursementList)/////////////////////////////////////////////////
        {
            //    foreach (Disbursement_Item di in d.Disbursement_Item)
            //    {
            //        foreach (RetrievalShortfallItem rsfi in RetrievalShortfallItemList)
            //        {
            //            if (rsfi.Description == di.Item.Description)
            //            {
            string deptName = d.Department.DeptName.ToString();
            //                //int requestedQty = Convert.ToInt32(context.Requisition_Item.Where(x => x.RequisitionID == ).Select(x => x.RequestedQty));
            RetrievalShortfallItemSub rsfs = new RetrievalShortfallItemSub(deptName);
            RetrievalShortfallItemSubList.Add(rsfs);
            i++;
            //            }
            //        }
            //    }
        }




        //foreach (RetrievalShortfallItem rsfi in RetrievalShortfallItemList)
        //{
        //    if (rsfi.Description == disbursement_Item.Item.Description)
        //    {
        //        foreach (RetrievalShortfallItemSub r in RetrievalShortfallItemSubList)
        //        {

        //        }
        //    }
        //    //RetrievalShortfallItemSubListList
        //}





        return RetrievalShortfallItemSubList;
    }


    public static List<CollectionPointItem> DisplayCollectionPoint(string retrievalId)
    {
        disbursementList = context.Disbursements.Include("Retrieval").Include("Department").Include("Disbursement_Item").Where(x => x.RetrievalID.ToString().Equals(retrievalId)).ToList();

        List<CollectionPointItem> collectionPointItemList = new List<CollectionPointItem>();
        int i = 0;
        foreach (Disbursement d in disbursementList)/////////////////////////////////////////////////
        {
            if (d.DeptCode[i] == d.Department.DeptCode[i])
            {
                string collectionPoint = context.CollectionPoints.Where(x => x.CollectionLocationID == d.Department.CollectionLocationID).Select(x => x.CollectionPoint1.ToString()).First();
                string defaultCollectionTime = context.CollectionPoints.Where(x => x.CollectionLocationID == d.Department.CollectionLocationID).Select(x => x.DefaultCollectionTime.ToString()).First();

                CollectionPointItem c = new CollectionPointItem(collectionPoint, defaultCollectionTime);
                collectionPointItemList.Add(c);
            }
            i++;
        }
        return collectionPointItemList;
    }
    public static void SaveCollectionTimeAndDateToDisbursement(List<DateTime> dateList, List<string> timeList)
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

    public static void CheckShortfall(List<int> ActualQty)
    {

        bool Shortfall = false;

        for (int i = 0; i < RetrievalListDetailItemList.Count; i++)
        {
            if (RetrievalListDetailItemList[i].TotalRequestedQty != ActualQty[i])
                Shortfall = true;
        }

        if (Shortfall == true)
        {
            HttpContext.Current.Response.Redirect("RetrievalShortfall.aspx");
        }
        else
        {
            HttpContext.Current.Response.Redirect("CollectionPointUpdate.aspx");
        }

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


    public static List<RetrievalListDetailItem> DisplayRetrievalListDetail(string retrievalId)
    {
        retrievalList = new List<Retrieval>();

        List<string> itemCodeList = new List<string>();
        RetrievalListDetailItemList = new List<RetrievalListDetailItem>();

        disbursementList = context.Disbursements.Include("Retrieval").Include("Department").Include("Disbursement_Item").Where(x => x.RetrievalID.ToString().Equals(retrievalId)).ToList();

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


    public static void SaveRetrieved(List<int> ActualQty)//////////////////////////////////////////////
    {
        foreach (Disbursement d in disbursementList)
        {
            foreach (Disbursement_Item di in d.Disbursement_Item)
            {
                bool Shortfall = false;
                for (int i = 0; i < RetrievalListDetailItemList.Count; i++)
                {
                    if (RetrievalListDetailItemList[i].TotalRequestedQty != ActualQty[i])
                        Shortfall = true;
                }
                if (Shortfall == false)
                {
                    di.ActualQty = di.TotalRequestedQty;
                }

                if (Shortfall == true)/////////////////////////////////////////////
                {
                    MessageBox.Show("RetrievalShortfall!");
                    HttpContext.Current.Response.Redirect("RetrievalShortfall.aspx");
                }
            }
        }

        //foreach (Requisition_Item ri in Requisition_ItemList)
        //{
        //    //if (diList.Count != 0)
        //    //{
        //        foreach (Disbursement_Item di in Disbursement_ItemList)
        //        {
        //            if (di.TotalRequestedQty == ri.RequestedQty)
        //            {

        //            }
        //        }
        //    //}
        //    //else
        //    //{
        //    //    di = new Disbursement_Item();
        //    //    di.DisbursementID = disbursementID;
        //    //    di.ItemCode = r.ItemCode;
        //    //    di.TotalRequestedQty = r.RequestedQty;
        //    //    context.Disbursement_Item.Add(di);
        //    //}
        //    //context.SaveChanges();
        //}

        ////foreach (int i in ActualQty)
        ////{
        ////    di.ActualQty = i;
        ////}
        //// context.Disbursement_Item.Add(di);  //??????????????????????????????
        context.SaveChanges();
    }




    public static List<Retrieval> DisplayRetrievalList()
    {
        return EFBroker_Disbursement.GetAllRetrievalList();
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
    }



    static List<Requisition_Item> Requisition_ItemList = new List<Requisition_Item>();
    static Disbursement_Item disbursement_Item;
    static List<Disbursement_Item> Disbursement_ItemList = new List<Disbursement_Item>();
    public static void AddDisbursemen_Item(int disbursementID)
    {

        List<int> requisitionIDList = new List<int>();
        requisitionIDList = context.Requisitions.Where(x => x.DisbursementID == disbursementID).Select(x => x.RequisitionID).ToList();

        foreach (int i in requisitionIDList)
        {
            Requisition_ItemList = context.Requisition_Item.Where(x => x.RequisitionID == i).ToList();
        }

        foreach (Requisition_Item r in Requisition_ItemList)
        {
            if (Disbursement_ItemList.Count != 0)
            {
                foreach (Disbursement_Item i in Disbursement_ItemList)
                {
                    if (i.ItemCode == r.ItemCode)
                    {
                        i.TotalRequestedQty += r.RequestedQty;
                    }
                }
            }
            else
            {
                disbursement_Item = new Disbursement_Item();
                disbursement_Item.DisbursementID = disbursementID;
                disbursement_Item.ItemCode = r.ItemCode;
                disbursement_Item.TotalRequestedQty = r.RequestedQty;
                context.Disbursement_Item.Add(disbursement_Item);
            }
            context.SaveChanges();
        }

        //di.ActualQty =
        //di.Remarks =

    }


}