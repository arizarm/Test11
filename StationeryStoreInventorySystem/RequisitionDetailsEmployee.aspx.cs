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
        LoadData();

        if (!IsPostBack)
        {
            showAllItems();
            if (r.Status == "Rejected" || r.Status == "Closed" || r.Status=="Approved")
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

    public void LoadData()
    {
       
        //int id = 24;

        r = ReqBS.getRequisition(id);
        Label5.Text = "ENGL/" + r.RequisitionID;
        Label2.Text = r.RequestedBy.ToString();
        Label3.Text = r.RequestDate.ToString();
        Label4.Text = r.Status.ToString();

        if (r.Status == "Rejected")
        {
            Label1.Visible = true;
            remarks.Text = r.Remarks.ToString();
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

    protected void Add_Click(object sender, EventArgs e)
    {
        Panel1.Visible = true;
        Add.Visible = false;
        Close.Visible = true;

    }

    protected void Close_Click(object sender, EventArgs e)
    {
        Panel1.Visible = false;
        Add.Visible = true;
        Close.Visible = false;

    }

    protected void New_Click(object sender, EventArgs e)
    {
        id = Convert.ToInt32(Request.QueryString["id"]);
        string code = ReqBS.getCode(des);
        int qty = Convert.ToInt32(TextBox1.Text);

        ReqBS.addItemToRequisition(code, qty, id);


        showAllItems();
    }

    protected void Delete_Click(object sender, EventArgs e)
    {
        //LoadData();
        GridViewRow row = ((System.Web.UI.WebControls.Button)sender).Parent.Parent as GridViewRow;
        string itemDes = GridView1.DataKeys[row.RowIndex].Value.ToString();


        Requisition_Item rItem = ReqBS.findByReqIDItemCode(id, itemDes);
        string iCode = rItem.ItemCode;
        int rId = rItem.RequisitionID;
        ReqBS.removeRequisitionItem(rId,iCode);

        showAllItems();
    }

    protected void ReqRow_Updating(object sender, GridViewUpdateEventArgs e)
    {
        System.Web.UI.WebControls.TextBox qtyText = (System.Web.UI.WebControls.TextBox)GridView1.Rows[e.RowIndex].FindControl("qtyText");
        int newQty = Convert.ToInt32(qtyText.Text);

        System.Web.UI.WebControls.Label itemDescLabel = (System.Web.UI.WebControls.Label)GridView1.Rows[e.RowIndex].FindControl("itemDes");
        string itemDesc = itemDescLabel.Text;

        Requisition_Item item = ReqBS.findByReqIDItemCode(id, itemDesc);
        string iCode = item.ItemCode;
        int rId = item.RequisitionID;

        ReqBS.updateRequisitionItem(rId,iCode, newQty);

        GridView1.EditIndex = -1;
        showAllItems();
    }

    protected void RowEdit(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        showAllItems();
    }

    protected void RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        showAllItems();
    }
}

