using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StationeryCatalogueDetail : System.Web.UI.Page
{
    List<Item> iList;

    protected void Page_Load(object sender, EventArgs e)
    {
        ItemBusinessLogic ilogic = new ItemBusinessLogic();
        List<Category> catList = ilogic.GetCategoryList();
        Category temp = new Category();
        temp.CategoryID = 0;
        temp.CategoryName = "Other";
        catList.Add(temp);
        DropDownListCategory.DataSource = catList;
        DropDownListCategory.DataTextField = "CategoryName";
        DropDownListCategory.DataValueField = "CategoryID";
        List<string> UOMList = ilogic.GetDistinctUOMList();
        UOMList.Add("Other");
        DropDownListUOM.DataSource = UOMList;
        if (Session["itemlist"] == null)
        {
            iList = new List<Item>();
        }
        else
        {
            iList = (List<Item>)Session["itemlist"];
        }
        GridView1.DataSource = iList;
        GridView1.DataBind();
        if (!IsPostBack)
        {
            DropDownListUOM.DataBind();
            DropDownListCategory.DataBind();
        }


    }

    protected bool addItem(string itemCode, string categoryName, string description, string reorderLevel, string reorderQty, string UOM, string bin)
    {
        bool failure = false, success = true;
        ItemBusinessLogic ilogic = new ItemBusinessLogic();
        EFBroker_Item itemDB= new EFBroker_Item();
        Item item = new Item();
        int level, qty;
        if (string.IsNullOrEmpty(itemCode) || string.IsNullOrEmpty(categoryName) || string.IsNullOrEmpty(description) || string.IsNullOrEmpty(UOM) || string.IsNullOrEmpty(reorderLevel) || string.IsNullOrEmpty(reorderQty))
        {
            return failure;
        }
        else if (!int.TryParse(reorderLevel, out level) || !int.TryParse(reorderQty, out qty))
        {
            return failure;
        }
        else if (ilogic.GetItembyItemCode(itemCode) != null)
        {
            return failure;
        }
        else
        {
            Category cat = ilogic.GetCategorybyName(categoryName);
            if (cat == null)
            {
                categoryName = ilogic.FirstUpperCase(categoryName);
                ilogic.AddCategory(categoryName);
                cat = ilogic.GetCategorybyName(categoryName);
            }

            item.ItemCode = itemCode;
            item.Category = cat;
            item.Description = description;
            item.ReorderLevel = level;
            item.ReorderQty = qty;
            item.UnitOfMeasure = UOM;
            item.Bin = bin;
            item.ActiveStatus = "Y";
            item.BalanceQty = 0;
            itemDB.AddItem(item);
            iList.Add(item);
            Session["itemlist"] = iList;
        }
        return success;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        //addItem("itemcode","test","test","10","10","test");
        string itemCode, categoryName, description, reorderLevel, reorderQty, uom,bin;


        if (Page.IsValid)
        {
            itemCode = TextBoxItemNo.Text;
            description = TextBoxDesc.Text;
            reorderLevel = TextBoxReLvl.Text;
            reorderQty = TextBoxReQty.Text;
            categoryName = TextBoxCategory.Text;
            uom = TextBoxUOM.Text;
            bin = TextBoxBin.Text;
            if (addItem(itemCode, categoryName, description, reorderLevel, reorderQty, uom,bin))
            {
                TextBoxItemNo.Text = TextBoxDesc.Text = TextBoxReLvl.Text = TextBoxReQty.Text = TextBoxCategory.Text = uom = TextBoxUOM.Text = TextBoxBin.Text= "";
                Response.Redirect(Request.RawUrl);
            }

        }
        return;
    }

    protected void DropDownListCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!DropDownListCategory.SelectedValue.Equals("0"))
        {
            TextBoxCategory.Text = DropDownListCategory.SelectedItem.Text;
        }
    }

    protected void DropDownListUOM_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!DropDownListUOM.SelectedValue.Equals("Other"))
        {
            TextBoxUOM.Text = DropDownListUOM.SelectedItem.Text;
        }
    }

    protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
    {
        ItemBusinessLogic ilogic = new ItemBusinessLogic();
        string itemCode = args.Value;
        args.IsValid= (ilogic.GetItembyItemCode(args.Value) == null);
    }
}