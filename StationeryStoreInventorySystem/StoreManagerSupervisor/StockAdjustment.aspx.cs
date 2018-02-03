using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

public partial class StockAdjustment : System.Web.UI.Page
{
    List<Discrepency> monthly;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            monthly = EFBroker_Discrepancy.GetMonthlyDiscrepancyList();
            List<Discrepency> pending = EFBroker_Discrepancy.GetPendingDiscrepancyList();

            Dictionary<Discrepency, Item> monthlySource = new Dictionary<Discrepency, Item>();
            Dictionary<Discrepency, Item> pendingSource = new Dictionary<Discrepency, Item>();

            foreach (Discrepency d in monthly)
            {
                Item i = EFBroker_Item.GetItembyItemCode(d.ItemCode);
                decimal discrepancyAmount = Math.Abs((decimal)d.TotalDiscrepencyAmount);
                if (Session["empRole"] != null)
                {
                    string role = (string)Session["empRole"];
                    if (Session["empRole"].ToString() == "Store Manager" && discrepancyAmount >= 250)
                    {
                        monthlySource.Add(d, i);
                    }
                    else if (Session["empRole"].ToString() == "Store Supervisor" && discrepancyAmount < 250)
                    {
                        monthlySource.Add(d, i);
                    }
                }
            }
            gvMonthly.DataSource = monthlySource;
            gvMonthly.DataBind();

            foreach (Discrepency d in pending)
            {
                Item i = EFBroker_Item.GetItembyItemCode(d.ItemCode);
                decimal discrepancyAmount = Math.Abs((decimal)d.TotalDiscrepencyAmount);
                if (Session["empRole"] != null)
                {
                    if (Session["empRole"].ToString() == "Store Manager" && discrepancyAmount >= 250)
                    {
                        pendingSource.Add(d, i);
                    }
                    else if (Session["empRole"].ToString() == "Store Supervisor" && discrepancyAmount < 250)
                    {
                        pendingSource.Add(d, i);
                    }
                }
                else
                {
                    Utility.logout();
                }
            }
            gvPending.DataSource = pendingSource;
            gvPending.DataBind();

        }
    }

    protected void GvPending_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //Disallow processing of regular pending discrepancies that came before
        //a monthly inventory check discrepancy by hiding them
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            KeyValuePair<Discrepency, Item> kvp = (KeyValuePair<Discrepency, Item>)e.Row.DataItem;

            foreach (Discrepency d in monthly)
            {
                if (kvp.Key.ItemCode == d.ItemCode && kvp.Key.DiscrepencyID < d.DiscrepencyID)
                {
                    e.Row.Visible = false;
                }
            }
        }
    }

    protected void BtnProcess_Click(object sender, EventArgs e)
    {
        Dictionary<KeyValuePair<Discrepency, Item>, String> summary = new Dictionary<KeyValuePair<Discrepency, Item>, String>();
        ProcessApprovalAndRejections(gvMonthly, summary);
        ProcessApprovalAndRejections(gvPending, summary);
        Session["discrepancySummary"] = summary;
        Response.Redirect(LoginController.StockAdjustmentSummaryURI);
    }

    private void ProcessApprovalAndRejections(GridView gdv, Dictionary<KeyValuePair<Discrepency, Item>, String> summary)
    {
        foreach (GridViewRow row in gdv.Rows)
        {
            RadioButtonList rbl = row.FindControl("rblAction") as RadioButtonList;

            if (rbl.SelectedIndex == 0 || rbl.SelectedIndex == 1)
            {
                string itemCode = (row.FindControl("lblItemCode") as Label).Text;
                int discID = Int32.Parse((row.FindControl("lblDiscID") as Label).Text);
                Item i = EFBroker_Item.GetItembyItemCode(itemCode);
                Discrepency d = EFBroker_Discrepancy.GetDiscrepancyById(discID);
                KeyValuePair<Discrepency, Item> kvp = new KeyValuePair<Discrepency, Item>(d, i);
                if (rbl.SelectedIndex == 0)
                {
                    summary.Add(kvp, "Approved");
                }
                else if (rbl.SelectedIndex == 1)
                {
                    summary.Add(kvp, "Rejected");
                }
            }
        }
    }

    protected void BtnClear_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gvMonthly.Rows)
        {
            RadioButtonList rbl = row.FindControl("rblAction") as RadioButtonList;
            rbl.ClearSelection();
        }
        foreach (GridViewRow row in gvPending.Rows)
        {
            RadioButtonList rbl = row.FindControl("rblAction") as RadioButtonList;
            rbl.ClearSelection();
        }
    }
}

