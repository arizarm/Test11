using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SupplierList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            SupplierListController slc = new SupplierListController();
            List<Supplier> LS = slc.listAllSuppliers();

            GridView1.DataSource = LS;
            GridView1.DataBind();

            if ((string)Session["userType"] == "Store Supervisor" || (string)Session["userType"] == "Store Manager")
            {
                Label2.Enabled = true;
            }
        }
    }
}