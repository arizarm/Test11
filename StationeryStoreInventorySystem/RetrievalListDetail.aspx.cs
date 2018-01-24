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
        if (!IsPostBack) //first time 
        {
            int retrievalId = (int)Session["RetrievalID"];
            gvRe.DataSource = RetrievalControl.DisplayRetrievalListDetail(retrievalId);
            gvRe.DataBind();

            List<int> txtRetrievedList = (List<int>)Session["txtRetrievedList"];
            if (txtRetrievedList != null) //second time load to page 
            {
                int i = 0;
                foreach (GridViewRow row in gvRe.Rows)
                {
                    (row.FindControl("txtRetrieved") as TextBox).Text = txtRetrievedList[i].ToString();
                    i++;
                }
            }
        }
    }

    protected void Save_Click(object sender, EventArgs e)
    {
        List<int> txtRetrievedList = new List<int>();
        foreach (GridViewRow row in gvRe.Rows)
        {
            txtRetrievedList.Add(Convert.ToInt32((row.FindControl("txtRetrieved") as TextBox).Text));
        }
        Session["txtRetrievedList"] = txtRetrievedList;
    }

    protected void FinalizeDisbursmentList_Click(object sender, EventArgs e)
    {
        //if Quantity Requested == Quantity Retrieved > update collect
        //else  >RetrievalShortfall> update collect 

        List<int> txtRetrievedList = new List<int>();
        List<RetrievalShortfallItem> RetrievalShortfallItemList = new List<RetrievalShortfallItem>();

        foreach (GridViewRow row in gvRe.Rows)
        {
            txtRetrievedList.Add(Convert.ToInt32((row.FindControl("txtRetrieved") as TextBox).Text));
        }
        RetrievalControl.UpdateDisbursementNonShortfallItemActualQty(txtRetrievedList);

        RetrievalShortfallItemList = RetrievalControl.CheckShortfall(txtRetrievedList);

        if (RetrievalShortfallItemList != null)  //if any shortfall
        {
            Session["RetrievalShortfallItemList"] = RetrievalShortfallItemList;
            Response.Redirect("RetrievalShortfall.aspx");
        }
        else
        {
            Response.Redirect("CollectionPointUpdate.aspx");
        }
    }
}