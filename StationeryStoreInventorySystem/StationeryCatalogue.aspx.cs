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
        ItemLogic ilogic = new ItemLogic();
        GridView1.DataSource = ilogic.getCatalogueList();
        List <Category> catList = ilogic.getCategoryList();
        Category temp = new Category();
        temp.CategoryID = 0;
        temp.CategoryName = "Other";
        catList.Add(temp);
        DropDownListUOM.DataSource = catList;
        List<string> UOMList = ilogic.getDistinctUOMList();
        UOMList.Add("Other");
        DropDownListCategory.DataSource = UOMList;
        if (!IsPostBack)
        {
            GridView1.DataBind();
            DropDownListUOM.DataBind();
            DropDownListCategory.DataBind();  
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
                        editRow(index);
                    }
                    break;
                case "RemoveRow":
                    {
                        removeRow(index);
                        refreshPage();
                    }
                    break;
                case "CancelEdit":
                    {
                        cancelEdit();
                    }
                    break;
                case "UpdateRow":
                    {
                        updateRow(index);
                        refreshPage();
                    }
                    break;
                default:
                    break;
            }
        }
    }
    protected void refreshPage()
    {
        Response.Redirect(Request.RawUrl);
    }
    protected void editRow(int index)
    {
        ItemLogic ilogic = new ItemLogic();
        GridView1.EditIndex = index;
        GridView1.DataBind();
        GridViewRowCollection a = GridView1.Rows;
        GridViewRow row = a[index];
        Label itemLabel = (Label)row.FindControl("Label1");
        DropDownList ddl = (DropDownList)row.FindControl("DropDownList3");
        DropDownList ddl2 = (DropDownList)row.FindControl("DropDownList4");

        ddl.DataTextField = "CategoryName";
        ddl.DataValueField = "CategoryID";
        List<Category> categories = ilogic.getCategoryList();
        Item item = ilogic.getItem(itemLabel.Text);
        ddl.DataSource = categories;
        ddl.SelectedValue = item.CategoryID.ToString();
        ddl.DataBind();

        ddl2.DataSource = ilogic.getDistinctUOMList();
        ddl2.SelectedValue = item.UnitOfMeasure;
        ddl2.DataBind();
        return;
    }
    protected void removeRow(int index)
    {
        ItemLogic ilogic = new ItemLogic();
        Label r = (Label)GridView1.Rows[index].FindControl("Label1");
        string output = r.Text;
        ilogic.removeItem(output);
        return;
    }
    protected void updateRow(int index)
    {
        ItemLogic ilogic = new ItemLogic();
        GridViewRow row = GridView1.Rows[index];
        Label itemCode = (Label)row.FindControl("Label1");
        DropDownList categoryList = (DropDownList)row.FindControl("DropDownList3");
        int categoryID = Convert.ToInt32(categoryList.SelectedValue);
        TextBox description = (TextBox)row.FindControl("TextBox6");
        TextBox reorderLevel = (TextBox)row.FindControl("TextBox9");
        int level = Convert.ToInt32(reorderLevel.Text);
        TextBox reorderQty = (TextBox)row.FindControl("TextBox8");
        int qty = Convert.ToInt32(reorderQty.Text);
        DropDownList unitMeasure = (DropDownList)row.FindControl("DropDownList4");
        ilogic.updateItem(itemCode.Text, categoryID, description.Text, level, qty, unitMeasure.SelectedValue);
        cancelEdit();
    }
    protected void cancelEdit()
    {
        GridView1.EditIndex = -1;
        GridView1.DataBind();
    }
    protected bool addItem(string itemCode, string categoryName, string description, string reorderLevel, string reorderQty, string UOM)
    {
        bool failure = false, success = true;
        ItemLogic ilogic = new ItemLogic();
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
        else if (ilogic.getItem(itemCode) != null)
        {
            return failure;
        }
        else
        {
            Category cat = ilogic.getCategorybyName(categoryName);
            if (cat == null)
            {
                categoryName = ilogic.firstUpperCase(categoryName);
                addCategory(categoryName);
                cat = ilogic.getCategorybyName(categoryName);
            }

            item.ItemCode = itemCode;
            item.Category = cat;
            item.Description = description;
            item.ReorderLevel = level;
            item.ReorderQty = qty;
            item.UnitOfMeasure = UOM;
            item.ActiveStatus = "Y";
            ilogic.addItem(item);
        }
        return success;
    }
    protected void addCategory(string categoryName)
    {
        ItemLogic iLogic = new ItemLogic();
        Category cat = new Category();
        cat.CategoryName = categoryName;
        iLogic.addCategory(cat);
        return;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        //addItem("itemcode","test","test","10","10","test");
        string itemCode, categoryName, description, reorderLevel, reorderQty, uom;

        if (!DropDownListCategory.SelectedValue.Equals("Other"))
        {
            TextBoxCategory.Text = DropDownListCategory.SelectedValue;
        }
        if (!DropDownListUOM.SelectedValue.Equals("Other"))
        {
            TextBoxUOM.Text= DropDownListUOM.SelectedValue;
        }
        if (Page.IsValid) { 
        itemCode = TextBoxItemNo.Text;
        description = TextBoxDesc.Text;
        reorderLevel = TextBoxReLvl.Text;
        reorderQty = TextBoxReQty.Text;
        categoryName = TextBoxCategory.Text;
        uom = TextBoxUOM.Text;
        addItem(itemCode, categoryName, description, reorderLevel, reorderQty, uom);
        }
        else
        {

        }
        return;
    }
}