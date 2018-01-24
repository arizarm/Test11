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
        if (Page.IsValid)
        {
            DateTime startDate = Convert.ToDateTime(txtSDate.Text);
            DateTime endDate = Convert.ToDateTime(txtEDate.Text);
            sDate.Text = startDate.ToShortDateString();
            eDate.Text = endDate.ToShortDateString();
            txtLbl.Text = "List of items running low on stock which are yet to be delivered from supplier";
            gvPurchasedreoderItem.DataSource = pCtrlr.GenerateReorderReportForPurchasedItems(startDate, endDate);
            gvPurchasedreoderItem.DataBind();

            txtLbl2.Text = "List of items running low on stock with no purchases done yet";
            gvShortfallItems.DataSource = pCtrlr.GenerateShortfallItemsReport(startDate, endDate);
            gvShortfallItems.DataBind();
        }


    }
    protected void CompareDateValidator(object sender, ServerValidateEventArgs e)
    {
        DateTime sDate = Convert.ToDateTime(txtSDate.Text);
        DateTime eDate = Convert.ToDateTime(txtEDate.Text);
        if (sDate > eDate)
        {
            e.IsValid = false;
        }
        else
        {
            e.IsValid = true;
        }
    }
}