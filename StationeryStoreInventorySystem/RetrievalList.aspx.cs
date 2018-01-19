using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RetrievalList : System.Web.UI.Page
{
    static StationeryEntities context = new StationeryEntities();

    protected void Page_Load(object sender, EventArgs e)
    {

        gvReq.DataSource = context.Retrievals.ToList();
        gvReq.DataBind();


        //if (!IsPostBack)
        //{
        //    gvReq.DataSource = RequisitionControl.DisplayAll();
        //    gvReq.DataBind();
        //}
    }

    protected void SearchBtn_Click(object sender, EventArgs e)
    {

      //  string searchWord = SearchBox.Text;

      //  if (SearchBox.Text == String.Empty)
      //  {
      //      ClientScript.RegisterStartupScript(Page.GetType(),
      //"MessageBox",
      //"<script language='javascript'>alert('" + "Please enter value to search!" + "');</script>");
      //  }
      //  else
      //  {
      //      gvReq.DataSource = RequisitionControl.DisplaySearch(searchWord);
      //      gvReq.DataBind();
      //  }
    }

    protected void DisplayBtn_Click(object sender, EventArgs e)
    {
        //gvReq.DataSource = RequisitionControl.DisplayAll();
        //gvReq.DataBind();
    }


    protected void gvDetailBtn_Click(object sender, EventArgs e)
    {

    }
}