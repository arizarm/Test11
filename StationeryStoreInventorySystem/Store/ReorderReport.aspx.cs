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
            txtLbl.Text = "";
            txtLbl2.Text = "";
            gvPurchasedreoderItem.DataSource = null;
            gvPurchasedreoderItem.DataBind();
            gvShortfallItems.DataSource = null;
            gvShortfallItems.DataBind();
        }
    }
    protected void CompareDateValidator(object sender, ServerValidateEventArgs e)
    {

   
        DateTime sDate = DateTime.ParseExact(startDate.Text, "M/dd/yyyy", CultureInfo.InvariantCulture);
        DateTime eDate = DateTime.ParseExact(endDate.Text, "M/dd/yyyy", CultureInfo.InvariantCulture);
        DateTime today = DateTime.Parse(DateTime.Today.Date.ToString("M/dd/yyyy"));
        if (sDate>today)
        {
            e.IsValid = false;
            errorTxt.Text = "Start Date cannot be greater than today";
            errorTxt.ForeColor = System.Drawing.Color.Red;
            
        }
        else
        {
            if (sDate > eDate)
            {
                e.IsValid = false;
                errorTxt.Text = "Start Date cannot be greater than end Date";
                errorTxt.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                e.IsValid = true;
            }
        }
        
    }
}