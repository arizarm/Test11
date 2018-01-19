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


    List<int>txtRetrievedList = new List<int>();
    protected void Save_Click(object sender, EventArgs e)
    {
        //GridViewRow row = ((TextBox)sender).NamingContainer as GridViewRow;  //TextBox
        //string s = (row.FindControl("txtRetrieved") as TextBox).Text; //row.Cells[2]

        //if two qty equal, get reqQty and put in database actual


        foreach (GridViewRow row in gvRe.Rows)
        {
            txtRetrievedList.Add(Convert.ToInt32((row.FindControl("txtRetrieved") as TextBox).Text));
       
        }
        RetrievalControl.SaveRetrieved(txtRetrievedList);
    }

    protected void FinalizeDisbursmentList_Click(object sender, EventArgs e)
    {

    }
}