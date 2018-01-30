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
            HttpContext.Current.Response.Redirect(RequisitionListClerkURI);
        }
        else if (role == "Store Supervisor" || role == "Store Manager")
        {
            HttpContext.Current.Response.Redirect(PurchaseOrderListURI);
        }
        else if (role == "DepartmentHead")
        {
            HttpContext.Current.Response.Redirect(RequisitionListDepHeadURI);
        }
        else if (role == "DepartmentTempHead")
        {
            HttpContext.Current.Response.Redirect(RequisitionListTempDepHeadURI);
        }
        else if (role == "Employee")
        {
            HttpContext.Current.Response.Redirect(EmployeeRequisitionFormURI);
        }
        else if (role == "Representative")
        {
            HttpContext.Current.Response.Redirect(RepresentativeRequisitionFormURI);
        }
    }
    public static readonly string GenerateDiscrepancyV2URI = "~/Store/GenerateDiscrepancyV2.aspx";
    public static readonly string LoginURI = "~/Login.aspx";
    public static readonly string DepartmentListDHeadURI = "~/DepartmentHead/DepartmentListDHead.aspx";
    public static readonly string DepartmentListDRepURI= "~/DepartmentRepresentative/DepartmentListDRep.aspx";
    public static readonly string DepartmentListActingDHeadURI ="~/DepartmentTempHead/DepartmentListActingDHead.aspx";
    //public static readonly string RequisitionListDepEmpURI = "RequisitionListDepEmp.aspx";
    public static readonly string RequisitionListDepEmpURI = "~/DepartmentEmployee/RequisitionListDepEmp.aspx";
    public static readonly string RequisitionListDepHeadURI = "~/DepartmentHead/RequisitionListDepHead.aspx";
    public static readonly string RequisitionListTempDepHeadURI = "~/DepartmentTempHead/RequisitionListDepTempHead.aspx";
    public static readonly string RequisitionListTempDepRedURI = "~/DepartmentRepresentative/RequisitionListDepRep.aspx";
    public static readonly string RequisitionListClerkURI = "~/Store/RequisitionListClerk.aspx";
    public static readonly string PurchaseOrderListURI = "~/Store/PurchaseOrderList.aspx";
    public static readonly string EmployeeRequisitionFormURI = "~/DepartmentEmployee/RequisitionForm.aspx";
    public static readonly string RepresentativeRequisitionFormURI = "~/DepartmentRepresentative/RequisitionForm.aspx";
    public static readonly string DepartmentDetailInfoURI = "~/Department/DepartmentDetailInfo.aspx";
    public static readonly string RequistionListDepartmentURI = "RequisitionListDepartment.aspx";
    public static readonly string DisbursementListURI = "~/Store/DisbursementList.aspx";
    public static readonly string DisbursementListDetailURI = "~/Store/DisbursementListDetail.aspx";
    public static readonly string RegenerateRequestURI = "~/Store/RegenerateRequest.aspx";
    public static readonly string ErrorPageURI = "~/ErrorPage.aspx";
}