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

        }
    }
}