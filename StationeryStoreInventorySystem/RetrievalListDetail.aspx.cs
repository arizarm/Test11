using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RetrievalForm : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string retrievalId = (string)Session["RetrievalID"];
            gvRe.DataSource = RetrievalControl.DisplayRetrievalListDetail(retrievalId);
            gvRe.DataBind();
        }
    }


   static List<int> txtRetrievedList = new List<int>();
    protected void Save_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gvRe.Rows)
        {
            txtRetrievedList.Add(Convert.ToInt32((row.FindControl("txtRetrieved") as TextBox).Text));
        }
        Session["txtRetrievedList"] = txtRetrievedList;
        RetrievalControl.SaveRetrieved(txtRetrievedList);
    }

    protected void FinalizeDisbursmentList_Click(object sender, EventArgs e)
    {
        //if Quantity Requested == Quantity Retrieved > update collect
        //else  >RetrievalShortfall> update collect 

        foreach (GridViewRow row in gvRe.Rows)
        {
            txtRetrievedList.Add(Convert.ToInt32((row.FindControl("txtRetrieved") as TextBox).Text));
        }
        Session["txtRetrievedList"] = txtRetrievedList;
        RetrievalControl.CheckShortfall(txtRetrievedList);


        //Session["RequisitionNo"] = s;
        //Response.Redirect("CollectionPointUpdate.aspx");
    }
}