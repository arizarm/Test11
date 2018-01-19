using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RetrievalList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<Retrieval> retList = RetrievalControl.DisplayRetrievalList();
            gvReq.DataSource = retList;
            gvReq.DataBind();            
        }
    }

    protected void SearchBtn_Click(object sender, EventArgs e)
    {
        string searchWord = SearchBox.Text;

        if (SearchBox.Text == String.Empty)
        {
            ClientScript.RegisterStartupScript(Page.GetType(),
      "MessageBox",
      "<script language='javascript'>alert('" + "Please enter value to search!" + "');</script>");
        }
        else
        {
            gvReq.DataSource = RetrievalControl.DisplaySearch(searchWord);
            gvReq.DataBind();
        }
    }

    protected void DisplayBtn_Click(object sender, EventArgs e)
    {
        gvReq.DataSource = RetrievalControl.DisplayRetrievalList();
        gvReq.DataBind();
    }


    protected void gvDetailBtn_Click(object sender, EventArgs e)
    {
        GridViewRow row = ((Button)sender).NamingContainer as GridViewRow;  //detail btn
        string s = (row.FindControl("LabelRetrievalID") as Label).Text; //row.Cells[1]
        Session["RetrievalID"] = s;
        Response.Redirect("RetrievalListDetail.aspx");
    }
}