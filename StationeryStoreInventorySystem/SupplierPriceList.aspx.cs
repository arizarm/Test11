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
                //Set Default Supplier Info on Page
                Supplier S = slc.getSupplier(code);
                TextBox1.Text = S.SupplierCode;
                TextBox2.Text = S.SupplierName;
                TextBox3.Text = S.SupplierContactName;
                TextBox4.Text = S.SupplierPhone;
                TextBox5.Text = S.SupplierFax;
                TextBox6.Text = S.SupplierAddress;
                TextBox8.Text = S.SupplierEmail;
                TextBox9.Text = S.ActiveStatus;


                //Populate dropdownlists for Item and Category
                defaultDropDownListRestore();
                //Populate dropdownlist for TenderSupply
                populateTenderSupplyList();
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
        S.SupplierName = TextBox2.Text ;
        S.SupplierContactName = TextBox3.Text ;
        S.SupplierPhone = TextBox4.Text ;
        S.SupplierFax = TextBox5.Text ;
        S.SupplierAddress = TextBox6.Text ;
        S.SupplierEmail = TextBox8.Text ;
        S.ActiveStatus = TextBox9.Text ;
        slc.updateSupplier(S);
        Response.Write("<script>alert('" + Message.UpdateSuccessful + "');</script>");
    }

    protected void DeleteButton_Click(object sender, EventArgs e)
    {
        slc.deleteSupplier(code);
        Response.Write("<script>alert('" + Message.DeleteSuccessful + "');</script>");
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
            PriceList pl = new PriceList();
            pl.SupplierCode = code;
            pl.ItemCode = mplc.getItemCodeForGivenItemName(ItemDropDownList.SelectedItem.Value);
            if (!ValidatorUtil.isEmpty(TextBox7.Text))
                pl.Price = decimal.Parse(TextBox7.Text);
            if (!(PriorityRankList.SelectedValue == "NA"))
                pl.SupplierRank = int.Parse(PriorityRankList.SelectedValue);
            if (!ValidatorUtil.isEmpty(TextBox11.Text))
                pl.TenderYear = (TextBox11.Text);
            mplc.addPriceListItem(pl);
            defaultDropDownListRestore();
            populateTenderSupplyList();
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
            List<string> shortlistedItems = mplc.getAllItemNamesForGivenCat(CategoryDropDownList.SelectedItem.Value.ToString());
            ItemDropDownList.DataSource = shortlistedItems;
            ItemDropDownList.DataBind();
            //ItemDropDownList.AutoPostBack = false;
        }
        else
        {
            defaultDropDownListRestore();
        }
    }

    protected void ItemDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ItemDropDownList.SelectedIndex != 0)
        {
            string catName = mplc.getCatForGivenItem(ItemDropDownList.SelectedItem.Value.ToString());
            CategoryDropDownList.SelectedValue = catName;
        }
        else
        {
            defaultDropDownListRestore();
        }
    }

    protected void defaultDropDownListRestore()
    {
        List<string> itemList = mplc.getAllItemNames();
        ItemDropDownList.DataSource = itemList;
        ItemDropDownList.DataBind();
        ItemDropDownList.Items.Insert(0, new ListItem("Select", "NA"));

        List<string> catList = mplc.getAllCatNames();
        CategoryDropDownList.DataSource = catList;
        CategoryDropDownList.DataBind();
        CategoryDropDownList.Items.Insert(0, new ListItem("Select", "NA"));

        List<int> rank = new List<int>() { 1, 2, 3 };
        PriorityRankList.DataSource = rank;
        PriorityRankList.DataBind();
        PriorityRankList.Items.Insert(0, new ListItem("Select", "NA"));

        TextBox7.Text = "";
        TextBox11.Text = "";
    }

    protected void populateTenderSupplyList()
    {
        List<PriceList> lpl = mplc.getSupplierPriceList(code);
        //populate tender supply list
        for (int i = 0; i < lpl.Count; i++)
        {
            string itemDesc = mplc.getItemNameForGivenItemCode(lpl[i].ItemCode);
            string itemPrice = "$" + lpl[i].Price + "/" + mplc.getUnitOfMeasureForGivenItemCode(lpl[i].ItemCode) + "  ";
            tenderListObj A = new tenderListObj(itemDesc, itemPrice);
            tenderSupplyList.Add(A);
        }
        TenderPriceDropDownList.DataSource = tenderSupplyList;
        TenderPriceDropDownList.DataBind();
    }

    protected void ItemDelete_Click(object sender, EventArgs e)
    {
        //get description of item for this supplier
        GridViewRow row = ((System.Web.UI.WebControls.Button)sender).Parent.Parent as GridViewRow;
        string itemP = TenderPriceDropDownList.DataKeys[row.RowIndex].Value.ToString();

        //get the pricelist composite primary key
        PriceList pl = mplc.getPriceListObjForGivenDescNSupplier(itemP, code);
        string itemCode = pl.ItemCode;
        string tenderY = pl.TenderYear;
        //delete by giving the cpk
        mplc.removePriceListObject(code, itemCode, tenderY);
        //repopulate tender list
        populateTenderSupplyList();
        Response.Write("<script>alert('" + Message.DeleteSuccessful + "');</script>");
    }

    protected void TenderPriceDropDownList_RowEditing(object sender, GridViewEditEventArgs e)
    {
        TenderPriceDropDownList.EditIndex = e.NewEditIndex;
        populateTenderSupplyList();
    }

    protected void TenderPriceDropDownList_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        TenderPriceDropDownList.EditIndex = -1;
        populateTenderSupplyList();
    }

    protected void TenderPriceDropDownList_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        //get new price
        System.Web.UI.WebControls.TextBox newPriceTB = (System.Web.UI.WebControls.TextBox)TenderPriceDropDownList.Rows[e.RowIndex].FindControl("TextBox10");
        string newPriceText = newPriceTB.Text;
        string newPrice = newPriceText.Substring(1, 5);

        //get description of item for this supplier
        System.Web.UI.WebControls.Label itemDescLabel = (System.Web.UI.WebControls.Label)TenderPriceDropDownList.Rows[e.RowIndex].FindControl("ItemDesLabel");
        string itemDesc = itemDescLabel.Text;               

        //get the pricelist composite primary key
        PriceList pl = mplc.getPriceListObjForGivenDescNSupplier(itemDesc, code);
        string itemCode = pl.ItemCode;
        string tenderY = pl.TenderYear;

        mplc.updatePrice(newPrice, code, itemCode, tenderY);

        TenderPriceDropDownList.EditIndex = -1;
        populateTenderSupplyList();
    }
}

public class tenderListObj
{
    string itemD, itemP;
    public tenderListObj(string iD, string iP)
    {
        this.itemD = iD;
        this.itemP = iP;
    }

    public string ItemDescription
    {
        get { return itemD; }
        set { itemD = value; }
    }

    public string ItemPrice
    {
        get { return itemP; }
        set { itemP = value; }
    }
}