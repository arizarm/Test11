using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Transactions;

/// <summary>
/// Summary description for RequisitionControl
/// </summary>
public class RequisitionControl
{

    static StationeryEntities context = new StationeryEntities();

    static List<Requisition> rlist;

    static string date;
    static int requisitionNo;
    static string department;
    static string status;
    static int requestedBy;
    static string depCode;
    static string searchWord;
    static string employeeName;

    static ReqisitionListItem item;
    static List<ReqisitionListItem> itemList;
    static List<ReqisitionListItem> searchList;


    static int retrievalId;
    static List<int> disbursementID = new List<int>();

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


    public static void AddDisbursement(List<int> requNo)
    {
        Disbursement d = new Disbursement();

        List<int> requestedBy = new List<int>(); //EmpID
        List<string> deptCode = new List<string>();

        foreach (int i in requNo)
        {
            requestedBy.Add((int)(context.Requisitions.Where(x => x.RequisitionID.Equals(i)).Select(x => x.RequestedBy).First()));
        }

        bool add = true;
        //foreach requestedBy get depcode
        foreach (int i in requestedBy)
        {
            string dC = context.Employees.Where(x => x.EmpID.Equals(i)).Select(x => x.DeptCode).First().ToString();

            if (deptCode.Count() != 0)
            {
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
    public static List<ReqisitionListItem> DisplayAll()
    {
        rlist = new List<Requisition>();
        rlist = context.Requisitions.Where(x => x.Status == "Priority" || x.Status == "Approved").ToList();
        return PopulateGridView(rlist);
    }

    public static List<ReqisitionListItem> DisplayAllDepartment()
    {
        rlist = new List<Requisition>();
        rlist = context.Requisitions.ToList();
        return PopulateGridViewForDepartment(rlist);
    }

    public static List<ReqisitionListItem> DisplayPriority()
    {
        rlist = new List<Requisition>();
        rlist = context.Requisitions.Where(x => x.Status == "Priority").ToList();
        return PopulateGridView(rlist);
    }

    public static List<ReqisitionListItem> DisplayApproved()
    {
        rlist = new List<Requisition>();
        rlist = context.Requisitions.Where(x => x.Status == "Approved").ToList();
        return PopulateGridView(rlist);
    }

    public static List<ReqisitionListItem> DisplaySearch(string searchWord)
    {
        itemList = DisplayAll();
        foreach (ReqisitionListItem i in itemList)
        {
            searchList = itemList.Where(x => x.Date.ToLower().Contains(searchWord.ToLower()) || x.RequisitionNo.ToString().Contains(searchWord) || x.Department.ToLower().Contains(searchWord.ToLower()) || x.Status.ToLower().Contains(searchWord.ToLower())).ToList();
        }
        return searchList;
    }

    public static List<ReqisitionListItem> DisplaySearchDepartment(string searchWord)
    {
        itemList = DisplayAllDepartment();
        foreach (ReqisitionListItem i in itemList)
        {
            searchList = itemList.Where(x => x.Date.ToLower().Contains(searchWord.ToLower()) || x.RequisitionNo.ToString().Contains(searchWord) || x.EmployeeName.ToLower().Contains(searchWord.ToLower()) || x.Status.ToLower().Contains(searchWord.ToLower())).ToList();
        }
        return searchList;
    }

    public static List<ReqisitionListItem> PopulateGridView(List<Requisition> rlist)
    {
        itemList = new List<ReqisitionListItem>();
        foreach (Requisition r in rlist)
        {
            date = r.RequestDate.Value.ToLongDateString();
            requisitionNo = Convert.ToInt32(r.RequisitionID.ToString());
            status = r.Status.ToString();

            requestedBy = Convert.ToInt32(r.RequestedBy.ToString());
            depCode = context.Employees.Where(x => x.EmpID.Equals(requestedBy)).Select(x => x.DeptCode).First().ToString();

            department = context.Departments.Where(x => x.DeptCode.Equals(depCode)).Select(x => x.DeptName).First().ToString();
            item = new ReqisitionListItem(date, requisitionNo, department, status, "");
            itemList.Add(item);
        }
        return itemList;
    }
    //GET ITEM DESCRIPTION
    public static List<String> getItem()
    {
        using (StationeryEntities context = new StationeryEntities())
        {
            return context.Items.Where(i => i.ActiveStatus.Equals("Y")).Select(i => i.Description).ToList();

        }
    }

    //GET ITEM UOM
    public static String getUOM(string item)
    {
        using (StationeryEntities context = new StationeryEntities())
        {
            return context.Items.Where(i => i.Description.Equals(item)).Select(i => i.UnitOfMeasure).FirstOrDefault();
        }
    }

    //GET LAST REQUISITION
    public static String getLastReq()
    {
        using (StationeryEntities context = new StationeryEntities())
        {
            return context.Requisitions.OrderByDescending(r => r.RequisitionID).Select(r => r.RequisitionID).FirstOrDefault().ToString();
        }
    }

    //GET ITEM DESCRIPTION BY ITEM CODE
    public static String getCode(string item)
    {
        using (StationeryEntities context = new StationeryEntities())
        {
            return context.Items.Where(i => i.Description.Equals(item)).Select(i => i.ItemCode).FirstOrDefault();
        }
    }

    //FIND REQUISITION WITH PENDING STATUS
    public static List<Requisition> getRequisitionList()
    {
        using (StationeryEntities context = new StationeryEntities())
        {
            return context.Requisitions.Where(x => x.Status == "Pending").ToList<Requisition>();
        }
    }

    //FIND REQUISITION BY ID
    public static Requisition getRequisition(int id)
    {
        using (StationeryEntities context = new StationeryEntities())
        {
            return (context.Requisitions.Where(r => r.RequisitionID.Equals(id))).FirstOrDefault();
        }
    }

    public static void getList(int id)
    {
        using (StationeryEntities context = new StationeryEntities())
        {
            var q = from i in context.Items
                    join ri in context.Requisition_Item
                    on i.ItemCode equals ri.ItemCode
                    join rt in context.Requisitions
                    on ri.RequisitionID equals rt.RequisitionID
                    where ri.RequisitionID == id
                    select new
                    {
                        i.Description,
                        ri.RequestedQty,
                        i.UnitOfMeasure,
                        rt.Status
                    };
        }
    }

    //CANCEL REQUISITION
    public static void cancelRejectRequisition(int id)
    {
        using (StationeryEntities context = new StationeryEntities())
        {
            Requisition r = context.Requisitions.Where(x => x.RequisitionID == id).First();
            r.Status = "Rejected";
            r.Remarks = "Request cancelled";
            context.SaveChanges();
        }
    }

    //SEARCH REQUISITION BY STATUS
    public static List<ReqisitionListItem> getRequisitionListByStatus(String status)
    {
        using (StationeryEntities context = new StationeryEntities())
        {
            List<Requisition> rlist = context.Requisitions.Where(x => x.Status == status).ToList<Requisition>();
            return PopulateGridViewForDepartment(rlist);
        }
    }

    //FIND REQUISITION ITEM BY REQUISITION ID
    public static Requisition_Item findRequisitionID(int id)
    {
        using (StationeryEntities context = new StationeryEntities())
        {
            return context.Requisition_Item.Where(ri => ri.RequisitionID.Equals(id)).FirstOrDefault();
        }
    }

    //FIND REQUISITION ITEM BY REQUISITION ID AND ITEM CODE
    public static Requisition_Item findByReqIDItemCode(int id, string des)
    {
        using (StationeryEntities context = new StationeryEntities())
        {
            string code = context.Items.Where(i => i.Description.Equals(des)).Select(i => i.ItemCode).FirstOrDefault();
            return context.Requisition_Item.Where(ri => ri.ItemCode.Equals(code)).Where(ri => ri.RequisitionID.Equals(id)).FirstOrDefault();
        }
    }

    //REMOVE REQUISITION
    public static void removeRequisitionItem(int id, string code)
    {
        using (TransactionScope ts = new TransactionScope())
        {
            StationeryEntities context = new StationeryEntities();
            Requisition_Item ri = context.Requisition_Item.Where(r => r.RequisitionID.Equals(id)).Where(r => r.ItemCode.Equals(code)).FirstOrDefault();
            context.Requisition_Item.Remove(ri);
            context.SaveChanges();
            ts.Complete();
        }
    }

    //UPDATE REQUISITION ITEM
    public static void updateRequisitionItem(int id, string code, int qty)
    {
        using (TransactionScope ts = new TransactionScope())
        {
            StationeryEntities context = new StationeryEntities();
            Requisition_Item ri = context.Requisition_Item.Where(r => r.RequisitionID.Equals(id)).Where(r => r.ItemCode.Equals(code)).FirstOrDefault();
            ri.RequestedQty = qty;
            context.Entry(ri).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
            ts.Complete();
        }
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

    //CHANGE REQUISITION STATUS
    public static void approveRequisition(int id, string reason)
    {
        using (StationeryEntities context = new StationeryEntities())
        {
            Requisition r = context.Requisitions.Where(x => x.RequisitionID == id).First();
            r.Remarks = reason;
            r.RequisitionID = id;
            r.Status = "Approved";
            context.SaveChanges();
        }
    }
    public static void rejectRequisition(int id, string reason)
    {
        using (StationeryEntities context = new StationeryEntities())
        {
            Requisition r = context.Requisitions.Where(x => x.RequisitionID == id).First();
            r.Remarks = reason;
            r.RequisitionID = id;
            r.Status = "Rejected";
            r.Remarks = "Rejected By Head";
            context.SaveChanges();
        }
    }
    public static List<ReqisitionListItem> PopulateGridViewForDepartment(List<Requisition> rlist)
    {
        itemList = new List<ReqisitionListItem>();
        foreach (Requisition r in rlist)
        {
            date = r.RequestDate.Value.ToLongDateString();
            requisitionNo = Convert.ToInt32(r.RequisitionID.ToString());
            status = r.Status.ToString();
            int empCode = Convert.ToInt32(r.RequestedBy);
            employeeName = context.Employees.Where(x => x.EmpID == empCode).Select(x => x.EmpName).First().ToString();
            item = new ReqisitionListItem(date, requisitionNo, department, status, employeeName);
            itemList.Add(item);
        }
        return itemList;
    }

    public static void editRequisitionItemQty(int id, string code, int qty)
    {
        using (TransactionScope ts = new TransactionScope())
        {
            StationeryEntities context = new StationeryEntities();
            Requisition_Item ri = context.Requisition_Item.Where(i => i.RequisitionID.Equals(id)).Where(i => i.ItemCode.Equals(code)).FirstOrDefault();
            ri.RequestedQty += qty;
            context.SaveChanges();
            ts.Complete();
        }
    }
}