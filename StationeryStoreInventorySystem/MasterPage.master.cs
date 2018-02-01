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
                DepMenu.Visible = true;
                hLinkRequisitionListDepHead.Visible = true;
            }

            if (emp.Role == "DepartmentTempHead")
            {
                DepMenu.Visible = true;
                hLinkRequisitionListDepTempHead.Visible = true;
            }

            if (emp.Role == "Representative")
            {
                DepMenu.Visible = true;
                hLinkCollectionListDepRep.Visible = true;
                hLinkRequisitionListDepRep.Visible = true;
                hLinkGenReqDepRep.Visible = true;
            }

            if (emp.Role == "Employee")
            {
                DepMenu.Visible = true;
                hLinkRequisitionListDepEmp.Visible = true;
                hLinkGenReqEmp.Visible = true;
            }

            if (emp.Role == "Store Manager")
            {
                storeMenu.Visible = true;
                hLinkCreateSupplier.Visible = true;
                hLinkStockAdjustment.Visible = true;
            }

            if (emp.Role == "Store Supervisor")
            {
                storeMenu.Visible = true;
                hLinkCreateSupplier.Visible = true;
                hLinkStockAdjustment.Visible = true;
            }

            if (emp.Role == "Store Clerk")
            {
                storeMenu.Visible = true;
                hLinkGenPurchaseOrder.Visible = true;
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
