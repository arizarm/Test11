using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ItemStockCard : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string itemCode = Request.QueryString["itemCode"];
        string adjustment = "Adjustment";    //strings for stock card transaction type
        string disbursement = "Disbursement";
        string purchase = "Purchase";

        if (!ValidatorUtil.isEmpty(itemCode))
        {
            Item item = EFBroker_Item.GetItembyItemCode(itemCode);
            List<PriceList> plList = EFBroker_PriceList.GetPriceListByItemCode(itemCode);
            if (item != null && plList.Count > 0)
            {
                lblItemCode.Text = item.ItemCode;
                lblItemName.Text = item.Description;
                lblBin.Text = item.Bin;
                lblUom.Text = item.UnitOfMeasure;

                foreach(PriceList pl in plList)
                {
                    switch (pl.SupplierRank)
                    {
                        case 1:
                            lblSupp1.Text = pl.SupplierCode;
                            break;
                        case 2:
                            lblSupp2.Text = pl.SupplierCode;
                            break;
                        case 3:
                            lblSupp3.Text = pl.SupplierCode;
                            break;
                    }
                }

                if(plList.Count < 3)   // N/A on supplier labels if there are less than 3 suppliers
                {
                    lblSupp3.Text = "N/A";
                    if(plList.Count < 2)
                    {
                        lblSupp2.Text = "N/A";
                    }
                }

                List<StockCard> scList = EFBroker_StockCard.GetStockCardsByItemCode(itemCode);
                List<StockCardDisplayRow> scDisplayList = new List<StockCardDisplayRow>();

                foreach (StockCard sc in scList)
                {
                    if(sc.TransactionType == adjustment || sc.TransactionType == disbursement || sc.TransactionType == purchase)
                    {
                        //Possible to display 3 types of stock card entries (each accessing different tables)
                        StockCardDisplayRow scdr = new StockCardDisplayRow();
                        if (sc.TransactionType == adjustment)
                        {
                            Discrepency d = EFBroker_Discrepancy.GetDiscrepancyById((int)sc.TransactionDetailID);
                            scdr.TransDate = ((DateTime)d.Date).ToShortDateString();
                            scdr.TransDetails = "Adjustment ID. " + sc.TransactionDetailID;
                            scdr.Quantity = "ADJ " + GetQuantityString((int)sc.Qty);
                        }
                        else if (sc.TransactionType == purchase)
                        {
                            PurchaseOrder po = EFBroker_PurchaseOrder.GetPurchaseOrderById((int)sc.TransactionDetailID);
                            scdr.TransDate = ((DateTime)po.ExpectedDate).ToShortDateString();
                            scdr.TransDetails = "Supplier - " + po.SupplierCode;
                            Item_PurchaseOrder ipo = EFBroker_PurchaseOrder.GetPurchaseOrderItem(po.PurchaseOrderID, itemCode);
                            scdr.Quantity = GetQuantityString((int)sc.Qty);
                        }
                        else if (sc.TransactionType == disbursement)
                        {
                            Disbursement db = EFBroker_Disbursement.GetDisbursmentbyDisbID((int)sc.TransactionDetailID);
                            scdr.TransDate = ((DateTime)db.CollectionDate).ToShortDateString();
                            scdr.TransDetails = EFBroker_DeptEmployee.GetDepartByDepCode(db.DeptCode).DeptName;
                            scdr.Quantity = GetQuantityString((int)sc.Qty);
                        }
                        scdr.Balance = (int)sc.Balance;
                        scDisplayList.Add(scdr);
                    }
                }

                GridView1.DataSource = scDisplayList;
                GridView1.DataBind();
            }
            else    //if item is not found or no entries found in price list table
            {
                Response.Redirect(LoginController.StationeryCatalogueURI);
            }
        }
        else   //if there is no itemCode in querystring
        {
            Response.Redirect(LoginController.StationeryCatalogueURI);
        }
    }

    private string GetQuantityString(int qty)
    {
        string output = "";

        if(qty > 0)
        {
            output = "+" + qty.ToString();
        }
        else
        {
            output = qty.ToString();
        }
        return output;
    }
}