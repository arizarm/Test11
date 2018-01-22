using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CollectionPointUpdate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            string retrievalId = (string)Session["RetrievalID"];
            gvCollectionPoint.DataSource = RetrievalControl.DisplayCollectionPoint(retrievalId);
            gvCollectionPoint.DataBind();

            
        }
        
    }


    List<DateTime> dateList = new List<DateTime>();
    List<string> timeList = new List<string>();
    protected void Submit_Click(object sender, EventArgs e)
    {
        //foreach (GridViewRow row in gvCollectionPoint.Rows)
        //{
        //    dateList.Add((row.FindControl("date") as TextBox).Text));
        //}
        //RetrievalControl.SaveCollectionTimeAndDateToDisbursement(dateList);

        foreach (GridViewRow row in gvCollectionPoint.Rows)
        {
            dateList.Add(DateTime.Parse((row.FindControl("txtDate") as TextBox).Text));
            timeList.Add(((row.FindControl("time") as TextBox).Text));
        }
        RetrievalControl.SaveCollectionTimeAndDateToDisbursement(dateList,timeList);
    }
}