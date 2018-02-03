using System;
using CrystalDecisions.Web;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;

public partial class TrendReportDisplay : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        List<TrendReport> ListOfBROs = (List<TrendReport>)Session["reportsToDisplay"];
        int typeOfReport = (int)Session["typeOfReport"];
        //ReportDocument myReport = new ReportDocument();
        //string reportPath;
        switch (typeOfReport)
        {
            case 0:
                    CrystalReportViewer crv1 = new CrystalReportViewer();
                    //reportPath = Server.MapPath("crystalreport1.rpt");
                    //myReport.Load(reportPath);
                    ContentPlaceHolder content = (ContentPlaceHolder)this.Master.FindControl("ContentPlaceHolder1");
                    crv1.AutoDataBind = true;
                    content.Controls.Add(crv1);

                    content.Controls.Add(new LiteralControl("<br />"));
                    content.Controls.Add(new LiteralControl("<br />"));

                    RequisitionTrendByDept cr1 = new RequisitionTrendByDept();
                    crv1.ReportSource = cr1;
                    cr1.SetDataSource(ListOfBROs);              

                break;

            case 1:
                    CrystalReportViewer crv2 = new CrystalReportViewer();

                    ContentPlaceHolder content1 = (ContentPlaceHolder)this.Master.FindControl("ContentPlaceHolder1");
                    content1.Controls.Add(crv2);
                    crv2.AutoDataBind = true;

                    content1.Controls.Add(new LiteralControl("<br />"));
                    content1.Controls.Add(new LiteralControl("<br />"));

                    RequisitionTrendByCat cr2 = new RequisitionTrendByCat();

                    crv2.ReportSource = cr2;
                    cr2.SetDataSource(ListOfBROs);
                break;

            case 2:
                    CrystalReportViewer crv3 = new CrystalReportViewer();

                    ContentPlaceHolder content2 = (ContentPlaceHolder)this.Master.FindControl("ContentPlaceHolder1");
                    content2.Controls.Add(crv3);
                    crv3.AutoDataBind = true;

                    content2.Controls.Add(new LiteralControl("<br />"));
                    content2.Controls.Add(new LiteralControl("<br />"));

                    RORSupplier cr3 = new RORSupplier();

                    crv3.ReportSource = cr3;
                    cr3.SetDataSource(ListOfBROs);
                break;

            case 3:
                    CrystalReportViewer crv4 = new CrystalReportViewer();

                    ContentPlaceHolder content3 = (ContentPlaceHolder)this.Master.FindControl("ContentPlaceHolder1");
                    content3.Controls.Add(crv4);
                    crv4.AutoDataBind = true;

                    content3.Controls.Add(new LiteralControl("<br />"));
                    content3.Controls.Add(new LiteralControl("<br />"));

                    RORCategory cr4 = new RORCategory();

                    crv4.ReportSource = cr4;
                    cr4.SetDataSource(ListOfBROs);
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