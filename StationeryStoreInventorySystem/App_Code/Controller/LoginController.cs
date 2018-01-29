using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for LoginController
/// </summary>
public class LoginController
{
    public LoginController()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static void NavigateMain()
    {

        Employee e = (Employee)HttpContext.Current.Session["emp"];
        string role = e.Role;

        if (role == "Store Clerk")
        {
            HttpContext.Current.Response.Redirect("~/RequisitionListClerk.aspx");
        }
        else if (role == "Store Supervisor" || role == "Store Manager")
        {
            HttpContext.Current.Response.Redirect("~/Store/PurchaseOrderList.aspx");
        }
        else if (role == "DepartmentHead")
        {
            HttpContext.Current.Response.Redirect("~/DepartmentHead/RequisitionListDepHead.aspx");
        }
        else if (role == "DepartmentTempHead")
        {
            HttpContext.Current.Response.Redirect("~/DepartmentTempHead/RequisitionListDepTempHead.aspx");
        }
        else if (role == "Employee")
        {
            HttpContext.Current.Response.Redirect("~/DepartmentEmployee/RequisitionForm.aspx");
        }
        else if (role == "Representative")
        {
            HttpContext.Current.Response.Redirect("~/DepartmentRepresentative/RequisitionForm.aspx");
        }
    }
    public static readonly string GenerateDiscrepancyV2URI = "~/Store/GenerateDiscrepancyV2.aspx";
}