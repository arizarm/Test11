using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

public partial class StationeryCatalogue : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        Employee user = (Employee)Session["emp"];
        if (user != null)
        {
            try
            {
                gvForStationeryCatalogue.DataSource = EFBroker_Item.GetCatalogueList();
            }
            catch (Exception sql)
            {
                Response.Redirect(LoginController.ErrorPageURI);
            }
            if (!IsPostBack)
            {
                gvForStationeryCatalogue.DataBind();
                DisplayClickableURL();

            }
        }
        else
        {
            Utility.logout();
        }

    }
    public void DisplayClickableURL()
    {
        foreach (GridViewRow row in gvForStationeryCatalogue.Rows)
        {
            HyperLink itemLink = row.FindControl("lnkStockCard") as HyperLink;
            Label lblItemCode = row.FindControl("LblItemCode") as Label;
            Label lbldesc = row.FindControl("LblDescription") as Label;
            itemLink.Visible = true;
            lbldesc.Visible = false;
            itemLink.NavigateUrl = LoginController.ItemStockCardURI + "?itemCode=" + lblItemCode.Text;
        }
    }


    protected void GvForStationeryCatalogue_RowCommand(object sender, GridViewCommandEventArgs e)
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
                        CancelEdit(index);
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

        gvForStationeryCatalogue.EditIndex = index;
        gvForStationeryCatalogue.DataBind();
        GridViewRowCollection a = gvForStationeryCatalogue.Rows;
        GridViewRow row = a[index];
        Label itemLabel = (Label)row.FindControl("LblItemCode");
        DropDownList ddl = (DropDownList)row.FindControl("DdlCategory");
        DropDownList ddl2 = (DropDownList)row.FindControl("DdlUOM");

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
        row.BackColor = Color.Yellow;
        return;
    }
    protected void RemoveRow(int index)
    {
        Label r = (Label)gvForStationeryCatalogue.Rows[index].FindControl("LblItemCode");
        string itemCode = r.Text;
        EFBroker_Item.RemoveItem(itemCode);
        Utility.DisplayAlertMessage(Message.DeleteSuccessful);
        return;
    }
    protected void UpdateRow(int index)
    {
        if (Page.IsValid)
        {
            GridViewRow row = gvForStationeryCatalogue.Rows[index];
            Label itemCode = (Label)row.FindControl("LblItemCode");
            DropDownList categoryList = (DropDownList)row.FindControl("DdlCategory");
            TextBox description = (TextBox)row.FindControl("TxtDescription");
            TextBox reorderLevel = (TextBox)row.FindControl("TxtReorderLvl");
            TextBox reorderQty = (TextBox)row.FindControl("TxtReorderQty");
            TextBox bin = (TextBox)row.FindControl("TxtBin");
            DropDownList unitMeasure = (DropDownList)row.FindControl("DdlUOM");
            if (ItemBusinessLogic.IsValidItemFields(itemCode.Text, categoryList.SelectedItem.Text, description.Text, reorderLevel.Text, reorderQty.Text, unitMeasure.SelectedValue, bin.Text))
            {
                int level = Convert.ToInt32(reorderLevel.Text);
                int qty = Convert.ToInt32(reorderQty.Text);
                ItemBusinessLogic.UpdateItem(itemCode.Text, categoryList.SelectedItem.Text, description.Text, level, qty, unitMeasure.SelectedValue, bin.Text);
                CancelEdit(index);
            }
        }
    }
    protected void CancelEdit(int index)
    {
        GridViewRow row = gvForStationeryCatalogue.Rows[index];
        row.BackColor = Color.Transparent;
        gvForStationeryCatalogue.EditIndex = -1;
        gvForStationeryCatalogue.DataBind();
    }
    protected void BtnPrintView_Click(object sender, EventArgs e)
    {
        if (BtnPrintView.Text == "View Printable Version")
        {
            gvForStationeryCatalogue.Columns[7].Visible = false;
            gvForStationeryCatalogue.Columns[8].Visible = false;
            BtnPrintView.Text = "Back";
            BtnPrint.Visible = true;
        }
        else
        {
            gvForStationeryCatalogue.Columns[7].Visible = true;
            gvForStationeryCatalogue.Columns[8].Visible = true;
            BtnPrintView.Text = "View Printable Version";
            BtnPrint.Visible = false;

        }
    }

}