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
        if (Session["empRole"].ToString() == "DepartementHead")
        {
            DepHeadMenu.Visible = true;
            DepRepMenu.Visible = false;
            DepMember.Visible = false;
            StoreManMenu.Visible = false;
            StoreSuperMenu.Visible = false;
            StoreClerkMenu.Visible = false;
        }

       if (Session["empRole"].ToString() == "Representative")
       {
            DepHeadMenu.Visible = false;
            DepRepMenu.Visible = true;
            DepMember.Visible = false;
            StoreManMenu.Visible = false;
            StoreSuperMenu.Visible = false;
            StoreClerkMenu.Visible = false;
        }

       if (Session["empRole"].ToString() == "Employee")
       {
            DepHeadMenu.Visible = false;
            DepRepMenu.Visible = false;
            DepMember.Visible = true;
            StoreManMenu.Visible = false;
            StoreSuperMenu.Visible = false;
            StoreClerkMenu.Visible = false;
        }

        if (Session["empRole"].ToString() == "Store Manager")
        {
            DepHeadMenu.Visible = false;
            DepRepMenu.Visible = false;
            DepMember.Visible = false;
            StoreManMenu.Visible = true;
            StoreSuperMenu.Visible = false;
            StoreClerkMenu.Visible = false;
        }

        if (Session["empRole"].ToString() == "Store Supervisor")
         {
            DepHeadMenu.Visible = false;
            DepRepMenu.Visible = false;
            DepMember.Visible = false;
            StoreManMenu.Visible = false;
            StoreSuperMenu.Visible = true;
            StoreClerkMenu.Visible = false;
         }

         if (Session["empRole"].ToString() == "Store Clerk")
          {
             DepHeadMenu.Visible = false;
             DepRepMenu.Visible = false;
             DepMember.Visible = false;
             StoreManMenu.Visible = false;
             StoreSuperMenu.Visible = false;
             StoreClerkMenu.Visible = true;
           }

            //if (Session["userType"].ToString() == "Employee")
            //{
            //    this.lblRetrieval.Visible = false;
            //    this.hLinkRetrievalForm.Visible = false;

            //    this.lblDisbursement.Visible = false;
            //    this.hLinkDisbursementList.Visible = false;

            //    this.hLinkDiscrepency.Visible = false;
            //    this.hLinkAdjustment.Visible = false;
            //    this.hLinkStockCard.Visible = false;

            //    this.lblReport.Visible = false;
            //    this.hLinkReorderReport.Visible = false;
            //    this.hLinkReorderTrend.Visible = false;
            //    this.hLinkRequisitionTrend.Visible = false;
            //    this.hLinkInventoryStatus.Visible = false;

            //    this.lblPruchaseOrder.Visible = false;
            //    this.hLinkPurchaseOrder.Visible = false;
            //    this.hLinkPurchaseOrderList.Visible = false;
            //    this.hLinkPurchaseOrderListClerk.Visible = false;

            //    this.lblSupplier.Visible = false;
            //    this.hLinkSupplierList.Visible = false;
            //    this.hLinkSupplierListClerk.Visible = false;
            //}

            //if (Session["userType"].ToString() == "Head")
            //{

            //}
            //if (Session["userType"].ToString() == "Supervisor")
            //{
            //    this.hLinkRequisitionForm.Visible = false;
            //    this.hLinkDepartmentDetail.Visible = false;
            //    this.hLinkSupplierListClerk.Visible = false;
            //    this.hLinkPurchaseOrderListClerk.Visible = false;
            //}
            //if (Session["userType"].ToString() == "Clerk")
            //{
            //    this.hLinkAdjustment.Visible = false;
            //    this.hLinkRequisitionForm.Visible = false;
            //    this.hLinkDepartmentDetail.Visible = false;
            //    this.hLinkSupplierList.Visible = false;
            //    this.hLinkPurchaseOrderList.Visible = false;
            //}
    }
    
    protected void btnSignOut_Click(object sender, EventArgs e)
    {
        FormsAuthentication.SignOut();
        Response.Redirect("~/Login.aspx");
    }
}
