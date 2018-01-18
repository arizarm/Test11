using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DisbursementList : System.Web.UI.Page
{   
    
    protected void Page_Load(object sender, EventArgs e)
    {
        gdvDisbList.DataSource = DisbursementCotrol.gvDisbursementPopulate();
        gdvDisbList.DataBind();
    }
    

    protected void btnDetail_Click(object sender, EventArgs e)
    {
        GridViewRow gvRow = ((Button)sender).NamingContainer as GridViewRow;
        Session["SelectedDisb"] = (gvRow.FindControl("lbldisbId") as Label).Text;
        Response.Redirect("~/DisbursementListDetail.aspx");
    }
}
