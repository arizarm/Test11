using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ReqisitionListClerk : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            gvReq.DataSource = RequisitionControl.DisplayAll();        
        }

        if (DropDownList1.Text == "Priority")
        {
            gvReq.DataSource = RequisitionControl.DisplayPriority();
        }
        else if (DropDownList1.Text == "Approved")
        {
            gvReq.DataSource = RequisitionControl.DisplayApproved();           
        }
         gvReq.DataBind();
    }

    protected void GenerateBtn_Click(object sender, EventArgs e)
    {

    }


    protected void SearchBtn_Click(object sender, EventArgs e)
    {

        string searchWord = SearchBox.Text;

        if (SearchBox.Text == String.Empty)
        {
            ClientScript.RegisterStartupScript(Page.GetType(),
      "MessageBox",
      "<script language='javascript'>alert('" + "Please enter value to search!" + "');</script>");
        }
        else
        {
            gvReq.DataSource = RequisitionControl.DisplaySearch(searchWord);
            gvReq.DataBind();

            //gvReq.DataSource = context.Requisitions.Where(x => x.RequestDate.ToString().Contains(searchWord)|| x.RequisitionID.ToString().Contains(searchWord) || x.RequestedBy.ToString().Contains((searchWord)) || x.Status.Contains(searchWord)).ToList();
            //gvReq.DataBind();
        }
    }

    protected void DisplayBtn_Click(object sender, EventArgs e)
    {
        gvReq.DataSource = RequisitionControl.DisplayAll();
        gvReq.DataBind();

        ////gvReq.DataSource = context.Requisitions.ToList();
        ////gvReq.DataBind();
        //List<Requisition> rlist = new List<Requisition>();
        //rlist = context.Requisitions.ToList();

        //string date;
        //int requisitionNo;
        //string department;
        //string status;

        //int requestedBy;
        //string depCode;

        //ReqisitionListItem item;
        //List<ReqisitionListItem> itemList = new List<ReqisitionListItem>();

        //    foreach (Requisition r in rlist)
        //    {
        //        date = r.RequestDate.Value.ToLongDateString();
        //        requisitionNo = Convert.ToInt32(r.RequisitionID.ToString());
        //        status = r.Status.ToString();

        //        requestedBy = Convert.ToInt32(r.RequestedBy.ToString());
        //        depCode = context.Employees.Where(x => x.EmpID.Equals(requestedBy)).Select(x => x.DeptCode).First().ToString();

        //        department = context.Departments.Where(x => x.DeptCode.Equals(depCode)).Select(x => x.DeptName).First().ToString();
        //        item = new ReqisitionListItem(date, requisitionNo, department, status);
        //        itemList.Add(item);
        //    }
        //    gvReq.DataSource = itemList;
        //    gvReq.DataBind();
    }

    protected void GenerateBtn_Click1(object sender, EventArgs e)
    {
        string data = "";
        // List<String> tempList = new List<String>();
        ArrayList tempList = new ArrayList();

        foreach (GridViewRow row in gvReq.Rows)
        {
            //if (row.RowType == DataControlRowType.DataRow)
            //{
            CheckBox chkRow = (row.Cells[0].FindControl("CheckBox") as CheckBox);
            if (chkRow.Checked && chkRow != null)
            {
                //string temp = row.Cells[0].Text;
                string temp = gvReq.DataKeys[row.DataItemIndex].Values["RequisitionID"].ToString();
                data = temp;
            }
            // }
        }

        Label6.Text = data;

        //Response.Redirect("RetrievalList.aspx");


        //
        //DataTable dt = new DataTable();
        //dt.Columns.AddRange(new DataColumn[4] { new DataColumn("RequestDate"), new DataColumn("RequisitionID"), new DataColumn("RequestedBy"), new DataColumn("Status") });
        //foreach (GridViewRow row in GridView1.Rows)
        //{
        //    if (row.RowType == DataControlRowType.DataRow)
        //    {
        //        CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);
        //        if (chkRow.Checked)
        //        {
        //            string RequestDate = row.Cells[1].Text;
        //            string RequisitionID = (row.Cells[2].FindControl("RequisitionID") as Label).Text;
        //            dt.Rows.Add(RequestDate, RequisitionID);
        //        }
        //    }
        //}
        //GridView1.DataSource = dt;
        //GridView1.DataBind();

        //
    }
}