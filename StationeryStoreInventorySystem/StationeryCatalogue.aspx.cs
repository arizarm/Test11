using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StationeryCatalogue : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        Employee user = (Employee)Session["emp"];
        if (user != null)
        {
            try
            {
                GridView1.DataSource = EFBroker_Item.GetCatalogueList();
            }
            catch(Exception sql)
            {
                Response.Redirect(LoginController.ErrorPageURI);
            }
            if (!IsPostBack)
            {
                if (user.DeptCode != "STATS")
                    if (true)
                    {
                        HyperLink7.Visible = false;
                        PrintViewButton.Visible = false;
                        GridView1.Columns[0].Visible = false;
                        GridView1.Columns[3].Visible = false;
                        GridView1.Columns[4].Visible = false;
                        GridView1.Columns[6].Visible = false;
                        GridView1.Columns[7].Visible = false;
                        GridView1.Columns[8].Visible = false;
                    }
                GridView1.DataBind();
            }
        }
        else
        {
            Utility.logout();
        }

    }



    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {


        if (IsPostBack)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            switch (e.CommandName)
            {
                case "EditRow":
                    {
                        //editRow method
                        EditRow(index);
                    }
                    break;
                case "RemoveRow":
                    {
                        RemoveRow(index);
                        RefreshPage();
                    }
                    break;
                case "CancelEdit":
                    {
                        cancelEdit(index);
                    }
                    break;
                case "UpdateRow":
                    {
                        UpdateRow(index);
                        RefreshPage();

                    }
                    break;
                default:
                    break;
            }
        }
    }
    protected void RefreshPage()
    {
        Response.Redirect(Request.RawUrl);
    }
    protected void EditRow(int index)
    {

        GridView1.EditIndex = index;
        GridView1.DataBind();
        GridViewRowCollection a = GridView1.Rows;
        GridViewRow row = a[index];
        Label itemLabel = (Label)row.FindControl("LabelICode");
        DropDownList ddl = (DropDownList)row.FindControl("DropDownListCat");
        DropDownList ddl2 = (DropDownList)row.FindControl("DropDownListUOM");

        ddl.DataTextField = "CategoryName";
        ddl.DataValueField = "CategoryID";
        List<Category> categories = EFBroker_Category.GetCategoryList();
        Item item = EFBroker_Item.GetItembyItemCode(itemLabel.Text);
        ddl.DataSource = categories;
        ddl.SelectedValue = item.CategoryID.ToString();
        ddl.DataBind();

        ddl2.DataSource = EFBroker_Item.GetDistinctUOMList();
        ddl2.SelectedValue = item.UnitOfMeasure;
        ddl2.DataBind();
        return;
    }
    protected void RemoveRow(int index)
    {
        Label r = (Label)GridView1.Rows[index].FindControl("LabelICode");
        string itemCode = r.Text;
        EFBroker_Item.RemoveItem(itemCode);
        Utility.DisplayAlertMessage(Message.DeleteSuccessful);
        return;
    }
    protected void UpdateRow(int index)
    {
        if (Page.IsValid) { 
        GridViewRow row = GridView1.Rows[index];
        Label itemCode = (Label)row.FindControl("LabelICode");
        DropDownList categoryList = (DropDownList)row.FindControl("DropDownListCat");
        TextBox description = (TextBox)row.FindControl("TextboxDesc");
        TextBox reorderLevel = (TextBox)row.FindControl("TextBoxReLvl");
        int level = Convert.ToInt32(reorderLevel.Text);
        TextBox reorderQty = (TextBox)row.FindControl("TextBoxReQty");
        int qty = Convert.ToInt32(reorderQty.Text);
        TextBox bin = (TextBox)row.FindControl("TextBoxBin");
        DropDownList unitMeasure = (DropDownList)row.FindControl("DropDownListUOM");
        ItemBusinessLogic.UpdateItem(itemCode.Text, categoryList.SelectedItem.Text, description.Text, level, qty, unitMeasure.SelectedValue, bin.Text);
        cancelEdit(index);
        }
    }
    protected void cancelEdit(int index)
    {
        //GridViewRowCollection a = GridView1.Rows;
        //GridViewRow row = a[index];
        //CustomValidator customValidator1 = (CustomValidator)row.FindControl("CustomValidator1");
        //customValidator1.IsValid = true;
        //RequiredFieldValidator requiredFieldValidator1 = (RequiredFieldValidator)row.FindControl("RequiredFieldValidator1");
        //requiredFieldValidator1.IsValid = true;
        GridView1.EditIndex = -1;
        GridView1.DataBind();
    }
    //protected bool addItem(string itemCode, string categoryName, string description, string reorderLevel, string reorderQty, string UOM)
    //{
    //    bool failure = false, success = true;
    //    ItemBusinessLogic ilogic = new ItemBusinessLogic();
    //    Item item = new Item();
    //    int level, qty;
    //    if (string.IsNullOrEmpty(itemCode) || string.IsNullOrEmpty(categoryName) || string.IsNullOrEmpty(description) || string.IsNullOrEmpty(UOM) || string.IsNullOrEmpty(reorderLevel) || string.IsNullOrEmpty(reorderQty))
    //    {
    //        return failure;
    //    }
    //    else if (!int.TryParse(reorderLevel, out level) || !int.TryParse(reorderQty, out qty))
    //    {
    //        return failure;
    //    }
    //    else if (ilogic.GetItembyItemCode(itemCode) != null)
    //    {
    //        return failure;
    //    }
    //    else
    //    {
    //        Category cat = ilogic.getCategorybyName(categoryName);
    //        if (cat == null)
    //        {
    //            categoryName = ilogic.firstUpperCase(categoryName);
    //            ilogic.addCategory(categoryName);
    //            cat = ilogic.getCategorybyName(categoryName);
    //        }

    //        item.ItemCode = itemCode;
    //        item.Category = cat;
    //        item.Description = description;
    //        item.ReorderLevel = level;
    //        item.ReorderQty = qty;
    //        item.UnitOfMeasure = UOM;
    //        item.ActiveStatus = "Y";
    //        ilogic.addItem(item);
    //    }
    //    return success;
    //}

    //protected void Button1_Click(object sender, EventArgs e)
    //{
    //    //addItem("itemcode","test","test","10","10","test");
    //    string itemCode, categoryName, description, reorderLevel, reorderQty, uom;


    //    if (Page.IsValid)
    //    {
    //        itemCode = TextBoxItemNo.Text;
    //        description = TextBoxDesc.Text;
    //        reorderLevel = TextBoxReLvl.Text;
    //        reorderQty = TextBoxReQty.Text;
    //        categoryName = TextBoxCategory.Text;
    //        uom = TextBoxUOM.Text;
    //        if (addItem(itemCode, categoryName, description, reorderLevel, reorderQty, uom))
    //        {
    //            TextBoxItemNo.Text = TextBoxDesc.Text = TextBoxReLvl.Text = TextBoxReQty.Text = TextBoxCategory.Text = uom = TextBoxUOM.Text = "";
    //            refreshPage();
    //        }

    //    }
    //    return;
    //}

    //protected void DropDownListCategory_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (!DropDownListCategory.SelectedValue.Equals("0"))
    //    {
    //        TextBoxCategory.Text = DropDownListCategory.SelectedItem.Text;
    //    }
    //}

    //protected void DropDownListUOM_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (!DropDownListUOM.SelectedValue.Equals("Other"))
    //    {
    //        TextBoxUOM.Text = DropDownListUOM.SelectedItem.Text;
    //    }
    //}

    //protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
    //{
    //    args.IsValid = (EFBroker_Item.GetItembyDescription(args.Value) == null);
    //    return;
    //}

    protected void PrintViewButton_Click(object sender, EventArgs e)
    {
        if (PrintViewButton.Text == "View Printable Version")
        {
            GridView1.Columns[7].Visible = false;
            GridView1.Columns[8].Visible = false;
            PrintViewButton.Text = "Back";
        }
        else
        {
            GridView1.Columns[7].Visible = true;
            GridView1.Columns[8].Visible = true;
            PrintViewButton.Text = "View Printable Version";

        }
    }
}