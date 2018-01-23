using System;
using System.Transactions;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

public partial class SupplierPriceList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void SaveButton_Click(object sender, EventArgs e)
    {
        Supplier S = new Supplier();
        S.SupplierCode = TextBox1.Text;
        S.SupplierName = TextBox2.Text;
        S.SupplierContactName = TextBox3.Text;
        S.SupplierPhone = TextBox4.Text;
        S.SupplierFax = TextBox5.Text;
        S.SupplierAddress = TextBox6.Text;
        S.SupplierEmail = TextBox7.Text;
        S.ActiveStatus = TextBox8.Text;

        SupplierListController slc = new SupplierListController();
        slc.CreateSupplier(S);
        Page.ClientScript.RegisterStartupScript(Page.GetType(), "my", "alert('" + Message.SupplierSuccessfulAdd + "');", true);
        //Response.Write("<script>alert('" + Message.SupplierSuccessfulAdd + "');</script>");

        Response.Redirect("SupplierList.aspx", false);
    }
}