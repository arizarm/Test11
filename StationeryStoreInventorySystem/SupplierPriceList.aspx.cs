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

public partial class SupplierPriceList : System.Web.UI.Page
{
    string Code;
    SupplierListController Slc = new SupplierListController();
    MaintainPriceListController Mplc = new MaintainPriceListController();
    ArrayList TenderSupplyList;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //get string parameter from URL
            Code = Request.QueryString["SupplierCode"];
            TenderSupplyList = new ArrayList();

            if (!IsPostBack)
            {
                if ((string)Session["empRole"] != "Store Supervisor" || (string)Session["empRole"] != "Store Manager")
                {
                    UpdateButton.Enabled = false;
                    UpdateButton.Visible = false;
                }
                //Set Default Supplier Info on Page
                Supplier S = Slc.GetSupplier(Code);
                TextBox1.Text = S.SupplierCode;
                TextBox2.Text = S.SupplierName;
                TextBox3.Text = S.SupplierContactName;
                SupplierPhoneNoTextBox.Text = S.SupplierPhone;
                TextBox5.Text = S.SupplierFax;
                TextBox6.Text = S.SupplierAddress;
                TextBox8.Text = S.SupplierEmail;
                TextBox9.Text = S.ActiveStatus;


                //Populate dropdownlists for Item and Category
                DefaultDropDownListRestore();
                //Populate dropdownlist for TenderSupply
                PopulateTenderSupplyList();
            }
        }
        catch (InvalidOperationException)
        {
            Response.Write("<script>alert('" + Message.PageInvalidEntry + "');</script>");
            Response.Redirect("SupplierList.aspx");
        }

    }

    protected void UpdateButton_Click(object sender, EventArgs e)
    {
        Supplier S = new Supplier();
        S.SupplierCode = TextBox1.Text;
        S.SupplierName = TextBox2.Text;
        S.SupplierContactName = TextBox3.Text;
        S.SupplierPhone = SupplierPhoneNoTextBox.Text;
        S.SupplierFax = TextBox5.Text;
        S.SupplierAddress = TextBox6.Text;
        S.SupplierEmail = TextBox8.Text;
        S.ActiveStatus = TextBox9.Text;
        Slc.UpdateSupplier(S);
        Response.Write("<script>alert('" + Message.UpdateSuccessful + "');</script>");
    }

    protected void DeleteButton_Click(object sender, EventArgs e)
    {
        Slc.DeleteSupplier(Code);

        Response.Write(@"
         <script>
        alert('" + Message.InactiveSuccessful + "'); window.location = '" + "SupplierList.aspx" + @"';    
            </script>");
    }

    protected void AddNewItemButton_Click(object sender, EventArgs e)
    {
        Table3.Visible = true;
        AddNewItemButton.Visible = false;
    }

    protected void AddItemButton_Click(object sender, EventArgs e)
    {
        try
        {
            PriceList Pl = new PriceList();
            Pl.SupplierCode = Code;
            Pl.ItemCode = Mplc.GetItemCodeForGivenItemName(ItemDropDownList.SelectedItem.Value);
            if (!ValidatorUtil.isEmpty(TextBox7.Text))
                Pl.Price = decimal.Parse(TextBox7.Text);
            if (!(PriorityRankList.SelectedValue == "NA"))
                Pl.SupplierRank = int.Parse(PriorityRankList.SelectedValue);
            if (!ValidatorUtil.isEmpty(TextBox11.Text))
                Pl.TenderYear = (TextBox11.Text);

            Mplc.AddPriceListItem(Pl);
            DefaultDropDownListRestore();
            PopulateTenderSupplyList();
            Response.Write("<script>alert('" + Message.SuccessfulItemAdd + "');</script>");
        }
        catch (DbUpdateException)
        {
            Response.Write("<script>alert('" + Message.OneItemPerSupplier + "');</script>");
        }
        catch (InvalidOperationException)
        {
            Response.Write("<script>alert('" + Message.InvalidEntry + "');</script>");
        }
    }

    protected void CategoryDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if ((CategoryDropDownList.SelectedIndex != 0))
        {
            List<string> ShortlistedItems = Mplc.GetAllItemNamesForGivenCat(CategoryDropDownList.SelectedItem.Value.ToString());
            ItemDropDownList.DataSource = ShortlistedItems;
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
            string CatName = Mplc.GetCatForGivenItem(ItemDropDownList.SelectedItem.Value.ToString());
            CategoryDropDownList.SelectedValue = CatName;
        }
        else
        {
            DefaultDropDownListRestore();
        }
    }

    protected void DefaultDropDownListRestore()
    {
        List<string> ItemList = Mplc.GetAllItemNames();
        ItemDropDownList.DataSource = ItemList;
        ItemDropDownList.DataBind();
        ItemDropDownList.Items.Insert(0, new ListItem("Select", "NA"));

        List<string> CatList = Mplc.GetAllCatNames();
        CategoryDropDownList.DataSource = CatList;
        CategoryDropDownList.DataBind();
        CategoryDropDownList.Items.Insert(0, new ListItem("Select", "NA"));

        List<int> Rank = new List<int>() { 1, 2, 3 };
        PriorityRankList.DataSource = Rank;
        PriorityRankList.DataBind();
        PriorityRankList.Items.Insert(0, new ListItem("Select", "NA"));

        TextBox7.Text = "";
        TextBox11.Text = "";
    }

    protected void PopulateTenderSupplyList()
    {
        List<PriceList> Lpl = Mplc.GetSupplierPriceList(Code);
        //populate tender supply list
        for (int i = 0; i < Lpl.Count; i++)
        {
            string ItemDesc = Mplc.GetItemNameForGivenItemCode(Lpl[i].ItemCode);
            string ItemPrice = "$" + Lpl[i].Price + "/" + Mplc.GetUnitOfMeasureForGivenItemCode(Lpl[i].ItemCode) + "  ";
            TenderListObj A = new TenderListObj(ItemDesc, ItemPrice);
            TenderSupplyList.Add(A);
        }
        TenderPriceDropDownList.DataSource = TenderSupplyList;
        TenderPriceDropDownList.DataBind();
    }

    protected void ItemDelete_Click(object sender, EventArgs e)
    {
        //get description of item for this supplier
        GridViewRow Row = ((System.Web.UI.WebControls.Button)sender).Parent.Parent as GridViewRow;
        string ItemP = TenderPriceDropDownList.DataKeys[Row.RowIndex].Value.ToString();

        //get the pricelist composite primary key
        PriceList Pl = Mplc.GetPriceListObjForGivenDescNSupplier(ItemP, Code);
        string ItemCode = Pl.ItemCode;
        string TenderY = Pl.TenderYear;
        //delete by giving the cpk
        Mplc.RemovePriceListObject(Code, ItemCode, TenderY);
        //repopulate tender list
        PopulateTenderSupplyList();

        Response.Write("<script>alert('" + Message.DeleteSuccessful + "');</script>");
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
        string NewPrice = newPriceTB.Text;

        //get description of item for this supplier
        System.Web.UI.WebControls.Label ItemDescLabel = (System.Web.UI.WebControls.Label)TenderPriceDropDownList.Rows[e.RowIndex].FindControl("ItemDesLabel");
        string ItemDesc = ItemDescLabel.Text;

        //get the pricelist composite primary key
        PriceList Pl = Mplc.GetPriceListObjForGivenDescNSupplier(ItemDesc, Code);
        string ItemCode = Pl.ItemCode;
        string TenderY = Pl.TenderYear;

        Mplc.UpdatePrice(NewPrice, Code, ItemCode, TenderY);

        TenderPriceDropDownList.EditIndex = -1;
        PopulateTenderSupplyList();
    }


    protected void SupplierPhoneNoTextBox_TextChanged(object sender, EventArgs e)
    {
        PhoneNoValidator.Enabled = true;
    }


    protected void NewPriceTextBox_TextChanged(object sender, EventArgs e)
    {
        //NewPriceRangeValidator.Enabled = true;
    }
}

public class TenderListObj
{
    string ItemD, ItemP;
    public TenderListObj(string iD, string iP)
    {
        this.ItemD = iD;
        this.ItemP = iP;
    }

    public string ItemDescription
    {
        get { return ItemD; }
        set { ItemD = value; }
    }

    public string ItemPrice
    {
        get { return ItemP; }
        set { ItemP = value; }
    }
}