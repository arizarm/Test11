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

    static string employeeName;

    static ReqisitionListItem item;
    static List<ReqisitionListItem> itemList;
    static string searchWord;
    static List<ReqisitionListItem> searchList;


    public static List<ReqisitionListItem> DisplayAll()
    {
        rlist = new List<Requisition>();
        rlist = EFBroker_Requisition.GetAllApprovedOrPriorityReq();
        return PopulateGridView(rlist);
    }

    public static List<ReqisitionListItem> DisplayAllDepartment()
    {
        rlist = new List<Requisition>();
        rlist = EFBroker_Requisition.GetAllRequisitionList();
        return PopulateGridViewForDepartment(rlist);
    }

    public static List<ReqisitionListItem> DisplayPriority()
    {
        rlist = new List<Requisition>();
        rlist = EFBroker_Requisition.GetAllRequisitionsByStatus("Priority");
        return PopulateGridView(rlist);
    }

    public static List<ReqisitionListItem> DisplayApproved()
    {
        rlist = new List<Requisition>();
        rlist = EFBroker_Requisition.GetAllRequisitionsByStatus("Approved");
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

    public static List<ReqisitionListItem> SearchForRepRequisitionWithStatus(string searchWord, int empID, string status)
    {
        List<ReqisitionListItem> list = PopulateGridView(EFBroker_Requisition.getRequisitionListByEmpIDAndStatus(empID, status));

        searchList = list.Where(x => x.RequisitionNo.ToString().Contains(searchWord.ToLower()) || x.Date.ToString().Contains(searchWord.ToString())).ToList();
        //itemList = getCollectionList();
        //foreach (ReqisitionListItem i in itemList)
        //{
        //    searchList = itemList.Where(x => x.Date.ToLower().Contains(searchWord.ToLower()) || x.RequisitionNo.ToString().Contains(searchWord) || x.Department.ToLower().Contains(searchWord.ToLower()) || x.Status.ToLower().Contains(searchWord.ToLower())).ToList();
        //}
        return searchList;
    }

    public static List<ReqisitionListItem> SearchForRepRequisitionWithoutStatus(string searchWord, int empID)
    {
        List<ReqisitionListItem> list = PopulateGridView(EFBroker_Requisition.GetRequisitionListByRequestorID(empID));
        searchList = list.Where(x => x.RequisitionNo.ToString().Contains(searchWord.ToLower()) || Convert.ToDateTime(x.Date).ToLongDateString().Contains(searchWord.ToString())).ToList();

        //itemList = getCollectionList();
        //foreach (ReqisitionListItem i in itemList)
        //{
        //    searchList = itemList.Where(x => x.Date.ToLower().Contains(searchWord.ToLower()) || x.RequisitionNo.ToString().Contains(searchWord) || x.Department.ToLower().Contains(searchWord.ToLower()) || x.Status.ToLower().Contains(searchWord.ToLower())).ToList();
        //}
        return searchList;
    }

    public static List<ReqisitionListItem> HeadSearchWithoutStatus(string searchWord, string deptCode)
    {
        List<ReqisitionListItem> list = PopulateGridView(EFBroker_Requisition.SearchForReqHeadWithoutStatus(deptCode)).ToList();
        searchList = list.Where(x => x.RequisitionNo.ToString().Contains(searchWord) || Convert.ToDateTime(x.Date).ToLongDateString().ToLower().Contains(searchWord.ToLower()) || x.EmployeeName.ToLower().Contains(searchWord.ToLower())).ToList();
        return searchList;
    }

    public static List<ReqisitionListItem> HeadSearchWithStatus(string searchWord,string deptCode,string status)
    {
        List<ReqisitionListItem> list = PopulateGridView(EFBroker_Requisition.SearchForReqHeadWithStatus(deptCode,status)).ToList();
        searchList = list.Where(x => x.RequisitionNo.ToString().Contains(searchWord) || Convert.ToDateTime(x.Date).ToLongDateString().ToLower().Contains(searchWord.ToLower()) || x.EmployeeName.ToLower().Contains(searchWord.ToLower())).ToList();
        return searchList;
    }

    public static List<ReqisitionListItem> DisplaySearchByEmpID(string searchWord,int empId)
    {
        itemList = DisplayAllDepartment();
        foreach (ReqisitionListItem i in itemList)
        {
            searchList = itemList.Where(x => x.Date.ToLower().Contains(searchWord.ToLower()) || x.RequisitionNo.ToString().Contains(searchWord) || x.EmployeeName.ToLower().Contains(searchWord.ToLower()) || x.Status.ToLower().Contains(searchWord.ToLower())).ToList();
        }
        return searchList;
    }
  
    public static List<ReqisitionListItem> DisplayCollectionListSearch(string deptCode, string searchWord)
    {
        List<ReqisitionListItem> l = PopulateGridView(EFBroker_Requisition.SearchForCollectionList(deptCode));
        foreach(ReqisitionListItem i in l)
        {
            searchList = l.Where(x => x.Date.ToLower().Contains(searchWord.ToLower()) || x.RequisitionNo.ToString().Contains(searchWord)).ToList();
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
            string empName =EmployeeController.getEmployee(requestedBy);
            Department dep = EFBroker_DeptEmployee.GetDepartByEmpID(requestedBy);
            depCode = dep.DeptCode;

            department = dep.DeptName;
            item = new ReqisitionListItem(date, requisitionNo, department, status, empName);
            itemList.Add(item);
        }
        return itemList;
    }
    //GET ITEM DESCRIPTION
    public static List<String> getItem()
    {
        return EFBroker_Item.GetActiveItemDescriptionList();
    }

    //GET ITEM UOM
    public static String getUOM(string item)
    {
        return EFBroker_Item.GetUnitbyItemDesc(item);
    }

    //GET LAST REQUISITION
    public static String getLastReq()
    {
        return EFBroker_Requisition.GetLatestRequisitionID();
    }

    //GET ITEM DESCRIPTION BY ITEM CODE
    public static String getCode(string item)
    {
        return EFBroker_Item.GetItembyDescription(item).ItemCode;
    }

    //FIND REQUISITION WITH PENDING STATUS
    public static List<Requisition> getRequisitionList()
    {
        return EFBroker_Requisition.GetAllRequisitionsByStatus("Pending");
    }

    //FIND REQUISITION BY ID
    public static ReqisitionListItem getRequisitionForApprove(int id)
    {
        Requisition r = EFBroker_Requisition.GetRequisitionByID(id);
        date = r.RequestDate.Value.ToLongDateString();
        requisitionNo = Convert.ToInt32(r.RequisitionID.ToString());
        status = r.Status.ToString();
        int empCode = Convert.ToInt32(r.RequestedBy);
        employeeName = EFBroker_DeptEmployee.GetEmployeebyEmpID(empCode).EmpName;
        return new ReqisitionListItem(date, requisitionNo, department, status, employeeName);
    }

    //FIND REQUISITION BY ID
    public static Requisition getRequisition(int id)
    {
        return EFBroker_Requisition.GetRequisitionByID(id);
    }

    public static List<Requisition_ItemList> getList(int id)
    {
        List<Requisition_ItemList> rlist = new List<Requisition_ItemList>();
        List<Requisition_Item> tlist = EFBroker_Requisition.GetRequisitionItemListbyReqID(id);
        foreach(Requisition_Item x in tlist)
        {
            Requisition_ItemList r = new Requisition_ItemList(x.Item.Description, x.RequestedQty, x.Item.UnitOfMeasure, x.Requisition.Status);
            rlist.Add(r);
        }
        return rlist;
    }
    //CANCEL REQUISITION
    public static void cancelRejectRequisition(int id)
    {
        EFBroker_Requisition.CancelRejectRequisition(id);
    }

    //SEARCH REQUISITION BY STATUS
    public static List<ReqisitionListItem> getRequisitionListByStatus(string status)
    {
        List<Requisition> rlist = EFBroker_Requisition.GetAllRequisitionsByStatus(status);
        return PopulateGridViewForDepartment(rlist);
    }

    public static List<ReqisitionListItem> getRequisitionListByEmpIDAndStatus(int empID,string status)
    {
        List<Requisition> rlist = EFBroker_Requisition.getRequisitionListByEmpIDAndStatus(empID,status);
        return PopulateGridViewForDepartment(rlist);
    }
    public static List<ReqisitionListItem> getRequisitionListByStatusAndDepCode(string status, string depCode)
    {
        List<Requisition> rlist = EFBroker_Requisition.getRequisitionListByStatusAndDepCode( status, depCode);
        return PopulateGridViewForDepartment(rlist);
    }
    

    //FIND REQUISITION ITEM BY REQUISITION ID
    public static Requisition_Item findRequisitionID(int id)
    {
        return EFBroker_Requisition.FindReqItemsByReqID(id).FirstOrDefault();
    }

    //FIND REQUISITION ITEM BY REQUISITION ID AND ITEM CODE
    public static Requisition_Item findByReqIDItemCode(int id, string des)
    {
        return EFBroker_Requisition.FindReqItemsByReqIDItemDescription(id, des).FirstOrDefault();
    }

    //REMOVE REQUISITION
    public static void removeRequisitionItem(int id, string code)
    {
        EFBroker_Requisition.removeRequisitionItem(id, code);
    }

    //UPDATE REQUISITION ITEM
    public static void updateRequisitionItem(int id, string code, int qty)
    {
        EFBroker_Requisition.UpdateRequisitionItem(id, code, qty);
    }

    //ADD REQUISITION ITEM
    public static void addItemToRequisition(string code, int qty, int id)
    {
        Requisition_Item ri = new Requisition_Item();
        ri.RequisitionID = id;
        ri.ItemCode = code;
        ri.RequestedQty = qty;
        EFBroker_Requisition.AddItemToRequisition(ri);
    }


    //CHANGE REQUISITION STATUS
    public static void approveRequisition(int id, string reason, int? empID)
    {
        EFBroker_Requisition.ApproveRequisition(id, reason, empID);
    }
    public static void rejectRequisition(int id, string reason, int empID)
    {
        EFBroker_Requisition.RejectRequisition(id, reason, empID);
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
            employeeName = EFBroker_DeptEmployee.GetEmployeebyEmpID(empCode).EmpName;
            item = new ReqisitionListItem(date, requisitionNo, department, status, employeeName);
            itemList.Add(item);
        }
        return itemList;
    }

    public static void editRequisitionItemQty(int id, string code, int qty)
    {
        EFBroker_Requisition.EditRequisitionItemQty(id, code, qty);
    }

    public static void addNewRequisitionItem(List<RequestedItem> item, DateTime date, string status, int requestedBy,string depCode)
    {
        EFBroker_Requisition.AddNewRequisition(item, date, status, requestedBy,depCode);
    }

    // get requisition by emp ID
    public static List<ReqisitionListItem> getRequisitionListByID(int empCode)
    {
        rlist = new List<Requisition>();
        rlist = EFBroker_Requisition.GetRequisitionListByRequestorID(empCode);
        return PopulateGridView(rlist);

    }

    public static List<ReqisitionListItem> getCollectionList(string deptCode)
    {
        rlist = new List<Requisition>();
        rlist = EFBroker_Requisition.getCollectionList(depCode);
        return PopulateGridView(rlist);
    }
    
}