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
    RetrievalControl reqCon = new RetrievalControl();

    protected void Page_Load(object sender, EventArgs e)
    {

        if (RequisitionControl.DisplayAll().Count == 0)
        {
            btnGenerate.Visible = false;
            btnSearch.Visible = false;
            btnDisplay.Visible = false;
            ddlStatus.Visible = false;
            txtSearchBox.Visible = false;
            lblCheckBoxValidation.Text = "There is no pending requisition!";
        }
        else
        {
            if (!IsPostBack)
            {
                gvReq.DataSource = RequisitionControl.DisplayAll();
                gvReq.DataBind();
            }

            if (ddlStatus.Text == "Priority")
            {
                ddlStatus.Text = "Select Status";

                gvReq.DataSource = null;
                gvReq.DataSource = RequisitionControl.DisplayPriority();
                gvReq.DataBind();
            }

            if (ddlStatus.Text == "Approved")
            {
                ddlStatus.Text = "Select Status";

                gvReq.DataSource = null;
                gvReq.DataSource = RequisitionControl.DisplayApproved();
                gvReq.DataBind();
            }
        }
    }


    protected void CheckAll_CheckedChanged(object sender, EventArgs e)
    {
        
        if (((CheckBox)gvReq.HeaderRow.FindControl("cbxCheckAll")).Checked)
        {
            foreach (GridViewRow row in gvReq.Rows)
            {
                ((CheckBox)row.FindControl("cbxCheckBox")).Checked = true;
            }
        }

        if (!((CheckBox)gvReq.HeaderRow.FindControl("cbxCheckAll")).Checked)
        {
            foreach (GridViewRow row in gvReq.Rows)
            {
                ((CheckBox)row.FindControl("cbxCheckBox")).Checked = false;
            }
        }
    }


    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        string searchWord = txtSearchBox.Text;

        gvReq.DataSource = RequisitionControl.DisplaySearch(searchWord);
        gvReq.DataBind();

    }

    protected void BtnDisplay_Click(object sender, EventArgs e)
    {
        ddlStatus.Text = "Select Status";
        gvReq.DataSource = RequisitionControl.DisplayAll();
        gvReq.DataBind();
    }

    protected void BtnGenerate_Click(object sender, EventArgs e)
    {
        bool check = false;

        List<int> requisitionNo = new List<int>();

        foreach (GridViewRow row in gvReq.Rows)
        {
            if (((CheckBox)row.FindControl("cbxCheckBox")).Checked == false)
            {
                lblCheckBoxValidation.Text = "Please select at least one requisition!";
            }
            else if (((CheckBox)row.FindControl("cbxCheckBox")).Checked)
            {
                check = true;
                requisitionNo.Add(Convert.ToInt32((row.FindControl("lblrequisitionNo") as Label).Text));
            }
        }

        if (check)
        {
            int empId = (int)Session["empID"];//////////
            int retrievalId = EFBroker_Disbursement.AddNewRetrieval(empId);
            Session["RetrievalID"] = retrievalId;
            reqCon.AddDisbursement(retrievalId, requisitionNo);
            Response.Redirect("~/Store/RetrievalListDetail.aspx");
        }
    }

    protected void BtnGvDetail_Click(object sender, EventArgs e)
    {
        GridViewRow row = ((Button)sender).NamingContainer as GridViewRow;  //detail btn
        string s = (row.FindControl("lblrequisitionNo") as Label).Text; //row.Cells[2]
        Session["RequisitionNo"] = s;
        Response.Redirect("~/Store/RequisitionDetail.aspx");
    }

    protected void GvReq_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblStatus = (Label)e.Row.FindControl("lblStatus");

            string status = lblStatus.Text;

            if (status == "Approved")
            {
                lblStatus.ForeColor = System.Drawing.Color.Green;
            }
            else if (status == "Priority")
            {
                lblStatus.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}