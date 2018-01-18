using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class GenerateDiscrepancyAdhocV2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Dictionary<InventoryItem, String> discrepancies = new Dictionary<InventoryItem, String>();
        if (!IsPostBack)
        {
            if (Session["discrepancyList"] != null)
            {
                discrepancies = (Dictionary<InventoryItem, String>)Session["discrepancyList"];
            }
            GridView1.DataSource = discrepancies;
            GridView1.DataBind();
        }
    }
}