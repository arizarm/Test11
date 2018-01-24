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
                //Dep Emp
                GridView1.DataSource = RequisitionControl.getRequisitionListByID(emp.EmpID);
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
        string searchWord = SearchBox.Text;
        if (SearchBox.Text == String.Empty)
        {
            ClientScript.RegisterStartupScript(Page.GetType(),
      "MessageBox",
      "<script language='javascript'>alert('" + "Please enter value to search!" + "');</script>");
        }
        else
        {

        }
    }

    protected void DisplayBtn_Click(object sender, EventArgs e)
    {
    }


}