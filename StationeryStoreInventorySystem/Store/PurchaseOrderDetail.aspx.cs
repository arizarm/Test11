using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


//AUTHOR : KIRUTHIKA VENKATESH
public partial class PurchaseOrderDetail: System.Web.UI.Page
{
    PurchaseController pCtrlr = new PurchaseController();
    PurchaseOrder pOrder = null;
    static int orderid = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
       if(!IsPostBack)
        {
            BindGrid();
        }            

    }
    private void BindGrid()
    {
        int orderID;
        if (Request.QueryString["OrderID"] == null)
        {
            Response.Redirect(LoginController.PurchaseOrderListURI);
        }
        else
        {
            orderID = Convert.ToInt32(Request.QueryString["OrderID"]);
            pOrder = pCtrlr.GetPurchaseOrderByID(orderID);
            orderid = pOrder.PurchaseOrderID;
            lblsupervisorName.Text = pOrder.Employee.EmpName;
            lblSupplierName.Text = pOrder.Supplier.SupplierName;
            lblOrderID.Text = Convert.ToString(pOrder.PurchaseOrderID);
            lblorderStatus.Text = pOrder.Status;
            if (pOrder.Status == "Pending")
            {
                lblorderStatus.ForeColor = System.Drawing.Color.Blue;
            }
            else if(pOrder.Status =="Approved")
            {
                lblorderStatus.ForeColor = System.Drawing.Color.Green;
            }
            else if(pOrder.Status == "Rejected")
            {
                lblorderStatus.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                lblorderStatus.ForeColor = System.Drawing.Color.Black;
            }
           List<PurchaseOrderItemDetails>itemList = pCtrlr.GetPurchaseOrderItemsDetails(orderID);
            GvPurchaseDetail.DataSource = itemList;
            GvPurchaseDetail.DataBind();

            decimal? totAmnt = 0;
            foreach(PurchaseOrderItemDetails item in itemList)
            {
                totAmnt += item.Price * item.OrderQty;

            }
            lblTotalAmount.Text = String.Format("{0:C}", totAmnt);
           foreach (GridViewRow row in GvPurchaseDetail.Rows)
            {
                if (Session["emp"] != null)
                {
                    if (Session["empRole"].ToString() == "Store Clerk")
                    {

                        Employee emp = (Employee)Session["emp"];                    
                        if (emp.EmpID == pOrder.RequestedBy)
                        {
                        
                            if (pOrder.Status == "Closed" || pOrder.Status == "Rejected")
                            {
                                lbldelivery.Visible = false;
                                txtDeliveryOrderID.Visible = false;
                                BtnCloseOrder.Visible = false;
                                BtnReject.Visible = false;
                                GvPurchaseDetail.Columns[5].Visible = false;

                            }
                            else if (pOrder.Status == "Approved")
                            {
                                lbldelivery.Visible = true;
                                txtDeliveryOrderID.Visible = true;
                                BtnCloseOrder.Visible = true;
                                GvPurchaseDetail.Columns[5].Visible = true;
                            }
                            else if (pOrder.Status == "Pending")
                            {
                                lbldelivery.Visible = false;
                                txtDeliveryOrderID.Visible = false;
                                BtnCloseOrder.Visible = false;
                                GvPurchaseDetail.Columns[5].Visible = true;
                            }

                        }
                        else
                        {

                            if (pOrder.Status == "Closed" || pOrder.Status == "Rejected" || pOrder.Status == "Approved" || pOrder.Status == "Pending")
                            {
                                lbldelivery.Visible = false;
                                txtDeliveryOrderID.Visible = false;
                                BtnCloseOrder.Visible = false;
                                BtnReject.Visible = false;
                                GvPurchaseDetail.Columns[5].Visible = false;

                            }                       

                        }
                        lblRemark.Visible = false;
                        txtRemark.Visible = false;
                        BtnApprove.Visible = false;
                        BtnReject.Visible = false;

                    }
                    else if (Session["empRole"].ToString() == "Store Supervisor" || Session["empRole"].ToString() == "Store Manager")
                    {
                        if (pOrder.Status == "Closed" || pOrder.Status == "Rejected"|| pOrder.Status == "Approved")
                        {

                            lblRemark.Visible = false;
                            txtRemark.Visible = false;
                            BtnApprove.Visible = false;
                            BtnReject.Visible = false;                        
                        }                    
                        else
                        {
                            lblRemark.Visible = true;
                            txtRemark.Visible = true;
                            BtnApprove.Visible = true;
                            BtnReject.Visible = true;                       
                        }
                        GvPurchaseDetail.Columns[5].Visible = false;
                        lbldelivery.Visible = false;
                        txtDeliveryOrderID.Visible = false;
                        BtnCloseOrder.Visible = false;
                    }

                }
            

            }

          }               
       
    }

   
    protected void BtnApprove_Click(object sender, EventArgs e)
    {

        PurchaseOrder purchaseOrder = new PurchaseOrder();
        purchaseOrder.Remarks = txtRemark.Text;
        purchaseOrder.PurchaseOrderID = orderid;
        purchaseOrder.ApprovedBy = (int)Session["empID"];
        purchaseOrder.ApprovedByDate = DateTime.Now.Date;
        purchaseOrder.Status = "Approved";
        pCtrlr.UpdatePurchaseOrder(purchaseOrder);
        ClientScript.RegisterStartupScript(Page.GetType(), "MessageBox",
    "<script language='javascript'>alert('" + "Order Approved!" + "');</script>");
        Response.Redirect(LoginController.PurchaseOrderListURI);
    }

    protected void BtnReject_Click(object sender, EventArgs e)
    {

        PurchaseOrder purchaseOrder = new PurchaseOrder();
        purchaseOrder.Remarks = txtRemark.Text;
        purchaseOrder.PurchaseOrderID = orderid;
        purchaseOrder.Remarks = txtRemark.Text;
        purchaseOrder.ApprovedBy = (int)Session["empID"];
        purchaseOrder.ApprovedByDate = DateTime.Now.Date;
        purchaseOrder.Status = "Rejected";
        pCtrlr.UpdatePurchaseOrder(purchaseOrder);
        ClientScript.RegisterStartupScript(Page.GetType(), "MessageBox",
        "<script language='javascript'>alert('" + "Order Rejected!" + "');</script>");
        Response.Redirect(LoginController.PurchaseOrderListURI);
    }

    protected void BtnCloseOrder_Click(object sender, EventArgs e)
    {
        PurchaseOrder purchaseOrder = new PurchaseOrder();
        purchaseOrder.DONumber = txtDeliveryOrderID.Text;
        purchaseOrder.Status = "Closed";
        purchaseOrder.PurchaseOrderID = orderid;
        pCtrlr.ClosePurchaseOrder(purchaseOrder);
        Session["ReorderItem"] = null;
     //   ClientScript.RegisterStartupScript(Page.GetType(), "MessageBox",
     //"<script language='javascript'>alert('" + "Order Closed!" + "');</script>");
        Response.Redirect(LoginController.PurchaseOrderListURI);
    }




    protected void OrderQtyTxtBx_TextChanged(object sender, EventArgs e)
    {
        TextBox txt = sender as TextBox;
        GridViewRow row = txt.NamingContainer as GridViewRow;
        int rowIndex = row.RowIndex;
    }
    protected void GvPurchaseDetail_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GvPurchaseDetail.EditIndex = e.NewEditIndex;
        GvPurchaseDetail.EditRowStyle.BackColor = System.Drawing.Color.Yellow;
        BindGrid();
    }
    protected void GvPurchaseDetail_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

        // Retrieve the row being edited.
        decimal price, amount;
        int qty;
        int index = GvPurchaseDetail.EditIndex;
        GridViewRow row = GvPurchaseDetail.Rows[index];
        TextBox qtyTxt = row.FindControl("txtorderQty") as TextBox;
        
        bool ok = int.TryParse(qtyTxt.Text, NumberStyles.Currency,
        CultureInfo.CurrentCulture.NumberFormat, out qty);

        Label priceLbl = (Label)GvPurchaseDetail.Rows[e.RowIndex].FindControl("lblPrice");
        ok = decimal.TryParse(priceLbl.Text, NumberStyles.Currency,
        CultureInfo.CurrentCulture.NumberFormat, out price);

        Label amountLbl = (Label)GvPurchaseDetail.Rows[e.RowIndex].FindControl("lblAmount");
        ok = decimal.TryParse(amountLbl.Text, NumberStyles.Currency,
        CultureInfo.CurrentCulture.NumberFormat, out amount);

        Label itemLbl =(Label)GvPurchaseDetail.Rows[e.RowIndex].FindControl("lblItemCode");
        amount = qty * price;
        amountLbl.Text = String.Format("{0:C}", amount);

        Item_PurchaseOrder item = new Item_PurchaseOrder();
        item.Amount = amount;
        item.OrderQty = qty;
        item.PurchaseOrderID = orderid;
        item.ItemCode = itemLbl.Text;
        pCtrlr.UpdatePurchaseItem(item);
        GvPurchaseDetail.EditIndex = -1;
        BindGrid();
        ClientScript.RegisterStartupScript(Page.GetType(), "MessageBox",
   "<script language='javascript'>alert('" + "Item Updated!" + "');</script>");
    }

    protected void GvPurchaseDetail_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GvPurchaseDetail.EditIndex = -1;
        //Bind data to the GridView control.
        BindGrid();
    }

    protected void BtnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect(LoginController.PurchaseOrderListURI);
    }
}

