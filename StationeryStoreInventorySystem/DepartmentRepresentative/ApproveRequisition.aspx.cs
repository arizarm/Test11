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
        if (Request.QueryString["id"] != null)
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


    protected void ApproveButton_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["id"] != null && Session["emp"] != null)
        {
            Employee emp = (Employee)Session["emp"];
            id = Convert.ToInt32(Request.QueryString["id"]);
            string reason = TextBox2.Text;
            RequisitionControl.approveRequisition(id, reason, emp.EmpID);

            Page.Response.Redirect(Page.Request.Url.ToString(), true);

            approveSuccess.Text = "You apporved the requisition requested by " + Label1.Text + " Successfully";
        }
        else
        {
            approveSuccess.Text = "Process Failed , Please contact server admininstration";
        }

    }

    protected void RejectButton_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["id"] != null && Session["emp"] != null)
        {
            Employee emp = (Employee)Session["emp"];
            id = Convert.ToInt32(Request.QueryString["id"]);
            string reason = TextBox2.Text;
            RequisitionControl.rejectRequisition(id, reason, emp.EmpID);

            Page.Response.Redirect(Page.Request.Url.ToString(), true);

            approveSuccess.Text = "You Rejected the requisition requested by " + Label1.Text + " Successfully";
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
                Response.Redirect(LoginController.RequisitionListTempDepRedURI);
            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/DepartmentRepresentative/CollectionListDepRep.aspx");
    }
}