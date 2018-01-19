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
        if (!this.IsPostBack)
        {
            if (this.Page.User.Identity.IsAuthenticated)
            {
                FormsAuthentication.SignOut();
                Response.Redirect("~/Login.aspx");
            }
        }
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
            Session["isTempHead"] = emp.IsTempHead;

            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, emp.Email, DateTime.Now, DateTime.Now.AddMinutes(2880), Persist.Checked, emp.Role.ToString(), FormsAuthentication.FormsCookiePath);
            string hash = FormsAuthentication.Encrypt(ticket);
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hash);

            if (ticket.IsPersistent)
            {
                cookie.Expires = ticket.Expiration;
            }
            Response.Cookies.Add(cookie);


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

        if (role == "Store Clerk")
        {
            Response.Redirect("~/Store/RequisitionListClerk.aspx");
        }
        else if (role == "Store Supervisor" || role == "Store Manager")
        {
            Response.Redirect("~/PurchaseOrderList.aspx");
        }
        else if (role == "DepartmentHead")
        {
            Response.Redirect("~/Department/RequisitionListDepartment.aspx");
        }
        else if (role == "Employee")
        {
            Response.Redirect("~/Department/RequisitionForm..aspx");
        }
        else if (role == "Representative")
        {
            Response.Redirect("~/Department/RequisitionForm..aspx");
        }
    }
}