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
        if(!IsPostBack)
        {
            BindData();
        }
        
    }

    private void BindData()
    {

        GvPurchaseOrder.DataSource = pCtlr.GetPurchaseOrderList();
        GvPurchaseOrder.DataBind();
    }
    protected void OrderStatusDrpdwn_SelectedIndexChanged(object sender, EventArgs e)
    {
        string selectedStatus = ddlOrderStatus.SelectedItem.Text;
        if(selectedStatus =="--Select Status--")
        {
            GvPurchaseOrder.DataSource = pCtlr.GetPurchaseOrderList();
            GvPurchaseOrder.DataBind();
        }
        else
        {
            GvPurchaseOrder.DataSource = pCtlr.GetPurchaseOrderListByStatus(selectedStatus);
            GvPurchaseOrder.DataBind();
        }
        
    }

    protected void BtnDisplayAll_Click(object sender, EventArgs e)
    {
        BindData();
    }
    protected void GvPurchaseOrder_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GvPurchaseOrder.PageIndex = e.NewPageIndex;
        BindData();
    }

    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        
        if(!string.IsNullOrEmpty(txtSearch.Text))
        {
            string searchTxt = txtSearch.Text;
            List<PurchaseOrder> porderList = pCtlr.SearchPurchaseOrder(searchTxt);
            if (porderList != null)
            {
                GvPurchaseOrder.DataSource = porderList;
                GvPurchaseOrder.DataBind();
            }
            else
            {
                //gvPurchaseOrder.DataSource = pCtlr.SearchPurchaseOrderByID(searchID);
                //gvPurchaseOrder.DataBind();
                ClientScript.RegisterStartupScript(Page.GetType(), "MessageBox",
                "<script language='javascript'>alert('" + "No records found!" + "');</script>");
            }
        }
       
        
    }

    protected void LbtnPurchaseOrderID_Click(object sender, EventArgs e)
    {
        LinkButton lbtn = (LinkButton)(sender);
        string orderID = lbtn.CommandArgument;
        Response.Redirect("~/Store/PurchaseOrderDetail.aspx?OrderID=" + orderID);
    }

    protected void GvPurchaseOrder_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            PurchaseOrder pOrder = pCtlr.GetPurchaseOrderList().Where(x => x.PurchaseOrderID == (int)DataBinder.Eval(e.Row.DataItem, "PurchaseOrderID")).First();
            Label statusLbl = (Label)e.Row.FindControl("lblOrderStatus");
            Button delBtn = (Button)e.Row.FindControl("BtnDelete");
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
                statusLbl.ForeColor = System.Drawing.Color.Black;
                delBtn.Visible = true;
                delBtn.Enabled = false;
            }
         
            if (Session["emp"] != null)
            {
                if (Session["empRole"].ToString() == "Store Clerk")
                {
                    Employee emp = (Employee)Session["emp"];                    
                    Label reqby = (Label)e.Row.FindControl("lblReqstdBy");
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
                    GvPurchaseOrder.Columns[5].Visible = false;
                }
              
            }           
        }
    }

    protected void BtnDelete_Click(object sender, EventArgs e)
    {
        Button delteBtn = (Button)sender;
        int pID = Int32.Parse(delteBtn.CommandArgument.ToString());
        pCtlr.DeletePurchaseOrder(pID);
        ClientScript.RegisterStartupScript(Page.GetType(), "MessageBox",
            "<script language='javascript'>alert('" + "Purchase Order Deleted!" + "');</script>");
        BindData();
    }
}