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
            Item item = GenerateDiscrepancyController.GetItemByItemCode(itemCode);
            List<PriceList> plList = GenerateDiscrepancyController.GetPriceListsByItemCode(itemCode);
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

                List<StockCard> scList = GenerateDiscrepancyController.GetStockCardsByItemCode(itemCode);
                List<StockCardDisplayRow> scDisplayList = new List<StockCardDisplayRow>();

                foreach (StockCard sc in scList)
                {
                    if(sc.TransactionType == adjustment || sc.TransactionType == disbursement || sc.TransactionType == purchase)
                    {
                        StockCardDisplayRow scdr = new StockCardDisplayRow();
                        if (sc.TransactionType == adjustment)
                        {
                            Discrepency d = GenerateDiscrepancyController.GetDiscrepancyById((int)sc.TransactionDetailID);
                            scdr.TransDate = ((DateTime)d.Date).ToShortDateString();
                            scdr.TransDetails = "Adjustment Id. " + sc.TransactionDetailID;
                            scdr.Quantity = "ADJ " + GetQuantityString((int)sc.Qty);
                        }
                        else if (sc.TransactionType == purchase)
                        {
                            PurchaseOrder po = GenerateDiscrepancyController.GetPurchaseOrderById((int)sc.TransactionDetailID);
                            scdr.TransDate = ((DateTime)po.ExpectedDate).ToShortDateString();
                            scdr.TransDetails = "Supplier - " + po.SupplierCode;
                            Item_PurchaseOrder ipo = GenerateDiscrepancyController.GetPurchaseOrderItem(po.PurchaseOrderID, itemCode);
                            scdr.Quantity = GetQuantityString((int)sc.Qty);
                        }
                        else if (sc.TransactionType == disbursement)
                        {
                            Disbursement db = GenerateDiscrepancyController.GetDisbursementById((int)sc.TransactionDetailID);
                            scdr.TransDate = ((DateTime)db.CollectionDate).ToShortDateString();
                            scdr.TransDetails = GenerateDiscrepancyController.GetDepartmentByDeptCode(db.DeptCode).DeptName;
                            scdr.Quantity = GetQuantityString((int)sc.Qty);
                        }
                        scdr.Balance = (int)sc.Balance;
                        scDisplayList.Add(scdr);
                    }
                }

                GridView1.DataSource = scDisplayList;
                GridView1.DataBind();
            }
            else
            {
                Response.Redirect("~/ItemStockCardList.aspx");
            }
        }
        else
        {
            Response.Redirect("~/ItemStockCardList.aspx");
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