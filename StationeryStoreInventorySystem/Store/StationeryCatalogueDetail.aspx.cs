using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//AUTHOR : TAN WEN SONG
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
        DdlCategory.DataSource = catList;
        DdlCategory.DataTextField = "CategoryName";
        DdlCategory.DataValueField = "CategoryID";
        List<string> UOMList = EFBroker_Item.GetDistinctUOMList();
        UOMList.Add("Other");
        DdlUOM.DataSource = UOMList;
        if (Session["itemlist"] == null)
        {
            iList = new List<Item>();
            LblSubtitle.Visible = false;
        }
        else
        {
            iList = (List<Item>)Session["itemlist"];
            if (iList.Count != 0) { 
            LblSubtitle.Visible = true;
            }
        }

        // data population
        gvItemAdded.DataSource = iList;
        gvItemAdded.DataBind();
        if (!IsPostBack)
        {
            DdlUOM.DataBind();
            DdlCategory.DataBind();
        }
        ControlToUpdate(TxtCategory, DdlCategory);
        ControlToUpdate(TxtUOM, DdlUOM);
    }
    //void Page_PreRender(object sender, EventArgs e)
    //{
    //    // Save PageArrayList before the page is rendered.
    //    ViewState.Add("itemlist", iList);
    //}

    protected void BtnAdd_Click(object sender, EventArgs e)
    {
        ItemBusinessLogic ilogic = new ItemBusinessLogic();
        //addItem("itemcode","test","test","10","10","test");
        string itemCode, categoryName, description, reorderLevel, reorderQty, uom, bin;


        if (Page.IsValid)
        {
            itemCode = TxtItemCode.Text.ToUpper();
            description = TxtDescription.Text;
            reorderLevel = TxtReorderLvl.Text;
            reorderQty = TxtReorderQty.Text;
            categoryName = Utility.FirstUpperCase(TxtCategory.Text);
            uom = Utility.FirstUpperCase(TxtUOM.Text);
            bin = TxtBin.Text;
            if(ItemBusinessLogic.ValidateNewItemfields(itemCode, categoryName, description, reorderLevel, reorderQty, uom, bin))
            {
                Item item = ItemBusinessLogic.AddItem(itemCode, categoryName, description, reorderLevel, reorderQty, uom, bin);
                iList.Add(item);
                Session["itemlist"] = iList;
                TxtItemCode.Text = TxtDescription.Text = TxtReorderLvl.Text = TxtReorderQty.Text = TxtCategory.Text = uom = TxtUOM.Text = TxtBin.Text =LblMessage.Text="";
                LblMessage.Visible = false;
                Response.Redirect(Request.RawUrl);
            }
            else
            {
                LblMessage.Text = "One or more fields is invalid, please check your fields";
                LblMessage.Visible = true;
            }
        }
        return;
    }

    protected void DdlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        ControlToUpdate(TxtCategory, DdlCategory);
    }

    protected void DdlUOM_SelectedIndexChanged(object sender, EventArgs e)
    {
        ControlToUpdate(TxtUOM, DdlUOM);
    }
    protected void ControlToUpdate(TextBox textbox, DropDownList ddList)
    {
        if (ddList.SelectedItem.Text.Equals("Other"))
        {
            //textbox.Enabled = true;
            textbox.Visible = true;
            //textbox.Text = "";
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

    protected void CstTxtItemCode_ServerValidate(object source, ServerValidateEventArgs args)
    {
        //args.IsValid = (EFBroker_Item.GetItembyItemCode(args.Value) == null);
        args.IsValid = ValidatorUtil.ValidateNewItem(CstTxtItemCode, args.Value);
    }


    protected void CstTxtDescription_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (EFBroker_Item.GetItembyDescription(args.Value) != null)
        {
            args.IsValid = false;
        }
        else
        {
            args.IsValid = true;
        }
    }
}