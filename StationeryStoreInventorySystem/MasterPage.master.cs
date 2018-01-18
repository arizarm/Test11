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

        if (Session["userType"].ToString() == "Representative")
        {

        }
        if (Session["userType"].ToString() == "Employee")
        {
            this.lblRetrieval.Visible = false;
            this.hLinkRetrievalForm.Visible = false;

            this.lblDisbursement.Visible = false;
            this.hLinkDisbursementList.Visible = false;

            this.hLinkDiscrepency.Visible = false;
            this.hLinkAdjustment.Visible = false;
            this.hLinkStockCard.Visible = false;

            this.lblReport.Visible = false;
            this.hLinkReorderReport.Visible = false;
            this.hLinkReorderTrend.Visible = false;
            this.hLinkRequisitionTrend.Visible = false;
            this.hLinkInventoryStatus.Visible = false;

            this.lblPruchaseOrder.Visible = false;
            this.hLinkPurchaseOrder.Visible = false;
            this.hLinkPurchaseOrderList.Visible = false;
            this.hLinkPurchaseOrderListClerk.Visible = false;

            this.lblSupplier.Visible = false;
            this.hLinkSupplierList.Visible = false;
            this.hLinkSupplierListClerk.Visible = false;
        }

        if (Session["userType"].ToString() == "Head")
        {

        }
        if (Session["userType"].ToString() == "Supervisor")
        {
            this.hLinkRequisitionForm.Visible = false;
            this.hLinkDepartmentDetail.Visible = false;
            this.hLinkSupplierListClerk.Visible = false;
            this.hLinkPurchaseOrderListClerk.Visible = false;
        }
        if (Session["userType"].ToString() == "Clerk")
        {
            this.hLinkAdjustment.Visible = false;
            this.hLinkRequisitionForm.Visible = false;
            this.hLinkDepartmentDetail.Visible = false;
            this.hLinkSupplierList.Visible = false;
            this.hLinkPurchaseOrderList.Visible = false;
        }
    }



    protected void btnSignOut_Click(object sender, EventArgs e)
    {
        FormsAuthentication.SignOut();
        Response.Redirect("~/Login.aspx");
    }
}
