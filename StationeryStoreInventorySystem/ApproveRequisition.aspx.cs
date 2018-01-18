using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ApproveRequisition : System.Web.UI.Page
{
    StationeryEntities context = new StationeryEntities();
    Requisition r = new Requisition();
    int id = 0;
    string des;
    protected void Page_Load(object sender, EventArgs e)
    {
        id = Convert.ToInt32(Request.QueryString["id"]);
        //int id = 24;


        r = ReqBS.getRequisition(id);
        Session["empRole"] = "Head";
        //Session["empRole"] = "Employee";

        Label1.Text = r.RequestedBy.ToString();
        Label2.Text = r.RequestDate.ToString();
        Label3.Text = r.Status.ToString();

        if (!IsPostBack)
        {
            showAllItems();
        }

        if (Session["empRole"].ToString() == "Head")
        {
            ApproveButton.Visible = true;
            ReasonLabel.Visible = true;
        }
        else if (Session["empRole"].ToString() == "Employee")
        {
            ApproveButton.Visible = false;
            ReasonLabel.Visible = false;
        }
        
    }

    protected void showAllItems()
    {
        var q = from i in context.Items
                join ri in context.Requisition_Item
                on i.ItemCode equals ri.ItemCode
                join rt in context.Requisitions
                on ri.RequisitionID equals rt.RequisitionID
                where ri.RequisitionID == id
                select new
                {
                    i.Description,
                    ri.RequestedQty,
                    i.UnitOfMeasure,
                    rt.Status
                };

        GridView1.DataSource = q.ToList();
        GridView1.DataBind();
    }

    protected void Cancel_Click(object sender, EventArgs e)
    {
        try
        {
            id = Convert.ToInt32(Request.QueryString["id"]);
            ReqBS.cancelRejectRequisition(id);

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
        ReqBS.approveRequisition(id, reason);

        approveSuccess.Text = "Approved Success";

    }

    protected void RejectButton_Click(object sender, EventArgs e)
    {
        id = Convert.ToInt32(Request.QueryString["id"]);
        string reason = ReasonLabel.Text;
        ReqBS.rejectRequisition(id, reason);

        approveSuccess.Text = "Rejected Success";
    }
}