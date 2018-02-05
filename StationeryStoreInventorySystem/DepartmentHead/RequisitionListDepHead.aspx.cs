using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//AUTHOR : YIMON SOE
//AUTHOR : APRIL SHAR
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
                gvRequisitionForm.DataSource = RequisitionControl.DisplayAllByDeptCode(emp.DeptCode);
                gvRequisitionForm.DataBind();
                ViewState["DataSource"] = "displayAll";
                //Dep Representative
                showEmptyLabel();

                int count = RequisitionControl.CountPending(emp.DeptCode);

                lblPendingCount.Text = "Total Pendings: " + count.ToString();
            }
            else
            {
                Utility.logout();
            }
        }
    }

    public void showEmptyLabel()
    {
        if (gvRequisitionForm.Rows.Count <= 0)
        {
            lblNoList.Visible = true;
            lblNoList.Text = "No Requisition Found.";
        }
        else
        {
            lblNoList.Visible = false;
        }
    }

    protected void DdlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Session["emp"] != null)
        {
            Employee emp = (Employee)Session["emp"];
            if (ddlStatus.SelectedItem.ToString() == "Select Status")
            {
                gvRequisitionForm.DataSource = RequisitionControl.DisplayAllByDeptCode(emp.DeptCode);
                gvRequisitionForm.DataBind();
                ViewState["DataSource"] = "displayAll";
                showEmptyLabel();
            }
            else
            {
                string selectedStatus = ddlStatus.SelectedValue.ToString();
                gvRequisitionForm.DataSource = RequisitionControl.getRequisitionListByStatusAndDepCode(selectedStatus, emp.DeptCode);
                gvRequisitionForm.DataBind();
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
        if (String.IsNullOrWhiteSpace(searchWord))
        {
            ClientScript.RegisterStartupScript(Page.GetType(), "MessageBox", "<script language='javascript'>alert('" + "Please enter value to search!" + "');</script>");
        }
        else
        {
            if (ddlStatus.SelectedItem.ToString() == "Select Status")
            {
                gvRequisitionForm.DataSource = RequisitionControl.HeadSearchWithoutStatus(searchWord.Trim(), emp.DeptCode);
                gvRequisitionForm.DataBind();
                ViewState["DataSource"] = "displaySearch";
                ViewState["searchString"] = searchWord;
                showEmptyLabel();
            }
            else
            {
                gvRequisitionForm.DataSource = RequisitionControl.HeadSearchWithStatus(searchWord.Trim(), emp.DeptCode, ddlStatus.SelectedItem.ToString());
                gvRequisitionForm.DataBind();
                ViewState["DataSource"] = "displaySearchStatus";
                ViewState["searchString"] = searchWord;
                showEmptyLabel();
            }
        }
    }

    protected void BtnDisplayAll_Click(object sender, EventArgs e)
    {
        Employee emp = (Employee)Session["emp"];
        gvRequisitionForm.DataSource = RequisitionControl.DisplayAllByDeptCode(emp.DeptCode);
        gvRequisitionForm.DataBind();
        ViewState["DataSource"] = "displayAll";
        showEmptyLabel();
    }

    protected void GvRequisitionForm_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Employee emp = (Employee)Session["emp"];
        gvRequisitionForm.PageIndex = e.NewPageIndex;
        if (((string)ViewState["DataSource"]).Equals("displayAll"))
        {
            gvRequisitionForm.DataSource = RequisitionControl.DisplayAllByDeptCode(emp.DeptCode);
        }
        else if (((string)ViewState["DataSource"]).Equals("displayStatusSearch"))
        {
            gvRequisitionForm.DataSource = RequisitionControl.getRequisitionListByStatusAndDepCode(ddlStatus.SelectedItem.ToString(), emp.DeptCode);
        }
        else if (((string)ViewState["DataSource"]).Equals("displaySearch"))
        {
            gvRequisitionForm.DataSource = RequisitionControl.HeadSearchWithoutStatus(((string)ViewState["searchString"]).Trim(), emp.DeptCode);
        }
        else
        {
            gvRequisitionForm.DataSource = RequisitionControl.HeadSearchWithStatus(((string)ViewState["searchString"]).Trim(), emp.DeptCode, ddlStatus.SelectedItem.ToString());
        }
        gvRequisitionForm.DataBind();
    }

    protected void GvRequisitionForm_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label statusLabel = (Label)e.Row.FindControl("Label4");
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