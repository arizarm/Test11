using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//AUTHOR : ARIZ ARMAND BIN ABDUL RAHMAN
public partial class SupplierList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            SupplierListController slc = new SupplierListController();
            List<Supplier> LS = slc.ListAllSuppliers();

            GVSupplierList.DataSource = LS;
            GVSupplierList.DataBind();

            if ((string)Session["empRole"] == "Store Supervisor" || (string)Session["empRole"] == "Store Manager")
            {
                AddSupplierButton.Enabled = true;
                AddSupplierButton.Visible = true;
            }
        }
    }

    protected void AddSupplierButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/StoreManagerSupervisor/SupplierCreate.aspx");
    }
}