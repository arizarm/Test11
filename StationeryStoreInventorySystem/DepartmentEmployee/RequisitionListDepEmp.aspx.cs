using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//AUTHOR : APRIL SHAR
//AUTHOR : YIMON SOE

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
                //Dep Emp
                gvRequisitionList.DataSource = RequisitionControl.getRequisitionListByID(emp.EmpID);
                gvRequisitionList.DataBind();
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
        if (gvRequisitionList.Rows.Count <= 0)
        {
            lblError.Visible = true;
            lblError.Text = "No Requisition Found.";
        }
        else
        {
            lblError.Visible = false;
        }
    }
    protected void DdlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (Session["emp"] != null)
        {
            Employee emp = (Employee)Session["emp"];
            string selectedStatus = ddlStatus.SelectedValue;

            if (ddlStatus.SelectedItem.Text == "Select Status")
            {
                gvRequisitionList.DataSource = RequisitionControl.getRequisitionListByID(emp.EmpID);
                gvRequisitionList.DataBind();
                ViewState["DataSource"] = "displayAll";
                showEmptyLabel();
            }
            else
            {
                gvRequisitionList.DataSource = RequisitionControl.getRequisitionListByEmpIDAndStatus(emp.EmpID, selectedStatus);
                gvRequisitionList.DataBind();
                ViewState["DataSource"] = "displayStatusSearch";
                showEmptyLabel();
            }
        }
        else
        {
            Utility.logout();
        }
    }
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        Employee emp = (Employee)Session["emp"];
        searchWord = txtSearch.Text;
        if (txtSearch.Text == String.Empty)
        {
            ClientScript.RegisterStartupScript(Page.GetType(),
      "MessageBox",
      "<script language='javascript'>alert('" + "Please enter value to search!" + "');</script>");
        }
        else
        {
            if (ddlStatus.SelectedItem.ToString() == "Select Status")
            {
                gvRequisitionList.DataSource = RequisitionControl.SearchForRepRequisitionWithoutStatus(searchWord.Trim(), emp.EmpID);
                gvRequisitionList.DataBind();
                ViewState["DataSource"] = "displaySearch";
                ViewState["searchWord"] = searchWord;
                showEmptyLabel();
            }
            else
            {
                gvRequisitionList.DataSource = RequisitionControl.SearchForRepRequisitionWithStatus(searchWord.Trim(), emp.EmpID, ddlStatus.SelectedItem.ToString());
                gvRequisitionList.DataBind();
                ViewState["DataSource"] = "displaySearchStatus";
                ViewState["searchWord"] = searchWord;
                showEmptyLabel();
            }
        }
    }

    protected void BtnDisplay_Click(object sender, EventArgs e)
    {
        if (Session["emp"] != null)
        {
            Employee emp = (Employee)Session["emp"];
            gvRequisitionList.DataSource = RequisitionControl.getRequisitionListByID(emp.EmpID);
            gvRequisitionList.DataBind();
            ViewState["DataSource"] = "displayAll";
            showEmptyLabel();
        }
        else
        {
            Utility.logout();
        }
    }

    protected void GVRequisitionList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Employee emp = (Employee)Session["emp"];
        gvRequisitionList.PageIndex = e.NewPageIndex;
        if(((string)ViewState["DataSource"]).Equals("displayAll"))
        {
            gvRequisitionList.DataSource = RequisitionControl.getRequisitionListByID(emp.EmpID);
        }
        else if (((string)ViewState["DataSource"]).Equals("displayStatusSearch"))
        {
            gvRequisitionList.DataSource = RequisitionControl.getRequisitionListByEmpIDAndStatus(emp.EmpID, ddlStatus.SelectedItem.ToString());
        }
        else if (((string)ViewState["DataSource"]).Equals("displaySearch"))
        {
            gvRequisitionList.DataSource = RequisitionControl.SearchForRepRequisitionWithoutStatus(((string)ViewState["searchWord"]).Trim(), emp.EmpID);
        }
        else
        {
            gvRequisitionList.DataSource = RequisitionControl.SearchForRepRequisitionWithStatus(((string)ViewState["searchWord"]).Trim(), emp.EmpID, ddlStatus.SelectedItem.ToString());
        }
        gvRequisitionList.DataBind();
    }

    protected void GVRquisitionList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label statusLabel = (Label)e.Row.FindControl("lblStatus");
            statusLabel.Font.Bold = true;
            string status = statusLabel.Text;
            statusLabel.Font.Bold = true;

            if (status == "Approved" || status=="approved" || status=="InProgress")
            {
                statusLabel.ForeColor = System.Drawing.Color.Green;
            }
            else if (status == "Priority")
            {
                statusLabel.ForeColor = System.Drawing.Color.Red;
            }
            else if(status=="Pending")
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
