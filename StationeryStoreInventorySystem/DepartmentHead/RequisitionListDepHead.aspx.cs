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
                showEmptyLabel();

                int count = RequisitionControl.CountPending(emp.DeptCode);

                pendingCount.Text = "Total Pendings: " + count.ToString();
            }
            else
            {
                Utility.logout();
            }
        }
    }

    public void showEmptyLabel()
    {
        if (GridView1.Rows.Count <= 0)
        {
            Label5.Visible = true;
            Label5.Text = "No Requisition Found.";
        }
        else
        {
            Label5.Visible = false;
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
                showEmptyLabel();
            }
            else
            {
                string selectedStatus = DropDownList1.SelectedItem.ToString();
                GridView1.DataSource = RequisitionControl.getRequisitionListByStatusAndDepCode(DropDownList1.SelectedItem.ToString(), emp.DeptCode);
                GridView1.DataBind();
                ViewState["DataSource"] = "displayStatusSearch";
                showEmptyLabel();
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
                ViewState["searchString"] = searchWord;
                showEmptyLabel();
            }
            else
            {
                GridView1.DataSource = RequisitionControl.HeadSearchWithStatus(searchWord.Trim(), emp.DeptCode, DropDownList1.SelectedItem.ToString());
                GridView1.DataBind();
                ViewState["DataSource"] = "displaySearchStatus";
                ViewState["searchString"] = searchWord;
                showEmptyLabel();
            }
        }
    }

    protected void DisplayBtn_Click(object sender, EventArgs e)
    {
        Employee emp = (Employee)Session["emp"];
        GridView1.DataSource = RequisitionControl.DisplayAllByDeptCode(emp.DeptCode);
        GridView1.DataBind();
        ViewState["DataSource"] = "displayAll";
        showEmptyLabel();
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
            GridView1.DataSource = RequisitionControl.HeadSearchWithoutStatus(((string)ViewState["searchString"]).Trim(), emp.DeptCode);
        }
        else
        {
            GridView1.DataSource = RequisitionControl.HeadSearchWithStatus(((string)ViewState["searchString"]).Trim(), emp.DeptCode, DropDownList1.SelectedItem.ToString());
        }
        GridView1.DataBind();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label statusLabel = (Label)e.Row.FindControl("Label4");

            string status = statusLabel.Text;
            if (status == "Approved" || status == "approved" || status == "InProgress")
            {
                statusLabel.ForeColor = System.Drawing.Color.Green;
            }
            else if (status == "Priority")
            {
                statusLabel.ForeColor = System.Drawing.Color.Red;
            }
            else if (status == "Pending")
            {
                statusLabel.ForeColor = System.Drawing.Color.Blue;
            }
            else
            {
                statusLabel.ForeColor = System.Drawing.Color.Black;
            }
        }
    }
}