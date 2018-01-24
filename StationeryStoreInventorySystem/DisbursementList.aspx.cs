using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DisbursementList : System.Web.UI.Page
{
    DisbursementCotrol disbCon = new DisbursementCotrol();

    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            List<DisbursementListItems> disbursementListItemsList = disbCon.gvDisbursementPopulate();
            Session["disbItemsList"] = disbursementListItemsList;
            gdvDisbList.DataSource = disbursementListItemsList;
            gdvDisbList.DataBind();
        }
    }
    

    protected void btnDetail_Click(object sender, EventArgs e)
    {
        GridViewRow gvRow = ((Button)sender).NamingContainer as GridViewRow;
        Session["SelectedDisb"] = Convert.ToInt32((gvRow.FindControl("lbldisbId") as Label).Text);
        Response.Redirect("~/DisbursementListDetail.aspx");
    }
}
