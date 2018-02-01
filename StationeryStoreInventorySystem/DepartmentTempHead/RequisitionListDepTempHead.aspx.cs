using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ReqisitionListEmployee : System.Web.UI.Page
{
    string searchWord = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["emp"] != null)
            {
                Employee emp = (Employee)Session["emp"];

                //Dep Head
                GridView1.DataSource = RequisitionControl.DisplayAllByDeptCode(emp.DeptCode);
                GridView1.DataBind();
                ViewState["DataSource"] = "displayAll";
                //Dep Representative

                int count = RequisitionControl.CountPending(emp.DeptCode);

                pendingCount.Text = "Total Pendings: " + count.ToString();
            }
            else
            {
                Utility.logout();
            }
        }
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Session["emp"] != null)
        {
            Employee emp = (Employee)Session["emp"];
            if (DropDownList1.SelectedItem.ToString() == "Select Status")
            {
                GridView1.DataSource = RequisitionControl.DisplayAllByDeptCode(emp.DeptCode);
                GridView1.DataBind();
                ViewState["DataSource"] = "displayAll";
            }
            else
            {
                string selectedStatus = DropDownList1.SelectedItem.ToString();
                GridView1.DataSource = RequisitionControl.getRequisitionListByStatusAndDepCode(DropDownList1.SelectedItem.ToString(), emp.DeptCode);
                GridView1.DataBind();
                ViewState["DataSource"] = "displayStatusSearch";
            }
        }
        else
        {
            Utility.logout();
        }
    }
    protected void SearchBtn_Click(object sender, EventArgs e)
    {
        Employee emp = (Employee)Session["emp"];
        searchWord = SearchBox.Text;
        if (String.IsNullOrWhiteSpace(searchWord))
        {
            ClientScript.RegisterStartupScript(Page.GetType(), "MessageBox", "<script language='javascript'>alert('" + "Please enter value to search!" + "');</script>");
        }
        else
        {
            if (DropDownList1.SelectedItem.ToString() == "Select Status")
            {
                GridView1.DataSource = RequisitionControl.HeadSearchWithoutStatus(searchWord.Trim(), emp.DeptCode);
                GridView1.DataBind();
                ViewState["DataSource"] = "displaySearch";
            }
            else
            {
                GridView1.DataSource = RequisitionControl.HeadSearchWithStatus(searchWord.Trim(), emp.DeptCode, DropDownList1.SelectedItem.ToString());
                GridView1.DataBind();
                ViewState["DataSource"] = "displaySearchStatus";
            }
        }
    }

    protected void DisplayBtn_Click(object sender, EventArgs e)
    {
        Employee emp = (Employee)Session["emp"];
        GridView1.DataSource = RequisitionControl.DisplayAllByDeptCode(emp.DeptCode);
        GridView1.DataBind();
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Employee emp = (Employee)Session["emp"];
        GridView1.PageIndex = e.NewPageIndex;
        if (((string)ViewState["DataSource"]).Equals("displayAll"))
        {
            GridView1.DataSource = RequisitionControl.DisplayAllByDeptCode(emp.DeptCode);
        }
        else if (((string)ViewState["DataSource"]).Equals("displayStatusSearch"))
        {
            GridView1.DataSource = RequisitionControl.getRequisitionListByStatusAndDepCode(DropDownList1.SelectedItem.ToString(), emp.DeptCode);
        }
        else if (((string)ViewState["DataSource"]).Equals("displaySearch"))
        {
            GridView1.DataSource = RequisitionControl.HeadSearchWithoutStatus(searchWord.Trim(), emp.DeptCode);
        }
        else
        {
            GridView1.DataSource = RequisitionControl.HeadSearchWithStatus(searchWord.Trim(), emp.DeptCode, DropDownList1.SelectedItem.ToString());
        }
        GridView1.DataBind();
    }
}