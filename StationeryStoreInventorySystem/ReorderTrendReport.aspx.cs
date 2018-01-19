using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StationeryReorderReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<string> catAdded = new List<string>();
            ViewState["catAdded"] = catAdded;

            List<string> supplierAdded = new List<string>();
            ViewState["supplierAdded"] = supplierAdded;

            List<string> dateAdded = new List<string>();
            ViewState["dateAdded"] = dateAdded;
        }
    }

    protected void CategoryRadioButtonList_SelectedIndexChanged(object sender, EventArgs e)
    {
        int selectedIndex = CategoryRadioButtonList.SelectedIndex;
        switch (selectedIndex)
        {
            case 0:
                CategoryDropDownList.Visible = false;
                CategoryAddButton.Visible = false;
                CategoryGridView.Visible = false;
                break;
            case 1:
                CategoryDropDownList.Visible = true;
                CategoryAddButton.Visible = true;
                CategoryGridView.Visible = true;
                GenerateReorderTrendController grtc = new GenerateReorderTrendController();
                List<string> catNames = grtc.getAllCategoryNames();
                CategoryDropDownList.DataSource = catNames;
                CategoryDropDownList.DataBind();
                break;
        }
    }

    protected void DurationRadioButtonList_SelectedIndexChanged(object sender, EventArgs e)
    {
        int selectedIndex = DurationRadioButtonList.SelectedIndex;
        switch (selectedIndex)
        {
            case 0:
                FromLabel.Visible = false;
                FromDropDownList.Visible = false;
                ToLabel.Visible = false;
                DurationDropDownList.Visible = false;
                DurationAddButton.Visible = false;
                DurationGridView.Visible = false;
                break;
            case 1:
                FromLabel.Visible = true;
                FromDropDownList.Visible = true;
                ToLabel.Visible = true;
                DurationDropDownList.Visible = false;
                DurationAddButton.Visible = false;
                DurationGridView.Visible = false;
                GenerateReorderTrendController grtc = new GenerateReorderTrendController();
                List<string> allMonths = grtc.getRequisitionsUpTo2MonthsAgo();
                FromDropDownList.DataSource = allMonths;
                FromDropDownList.DataBind();
                break;
            case 2:
                FromLabel.Visible = false;
                FromDropDownList.Visible = false;
                ToLabel.Visible = false;
                DurationDropDownList.Visible = true;
                DurationAddButton.Visible = true;
                DurationGridView.Visible = true;
                GenerateReorderTrendController grtc1 = new GenerateReorderTrendController();
                List<string> fromMths = grtc1.getUniqueRequisitionMonths();
                DurationDropDownList.DataSource = fromMths;
                DurationDropDownList.DataBind();
                break;
        }
    }

    protected void SupplierRadioButtonList_SelectedIndexChanged(object sender, EventArgs e)
    {
        int selectedIndex = SupplierRadioButtonList.SelectedIndex;
        switch (selectedIndex)
        {
            case 0:
                SupplierDropDownList.Visible = false;
                SupplierAddButton.Visible = false;
                SupplierGridView.Visible = false;
                break;
            case 1:
                SupplierDropDownList.Visible = true;
                SupplierAddButton.Visible = true;
                SupplierGridView.Visible = true;
                GenerateReorderTrendController grtc = new GenerateReorderTrendController();
                List<string> supplNames = grtc.getAllSupplierNames();
                SupplierDropDownList.DataSource = supplNames;
                SupplierDropDownList.DataBind();
                break;
        }


    }

    protected void RemovSupplierBtn_Click(object sender, EventArgs e)
    {
        GridViewRow row = ((System.Web.UI.WebControls.Button)sender).Parent.Parent as GridViewRow;

        ((List<string>)ViewState["supplierAdded"]).RemoveAt(row.RowIndex);
        SupplierGridView.DataSource = ((List<string>)ViewState["supplierAdded"]);
        SupplierGridView.DataBind();
    }

    protected void SupplierAddButton_Click(object sender, EventArgs e)
    {
        string addMe = SupplierDropDownList.SelectedItem.Text;

        if (((List<string>)ViewState["supplierAdded"]).Count == 0)
        {
            ((List<string>)ViewState["supplierAdded"]).Add(addMe);
            SupplierGridView.DataSource = ((List<string>)ViewState["supplierAdded"]);
            SupplierGridView.DataBind();
        }
        else
        {
            for (int i = 0; i < ((List<string>)ViewState["supplierAdded"]).Count; i++)
            {
                if (((List<string>)ViewState["supplierAdded"])[i].ToString() == addMe)
                {
                    Response.Write("<script>alert('" + Message.SupplierAlreadyInList + "');</script>");
                    break;
                }
                if (i == ((List<string>)ViewState["supplierAdded"]).Count - 1)
                {
                    ((List<string>)ViewState["supplierAdded"]).Add(addMe);
                    SupplierGridView.DataSource = ((List<string>)ViewState["supplierAdded"]);
                    SupplierGridView.DataBind();
                    break;
                }
            }
        }

    }

    protected void RemoveCategoryBtn_Click(object sender, EventArgs e)
    {
        GridViewRow row = ((System.Web.UI.WebControls.Button)sender).Parent.Parent as GridViewRow;

        ((List<string>)ViewState["catAdded"]).RemoveAt(row.RowIndex);
        CategoryGridView.DataSource = ((List<string>)ViewState["catAdded"]);
        CategoryGridView.DataBind();

    }

    protected void CategoryAddButton_Click(object sender, EventArgs e)
    {
        string addMe = CategoryDropDownList.SelectedItem.Text;

        if (((List<string>)ViewState["catAdded"]).Count == 0)
        {
            ((List<string>)ViewState["catAdded"]).Add(addMe);
            CategoryGridView.DataSource = ((List<string>)ViewState["catAdded"]);
            CategoryGridView.DataBind();
        }
        else
        {
            for (int i = 0; i < ((List<string>)ViewState["catAdded"]).Count; i++)
            {
                if (((List<string>)ViewState["catAdded"])[i].ToString() == addMe)
                {
                    Response.Write("<script>alert('" + Message.CategoryAlreadyInList + "');</script>");
                    break;
                }
                if (i == ((List<string>)ViewState["catAdded"]).Count - 1)
                {
                    ((List<string>)ViewState["catAdded"]).Add(addMe);
                    CategoryGridView.DataSource = ((List<string>)ViewState["catAdded"]);
                    CategoryGridView.DataBind();
                    break;
                }
            }
        }

    }

    protected void DurationAddButton_Click(object sender, EventArgs e)
    {
        string addMe = DurationDropDownList.SelectedItem.Text;

        if (((List<string>)ViewState["dateAdded"]).Count == 0)
        {
            ((List<string>)ViewState["dateAdded"]).Add(addMe);
            DurationGridView.DataSource = ((List<string>)ViewState["dateAdded"]);
            DurationGridView.DataBind();
        }
        else
        {
            for (int i = 0; i < ((List<string>)ViewState["dateAdded"]).Count; i++)
            {
                if (((List<string>)ViewState["dateAdded"])[i].ToString() == addMe)
                {
                    Response.Write("<script>alert('" + Message.DateAlreadyInList + "');</script>");
                    break;
                }
                if (i == ((List<string>)ViewState["dateAdded"]).Count - 1)
                {
                    ((List<string>)ViewState["dateAdded"]).Add(addMe);
                    DurationGridView.DataSource = ((List<string>)ViewState["dateAdded"]);
                    DurationGridView.DataBind();
                    break;
                }
            }
        }
    }

    protected void RemoveDurationBtn_Click(object sender, EventArgs e)
    {
        GridViewRow row = ((System.Web.UI.WebControls.Button)sender).Parent.Parent as GridViewRow;

        ((List<string>)ViewState["dateAdded"]).RemoveAt(row.RowIndex);
        DurationGridView.DataSource = ((List<string>)ViewState["dateAdded"]);
        DurationGridView.DataBind();
    }
}