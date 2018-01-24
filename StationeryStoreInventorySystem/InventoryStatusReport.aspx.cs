using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class InventoryStatusReport : System.Web.UI.Page
{
    ItemBusinessLogic ilogic;

    protected void Page_Load(object sender, EventArgs e)
    {
        //InventoryCrystalReport cr = new InventoryCrystalReport();
        //cr.SetDataSource(InventoryReportItem.getInventoryReportItems());
        //CrystalReportViewer1.ReportSource = cr;
        GridView1.DataSource = ItemBusinessLogic.GetInventoryReportItemList();
        GridView1.DataBind();
    }
}