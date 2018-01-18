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

    }

    protected void CategoryRadioButtonList_SelectedIndexChanged(object sender, EventArgs e)
    {
        int selectedIndex = CategoryRadioButtonList.SelectedIndex;
        switch (selectedIndex)
        {
            case 0:
                CategoryDropDownList.Visible = false;
                CategoryAddButton.Visible = false;
                break;
            case 1:
                CategoryDropDownList.Visible = true;
                CategoryAddButton.Visible = true;
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
                ToDropDownList.Visible = false;
                DurationDropDownList.Visible = false;
                DurationAddButton.Visible = false;
                break;
            case 1:
                FromLabel.Visible = true;
                FromDropDownList.Visible = true;
                ToLabel.Visible = true;
                ToDropDownList.Visible = true;
                DurationDropDownList.Visible = false;
                DurationAddButton.Visible = false;
                break;
            case 2:
                FromLabel.Visible = false;
                FromDropDownList.Visible = false;
                ToLabel.Visible = false;
                ToDropDownList.Visible = false;
                DurationDropDownList.Visible = true;
                DurationAddButton.Visible = true;
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
                break;
            case 1:
                SupplierDropDownList.Visible = true;
                SupplierAddButton.Visible = true;
                break;
        }


    }
}