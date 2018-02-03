using System;
using System.Data.SqlTypes;
using System.Windows.Forms;
using System.Transactions;
using System.Collections;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;

public partial class SupplierPriceList : System.Web.UI.Page
{
    string code;
    SupplierListController slc = new SupplierListController();
    MaintainPriceListController mplc = new MaintainPriceListController();
    ArrayList tenderSupplyList;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //get string parameter from URL
            code = Request.QueryString["SupplierCode"];
            tenderSupplyList = new ArrayList();

            if (!IsPostBack)
            {
                if ((string)Session["empRole"] == "Store Supervisor" || (string)Session["empRole"] == "Store Manager")
                {
                    BtnUpdate.Enabled = true;
                    BtnUpdate.Visible = true;
                }
                //Set Default Supplier Info on Page
                Supplier s = slc.GetSupplierGivenSupplierCode(code);
                TxtSupCode.Text = s.SupplierCode;
                TxtSupName.Text = s.SupplierName;
                TxtContactName.Text = s.SupplierContactName;
                TxtSupplierPhoneNo.Text = s.SupplierPhone;
                TxtFaxNo.Text = s.SupplierFax;
                TxtAddress.Text = s.SupplierAddress;
                TxtEmail.Text = s.SupplierEmail;
                TxtActive.Text = s.ActiveStatus;
                if (s.ActiveStatus == "Y")
                {
                    BtnDelete.CssClass = "rejectBtn";
                    BtnDelete.Text = "Set To Inactive";
                }
                else
                {
                    BtnDelete.CssClass = "button";
                    BtnDelete.Text = "Set To Active";
                }

                //Populate dropdownlists for Item and Category
                DefaultDropDownListRestore();
                //Populate dropdownlist for TenderSupply
                PopulateTenderSupplyList();
            }
        }
        catch (InvalidOperationException)
        {
            Utility.AlertMessageThenRedirect(Message.PageInvalidEntry, "SupplierList.aspx");
        }

    }

    protected void BtnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            Supplier s = new Supplier();
            s.SupplierCode = TxtSupCode.Text;
            s.SupplierName = TxtSupName.Text;
            s.SupplierContactName = TxtContactName.Text;
            s.SupplierPhone = TxtSupplierPhoneNo.Text;
            s.SupplierFax = TxtFaxNo.Text;
            s.SupplierAddress = TxtAddress.Text;
            s.SupplierEmail = TxtEmail.Text;
            s.ActiveStatus = TxtActive.Text;
            slc.UpdateSupplier(s);
            Utility.DisplayAlertMessage(Message.UpdateSuccessful);
        }
        catch (Exception)
        {
            Utility.DisplayAlertMessage(Message.GeneralError);
        }
    }

    protected void BtnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            Supplier s = slc.GetSupplierGivenSupplierCode(code);

            if (s.ActiveStatus == "Y")
            {
                slc.DeleteSupplier(code);
                Utility.AlertMessageThenRedirect(Message.InactiveSuccessful, "/Store/SupplierList.aspx");
            }
            else
            {
                slc.MakeSupplierActive(code);
                BtnDelete.CssClass = "rejectBtn";
                BtnDelete.Text = "Set To Inactive";
                Utility.DisplayAlertMessage(Message.ActiveSuccessful);
                TxtActive.Text = "Y";
            }
        }
        catch (Exception)
        {
            Utility.DisplayAlertMessage(Message.GeneralError);
        }
    }

    protected void BtnAddNewItem_Click(object sender, EventArgs e)
    {
        string Year = DateTime.Now.Year.ToString();
        TxtTenderYear.Text = Year;
        TblNewItem.Visible = true;
        BtnAddNewItem.Visible = false;
    }

    protected void BtnAddItem_Click(object sender, EventArgs e)
    {
        try
        {
            PriceList pl = new PriceList();
            pl.SupplierCode = code;
            pl.ItemCode = DDLItem.SelectedValue;
            if (!ValidatorUtil.isEmpty(TxtAddNewItem.Text.Trim()))
                pl.Price = decimal.Parse(TxtAddNewItem.Text.Trim());
            if (!(DDLPriorityRank.SelectedValue == "NA"))
                pl.SupplierRank = int.Parse(DDLPriorityRank.SelectedValue);
            if (!ValidatorUtil.isEmpty(TxtTenderYear.Text.Trim()))
                pl.TenderYear = (TxtTenderYear.Text.Trim());

            PriceList verifiPL = mplc.GetPriceListGivenItemCodeRankNTenderYear(pl.ItemCode, int.Parse(DDLPriorityRank.SelectedValue), pl.TenderYear);
            if (verifiPL == null)
            {
                mplc.AddPriceListItem(pl);
                DefaultDropDownListRestore();
                PopulateTenderSupplyList();
                Utility.DisplayAlertMessage(Message.SuccessfulItemAdd);
            }
            else
                Utility.DisplayAlertMessage(Message.PriceListExistsForGivenTenderYear);
        }
        catch (InvalidOperationException)
        {
            Utility.DisplayAlertMessage(Message.InvalidEntry);
        }
        catch (DbUpdateException)
        {
            Utility.DisplayAlertMessage(Message.ValidationError);
        }
        catch (DbEntityValidationException)
        {
            Utility.DisplayAlertMessage(Message.ValidationError);
        }
        catch (Exception)
        {
            Utility.DisplayAlertMessage(Message.GeneralError);
        }
    }

    protected void DDLCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        if ((DDLCategory.SelectedIndex != 0))
        {
            List<Item> ShortlistedItems = mplc.GetAllItemsForGivenCat(DDLCategory.SelectedItem.Value.ToString());
            List<TenderListObj> tenderItemList = new List<TenderListObj>();
            foreach (Item i in ShortlistedItems)
            {
                TenderListObj a = new TenderListObj(i);
                tenderItemList.Add(a);
            }
            DDLItem.DataSource = tenderItemList;
            DDLItem.DataBind();
        }
        else
        {
            DefaultDropDownListRestore();
        }
    }

    protected void DDLItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DDLItem.SelectedIndex != 0)
        {
            string catName = mplc.GetCatForGivenItemCode(DDLItem.SelectedValue);
            DDLCategory.SelectedValue = catName;
        }
        else
        {
            DefaultDropDownListRestore();
        }
    }

    protected void DefaultDropDownListRestore()
    {
        List<Item> itemList = mplc.GetActiveItemList();
        List<TenderListObj> tenderItemList = new List<TenderListObj>();
        foreach (Item i in itemList)
        {
            TenderListObj a = new TenderListObj(i);
            tenderItemList.Add(a);
        }
        DDLItem.DataSource = tenderItemList;
        DDLItem.DataBind();
        DDLItem.Items.Insert(0, new ListItem("Select", "NA"));

        List<string> catList = mplc.GetAllCategoryNames();
        DDLCategory.DataSource = catList;
        DDLCategory.DataBind();
        DDLCategory.Items.Insert(0, new ListItem("Select", "NA"));

        List<int> rank = new List<int>() { 1, 2, 3 };
        DDLPriorityRank.DataSource = rank;
        DDLPriorityRank.DataBind();

        TxtAddNewItem.Text = "";
        string Year = DateTime.Now.Year.ToString();
        TxtTenderYear.Text = Year;
    }

    protected void PopulateTenderSupplyList()
    {
        List<PriceList> lpl = mplc.GetCurrentYearSupplierPriceList(code);
        //populate tender supply list
        for (int i = 0; i < lpl.Count; i++)
        {
            Item itemDesc = mplc.GetItemForGivenItemCode(lpl[i].ItemCode);
            string itemPrice = "$" + lpl[i].Price + "/" + mplc.GetUnitOfMeasureForGivenItemCode(lpl[i].ItemCode) + "  ";
            decimal? itemPriceOnly = lpl[i].Price;
            TenderListObj A = new TenderListObj(itemDesc, itemPrice, itemPriceOnly);
            tenderSupplyList.Add(A);
        }
        DDLTenderPrice.DataSource = tenderSupplyList;
        DDLTenderPrice.DataBind();
    }

    protected void BtnItemDelete_Click(object sender, EventArgs e)
    {
        try
        {
            //get description of item for this supplier
            GridViewRow Row = ((System.Web.UI.WebControls.Button)sender).Parent.Parent as GridViewRow;
            string itemP = DDLTenderPrice.DataKeys[Row.RowIndex].Value.ToString();

            //get the pricelist composite primary key
            PriceList pl = mplc.GetPriceListObjForGivenItemCodeNSupplier(itemP, code);
            string itemCode = pl.ItemCode;
            string tenderY = pl.TenderYear;
            //delete by giving the cpk
            mplc.RemovePriceListObject(code, itemCode, tenderY);
            //repopulate tender list
            PopulateTenderSupplyList();

            Utility.DisplayAlertMessage(Message.DeleteSuccessful);
        }
        catch (Exception)
        {
            Utility.DisplayAlertMessage(Message.GeneralError);
        }
    }

    protected void DDLTenderPrice_RowEditing(object sender, GridViewEditEventArgs e)
    {
        DDLTenderPrice.EditIndex = e.NewEditIndex;
        PopulateTenderSupplyList();
    }

    protected void DDLTenderPrice_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        DDLTenderPrice.EditIndex = -1;
        PopulateTenderSupplyList();
    }

    protected void DDLTenderPrice_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        //get new price
        System.Web.UI.WebControls.TextBox newPriceTB = (System.Web.UI.WebControls.TextBox)DDLTenderPrice.Rows[e.RowIndex].FindControl("TxtNewPrice");
        if (newPriceTB.Text.Trim() == "")
        {
            DDLTenderPrice.EditIndex = -1;
            PopulateTenderSupplyList();
        }
        else
        {
            string newPrice = newPriceTB.Text;

            //get itemCode for this supplier
            GridViewRow Row = DDLTenderPrice.Rows[e.RowIndex];
            string itemP = DDLTenderPrice.DataKeys[Row.RowIndex].Value.ToString();

            //get the pricelist composite primary key
            PriceList pl = mplc.GetPriceListObjForGivenItemCodeNSupplier(itemP, code);
            string itemCode = pl.ItemCode;
            string tenderY = pl.TenderYear;

            mplc.UpdatePrice(newPrice, code, itemCode, tenderY);

            DDLTenderPrice.EditIndex = -1;
            PopulateTenderSupplyList();
        }
    }


    protected void TxtSupplierPhoneNo_TextChanged(object sender, EventArgs e)
    {
        RegPhoneNo.Enabled = true;
    }

    protected void DDLTenderPrice_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        DDLTenderPrice.PageIndex = e.NewPageIndex;
        PopulateTenderSupplyList();
    }
}