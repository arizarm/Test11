﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class InventoryStatusReport : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        //InventoryCrystalReport cr = new InventoryCrystalReport();
        //cr.SetDataSource(InventoryReportItem.getInventoryReportItems());
        //CrystalReportViewer1.ReportSource = cr;
        GridView1.DataSource = InventoryReportItem.GetInventoryReportItems();
        GridView1.DataBind();
    }
}