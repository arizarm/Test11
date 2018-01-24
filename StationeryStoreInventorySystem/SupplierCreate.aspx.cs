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
        Supplier s = new Supplier();
        s.SupplierCode = TextBox1.Text;
        s.SupplierName = TextBox2.Text;
        s.SupplierContactName = TextBox3.Text;
        s.SupplierPhone = TextBox4.Text;
        s.SupplierFax = TextBox5.Text;
        s.SupplierAddress = TextBox6.Text;
        s.SupplierEmail = TextBox7.Text;
        s.ActiveStatus = TextBox8.Text;

        SupplierListController slc = new SupplierListController();
        slc.createSupplier(s);
        Page.ClientScript.RegisterStartupScript(Page.GetType(), "my", "alert('" + Message.SupplierSuccessfulAdd + "');", true);
        //Response.Write("<script>alert('" + Message.SupplierSuccessfulAdd + "');</script>");

        Response.Redirect("SupplierList.aspx", false);
    }
}