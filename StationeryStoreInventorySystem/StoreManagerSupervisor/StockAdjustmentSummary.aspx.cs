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
        if (Session["discrepancySummary"] != null)
        {
            Dictionary<KeyValuePair<Discrepency, Item>, String> summary = null;
            try
            {
                summary = (Dictionary<KeyValuePair<Discrepency, Item>, String>)Session["discrepancySummary"];
            }
            catch
            {
                Response.Redirect(LoginController.StockAdjustmentURI);
            }
            GridView1.DataSource = summary;
            GridView1.DataBind();
        }
        else
        {
            Response.Redirect(LoginController.StockAdjustmentURI);
        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in GridView1.Rows)
        {
            int discID = Int32.Parse((row.FindControl("lblDiscID") as Label).Text);
            string action = (row.FindControl("lblAction") as Label).Text;

            Discrepency d = EFBroker_Discrepancy.GetDiscrepancyById(discID);
            if (d.Status == "Monthly")
            {
                if (action == "Approved")
                {
                    List<Discrepency> dList = EFBroker_Discrepancy.GetPendingDiscrepanciesByItemCode(d.ItemCode);

                    foreach (Discrepency d2 in dList)
                    {
                        if (d2.DiscrepencyID < d.DiscrepencyID)
                        {   //Negating discrepancies reported before the monthly discrepancy after it is approved
                            EFBroker_Discrepancy.ProcessDiscrepancy(d2.DiscrepencyID, "Resolved");
                        }
                    }
                }

            }

            EFBroker_Discrepancy.ProcessDiscrepancy(discID, action);

            if(action == "Approved")
            {    //only update stock card and item tables if discrepancy is approved
                StockCard sc = new StockCard();

                StockCard lastEntry = EFBroker_StockCard.GetStockCardsByItemCode(d.ItemCode).Last();

                sc.ItemCode = d.ItemCode;
                sc.TransactionType = "Adjustment";
                sc.Qty = d.AdjustmentQty;
                sc.Balance = lastEntry.Balance + d.AdjustmentQty;
                sc.TransactionDetailID = d.DiscrepencyID;

                EFBroker_StockCard.ResolveDiscrepancy(sc, sc.ItemCode, (int)sc.Balance);
            }
        }
        Session["discrepancySummary"] = null;
        Utility.AlertMessageThenRedirect("Adjustments processed", "StockAdjustment.aspx");
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblAction = e.Row.FindControl("lblAction") as Label;
            string action = lblAction.Text;

            if (action == "Approved")
            {
                lblAction.ForeColor = System.Drawing.Color.Green;
            }
            else if (action == "Rejected")
            {
                lblAction.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}