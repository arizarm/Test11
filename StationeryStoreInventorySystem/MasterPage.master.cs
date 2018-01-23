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
        if(Session["empRole"] != null )
        {
            if (Session["empRole"].ToString() == "DepartmentHead")
            {
                DepHeadMenu.Visible = true;
                DepTempHeadMenu.Visible = false;
                DepRepMenu.Visible = false;
                DepMember.Visible = false;
                StoreManMenu.Visible = false;
                StoreSuperMenu.Visible = false;
                StoreClerkMenu.Visible = false;
            }

            if (Session["empRole"].ToString() == "DepartmentTempHead")
            {
                DepHeadMenu.Visible = false;
                DepTempHeadMenu.Visible = true;
                DepRepMenu.Visible = false;
                DepMember.Visible = false;
                StoreManMenu.Visible = false;
                StoreSuperMenu.Visible = false;
                StoreClerkMenu.Visible = false;
            }

            if (Session["empRole"].ToString() == "Representative")
            {
                DepHeadMenu.Visible = false;
                DepTempHeadMenu.Visible = false;
                DepRepMenu.Visible = true;
                DepMember.Visible = false;
                StoreManMenu.Visible = false;
                StoreSuperMenu.Visible = false;
                StoreClerkMenu.Visible = false;
            }

            if (Session["empRole"].ToString() == "Employee")
            {
                DepHeadMenu.Visible = false;
                DepTempHeadMenu.Visible = false;
                DepRepMenu.Visible = false;
                DepMember.Visible = true;
                StoreManMenu.Visible = false;
                StoreSuperMenu.Visible = false;
                StoreClerkMenu.Visible = false;
            }

            if (Session["empRole"].ToString() == "Store Manager")
            {
                DepHeadMenu.Visible = false;
                DepTempHeadMenu.Visible = false;
                DepRepMenu.Visible = false;
                DepMember.Visible = false;
                StoreManMenu.Visible = true;
                StoreSuperMenu.Visible = false;
                StoreClerkMenu.Visible = false;
            }

            if (Session["empRole"].ToString() == "Store Supervisor")
            {
                DepHeadMenu.Visible = false;
                DepTempHeadMenu.Visible = false;
                DepRepMenu.Visible = false;
                DepMember.Visible = false;
                StoreManMenu.Visible = false;
                StoreSuperMenu.Visible = true;
                StoreClerkMenu.Visible = false;
            }

            if (Session["empRole"].ToString() == "Store Clerk")
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
