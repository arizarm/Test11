using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Transactions;

public partial class RequisitionDetails : System.Web.UI.Page
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
        Label2.Text = r.RequestedBy.ToString();
        Label3.Text = r.RequestDate.ToString();
        Label4.Text = r.Status.ToString();

        if (!IsPostBack)
        {
            showAllItems();
            if(r.Status=="Rejected" || r.Status=="Closed")
            {
                Cancel.Visible = false;
                Add.Visible = false;
            }

            DropDownList2.DataSource = ReqBS.getItem();
            DropDownList2.DataBind();
        }

        des = DropDownList2.SelectedItem.ToString();
        Label6.Text = ReqBS.getUOM(des);
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

    protected void Add_Click(object sender, EventArgs e)
    {
        Panel1.Visible = true;
        
    }

    protected void New_Click(object sender, EventArgs e)
    {
        id = Convert.ToInt32(Request.QueryString["id"]);
        string code = ReqBS.getCode(des);
        int qty = Convert.ToInt32(TextBox1.Text);

        ReqBS.addItemToRequisition(code, qty, id);


        showAllItems();
    }

    protected void Close_Click(object sender, EventArgs e)
    {
        Panel1.Visible = false;
        Add.Visible = true;
    }

    protected void ApproveButton_Click(object sender, EventArgs e)
    {
      

        id = Convert.ToInt32(Request.QueryString["id"]);
        string reason = ReasonLabel.Text;
        ReqBS.approveRequisition(id,reason);

        approveSuccess.Text = "Approved Success";      
    }
}


