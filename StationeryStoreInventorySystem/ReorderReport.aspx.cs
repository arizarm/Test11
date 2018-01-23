using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ReorderReport : System.Web.UI.Page
{
    PurchaseController pCtrlr = new PurchaseController();
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void GenerateBtn_Click(object sender, EventArgs e)
    {
        //DateTime startDate = Convert.ToDateTime(txtDate.Text);
        //DateTime endDate = Convert.ToDateTime(txtEDate.Text);
        //sDate.Text = startDate.ToShortDateString();
        //eDate.Text = endDate.ToShortDateString();              
        //txtLbl.Text= "The following items have fallen below re-order level";
        //gvPurchasedreoderItem.DataSource=pCtrlr.GenerateReorderReportForPurchasedItems(startDate, endDate);
        //gvPurchasedreoderItem.DataBind();

        //gvShortfallItems.DataSource = pCtrlr.GenerateShortfallItemsReport();
        //gvShortfallItems.DataBind();

    }
}