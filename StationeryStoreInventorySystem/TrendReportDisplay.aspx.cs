using CrystalDecisions.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TrendReportDisplay : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        List<List<TrendReport>> ListOfBROs = (List<List<TrendReport>>)Session["reportsToDisplay"];
        int typeOfReport = (int)Session["typeOfReport"];

        switch (typeOfReport)
        {
            case 0:
                foreach (List<TrendReport> list in ListOfBROs)
                {
                    CrystalReportViewer crv1 = new CrystalReportViewer();

                    ContentPlaceHolder content = (ContentPlaceHolder)this.Master.FindControl("ContentPlaceHolder1");
                    crv1.AutoDataBind = true;
                    content.Controls.Add(crv1);

                    content.Controls.Add(new LiteralControl("<br />"));
                    content.Controls.Add(new LiteralControl("<br />"));

                    //Only Report Type that will appear differs

                    RequisitionTrendByDept cr1 = new RequisitionTrendByDept();
                    crv1.ReportSource = cr1;
                    cr1.SetDataSource(list);
                }

                break;

            case 1:
                foreach (List<TrendReport> list in ListOfBROs)
                {
                    CrystalReportViewer crv1 = new CrystalReportViewer();

                    ContentPlaceHolder content = (ContentPlaceHolder)this.Master.FindControl("ContentPlaceHolder1");
                    content.Controls.Add(crv1);
                    crv1.AutoDataBind = true;

                    content.Controls.Add(new LiteralControl("<br />"));
                    content.Controls.Add(new LiteralControl("<br />"));

                    //Only Report Type that will appear differs


                    RequisitionTrendByCat cr1 = new RequisitionTrendByCat();

                    crv1.ReportSource = cr1;
                    cr1.SetDataSource(list);

                }
                break;

            case 2:
                foreach (List<TrendReport> list in ListOfBROs)
                {
                    CrystalReportViewer crv1 = new CrystalReportViewer();

                    ContentPlaceHolder content = (ContentPlaceHolder)this.Master.FindControl("ContentPlaceHolder1");
                    content.Controls.Add(crv1);
                    crv1.AutoDataBind = true;

                    content.Controls.Add(new LiteralControl("<br />"));
                    content.Controls.Add(new LiteralControl("<br />"));

                    //Only Report Type that will appear differs


                    RORSupplier cr1 = new RORSupplier();

                    crv1.ReportSource = cr1;
                    cr1.SetDataSource(list);

                }
                break;

            case 3:
                foreach (List<TrendReport> list in ListOfBROs)
                {
                    CrystalReportViewer crv1 = new CrystalReportViewer();

                    ContentPlaceHolder content = (ContentPlaceHolder)this.Master.FindControl("ContentPlaceHolder1");
                    content.Controls.Add(crv1);
                    crv1.AutoDataBind = true;

                    content.Controls.Add(new LiteralControl("<br />"));
                    content.Controls.Add(new LiteralControl("<br />"));

                    //Only Report Type that will appear differs


                    RequisitionTrendByCat cr1 = new RequisitionTrendByCat();

                    crv1.ReportSource = cr1;
                    cr1.SetDataSource(list);

                }
                break;
        }
    }

    protected void Page_Unload(object sender, EventArgs e)
    {
        Dispose();
        GC.Collect();
        GC.WaitForPendingFinalizers();
    }
}