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
        Employee emp = LoginController.login(email, password);
        if ( emp != null)
        {
            HttpContext.Current.Session["empRole"] = emp.Role;
            HttpContext.Current.Session["empID"] = emp.EmpID;
            HttpContext.Current.Session["emp"] = emp;
           
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
            HttpContext.Current.Response.Cookies.Add(cookie1);
            LoginController.NavigateMain();
        }
        else
        {
            Label4.Text = "Invalid User";
        }
    }

   

}