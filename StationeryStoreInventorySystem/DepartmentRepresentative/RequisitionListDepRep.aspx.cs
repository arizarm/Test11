﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ReqisitionListEmployee : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["emp"] != null)
            {
                Employee emp = (Employee)Session["emp"];

                //Requested Requisition
                if (RequisitionControl.getRequisitionListByID(emp.EmpID).Count == 0)
                {
                    Label5.Text = "There is no requested requisition data";
                }
                else
                {
                    Label5.Visible = false;
                    GridView1.DataSource = RequisitionControl.getRequisitionListByID(emp.EmpID);
                    GridView1.DataBind();
                }
            }
            else

            {
                Utility.logout();
            }
        }
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Session["emp"] != null)
        {
            Employee emp = (Employee)Session["emp"];
            string selectedStatus = DropDownList1.SelectedValue;

            GridView1.DataSource = RequisitionControl.getRequisitionListByEmpIDAndStatus(emp.EmpID, selectedStatus);
            GridView1.DataBind();
        }
        else
        {
            Utility.logout();
        }

    }
    protected void SearchBtn_Click(object sender, EventArgs e)
    {
        if (Session["emp"] != null)
        {
            Employee emp = (Employee)Session["emp"];
            string searchWord = SearchBox.Text;
            if (SearchBox.Text == String.Empty)
            {
                ClientScript.RegisterStartupScript(Page.GetType(),
                "MessageBox",
                "<script language='javascript'>alert('" + "Please enter value to search!" + "');</script>");
            }
            else
            {
                if (DropDownList1.SelectedItem.ToString() == "Select Status")
                {
                    GridView1.DataSource = RequisitionControl.SearchForRepRequisitionWithoutStatus(searchWord.Trim(), emp.EmpID);
                    GridView1.DataBind();
                }
                else
                {
                    GridView1.DataSource = RequisitionControl.SearchForRepRequisitionWithStatus(searchWord.Trim(), emp.EmpID, DropDownList1.SelectedItem.ToString());
                    GridView1.DataBind();
                }
            }
        }
        else

        {
            Utility.logout();
        }
    }

    protected void DisplayBtn_Click(object sender, EventArgs e)
    {
        if (Session["emp"] != null)
        {
            Employee emp = (Employee)Session["emp"];
            GridView1.DataSource = RequisitionControl.DisplayAllByDeptCodeEmpID(emp.DeptCode,emp.EmpID);
            GridView1.DataBind();
        }else
        {
            Utility.logout();
        }
    }
}