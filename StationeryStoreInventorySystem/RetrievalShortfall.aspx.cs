using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RetrievalDecision : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //string retrievalId = (string)Session["RetrievalID"];

        List<int> txtRetrievedList = (List<int>)Session["txtRetrievedList"];
        gvMain.DataSource = RetrievalControl.DisplayRetrievalShortfall(txtRetrievedList);
        gvMain.DataBind();

    }

    protected void gvMain_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.DataRow) return;
        GridView nestedGV = (GridView)e.Row.FindControl("gvSub");

        nestedGV.DataSource = RetrievalControl.DisplayRetrievalShortfallSub();
        nestedGV.DataBind();
    }

    protected void BtnResetAll_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {

    }


    static List<int> txtActualDisburseList = new List<int>();
    protected void BtnGenerateDisbursementList_Click(object sender, EventArgs e)
    {
        //foreach (GridViewRow row in gvRe.Rows)
        //{
        //    txtRetrievedList.Add(Convert.ToInt32((row.FindControl("txtRetrieved") as TextBox).Text));
        //}
        //Session["txtActualDisburseList"] = txtRetrievedList;

        RetrievalControl.GenerateDisbursementList();

        Response.Redirect("DisbursementList.aspx");
    }

 
}