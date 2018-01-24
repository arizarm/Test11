using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RetrievalForm : System.Web.UI.Page
{
    RetrievalControl retCon = new RetrievalControl();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) //first time 
        {
            int retrievalId = (int)Session["RetrievalID"];

            List<RetrievalListDetailItem> RetrievalListDetailItemList = retCon.DisplayRetrievalListDetail(retrievalId);

            ViewState["RetrievalListDetailItemList"] = RetrievalListDetailItemList;

            gvRe.DataSource = RetrievalListDetailItemList;
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
        int retrievalId = (int)Session["RetrievalID"];

        List<RetrievalListDetailItem> RetrievalListDetailItemList = (List<RetrievalListDetailItem>)ViewState["RetrievalListDetailItemList"];

        List<int> txtRetrievedList = new List<int>();

        List<RetrievalShortfallItem> RetrievalShortfallItemList = new List<RetrievalShortfallItem>();

        foreach (GridViewRow row in gvRe.Rows)
        {
            txtRetrievedList.Add(Convert.ToInt32((row.FindControl("txtRetrieved") as TextBox).Text));
        }

        retCon.UpdateDisbursementNonShortfallItemActualQty(retrievalId, txtRetrievedList, RetrievalListDetailItemList);

        RetrievalShortfallItemList = retCon.CheckShortfall(txtRetrievedList);

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