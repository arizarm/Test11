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
        if (Request.QueryString["id"] != null && Session["emp"] != null)
        {
            Employee emp = (Employee)Session["emp"];

            id = Convert.ToInt32(Request.QueryString["id"]);

            ReqisitionListItem r = RequisitionControl.getRequisitionForApprove(id);

            lblReqname.Text = r.EmployeeName;
            lblReqDate.Text = r.Date;
            lblStatus.Text = r.Status;

            if (lblStatus.Text.Equals("Approved") || lblStatus.Text.Equals("approved") || lblStatus.Text.Equals("InProgress"))
            {
                lblStatus.ForeColor = System.Drawing.Color.Green;
            }
            else if (lblStatus.Text.Equals("Pending"))
            {
                lblStatus.ForeColor = System.Drawing.Color.Blue;
            }
            else if (lblStatus.Text.Equals("Priority"))
            {
                lblStatus.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                lblStatus.ForeColor = System.Drawing.Color.Black;
            }

            if (r.Status.ToString() != "Pending" || EmployeeController.isDeptHaveTempHead(emp.DeptCode))
            {
                ReasonLabel.Visible = false;
                txtReason.Visible = false;
                btnApprove.Visible = false;
                btnReject.Visible = false;
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
        gvRequisitionDetailList.DataSource = RequisitionControl.getList(id);
        gvRequisitionDetailList.DataBind();
    }


    protected void BtnApprove_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["id"] != null && Session["emp"] != null)
        {
            Employee emp = (Employee)Session["emp"];
            id = Convert.ToInt32(Request.QueryString["id"]);
            string reason = txtReason.Value;
            RequisitionControl.approveRequisition(id, reason, emp.EmpID);

            Page.Response.Redirect(Page.Request.Url.ToString(), true);
            approveSuccess.Text = "You apporved the requisition requested by " + lblReqname.Text + " Successfully";
        }
        else
        {
            approveSuccess.Text = "Process Failed , Please contact server admininstration";
        }

    }

    protected void BtnReject_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["id"] != null && Session["emp"] != null)
        {
            Employee emp = (Employee)Session["emp"];
            id = Convert.ToInt32(Request.QueryString["id"]);
            string reason = txtReason.Value;
            RequisitionControl.rejectRequisition(id, reason, emp.EmpID);

            Page.Response.Redirect(Page.Request.Url.ToString(), true);

            approveSuccess.Text = "You Rejected the requisition requested by " + lblReqname.Text + " Successfully";
        }
        else
        {
            approveSuccess.Text = "Process Failed , Please contact server admininstration";
        }

    }

    protected void backButton_Click(object sender, EventArgs e)
    {
        if (Session["emp"] != null)
        {
            Employee emp = (Employee)Session["emp"];

            if (emp.Role == "DepartmentHead")
            {
                Response.Redirect(LoginController.RequisitionListDepHeadURI);
            }
            else if (emp.Role == "Representative")
            {
                Response.Redirect(LoginController.RequisitionListDepRepURI);
            }
        }



    }

}