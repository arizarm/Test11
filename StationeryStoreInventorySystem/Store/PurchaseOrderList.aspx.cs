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
        if(porderList != null)
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
        Response.Redirect(LoginController.PurchaseOrderDetailURI +"? OrderID=" + orderID);
    }

    protected void gvPurchaseOrder_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            PurchaseOrder pOrder = pCtlr.GetPurchaseOrderList().Where(x => x.PurchaseOrderID == (int)DataBinder.Eval(e.Row.DataItem, "PurchaseOrderID")).First();
            Label statusLbl = (Label)e.Row.FindControl("OrderStatus");
            Button delBtn = (Button)e.Row.FindControl("btn_Delete");
            string status = statusLbl.Text;
            if (status == "Pending")
            {
                statusLbl.ForeColor = System.Drawing.Color.Blue;
                delBtn.Visible = true;
                delBtn.Enabled = true;
            }
            else if (status == "Approved")
            {
                statusLbl.ForeColor = System.Drawing.Color.Green;
                delBtn.Visible = true;
                delBtn.Enabled = false;
            }
            else if (status == "Rejected")
            {
                statusLbl.ForeColor = System.Drawing.Color.Red;
                delBtn.Visible = true;
                delBtn.Enabled = false;
            }
            else if (status == "Closed")
            {
                statusLbl.ForeColor = System.Drawing.Color.Orange;
                delBtn.Visible = true;
                delBtn.Enabled = false;
            }
         
            if (Session["emp"] != null)
            {
                if (Session["empRole"].ToString() == "Store Clerk")
                {
                    Employee emp = (Employee)Session["emp"];                    
                    Label reqby = (Label)e.Row.FindControl("ReqstdBy");
                    if(emp.EmpID == pOrder.RequestedBy)
                    {
                        delBtn.Visible = true;
                    }
                    else
                    {
                        delBtn.Enabled = false;
                    }
                   

                }
                else if (Session["empRole"].ToString() == "Store Supervisor"|| Session["empRole"].ToString() == "Store Manager")
                {
                    gvPurchaseOrder.Columns[5].Visible = false;
                }
              
            }           
        }
    }


    protected void btn_Delete_Click(object sender, EventArgs e)
    {

        Button delteBtn = (Button)sender;
        int pID = Int32.Parse(delteBtn.CommandArgument.ToString());
        pCtlr.DeletePurchaseOrder(pID);
        ClientScript.RegisterStartupScript(Page.GetType(), "MessageBox",
            "<script language='javascript'>alert('" + "Purchase Order Deleted!" + "');</script>");
        BindData();
    }
}