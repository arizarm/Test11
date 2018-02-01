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
            GenerateBtn.Visible = false;
            SearchBtn.Visible = false;
            DisplayBtn.Visible = false;
            DropDownList1.Visible = false;
            SearchBox.Visible = false;
            CheckBoxValidation.Text = "There is no pending requisition!";
        }
        else
        {
            if (!IsPostBack)
            {
                gvReq.DataSource = RequisitionControl.DisplayAll();
                gvReq.DataBind();
            }

            if (DropDownList1.Text == "Priority")
            {
                DropDownList1.Text = "Select Status";

                gvReq.DataSource = null;
                gvReq.DataSource = RequisitionControl.DisplayPriority();
                gvReq.DataBind();
            }

            if (DropDownList1.Text == "Approved")
            {
                DropDownList1.Text = "Select Status";

                gvReq.DataSource = null;
                gvReq.DataSource = RequisitionControl.DisplayApproved();
                gvReq.DataBind();
            }
        }
    }


    protected void CheckAll_CheckedChanged(object sender, EventArgs e)
    {
        if (((CheckBox)gvReq.HeaderRow.FindControl("CheckAll")).Checked)
        {
            foreach (GridViewRow row in gvReq.Rows)
            {
                ((CheckBox)row.FindControl("CheckBox")).Checked = true;
            }
        }

        if (!((CheckBox)gvReq.HeaderRow.FindControl("CheckAll")).Checked)
        {
            foreach (GridViewRow row in gvReq.Rows)
            {
                ((CheckBox)row.FindControl("CheckBox")).Checked = false;
            }
        }
    }


    protected void SearchBtn_Click(object sender, EventArgs e)
    {
        string searchWord = SearchBox.Text;

        gvReq.DataSource = RequisitionControl.DisplaySearch(searchWord);
        gvReq.DataBind();

    }

    protected void DisplayBtn_Click(object sender, EventArgs e)
    {
        DropDownList1.Text = "Select Status";
        gvReq.DataSource = RequisitionControl.DisplayAll();
        gvReq.DataBind();
    }

    protected void GenerateBtn_Click(object sender, EventArgs e)
    {
        bool check = false;

        List<int> requisitionNo = new List<int>();

        foreach (GridViewRow row in gvReq.Rows)
        {
            if (((CheckBox)row.FindControl("CheckBox")).Checked == false)
            {
                CheckBoxValidation.Text = "Please select at least one requisition!";
            }
            else if (((CheckBox)row.FindControl("CheckBox")).Checked)
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

    protected void gvDetailBtn_Click(object sender, EventArgs e)
    {
        GridViewRow row = ((Button)sender).NamingContainer as GridViewRow;  //detail btn
        string s = (row.FindControl("lblrequisitionNo") as Label).Text; //row.Cells[2]
        Session["RequisitionNo"] = s;
        Response.Redirect("~/Store/RequisitionDetail.aspx");
    }

    protected void gvReq_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label statusLabel = (Label)e.Row.FindControl("Label1");

            string status = statusLabel.Text;

            if (status == "Approved")
            {
                statusLabel.ForeColor = System.Drawing.Color.Green;
            }
            else if (status == "Priority")
            {
                statusLabel.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}