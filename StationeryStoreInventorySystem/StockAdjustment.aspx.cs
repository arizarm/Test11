using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StockAdjustment : System.Web.UI.Page
{
    List<Discrepency> monthly;
    protected void Page_Load(object sender, EventArgs e)
    {
        monthly = GenerateDiscrepancyController.GetAllPendingMonthlyDiscrepancies();
        List<Discrepency> pending = GenerateDiscrepancyController.GetAllPendingDiscrepancies();

        Dictionary<Discrepency, Item> monthlySource = new Dictionary<Discrepency, Item>();
        Dictionary<Discrepency, Item> pendingSource = new Dictionary<Discrepency, Item>();

        foreach(Discrepency d in monthly)
        {
            Item i = GenerateDiscrepancyController.GetItemByItemCode(d.ItemCode);
            monthlySource.Add(d, i);
        }
        GridView1.DataSource = monthlySource;
        GridView1.DataBind();

        foreach(Discrepency d in pending)
        {
            Item i = GenerateDiscrepancyController.GetItemByItemCode(d.ItemCode);
            pendingSource.Add(d, i);
        }
        GridView2.DataSource = pendingSource;
        GridView2.DataBind();
    }

    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            KeyValuePair<Discrepency, Item> kvp = (KeyValuePair<Discrepency, Item>)e.Row.DataItem;

            foreach (Discrepency d in monthly)
            {
                if (kvp.Key.ItemCode == d.ItemCode && kvp.Key.DiscrepencyID < d.DiscrepencyID)
                {
                    CheckBox chk = e.Row.FindControl("CheckBox1") as CheckBox;
                    chk.Enabled = false;
                    chk.Visible = false;
                }
            }
        }
    }
}

