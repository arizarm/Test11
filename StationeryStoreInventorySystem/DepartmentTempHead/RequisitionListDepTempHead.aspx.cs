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
        if (!IsPostBack)
        {
            if (Session["emp"] != null)
            {
                Employee emp = (Employee)Session["emp"];

                //Dep Temp Head
                GridView1.DataSource = RequisitionControl.getRequisitionListByStatusAndDepCode("Pending", emp.DeptCode);
                GridView1.DataBind();
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

            GridView1.DataSource = RequisitionControl.getRequisitionListByStatusAndDepCode("Pending", emp.DeptCode);
            GridView1.DataBind();
        }
        else
        {
            Utility.logout();
        }

    }
    protected void SearchBtn_Click(object sender, EventArgs e)
    {
        Employee emp = (Employee)Session["emp"];
        string searchWord = SearchBox.Text;
        if (String.IsNullOrWhiteSpace(searchWord))
        {
            ClientScript.RegisterStartupScript(Page.GetType(), "MessageBox", "<script language='javascript'>alert('" + "Please enter value to search!" + "');</script>");
        }
        else
        {
            if (DropDownList1.SelectedItem.ToString() == "Select Status")
            {
                GridView1.DataSource = RequisitionControl.HeadSearchWithoutStatus(searchWord, emp.DeptCode);
                GridView1.DataBind();
            }
            else
            {
                GridView1.DataSource = RequisitionControl.HeadSearchWithStatus(searchWord, emp.DeptCode, DropDownList1.SelectedItem.ToString());
                GridView1.DataBind();
            }
        }
    }

    protected void DisplayBtn_Click(object sender, EventArgs e)
    {
        GridView1.DataSource = RequisitionControl.DisplayAllDepartment();
        GridView1.DataBind();
    }


}