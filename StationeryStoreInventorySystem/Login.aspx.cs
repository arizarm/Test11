using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string email = TextBox1.Text;
        string password = Password1.Value;

        bool isValid = EmployeeController.verifyLogin(email, password);

        if (isValid)
        {
            Employee emp = EmployeeController.GetEmployeeByEmail(email);
            Session["empID"] = emp.EmpID;
            Session["empRole"] = emp.Role;

            Label4.Text = "Success User";

            NavigateMain();

        }
        else
        {
            Label4.Text = "Invalid User";
        }
    }

    protected void NavigateMain()
    {
        string role = Session["empRole"].ToString();

        if (role == "Clerk")
        {
            Response.Redirect("~/ReqisitionListClerk.aspx");
        }
        else if (role == "Supervisor" || role == "Manager")
        {
            Response.Redirect("~/PurchaseOrderList.aspx");
        }
        else if (role == "Head")
        {
            Response.Redirect("~/ReqisitionListDepartment.aspx");
        }
        else if (role == "Employee")
        {
            Response.Redirect("~/RegenerateRequest.aspx");
        }
        else if (role == "Representative")
        {
            Response.Redirect("~/RegenerateRequest.aspx");
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Utility u = new Utility();
        u.sendMail("yimonsoe.yms@gmail.com","Mail Subject","I am mail body");
        Response.Redirect("~/Login.aspx");
    }
}