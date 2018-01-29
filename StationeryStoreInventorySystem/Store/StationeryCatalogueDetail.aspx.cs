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
        List<Category> catList = EFBroker_Category.GetCategoryList();
        Category temp = new Category();
        temp.CategoryID = 0;
        temp.CategoryName = "Other";
        catList.Add(temp);
        DropDownListCategory.DataSource = catList;
        DropDownListCategory.DataTextField = "CategoryName";
        DropDownListCategory.DataValueField = "CategoryID";
        List<string> UOMList = EFBroker_Item.GetDistinctUOMList();
        UOMList.Add("Other");
        DropDownListUOM.DataSource = UOMList;
        if (Session["itemlist"] == null)
        {
            iList = new List<Item>();
            LabelSubtitle.Visible = false;
        }
        else
        {
            iList = (List<Item>)Session["itemlist"];
            if (iList.Count != 0) { 
            LabelSubtitle.Visible = true;
            }
        }

        // data population
        GridView1.DataSource = iList;
        GridView1.DataBind();
        if (!IsPostBack)
        {
            DropDownListUOM.DataBind();
            DropDownListCategory.DataBind();
        }
        ControlToUpdate(TextBoxCategory, DropDownListCategory);
        ControlToUpdate(TextBoxUOM, DropDownListUOM);
    }
    //void Page_PreRender(object sender, EventArgs e)
    //{
    //    // Save PageArrayList before the page is rendered.
    //    ViewState.Add("itemlist", iList);
    //}

    protected void Button1_Click(object sender, EventArgs e)
    {
        ItemBusinessLogic ilogic = new ItemBusinessLogic();
        //addItem("itemcode","test","test","10","10","test");
        string itemCode, categoryName, description, reorderLevel, reorderQty, uom, bin;


        if (Page.IsValid)
        {
            itemCode = TextBoxItemNo.Text.ToUpper();
            description = TextBoxDesc.Text;
            reorderLevel = TextBoxReLvl.Text;
            reorderQty = TextBoxReQty.Text;
            categoryName = Utility.FirstUpperCase(TextBoxCategory.Text);
            uom = Utility.FirstUpperCase(TextBoxUOM.Text);
            bin = TextBoxBin.Text;
            Item item = ItemBusinessLogic.AddItem(itemCode, categoryName, description, reorderLevel, reorderQty, uom, bin);
            if (item != null)
            {
                iList.Add(item);
                Session["itemlist"] = iList;
                TextBoxItemNo.Text = TextBoxDesc.Text = TextBoxReLvl.Text = TextBoxReQty.Text = TextBoxCategory.Text = uom = TextBoxUOM.Text = TextBoxBin.Text = "";
                Response.Redirect(Request.RawUrl);
            }
        }
        return;
    }

    protected void DropDownListCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        ControlToUpdate(TextBoxCategory, DropDownListCategory);
    }

    protected void DropDownListUOM_SelectedIndexChanged(object sender, EventArgs e)
    {
        ControlToUpdate(TextBoxUOM, DropDownListUOM);
    }
    protected void ControlToUpdate(TextBox textbox, DropDownList ddList)
    {
        if (ddList.SelectedItem.Text.Equals("Other"))
        {
            //textbox.Enabled = true;
            textbox.Visible = true;
            textbox.Text = "";
            textbox.ReadOnly = false;
        }
        else
        {
            //textbox.Enabled = false;
            textbox.Text = ddList.SelectedItem.Text;
            textbox.Visible = false;
            textbox.ReadOnly = true;
        }
        return;
    }

    protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
    {
        //args.IsValid = (EFBroker_Item.GetItembyItemCode(args.Value) == null);
        args.IsValid = Utility.ValidateNewItem(CustomValidator1, args.Value);
    }


    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect(LoginController.StationeryCatalogueURI);
    }
}