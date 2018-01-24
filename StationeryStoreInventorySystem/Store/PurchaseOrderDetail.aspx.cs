﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
        int orderID = Convert.ToInt32(Request.QueryString["OrderID"]);
        pOrder = pCtrlr.GetPurchaseOrderByID(orderID);
        orderid = pOrder.PurchaseOrderID;
        SupervisorName.Text = pOrder.Employee1.EmpName;
        SuplierName.Text = pOrder.Employee.EmpName;
        OrderID.Text = Convert.ToString(pOrder.PurchaseOrderID);

       List<PurchaseOrderItemDetails>itemList = pCtrlr.GetPurchaseOrderItemsDetails(orderID);
        gvPurchaseDetail.DataSource = itemList;
        gvPurchaseDetail.DataBind();

        decimal? totAmnt = 0;
        foreach(PurchaseOrderItemDetails item in itemList)
        {
            totAmnt += item.Price * item.OrderQty;

        }
        TotalAmount.Text = String.Format("{0:C}", totAmnt);
        if (Session["empRole"] != null)
        {
            if (Session["empRole"].ToString() == "Store Clerk")
            {
                DeliveryOrderIDTxtBx.Visible = true;
                CloseOrderBtn.Visible = true;
               
            }
            else if (Session["empRole"].ToString() == "Store Supervisor" || Session["empRole"].ToString() == "Store Manager")
            {
                RemarkLbl.Visible = true;
                RemarkTxtBx.Visible = true;
                ApproveBtn.Visible = true;
                RejectBtn.Visible = true;
                
            }
            else
            {
                DeliveryOrderIDTxtBx.Visible = false;
                CloseOrderBtn.Visible = false;
                RemarkLbl.Visible = false;
                RemarkTxtBx.Visible = false;
                ApproveBtn.Visible = false;
                RejectBtn.Visible = false;
               
            }
        }
    }

   
    protected void ApproveBtn_Click(object sender, EventArgs e)
    {

        PurchaseOrder purchaseOrder = new PurchaseOrder();
        purchaseOrder.Remarks = RemarkTxtBx.Text;
        purchaseOrder.PurchaseOrderID = orderid;
        purchaseOrder.ApprovedBy = (int)Session["empID"];
        purchaseOrder.ApprovedByDate = DateTime.Now.Date;
        purchaseOrder.Status = "Approved";
        pCtrlr.UpdatePurchaseOrder(purchaseOrder);
        ClientScript.RegisterStartupScript(Page.GetType(), "MessageBox",
    "<script language='javascript'>alert('" + "Order Approved!" + "');</script>");
    }

    protected void RejectBtn_Click(object sender, EventArgs e)
    {

        PurchaseOrder purchaseOrder = new PurchaseOrder();
        purchaseOrder.Remarks = RemarkTxtBx.Text;
        purchaseOrder.PurchaseOrderID = orderid;
        purchaseOrder.Remarks = RemarkTxtBx.Text;
        purchaseOrder.ApprovedBy = (int)Session["empID"];
        purchaseOrder.ApprovedByDate = DateTime.Now.Date;
        purchaseOrder.Status = "Rejected";
        pCtrlr.UpdatePurchaseOrder(purchaseOrder);
        ClientScript.RegisterStartupScript(Page.GetType(), "MessageBox",
        "<script language='javascript'>alert('" + "Order Rejected!" + "');</script>");
    }

    protected void CloseOrderBtn_Click(object sender, EventArgs e)
    {
        PurchaseOrder purchaseOrder = new PurchaseOrder();
        purchaseOrder.DONumber = DeliveryOrderIDTxtBx.Text;
        purchaseOrder.Status = "Closed";
        purchaseOrder.PurchaseOrderID = orderid;
        pCtrlr.ClosePurchaseOrder(purchaseOrder);
        ClientScript.RegisterStartupScript(Page.GetType(), "MessageBox",
    "<script language='javascript'>alert('" + "Order Closed!" + "');</script>");
    }




    protected void orderQtyTxtBx_TextChanged(object sender, EventArgs e)
    {
        TextBox txt = sender as TextBox;
        GridViewRow row = txt.NamingContainer as GridViewRow;
        int rowIndex = row.RowIndex;
    }
    protected void gvPurchaseDetail_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvPurchaseDetail.EditIndex = e.NewEditIndex;
        BindGrid();
    }
    protected void gvPurchaseDetail_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

        // Retrieve the row being edited.
        decimal price, amount;
        int qty;
        int index = gvPurchaseDetail.EditIndex;
        GridViewRow row = gvPurchaseDetail.Rows[index];
        TextBox qtyTxt = row.FindControl("orderQtyTxtBx") as TextBox;
        
        bool ok = int.TryParse(qtyTxt.Text, NumberStyles.Currency,
        CultureInfo.CurrentCulture.NumberFormat, out qty);

        Label priceLbl = (Label)gvPurchaseDetail.Rows[e.RowIndex].FindControl("Price");
        ok = decimal.TryParse(priceLbl.Text, NumberStyles.Currency,
        CultureInfo.CurrentCulture.NumberFormat, out price);

        Label amountLbl = (Label)gvPurchaseDetail.Rows[e.RowIndex].FindControl("Amount");
        ok = decimal.TryParse(amountLbl.Text, NumberStyles.Currency,
        CultureInfo.CurrentCulture.NumberFormat, out amount);

        Label itemLbl =(Label)gvPurchaseDetail.Rows[e.RowIndex].FindControl("ItemCode");
        amount = qty * price;
        amountLbl.Text = String.Format("{0:C}", amount);

        Item_PurchaseOrder item = new Item_PurchaseOrder();
        item.Amount = amount;
        item.OrderQty = qty;
        item.PurchaseOrderID = orderid;
        item.ItemCode = itemLbl.Text;
        pCtrlr.UpdatePurchaseItem(item);
        gvPurchaseDetail.EditIndex = -1;
        BindGrid();

    }

    protected void gvPurchaseDetail_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

    }
}
