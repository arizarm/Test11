using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


//AUTHOR : KIRUTHIKA VENKATESH
public partial class ReorderReport : System.Web.UI.Page
{
    PurchaseController pCtrlr = new PurchaseController();

    protected void BtnGenerate_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            DateTime sDate = Convert.ToDateTime(startDate.Text);
            DateTime eDate = Convert.ToDateTime(endDate.Text);

            lblsdate.Text = sDate.ToShortDateString();
            lbledate.Text = eDate.ToShortDateString();
            lblmsg.Text = "List of items running low on stock which are yet to be delivered from supplier";
            List<ShortfallItems> ritem = pCtrlr.GenerateReorderReportForPurchasedItems(sDate, eDate);
            if (ritem.Count == 0)
            {
                lblresult1.Text = "No Shortfall item";
            }
            else
            {
                lblresult1.Text = "";
            }
            GvPurchasedreoderItem.DataSource = ritem;
            GvPurchasedreoderItem.DataBind();
            List<ShortfallItems> ritem1 = pCtrlr.GenerateShortfallItemsReport(sDate, eDate);
            if (ritem1.Count == 0)
            {
                lblresult2.Text = "No Shortfall item";
            }
            else
            {
                lblresult2.Text = "";
            }
            lblmsg2.Text = "List of items running low on stock with no purchases done yet";
            GvShortfallItems.DataSource = ritem1;
            GvShortfallItems.DataBind();
        }
        else
        {
            lblmsg.Text = "";
            lblmsg2.Text = "";
            GvPurchasedreoderItem.DataSource = null;
            GvPurchasedreoderItem.DataBind();
            GvShortfallItems.DataSource = null;
            GvShortfallItems.DataBind();
        }
    }
    protected void CompareDateValidator(object sender, ServerValidateEventArgs e)
    {

        string sdate = startDate.Text;
        string edate = endDate.Text;

        DateTime d1 = Convert.ToDateTime(sdate);
        DateTime d2 = Convert.ToDateTime(edate);

        string todayDate = DateTime.Today.ToString("yyyy-MM-dd");
        DateTime today = Convert.ToDateTime(todayDate);

        //DateTime sDate = DateTime.ParseExact(startDate.Text, "dd-mm-yy", CultureInfo.InvariantCulture);
        //DateTime eDate = DateTime.ParseExact(endDate.Text, "dd-mm-yy", CultureInfo.InvariantCulture);
        //DateTime today = DateTime.Parse(DateTime.Today.Date.ToString("dd-mm-yy"));
        if (d1 > today && d2 > today)
        {
            e.IsValid = false;
            lblerror.Text = "Start Date & End Date cannot be greater than today";
            lblerror.ForeColor = System.Drawing.Color.Red;

        }
        else if (d1 > today)
        {
            e.IsValid = false;
            lblerror.Text = "Start Date cannot be greater than today";
            lblerror.ForeColor = System.Drawing.Color.Red;
        }
        else if (d2 > today)
        {
            e.IsValid = false;
            lblerror.Text = "End Date cannot be greater than today";
            lblerror.ForeColor = System.Drawing.Color.Red;
        }
        else
        {
            if (d1 > d2)
            {
                e.IsValid = false;
                lblerror.Text = "Start Date cannot be greater than end Date";
                lblerror.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                e.IsValid = true;
                lblerror.Text = "";
            }
        }

    }
}