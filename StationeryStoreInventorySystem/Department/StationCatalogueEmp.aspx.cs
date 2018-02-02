﻿using System;
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
                gvForStationCatalogue.DataSource = EFBroker_Item.GetCatalogueList();
            }
            catch (Exception sql)
            {
                Response.Redirect(LoginController.ErrorPageURI);
            }
            if (!IsPostBack)
            {
                gvForStationCatalogue.DataBind();

            }
        }
        else
        {
            Utility.logout();
        }
    }

    protected void BtnViewPrint_Click(object sender, EventArgs e)
    {
        if (btnViewPrint.Text == "View Printable Version")
        {
            btnViewPrint.Text = "Back";
            btnForPrint.Visible = true;
        }
        else
        {
            btnViewPrint.Text = "View Printable Version";
            btnForPrint.Visible = false;

        }
    }


    protected void GvForStationCatalogue_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label index = e.Row.FindControl("LabelNumber") as Label;
            index.Text = (e.Row.RowIndex+1).ToString();
        }
    }
}