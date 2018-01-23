using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ApproveRequisition : System.Web.UI.Page
{
    StationeryEntities context = new StationeryEntities();
    
    int id = 0;
    string des;
    protected void Page_Load(object sender, EventArgs e)
    {
        if(Request.QueryString["id"] != null)
        {
            id = Convert.ToInt32(Request.QueryString["id"]);

            ReqisitionListItem r = RequisitionControl.getRequisitionForApprove(id);

            Label1.Text = r.EmployeeName;
            Label2.Text = r.Date;
            Label3.Text = r.Status;

            if (r.Status.ToString() != "Pending")
            {
                ReasonLabel.Visible = false;
                TextBox2.Visible = false;
                ApproveButton.Visible = false;
                RejectButton.Visible = false;
            }
        }
        else
        {
            Utility.logout();
        }
        if (!IsPostBack)
        {
            showAllItems();
        }

    }

    protected void showAllItems()
    {
        GridView1.DataSource = RequisitionControl.getList(id);
        GridView1.DataBind();
    }

    protected void Cancel_Click(object sender, EventArgs e)
    {
        try
        {
            id = Convert.ToInt32(Request.QueryString["id"]);
            RequisitionControl.cancelRejectRequisition(id);

            Response.Redirect("ReqisitionListDepartment.aspx");
            //Response.Write("<script language='javascript'>alert('Requisition has been cancelled');</script>");
        }
        catch (Exception ex)
        {
            Response.Write("<script language='javascript'>alert('Error! Retry.');</script>");
        }
    }



    protected void ApproveButton_Click(object sender, EventArgs e)
    {
        id = Convert.ToInt32(Request.QueryString["id"]);
        string reason = ReasonLabel.Text;
        RequisitionControl.approveRequisition(id, reason);

        approveSuccess.Text = "Approved Success";

    }

    protected void RejectButton_Click(object sender, EventArgs e)
    {
        id = Convert.ToInt32(Request.QueryString["id"]);
        string reason = ReasonLabel.Text;
        RequisitionControl.rejectRequisition(id, reason);

        approveSuccess.Text = "Rejected Success";
    }

    protected void backButton_Click(object sender, EventArgs e)
    {

    }
}