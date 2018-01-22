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

    protected void Button1_Click(object sender, EventArgs e)
    {
        ItemBusinessLogic ilogic = new ItemBusinessLogic();
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
            Item item = ilogic.AddItem(itemCode, categoryName, description, reorderLevel, reorderQty, uom, bin);
            if (item!= null)
            {
                iList.Add(item);
                Session["itemlist"] = iList;
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