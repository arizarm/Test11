using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

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
            FormsAuthentication.RedirectFromLoginPage
          (email, Persist.Checked);


            Employee emp = EmployeeController.GetEmployeeByEmail(email);
            Session["empID"] = emp.EmpID;
            Session["empRole"] = emp.Role;
            Session["emp"] = emp;

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
        Employee e = (Employee)Session["emp"];

        if (role == "Store Clerk")
        {
            Response.Redirect("~/RequisitionListClerk.aspx");
        }
        else if (role == "Store Supervisor" || role == "Store Manager")
        {
            Response.Redirect("~/PurchaseOrderList.aspx");
        }
        else if (role == "DepartmentHead" || Utility.checkIsTempDepHead(e))
        {
            Response.Redirect("~/Department/RequisitionListDepartment.aspx");
        }
        else if (role == "Employee")
        {
            Response.Redirect("~/Department/RequisitionForm.aspx");
        }
        else if (role == "Representative")
        {
            Response.Redirect("~/Department/RequisitionForm.aspx");
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Utility.sendMail("yimonsoe.yms@gmail.com","Mail Subject","I am mail body");
        Response.Redirect("~/Login.aspx");
    }
}