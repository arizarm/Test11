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

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            Supplier S = new Supplier();
            S.SupplierCode = TxtSupCode.Text;
            S.SupplierName = TxtSupName.Text;
            S.SupplierContactName = TxtContactName.Text;
            S.SupplierPhone = TxtPhoneNo.Text;
            S.SupplierFax = TxtFaxNo.Text;
            S.SupplierAddress = TxtAddress.Text;
            S.SupplierEmail = TxtEmail.Text;
            S.ActiveStatus = TxtActive.Text;

            SupplierListController slc = new SupplierListController();
            slc.CreateSupplier(S);
            Utility.AlertMessageThenRedirect(Message.SupplierSuccessfulAdd, "/Store/SupplierList.aspx");
        }
        catch (Exception)
        {
            Utility.DisplayAlertMessage(Message.GeneralError);
        }
    }
}