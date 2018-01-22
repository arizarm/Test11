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
}