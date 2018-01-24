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
                if (RequisitionControl.getCollectionList(emp.DeptCode).Count ==  0)
                {
                    Label5.Text = "There is no collection data";
                }
                else
                {
                    Label5.Visible = false;
                    GridView1.DataSource = RequisitionControl.getCollectionList(emp.DeptCode);
                    GridView1.DataBind();
                }
            }
            else

            {
                Utility.logout();
            }
        }      
    }

    protected void SearchBtn_Click(object sender, EventArgs e)
    {
        if (Session["emp"] != null)
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
                GridView1.DataBind();
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
            GridView1.DataSource = RequisitionControl.getCollectionList(emp.DeptCode);
            GridView1.DataBind();
        }
        else

        {
            Utility.logout();
        }
    }


}