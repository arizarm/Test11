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
                //Approved requisition
                if (RequisitionControl.getRequisitionListByStatusAndDepCode("Approved", emp.DeptCode) ==  null)
                {
                    Label5.Text = "There is no requisition data";
                }
                else
                {
                    Label5.Visible = false;
                    GridView1.DataSource = RequisitionControl.getRequisitionListByStatusAndDepCode("Approved", emp.DeptCode);
                    GridView1.DataBind();
                }

                //Requested Requisition
                if(RequisitionControl.getRequisitionListByID(emp.EmpID) == null)
                {
                    Label6.Text = "There is no requested requisition data";
                }
                else
                {
                    Label6.Visible = false;
                    GridView2.DataSource = RequisitionControl.getRequisitionListByID(emp.EmpID);
                    GridView2.DataBind();
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