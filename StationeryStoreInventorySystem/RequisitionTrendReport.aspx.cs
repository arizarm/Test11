using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RequisitionTrend : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GenerateRequisitionTrendController grtc = new GenerateRequisitionTrendController();
            List<string> catNames = grtc.getAllCategoryNames();
            CategoryDropDownList.DataSource = catNames;
            CategoryDropDownList.DataBind();

            List<string> deptNames = grtc.getAllDepartmentNames();
            DepartmentDropDownList.DataSource = deptNames;
            DepartmentDropDownList.DataBind();
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

    protected void DepartmentRadioButtonList_SelectedIndexChanged(object sender, EventArgs e)
    {
        int selectedIndex = DepartmentRadioButtonList.SelectedIndex;
        switch (selectedIndex)
        {
            case 0:
                DepartmentDropDownList.Visible = false;
                DepartmentAddButton.Visible = false;
                break;
            case 1:
                DepartmentDropDownList.Visible = true;
                DepartmentAddButton.Visible = true;
                break;
        }

    }
}