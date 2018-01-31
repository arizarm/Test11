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
        if (!IsPostBack)
        {
            List<DisbursementListItems> disbursementListItemsList = FillDisbursementListItems();
            gdvDisbList.DataSource = disbursementListItemsList;
            gdvDisbList.DataBind();
            ViewState["sortDirection"] = "";
        }
    }


    protected void btnDetail_Click(object sender, EventArgs e)
    {
        GridViewRow gvRow = ((Button)sender).NamingContainer as GridViewRow;
        Session["SelectedDisb"] = Convert.ToInt32((gvRow.FindControl("lbldisbId") as Label).Text);
        Response.Redirect(LoginController.DisbursementListDetailURI);
    }

    protected void gdvDisbList_Sorting(object sender, GridViewSortEventArgs e)
    {
        if ((string)ViewState["sortDirection"] == "desc" || (string)ViewState["sortDirection"] == "")
        {
            List<DisbursementListItems> disbursementListItemsList = FillDisbursementListItems().OrderBy(c => c.DepName).ToList();
            Session["disbItemsList"] = disbursementListItemsList;
            gdvDisbList.DataSource = disbursementListItemsList;
            gdvDisbList.DataBind();
            ViewState["sortDirection"] = "asc";
        }
        else if ((string)ViewState["sortDirection"] == "asc")
        {
            List<DisbursementListItems> disbursementListItemsList = FillDisbursementListItems().OrderByDescending(c => c.DepName).ToList();
            Session["disbItemsList"] = disbursementListItemsList;
            gdvDisbList.DataSource = disbursementListItemsList;
            gdvDisbList.DataBind();
            ViewState["sortDirection"] = "desc";

        }
    }

    protected List<DisbursementListItems> FillDisbursementListItems()
    {
        List<DisbursementListItems> disbursementListItemsList = disbCon.gvDisbursementPopulate();
        Session["disbItemsList"] = disbursementListItemsList;
        return disbursementListItemsList;
    }
}
