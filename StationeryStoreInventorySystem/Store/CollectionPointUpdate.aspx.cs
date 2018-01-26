using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CollectionPointUpdate : System.Web.UI.Page
{
    RetrievalControl retCon = new RetrievalControl();     

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int retrievalId = (int)Session["RetrievalID"];
            gvCollectionPoint.DataSource = retCon.DisplayCollectionPoint(retrievalId);
            gvCollectionPoint.DataBind();
        }
    }

    
    protected void Submit_Click(object sender, EventArgs e)
    {
        int retrievalId = (int)Session["RetrievalID"];

        foreach (GridViewRow row in gvCollectionPoint.Rows)
        {
            string collectionPoint = (row.FindControl("labCollectionPoint") as Label).Text;
            DateTime date = DateTime.Parse((row.FindControl("txtDate") as TextBox).Text);
            string time = (row.FindControl("time") as TextBox).Text;
            retCon.SaveCollectionTimeAndDateToDisbursement(retrievalId, collectionPoint, date, time);
        }

        if (((Dictionary<Item, int>)Session["discrepancyList"]).Count != 0)
        {
            Response.Redirect("~/GenerateDiscrepancyAdhocV2.aspx");
        }
        else
        {
            Response.Redirect("~/Store/DisbursementList.aspx");
        }        
    }
}