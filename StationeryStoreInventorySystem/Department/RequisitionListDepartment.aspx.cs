using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ReqisitionListEmployee : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["empRole"] = "Employee";
        Session["isTempHead"] = "11";

        if (Session["empRole"].ToString() == "DepartmentHead" || Session["isTempHead"].ToString() == "Representative")
        {
            GridView1.Visible = false;
            GridView2.Visible = true;
        }
        else if (Session["empRole"].ToString() == "Employee" || Session["empRole"].ToString() == "Representative")
        {
            GridView1.Visible = true;
            GridView2.Visible = false;
        }

        if (!IsPostBack)
        {
            GridView1.DataSource = RequisitionControl.getRequisitionListByStatus("Pending");
            GridView1.DataBind();
            GridView2.DataSource = RequisitionControl.getRequisitionListByStatus("Pending");
            GridView2.DataBind();

        }
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        string selectedStatus = DropDownList1.SelectedValue;

        GridView1.DataSource = RequisitionControl.getRequisitionListByStatus(selectedStatus);
        GridView1.DataBind();

        GridView2.DataSource = RequisitionControl.getRequisitionListByStatus(selectedStatus);
        GridView2.DataBind();
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
            GridView1.DataSource = RequisitionControl.DisplaySearchDepartment(searchWord);
            GridView2.DataBind();
            GridView2.DataSource = RequisitionControl.DisplaySearchDepartment(searchWord);
            GridView2.DataBind();
        }
    }

    protected void DisplayBtn_Click(object sender, EventArgs e)
    {
        GridView1.DataSource = RequisitionControl.DisplayAllDepartment();
        GridView1.DataBind();
        GridView2.DataSource = RequisitionControl.DisplayAllDepartment();
        GridView2.DataBind();
    }


}