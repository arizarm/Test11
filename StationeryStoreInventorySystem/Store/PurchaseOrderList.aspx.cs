using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PurchaseOrderList : System.Web.UI.Page
{
    PurchaseController pCtlr = new PurchaseController();

    protected void Page_Load(object sender, EventArgs e)
    {
        BindData();
    }

    private void BindData()
    {
        gvPurchaseOrder.DataSource = pCtlr.GetPurchaseOrderList();
        gvPurchaseOrder.DataBind();
    }
    protected void OrderStatusDrpdwn_SelectedIndexChanged(object sender, EventArgs e)
    {
        string selectedStatus = OrderStatusDrpdwn.SelectedItem.Text;
        gvPurchaseOrder.DataSource = pCtlr.GetPurchaseOrderListByStatus(selectedStatus);
        gvPurchaseOrder.DataBind();
    }

    protected void DisplayAllBtn_Click(object sender, EventArgs e)
    {
        BindData();
    }
    protected void gvPurchaseOrder_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvPurchaseOrder.PageIndex = e.NewPageIndex;
        BindData();
    }

    protected void SearchBtn_Click(object sender, EventArgs e)
    {
        int searchID = Convert.ToInt32(SearchTxtBx.Text);
        List<PurchaseOrder> porderList = pCtlr.SearchPurchaseOrderByID(searchID);
        if(porderList !=null)
        {
            gvPurchaseOrder.DataSource = pCtlr.SearchPurchaseOrderByID(searchID);
            gvPurchaseOrder.DataBind();
        }
        else
        {
            //gvPurchaseOrder.DataSource = pCtlr.SearchPurchaseOrderByID(searchID);
            //gvPurchaseOrder.DataBind();
            ClientScript.RegisterStartupScript(Page.GetType(), "MessageBox",
            "<script language='javascript'>alert('" + "No records found!" + "');</script>");
        }
        
    }

    protected void purchaseDetailLinkBtn_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)(sender);
        string orderID = btn.CommandArgument;
        Response.Redirect("~/Store/PurchaseOrderDetail.aspx?OrderID=" + orderID);
    }
}