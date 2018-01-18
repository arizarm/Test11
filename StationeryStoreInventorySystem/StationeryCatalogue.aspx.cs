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
        GridView1.DataBind();


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
        GridViewRow row = GridView1.Rows[index];
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
        Category category = ilogic.getCategory(categoryList.Text);
        TextBox description = (TextBox)row.FindControl("TextBox6");
        TextBox reorderLevel = (TextBox)row.FindControl("TextBox9");
        int level = Convert.ToInt32(reorderLevel.Text);
        TextBox reorderQty = (TextBox)row.FindControl("TextBox8");
        int qty = Convert.ToInt32(reorderQty.Text);
        DropDownList unitMeasure = (DropDownList)row.FindControl("DropDownList4");
        ilogic.updateItem(itemCode.Text, category, description.Text, level, qty, unitMeasure.SelectedValue);
        cancelEdit();
    }
    protected void cancelEdit()
    {
        GridView1.EditIndex = -1;
        GridView1.DataBind();
    }
}