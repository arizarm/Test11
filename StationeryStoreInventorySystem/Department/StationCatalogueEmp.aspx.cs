using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

public partial class Department_StationCatalogueEmp : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Employee user = (Employee)Session["emp"];
        if (user != null)
        {
            try
            {
                GridView1.DataSource = EFBroker_Item.GetCatalogueList();
            }
            catch (Exception sql)
            {
                Response.Redirect(LoginController.ErrorPageURI);
            }
            if (!IsPostBack)
            {
                GridView1.DataBind();

            }
        }
        else
        {
            Utility.logout();
        }
    }

    protected void PrintViewButton_Click(object sender, EventArgs e)
    {
        if (PrintViewButton.Text == "View Printable Version")
        {
            PrintViewButton.Text = "Back";
            PrintButton.Visible = true;
        }
        else
        {
            PrintViewButton.Text = "View Printable Version";
            PrintButton.Visible = false;

        }
    }


    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label index = e.Row.FindControl("LabelNumber") as Label;
            index.Text = (e.Row.RowIndex+1).ToString();
        }
    }
}