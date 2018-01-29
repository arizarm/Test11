using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RetrievalList : System.Web.UI.Page
{
    RetrievalControl retCon = new RetrievalControl();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<Retrieval> retList = retCon.DisplayRetrievalList();
            gvReq.DataSource = retList;
            gvReq.DataBind();
        }
    }

    protected void SearchBtn_Click(object sender, EventArgs e)
    {
        string searchWord = SearchBox.Text;

        gvReq.DataSource = retCon.DisplaySearch(searchWord);
        gvReq.DataBind();

    }

    protected void DisplayBtn_Click(object sender, EventArgs e)
    {
        gvReq.DataSource = retCon.DisplayRetrievalList();
        gvReq.DataBind();
    }


    protected void gvDetailBtn_Click(object sender, EventArgs e)
    {
        GridViewRow row = ((Button)sender).NamingContainer as GridViewRow;  //detail btn        
        Session["RetrievalID"] = Convert.ToInt32((row.FindControl("LabelRetrievalID") as Label).Text); //row.Cells[1]
        Response.Redirect("~/Store/RetrievalListDetail.aspx");
    }
}