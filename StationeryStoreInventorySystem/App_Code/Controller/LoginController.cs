using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for LoginController
/// </summary>
/// 

//AUTHOR : YIMON SOE
public class LoginController
{
    public LoginController()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static bool verifyLogin(string email, string password)
    {
        return EFBroker_Employee.verifyLogin(email, password);
    }


    public static Employee GetEmployeeByEmail(string email)
    {
        return EFBroker_Employee.GetEmployeeByEmail(email);

    }
    public static Employee login(string email, string password)
    {

        bool isValid = EFBroker_Employee.verifyLogin(email, password);

        if (isValid)
        {
            Employee emp = EFBroker_Employee.GetEmployeeByEmail(email);
            //Check is temp head or not 
            if (Utility.checkIsTempDepHead(emp) == true)
            {
                //set role for temp head
                emp.Role = "DepartmentTempHead";
            }
            return emp;
        }
        return null;
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

    //AUTHOR : TAN WEN SONG
    public static readonly string GenerateDiscrepancyV2URI = "~/Store/GenerateDiscrepancyV2.aspx";
    public static readonly string GenerateDiscrepancyAdhocV2URI="~/Store/GenerateDiscrepancyAdhocV2.aspx";
    public static readonly string LoginURI = "~/Login.aspx";
    public static readonly string CollectionPointUpdateURI = "~/CollectionPointUpdate.aspx";
    public static readonly string DepartmentListDHeadURI = "~/DepartmentHead/DepartmentListDHead.aspx";
    public static readonly string DepartmentListDRepURI= "~/DepartmentRepresentative/DepartmentListDRep.aspx";
    public static readonly string DepartmentListActingDHeadURI ="~/DepartmentTempHead/DepartmentListActingDHead.aspx";
    public static readonly string RequisitionListDepEmpURI = "~/DepartmentEmployee/RequisitionListDepEmp.aspx";
    public static readonly string RequisitionListDepHeadURI = "~/DepartmentHead/RequisitionListDepHead.aspx";
    public static readonly string RequisitionListTempDepHeadURI = "~/DepartmentTempHead/RequisitionListDepTempHead.aspx";
    public static readonly string RequisitionListDepRepURI = "~/DepartmentRepresentative/RequisitionListDepRep.aspx";
    public static readonly string RequisitionListClerkURI = "~/Store/RequisitionListClerk.aspx";
    public static readonly string RequisitionDetailURI = "RequisitionDetails.aspx";
    public static readonly string RetrievalListDetailURI = "~/Store/RetrievalListDetail.aspx";
    public static readonly string RetrievalShortfallURI = "~/RetrievalShortfall.aspx";
    public static readonly string PurchaseOrderFormURI = "~/StoreClerk/PurchaseOrderForm.aspx";
    public static readonly string PurchaseOrderListURI = "~/Store/PurchaseOrderList.aspx";
    public static readonly string PurchaseOrderDetailURI= "~/Store/PurchaseOrderDetail.aspx";
    public static readonly string EmployeeRequisitionFormURI = "~/DepartmentEmployee/RequisitionForm.aspx";
    public static readonly string RepresentativeRequisitionFormURI = "~/DepartmentRepresentative/RequisitionForm.aspx";
    public static readonly string DepartmentDetailInfoURI = "~/Department/DepartmentDetailInfo.aspx";
    public static readonly string RequistionListDepartmentURI = "RequisitionListDepartment.aspx";
    public static readonly string DisbursementListURI = "~/Store/DisbursementList.aspx";
    public static readonly string DisbursementListDetailURI = "~/Store/DisbursementListDetail.aspx";
    public static readonly string RegenerateRequestURI = "~/Store/RegenerateRequest.aspx";
    public static readonly string ItemStockCardListURI = "~/Store/ItemStockCardList.aspx";
    public static readonly string SupplierCreateURI = "~/StoreManagerSupervisor/SupplierCreate.aspx";
    public static readonly string SupplierListURI = "~/Store/SupplierList.aspx";
    public static readonly string TrendReportDisplayURI = "~/Store/TrendReportDisplay.aspx";
    public static readonly string StockAdjustmentSummaryURI = "~/StoreManagerSupervisor/StockAdjustmentSummary.aspx";
    public static readonly string StationeryCatalogueDetailURI = "~/Store/StationeryCatalogueDetail.aspx";
    public static readonly string StationeryCatalogueURI = "~/Store/StationeryCatalogue.aspx";
    public static readonly string StationeryCatalogueEmpURI = "~/Department/StationeryCatalogueEmp.aspx";
    public static readonly string ItemStockCardURI= "~/Store/ItemStockCard.aspx";
    public static readonly string ErrorPageURI = "~/ErrorPage.aspx";
    public static readonly string StockAdjustmentURI = "~/StoreManagerSupervisor/StockAdjustment.aspx";
    public static readonly string AddItemDiscrepancyURI = "~/Store/AddItemDiscrepancy.aspx";
}