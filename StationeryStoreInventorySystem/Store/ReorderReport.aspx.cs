using System;
using System.Collections.Generic;
using System.Globalization;
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
            DateTime sDate = Convert.ToDateTime(startDate.Text);
            DateTime eDate = Convert.ToDateTime(endDate.Text);
            
            sdate.Text = sDate.ToShortDateString();
            edate.Text = eDate.ToShortDateString();
            txtLbl.Text = "List of items running low on stock which are yet to be delivered from supplier";
            gvPurchasedreoderItem.DataSource = pCtrlr.GenerateReorderReportForPurchasedItems(sDate, eDate);
            gvPurchasedreoderItem.DataBind();            
            txtLbl2.Text = "List of items running low on stock with no purchases done yet";
            gvShortfallItems.DataSource = pCtrlr.GenerateShortfallItemsReport(sDate, eDate);
            gvShortfallItems.DataBind();
        }
        else
        {
            gvPurchasedreoderItem.DataSource = null;
            gvPurchasedreoderItem.DataBind();
            gvShortfallItems.DataSource = null;
            gvShortfallItems.DataBind();
        }
    }
    protected void CompareDateValidator(object sender, ServerValidateEventArgs e)
    {

   
        DateTime sDate = DateTime.ParseExact(startDate.Text, "M/d/yyyy", CultureInfo.InvariantCulture);
        DateTime eDate = DateTime.ParseExact(endDate.Text, "M/d/yyyy", CultureInfo.InvariantCulture);
      
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