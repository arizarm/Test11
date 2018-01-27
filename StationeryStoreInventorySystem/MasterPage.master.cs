using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["emp"] != null)
        {
            Employee emp = (Employee)Session["emp"];
            LoginUserName.Text = emp.EmpName + " | " + emp.Role;
            if (emp.Role == "DepartmentHead")
            {
                DepHeadMenu.Visible = true;
                DepTempHeadMenu.Visible = false;
                DepRepMenu.Visible = false;
                DepMember.Visible = false;
                StoreManMenu.Visible = false;
                StoreSuperMenu.Visible = false;
                StoreClerkMenu.Visible = false;
            }

            if (emp.Role == "DepartmentTempHead")
            {
                DepHeadMenu.Visible = false;
                DepTempHeadMenu.Visible = true;
                DepRepMenu.Visible = false;
                DepMember.Visible = false;
                StoreManMenu.Visible = false;
                StoreSuperMenu.Visible = false;
                StoreClerkMenu.Visible = false;
            }

            if (emp.Role == "Representative")
            {
                DepHeadMenu.Visible = false;
                DepTempHeadMenu.Visible = false;
                DepRepMenu.Visible = true;
                DepMember.Visible = false;
                StoreManMenu.Visible = false;
                StoreSuperMenu.Visible = false;
                StoreClerkMenu.Visible = false;
            }

            if (emp.Role == "Employee")
            {
                DepHeadMenu.Visible = false;
                DepTempHeadMenu.Visible = false;
                DepRepMenu.Visible = false;
                DepMember.Visible = true;
                StoreManMenu.Visible = false;
                StoreSuperMenu.Visible = false;
                StoreClerkMenu.Visible = false;
            }

            if (emp.Role == "Store Manager")
            {
                DepHeadMenu.Visible = false;
                DepTempHeadMenu.Visible = false;
                DepRepMenu.Visible = false;
                DepMember.Visible = false;
                StoreManMenu.Visible = true;
                StoreSuperMenu.Visible = false;
                StoreClerkMenu.Visible = false;
            }

            if (emp.Role == "Store Supervisor")
            {
                DepHeadMenu.Visible = false;
                DepTempHeadMenu.Visible = false;
                DepRepMenu.Visible = false;
                DepMember.Visible = false;
                StoreManMenu.Visible = false;
                StoreSuperMenu.Visible = true;
                StoreClerkMenu.Visible = false;
            }

            if (emp.Role == "Store Clerk")
            {
                DepHeadMenu.Visible = false;
                DepTempHeadMenu.Visible = false;
                DepRepMenu.Visible = false;
                DepMember.Visible = false;
                StoreManMenu.Visible = false;
                StoreSuperMenu.Visible = false;
                StoreClerkMenu.Visible = true;
            }
        }
        else
        {
            Utility.logout();
        }

    }
    
    protected void btnSignOut_Click(object sender, EventArgs e)
    {
        Utility.logout();
    }


}
