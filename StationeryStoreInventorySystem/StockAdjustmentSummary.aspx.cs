using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StockAdjustmentSummary : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(Session["discrepancySummary"] != null)
        {
            Dictionary<KeyValuePair<Discrepency, Item>, String> summary = null;
            try
            {
                summary = (Dictionary<KeyValuePair<Discrepency, Item>, String>)Session["discrepancySummary"];
            }
            catch
            {
                Response.Redirect("~/StockAdjustment.aspx");
            }
            GridView1.DataSource = summary;
            GridView1.DataBind();
        }
        else
        {
            Response.Redirect("~/StockAdjustment.aspx");
        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        foreach(GridViewRow row in GridView1.Rows)
        {
            int discID = Int32.Parse((row.FindControl("lblDiscID") as Label).Text);
            string action = (row.FindControl("lblAction") as Label).Text;

            Discrepency d = EFBroker_Discrepancy.GetDiscrepancyById(discID);
            if (d.Status == "Monthly")
            {
                List<Discrepency> dList = EFBroker_Discrepancy.GetPendingDiscrepanciesByItemCode(d.ItemCode);

                foreach (Discrepency d2 in dList)
                {
                    if (d2.DiscrepencyID < d.DiscrepencyID)
                    {
                        EFBroker_Discrepancy.ProcessDiscrepancy(d2.DiscrepencyID, "Resolved");
                    }
                }
            }

            EFBroker_Discrepancy.ProcessDiscrepancy(discID, action);

            StockCard sc = new StockCard();
            

            StockCard lastEntry = EFBroker_StockCard.GetStockCardsByItemCode(d.ItemCode).Last();

            sc.ItemCode = d.ItemCode;
            sc.TransactionType = "Adjustment";
            sc.Qty = d.AdjustmentQty;
            sc.Balance = lastEntry.Balance + d.AdjustmentQty;
            sc.TransactionDetailID = d.DiscrepencyID;

            //EFBroker_StockCard.AddStockTransaction(sc);
        }

        Response.Redirect("~/StockAdjustment.aspx");
    }
}