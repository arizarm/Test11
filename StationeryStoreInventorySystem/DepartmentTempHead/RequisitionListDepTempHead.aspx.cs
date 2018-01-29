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
                GridView1.DataSource = RequisitionControl.DisplayAllByDeptCode(emp.DeptCode);
                GridView1.DataBind();

                int count = RequisitionControl.CountPending(emp.DeptCode);

                pendingCount.Text = "Total Pendings: " + count.ToString();
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

            if (DropDownList1.SelectedItem.ToString() == "Select Status")
            {
                GridView1.DataSource = RequisitionControl.DisplayAllByDeptCode(emp.DeptCode);
                GridView1.DataBind();
            }
            else
            {
                string selectedStatus = DropDownList1.SelectedItem.ToString();
                GridView1.DataSource = RequisitionControl.getRequisitionListByStatusAndDepCode(DropDownList1.SelectedItem.ToString(), emp.DeptCode);
                GridView1.DataBind();
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
        string searchWord = SearchBox.Text;
        if (String.IsNullOrWhiteSpace(searchWord))
        {
            ClientScript.RegisterStartupScript(Page.GetType(), "MessageBox", "<script language='javascript'>alert('" + "Please enter value to search!" + "');</script>");
        }
        else
        {
            if (DropDownList1.SelectedItem.ToString() == "Select Status")
            {
                GridView1.DataSource = RequisitionControl.HeadSearchWithoutStatus(searchWord.Trim(), emp.DeptCode);
                GridView1.DataBind();
            }
            else
            {
                GridView1.DataSource = RequisitionControl.HeadSearchWithStatus(searchWord.Trim(), emp.DeptCode, DropDownList1.SelectedItem.ToString());
                GridView1.DataBind();
            }
        }
    }

    protected void DisplayBtn_Click(object sender, EventArgs e)
    {
        Employee emp = (Employee)Session["emp"];
        GridView1.DataSource = RequisitionControl.DisplayAllByDeptCode(emp.DeptCode);
        GridView1.DataBind();
    }
}