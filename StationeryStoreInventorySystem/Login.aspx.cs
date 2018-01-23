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
            Employee emp = EmployeeController.GetEmployeeByEmail(email);
            //Check is temp head or not 
            if(Utility.checkIsTempDepHead(emp) == true)
            {
                //set role for temp head
                emp.Role = "DepartmentTempHead";              
            }
            Session["empRole"] = emp.Role;           
            Session["empID"] = emp.EmpID;         
            Session["emp"] = emp;

            Label4.Text = "Success User";

            FormsAuthentication.RedirectFromLoginPage
          (emp.Email, Persist.Checked);


            FormsAuthenticationTicket ticket1 =
               new FormsAuthenticationTicket(
                    1,                                   // version
                    emp.EmpName.Trim(),   // get username  from the form
                    DateTime.Now,                        // issue time is now
                    DateTime.Now.AddMinutes(30),         // expires in 30 minutes
                    false,      // cookie is not persistent
                    emp.Role                             // role assignment is stored
                                                      // in userData
                    );
            HttpCookie cookie1 = new HttpCookie(
              FormsAuthentication.FormsCookieName,
              FormsAuthentication.Encrypt(ticket1));
            Response.Cookies.Add(cookie1);

            NavigateMain();

        }
        else
        {
            Label4.Text = "Invalid User";
        }
    }

    protected void NavigateMain()
    {
       
        Employee e = (Employee)Session["emp"];
        string role = e.Role;

        if (role == "Store Clerk")
        {
            Response.Redirect("~/RequisitionListClerk.aspx");
        }
        else if (role == "Store Supervisor" || role == "Store Manager")
        {
            Response.Redirect("~/PurchaseOrderList.aspx");
        }
        else if (role == "DepartmentHead")
        {        
            Response.Redirect("~/DepartmentHead/RequisitionListDepartment.aspx");
        }
        else if(role == "DepartmentTempHead")
        {
            Response.Redirect("~/DepartmentTempHead/RequisitionListDepartment.aspx");
        }
        else if (role == "Employee")
        {
            Response.Redirect("~/DepartmentEmployee/RequisitionForm.aspx");
        }
        else if (role == "Representative")
        {
            Response.Redirect("~/DepartmentRepresentative/RequisitionForm.aspx");
        }
    }

}