using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.Transactions; 

public partial class RequisitionForm : System.Web.UI.Page
{
    //ReqBS bs = new ReqBS();
    List<RequestedItem> rItem = new List<RequestedItem>();
    //ArrayList reqItem =new ArrayList();
    RequestedItem ri;
    string des;
    protected void Page_Load(object sender, EventArgs e)
    {
        Label1.Text = DateTime.Now.ToLongDateString();
        if(!IsPostBack)
        {
            int id = Convert.ToInt32(RequisitionControl.getLastReq()) + 1;
            Label3.Text = "Form:ENGL/" + id;
            ViewState["list"] = rItem;
            DropDownList1.DataSource = RequisitionControl.getItem();
            DropDownList1.DataBind();
        }
        rItem = (List<RequestedItem>)ViewState["list"];
        des = DropDownList1.SelectedItem.ToString();
        Label2.Text = RequisitionControl.getUOM(des);
        Label4.Text = RequisitionControl.getCode(des);
        Label4.Visible = false;
    }

    protected void Add_Click(object sender, EventArgs e)
    {
        //reqItem = new ArrayList();
        des = DropDownList1.SelectedItem.ToString();
        int qty = Convert.ToInt32(TextBox4.Text);

        if (GridView2.Rows.Count <= 0)
        {
            ri = new RequestedItem(Label4.Text, des, qty, Label2.Text);
            rItem = (List<RequestedItem>)ViewState["list"];
            rItem.Add(ri);
            //reqItem.Add(ri);
            ViewState["list"] = rItem;
        }
        
        else
        {
            bool isEqual = false;
            foreach(GridViewRow row in GridView2.Rows)
            {
                System.Web.UI.WebControls.Label labelDes = (System.Web.UI.WebControls.Label)row.FindControl("code");
                string item = labelDes.Text;

                if (Label4.Text.Equals(item))
                {
                    isEqual = true;
                }
            }
            if(isEqual)
            {
                Response.Write("<script>alert('Item is already in the form.');</script>");
            }
            else
            {
                ri = new RequestedItem(Label4.Text, des, qty, Label2.Text);
                rItem = (List<RequestedItem>)ViewState["list"];
                rItem.Add(ri);
                ViewState["list"] = rItem;
            }
        }
        bindGrid();
    }

    public void bindGrid()
    {
        GridView2.DataSource = rItem;
        GridView2.DataBind();
    }

    protected void Submit_Click(object sender, EventArgs e)
    {
        if(GridView2.Rows.Count<=0)
        {
            Response.Write("<script>alert('You have not requested any item yet!');</script>");
        }
        using (TransactionScope ts = new TransactionScope())
        {
            StationeryEntities context = new StationeryEntities();
            Requisition r = new Requisition();
            r.RequestDate = DateTime.Now;
            r.Status = "Pending";
            r.RequestedBy = 1028;

            context.Requisitions.Add(r);
            context.SaveChanges();

            foreach (GridViewRow row in GridView2.Rows)
            {
                System.Web.UI.WebControls.Label Newqty = (System.Web.UI.WebControls.Label)row.FindControl("Label6");
                int item = Convert.ToInt32(Newqty.Text);

                System.Web.UI.WebControls.Label iCode = (System.Web.UI.WebControls.Label)row.FindControl("code");
                string code = iCode.Text;

                Requisition_Item ri = new Requisition_Item();
                ri.RequisitionID=r.RequisitionID;
                //string code = row.Cells[0].Text;

                ri.ItemCode = code;
                ri.RequestedQty = item;
                context.Requisition_Item.Add(ri);
                context.SaveChanges();
            }
            
            ts.Complete();
        }
        //Response.Write("<script language='javascript'>alert('Requisition Submitted');</script>");
        //Server.Transfer("RequisitionListDepartment.aspx", true);
        Response.Redirect("RequisitionListDepartment.aspx");

    }

    protected void Delete_Click(object sender, EventArgs e)
    {
        GridViewRow row = ((System.Web.UI.WebControls.Button)sender).Parent.Parent as GridViewRow;
        string itemDes = GridView2.DataKeys[row.RowIndex].Value.ToString();

        RequestedItem i= rItem.Find(r => r.Code.Equals(itemDes));

        rItem = (List<RequestedItem>)ViewState["list"];
        rItem.Remove(i);
        ViewState["list"] = rItem;
        
        bindGrid();
    }

    protected void ReqRow_Updating(object sender, GridViewUpdateEventArgs e)
    {
        System.Web.UI.WebControls.TextBox qtyText = (System.Web.UI.WebControls.TextBox)GridView2.Rows[e.RowIndex].FindControl("qtyText");
        int newQty = Convert.ToInt32(qtyText.Text);

        System.Web.UI.WebControls.Label itemDescLabel = (System.Web.UI.WebControls.Label)GridView2.Rows[e.RowIndex].FindControl("code");
        string code = itemDescLabel.Text;

        RequestedItem i = rItem.Find(r => r.Code.Equals(code));
        rItem = (List<RequestedItem>)ViewState["list"];

        i.Quantity = Convert.ToInt32(newQty);
        rItem[e.RowIndex].Quantity = i.Quantity;
        ViewState["list"] = rItem;

        GridView2.EditIndex = -1;
        bindGrid();
    }

    protected void RowEdit(object sender, GridViewEditEventArgs e)
    {
        GridView2.EditIndex = e.NewEditIndex;
        bindGrid();
    }

    protected void RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView2.EditIndex = -1;
        bindGrid();
    }

}

[Serializable]
public class RequestedItem
{
    private string code;
    private string description;
    private int quantity;
    private string uom;

    public RequestedItem(string code, string description, int quantity, string uom)
    {
        this.code = code;
        this.description = description;
        this.quantity = quantity;
        this.uom = uom;
    }

    public string Code { get { return code; } set { code = value; } }
    public string Description { get { return description; } set { description = value; } }
    public int Quantity { get { return quantity; } set { quantity = value; } }
    public string Uom { get { return uom; } set { uom = value; } }

    public override bool Equals(object obj)
    {
        if (obj == null) return false;
        RequestedItem objAsPart = obj as RequestedItem;
        if (objAsPart == null) return false;
        else return Equals(objAsPart);
    }


    public bool Equals(RequestedItem other)
    {
        if (other == null) return false;
        return (this.code.Equals(other.code));
    }
}
