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
                    UpdateButton.Enabled = true;
                    UpdateButton.Visible = true;
                }
                //Set Default Supplier Info on Page
                Supplier s = slc.GetSupplierGivenSupplierCode(code);
                TextBox1.Text = s.SupplierCode;
                TextBox2.Text = s.SupplierName;
                TextBox3.Text = s.SupplierContactName;
                SupplierPhoneNoTextBox.Text = s.SupplierPhone;
                TextBox5.Text = s.SupplierFax;
                TextBox6.Text = s.SupplierAddress;
                TextBox8.Text = s.SupplierEmail;
                TextBox9.Text = s.ActiveStatus;


                //Populate dropdownlists for Item and Category
                DefaultDropDownListRestore();
                //Populate dropdownlist for TenderSupply
                PopulateTenderSupplyList();
            }
        }
        catch (InvalidOperationException)
        {
            Utility.DisplayAlertMessage(Message.PageInvalidEntry);
            Response.Redirect("SupplierList.aspx");
        }

    }

    protected void UpdateButton_Click(object sender, EventArgs e)
    {
        Supplier s = new Supplier();
        s.SupplierCode = TextBox1.Text;
        s.SupplierName = TextBox2.Text;
        s.SupplierContactName = TextBox3.Text;
        s.SupplierPhone = SupplierPhoneNoTextBox.Text;
        s.SupplierFax = TextBox5.Text;
        s.SupplierAddress = TextBox6.Text;
        s.SupplierEmail = TextBox8.Text;
        s.ActiveStatus = TextBox9.Text;
        slc.UpdateSupplier(s);
        Utility.DisplayAlertMessage(Message.UpdateSuccessful);
    }

    protected void DeleteButton_Click(object sender, EventArgs e)
    {
        slc.DeleteSupplier(code);
        Utility.AlertMessageThenRedirect(Message.InactiveSuccessful, "/Store/SupplierList.aspx");
    }

    protected void AddNewItemButton_Click(object sender, EventArgs e)
    {
        string Year = DateTime.Now.Year.ToString();
        TextBox11.Text = Year;
        Table3.Visible = true;
        AddNewItemButton.Visible = false;
    }

    protected void AddItemButton_Click(object sender, EventArgs e)
    {
        try
        {
            PriceList pl = new PriceList();
            pl.SupplierCode = code;
            pl.ItemCode = ItemDropDownList.SelectedValue;
            if (!ValidatorUtil.isEmpty(TextBox7.Text))
                pl.Price = decimal.Parse(TextBox7.Text);
            if (!(PriorityRankList.SelectedValue == "NA"))
                pl.SupplierRank = int.Parse(PriorityRankList.SelectedValue);
            if (!ValidatorUtil.isEmpty(TextBox11.Text))
                pl.TenderYear = (TextBox11.Text);

            mplc.AddPriceListItem(pl);
            DefaultDropDownListRestore();
            PopulateTenderSupplyList();
            Utility.DisplayAlertMessage(Message.SuccessfulItemAdd);
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

    }

    protected void CategoryDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if ((CategoryDropDownList.SelectedIndex != 0))
        {
            List<Item> ShortlistedItems = mplc.GetAllItemsForGivenCat(CategoryDropDownList.SelectedItem.Value.ToString());
            List<TenderListObj> tenderItemList = new List<TenderListObj>();
            foreach (Item i in ShortlistedItems)
            {
                TenderListObj a = new TenderListObj(i);
                tenderItemList.Add(a);
            }
            ItemDropDownList.DataSource = tenderItemList;
            ItemDropDownList.DataBind();
        }
        else
        {
            DefaultDropDownListRestore();
        }
    }

    protected void ItemDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ItemDropDownList.SelectedIndex != 0)
        {
            string catName = mplc.GetCatForGivenItemCode(ItemDropDownList.SelectedValue);
            CategoryDropDownList.SelectedValue = catName;
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
        ItemDropDownList.DataSource = tenderItemList;
        ItemDropDownList.DataBind();
        ItemDropDownList.Items.Insert(0, new ListItem("Select", "NA"));

        List<string> catList = mplc.GetAllCategoryNames();
        CategoryDropDownList.DataSource = catList;
        CategoryDropDownList.DataBind();
        CategoryDropDownList.Items.Insert(0, new ListItem("Select", "NA"));

        List<int> rank = new List<int>() { 1, 2, 3 };
        PriorityRankList.DataSource = rank;
        PriorityRankList.DataBind();

        TextBox7.Text = "";
        string Year = DateTime.Now.Year.ToString();
        TextBox11.Text = Year;
    }

    protected void PopulateTenderSupplyList()
    {
        List<PriceList> lpl = mplc.GetCurrentYearSupplierPriceList(code);
        //populate tender supply list
        for (int i = 0; i < lpl.Count; i++)
        {
            Item itemDesc = mplc.GetItemForGivenItemCode(lpl[i].ItemCode);
            string itemPrice = "$" + lpl[i].Price + "/" + mplc.GetUnitOfMeasureForGivenItemCode(lpl[i].ItemCode) + "  ";
            TenderListObj A = new TenderListObj(itemDesc, itemPrice);
            tenderSupplyList.Add(A);
        }
        TenderPriceDropDownList.DataSource = tenderSupplyList;
        TenderPriceDropDownList.DataBind();
    }

    protected void ItemDelete_Click(object sender, EventArgs e)
    {
        //get description of item for this supplier
        GridViewRow Row = ((System.Web.UI.WebControls.Button)sender).Parent.Parent as GridViewRow;
        string itemP = TenderPriceDropDownList.DataKeys[Row.RowIndex].Value.ToString();

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

    protected void TenderPriceDropDownList_RowEditing(object sender, GridViewEditEventArgs e)
    {
        TenderPriceDropDownList.EditIndex = e.NewEditIndex;
        PopulateTenderSupplyList();
    }

    protected void TenderPriceDropDownList_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        TenderPriceDropDownList.EditIndex = -1;
        PopulateTenderSupplyList();
    }

    protected void TenderPriceDropDownList_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        //get new price
        System.Web.UI.WebControls.TextBox newPriceTB = (System.Web.UI.WebControls.TextBox)TenderPriceDropDownList.Rows[e.RowIndex].FindControl("NewPriceTextBox");
        if (newPriceTB.Text.Trim() == "")
        {
            TenderPriceDropDownList.EditIndex = -1;
            PopulateTenderSupplyList();
        }
        else
        {
            string newPrice = newPriceTB.Text;

            //get itemCode for this supplier
            GridViewRow Row = TenderPriceDropDownList.Rows[e.RowIndex];
            string itemP = TenderPriceDropDownList.DataKeys[Row.RowIndex].Value.ToString();

            //get the pricelist composite primary key
            PriceList pl = mplc.GetPriceListObjForGivenItemCodeNSupplier(itemP, code);
            string itemCode = pl.ItemCode;
            string tenderY = pl.TenderYear;

            mplc.UpdatePrice(newPrice, code, itemCode, tenderY);

            TenderPriceDropDownList.EditIndex = -1;
            PopulateTenderSupplyList();
        }
    }


    protected void SupplierPhoneNoTextBox_TextChanged(object sender, EventArgs e)
    {
        PhoneNoValidator.Enabled = true;
    }


    protected void NewPriceTextBox_TextChanged(object sender, EventArgs e)
    {
    }

    protected void TenderPriceDropDownList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        TenderPriceDropDownList.PageIndex = e.NewPageIndex;
        PopulateTenderSupplyList();
    }
}

public class TenderListObj
{
    string itemP, itemDesc, itemD;
    public TenderListObj(Item iD, string iP)
    {
        this.itemD = iD.ItemCode;
        this.itemP = iP;
        this.itemDesc = iD.Description;
    }

    public TenderListObj(Item iD)
    {
        this.itemD = iD.ItemCode;
        this.itemDesc = iD.Description;
    }

    public string ItemDescription
    {
        get { return itemDesc; }
        set { itemDesc = value; }
    }

    public string ItemPrice
    {
        get { return itemP; }
        set { itemP = value; }
    }

    public string ItemCode
    {
        get { return itemD; }
        set { itemD = value; }
    }
}