using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ReqisitionListEmployee : System.Web.UI.Page
{
    string searchWord = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["emp"] != null)
            {
                Employee emp = (Employee)Session["emp"];
                //Dep Emp
                GridView1.DataSource = RequisitionControl.getRequisitionListByID(emp.EmpID);
                GridView1.DataBind();
                ViewState["DataSource"] = "displayAll";
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

            if (DropDownList1.SelectedItem.Text == "Select Status")
            {
                GridView1.DataSource = RequisitionControl.getRequisitionListByID(emp.EmpID);
                GridView1.DataBind();
                ViewState["DataSource"] = "displayAll";
            }
            else
            {
                GridView1.DataSource = RequisitionControl.getRequisitionListByEmpIDAndStatus(emp.EmpID, selectedStatus);
                GridView1.DataBind();
                ViewState["DataSource"] = "displayStatusSearch";
            }
        }
        else
        {
            Utility.logout();
        }
    }
    protected void SearchBtn_Click(object sender, EventArgs e)
    {
        Employee emp = (Employee)Session["emp"];
        searchWord = SearchBox.Text;
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
                ViewState["DataSource"] = "displaySearch";
            }
            else
            {
                GridView1.DataSource = RequisitionControl.SearchForRepRequisitionWithStatus(searchWord.Trim(), emp.EmpID, DropDownList1.SelectedItem.ToString());
                GridView1.DataBind();
                ViewState["DataSource"] = "displaySearchStatus";
            }
        }
    }

    protected void DisplayBtn_Click(object sender, EventArgs e)
    {
        if (Session["emp"] != null)
        {
            Employee emp = (Employee)Session["emp"];
            GridView1.DataSource = RequisitionControl.getRequisitionListByID(emp.EmpID);
            GridView1.DataBind();
            ViewState["DataSource"] = "displayAll";
        }
        else
        {
            Utility.logout();
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Employee emp = (Employee)Session["emp"];
        GridView1.PageIndex = e.NewPageIndex;
        if (((string)ViewState["DataSource"]).Equals("displayAll"))
        {
            GridView1.DataSource = RequisitionControl.getRequisitionListByID(emp.EmpID);
        }
        else if (((string)ViewState["DataSource"]).Equals("displayStatusSearch"))
        {
            GridView1.DataSource = RequisitionControl.getRequisitionListByEmpIDAndStatus(emp.EmpID, DropDownList1.SelectedItem.ToString());
        }
        else if (((string)ViewState["DataSource"]).Equals("displaySearch"))
        {
            GridView1.DataSource = RequisitionControl.SearchForRepRequisitionWithoutStatus(searchWord.Trim(), emp.EmpID);
        }
        else
        {
            GridView1.DataSource = RequisitionControl.SearchForRepRequisitionWithStatus(searchWord.Trim(), emp.EmpID, DropDownList1.SelectedItem.ToString());
        }
        GridView1.DataBind();
    }
}