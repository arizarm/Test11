using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ReqisitionListEmployee : System.Web.UI.Page
{
    string searchWord = "";
    Employee emp = new Employee();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["emp"] != null)
            {
                emp = (Employee)Session["emp"];
                //Approved requisition
                gvCollectionList.DataSource = RequisitionControl.getCollectionList(emp.DeptCode);
                gvCollectionList.DataBind();
                    ViewState["DataSource"] = "displayAll";
                showEmptyLabel();
            }
            else
            {
                Utility.logout();
            }
        }
    }

    public void showEmptyLabel()
    {
        if (gvCollectionList.Rows.Count <= 0)
        {
            lblError.Visible = true;
            lblError.Text = "No Requisition Found.";
        }
        else
        {
            lblError.Visible = false;
        }
    }

    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        Employee emp = (Employee)Session["emp"];
        if (Session["emp"] != null)
        {
            searchWord = txtSearch.Text;
            if (txtSearch.Text == String.Empty)
            {
                ClientScript.RegisterStartupScript(Page.GetType(),
                "MessageBox",
                "<script language='javascript'>alert('" + "Please enter value to search!" + "');</script>");
            }
            else
            {
                gvCollectionList.DataSource = RequisitionControl.DisplayCollectionListSearch(emp.DeptCode, searchWord.Trim());
                gvCollectionList.DataBind();
                ViewState["DataSource"] = "displaySearch";
                ViewState["searchWord"] = searchWord;
                showEmptyLabel();
            }
        }
        else

        {
            Utility.logout();
        }
    }
    protected void BtnDisplayAll_Click(object sender, EventArgs e)
    {
        if (Session["emp"] != null)
        {
            Employee emp = (Employee)Session["emp"];
            gvCollectionList.DataSource = RequisitionControl.getCollectionList(emp.DeptCode);
            gvCollectionList.DataBind();
            ViewState["DataSource"] = "displayAll";
            showEmptyLabel();
        }
        else
        {
            Utility.logout();
        }
    }
    protected void GVCollectionList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Employee emp = (Employee)Session["emp"];
        gvCollectionList.PageIndex = e.NewPageIndex;
        if(((string)ViewState["DataSource"]).Equals("displayAll"))
        {
            gvCollectionList.DataSource = RequisitionControl.getCollectionList(emp.DeptCode);
        }
        else
        {
            gvCollectionList.DataSource = RequisitionControl.DisplayCollectionListSearch(emp.DeptCode, ((string)ViewState["searchWord"]).Trim());
        }
        gvCollectionList.DataBind();
    }

    protected void GVCollectionList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label statusLabel = (Label)e.Row.FindControl("lblStatus");
            statusLabel.Font.Bold = true;
            string status = statusLabel.Text;
            if (status == "Approved" || status == "approved" || status == "InProgress")
            {
                statusLabel.ForeColor = System.Drawing.Color.Green;
            }
            else if (status == "Priority")
            {
                statusLabel.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                statusLabel.ForeColor = System.Drawing.Color.Black;
            }
        }
    }
}