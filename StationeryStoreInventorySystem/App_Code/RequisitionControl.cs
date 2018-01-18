using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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

    static ReqisitionListItem item;
    static List<ReqisitionListItem> itemList;
    static List<ReqisitionListItem> searchList;

    public static List<ReqisitionListItem> DisplayAll()
    {
        rlist = new List<Requisition>();
        rlist = context.Requisitions.ToList();
        return PopulateGridView(rlist);
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
        //rlist = new List<Requisition>();
        //rlist = context.Requisitions.ToList();

        //searchList = new List<ReqisitionListItem>();

        //foreach (ReqisitionListItem i in PopulateGridView(rlist))
        //{
        //if(i.Date== searchWord || i.Department == searchWord|| i.RequisitionNo == Convert.ToInt32(searchWord)|| i.Status == searchWord)
        //    {
        //        i = searchList;
        //    }
        //}

        itemList = DisplayAll();
        foreach(ReqisitionListItem i in itemList)
        {
            searchList = itemList.Where(x => x.Date.ToLower().Contains(searchWord.ToLower()) || x.RequisitionNo.ToString().Contains(searchWord)|| x.Department.ToLower().Contains(searchWord.ToLower()) || x.Status.ToLower().Contains(searchWord.ToLower())).ToList();

            //searchList = itemList.Where(x => x.RequisitionNo.ToString().Contains(searchWord)).ToList();
            //x.Date.ToLower().Contains(searchWord.ToLower()) || Convert.ToString(x.RequisitionNo).Contains(searchWord)) ||
        }

        //gvReq.DataSource = context.Requisitions.Where(x => x.RequestDate.ToString().Contains(searchWord)|| x.RequisitionID.ToString().Contains(searchWord) || x.RequestedBy.ToString().Contains((searchWord)) || x.Status.Contains(searchWord)).ToList();
        //gvReq.DataBind();

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
            item = new ReqisitionListItem(date, requisitionNo, department, status);
            itemList.Add(item);
        }
        return itemList;
    } 
}