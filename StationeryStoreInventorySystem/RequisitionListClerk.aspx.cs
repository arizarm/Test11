﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ReqisitionListClerk : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {

        if(!IsPostBack)
        {
            gvReq.DataSource = RequisitionControl.DisplayAll();
            gvReq.DataBind();
        }

        if (DropDownList1.Text == "Priority")
        {
            gvReq.DataSource = null;
            gvReq.DataSource = RequisitionControl.DisplayPriority();
            gvReq.DataBind();
        }

        if (DropDownList1.Text == "Approved")
        {
            gvReq.DataSource = null;
            gvReq.DataSource = RequisitionControl.DisplayApproved();
            gvReq.DataBind();
        }
}


    protected void CheckAll_CheckedChanged(object sender, EventArgs e)
    {
        if (((CheckBox)gvReq.HeaderRow.FindControl("CheckAll")).Checked)
        {
            foreach (GridViewRow row in gvReq.Rows)
            {
                ((CheckBox)row.FindControl("CheckBox")).Checked = true;
            }
         }

        if (!((CheckBox)gvReq.HeaderRow.FindControl("CheckAll")).Checked)
        {
            foreach (GridViewRow row in gvReq.Rows)
            {
                ((CheckBox)row.FindControl("CheckBox")).Checked = false;
            }
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
            gvReq.DataSource = RequisitionControl.DisplaySearch(searchWord);
            gvReq.DataBind();
        }
    }

    protected void DisplayBtn_Click(object sender, EventArgs e)
    {
        gvReq.DataSource = RequisitionControl.DisplayAll();
        gvReq.DataBind();
    }



    protected void GenerateBtn_Click(object sender, EventArgs e)
    {
        List<string> reqNo = new List<string>();

        foreach (GridViewRow row in gvReq.Rows)
        {
            if (((CheckBox)row.FindControl("CheckBox")).Checked)
            {
                reqNo.Add((row.FindControl("lblrequisitionNo") as Label).Text);
            }
        }

        Session["reqNo"] = reqNo;
        Response.Redirect("RequisitionDetails.aspx");

    }

    protected void gvDetailBtn_Click(object sender, EventArgs e)
    {
        GridViewRow row = ((Button)sender).NamingContainer as GridViewRow;  //detail btn
        string s = (row.FindControl("lblrequisitionNo") as Label).Text; //row.Cells[2]
        Session["RequisitionNo"] = s;
        Response.Redirect("RequisitionDetails.aspx");
    }

}