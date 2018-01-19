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

            List<string> catAdded = new List<string>();
            ViewState["catAdded"] = catAdded;

            List<string> deptAdded = new List<string>();
            ViewState["deptAdded"] = deptAdded;

            List<string> dateAdded = new List<string>();
            ViewState["dateAdded"] = dateAdded;
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
                break;
            case 1:
                FromLabel.Visible = true;
                FromDropDownList.Visible = true;
                ToLabel.Visible = true;
                DurationDropDownList.Visible = false;
                DurationAddButton.Visible = false;
                break;
            case 2:
                FromLabel.Visible = false;
                FromDropDownList.Visible = false;
                ToLabel.Visible = false;
                DurationDropDownList.Visible = true;
                DurationAddButton.Visible = true;
                GenerateRequisitionTrendController grtc = new GenerateRequisitionTrendController();
                List<string> allMonths = grtc.getUniqueRequisitionMonths();
                DurationDropDownList.DataSource = allMonths;
                DurationDropDownList.DataBind();
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

    protected void RemoveCategoryBtn_Click(object sender, EventArgs e)
    {
        //get row position of item
        GridViewRow row = ((System.Web.UI.WebControls.Button)sender).Parent.Parent as GridViewRow;

        ((List<string>)ViewState["catAdded"]).RemoveAt(row.RowIndex);
        CategoryGridView.DataSource = ((List<string>)ViewState["catAdded"]);
        CategoryGridView.DataBind();
    }

    protected void RemoveDepartmentBtn_Click(object sender, EventArgs e)
    {
        GridViewRow row = ((System.Web.UI.WebControls.Button)sender).Parent.Parent as GridViewRow;

        ((List<string>)ViewState["deptAdded"]).RemoveAt(row.RowIndex);
        DepartmentGridView.DataSource = ((List<string>)ViewState["deptAdded"]);
        DepartmentGridView.DataBind();

    }

    protected void DepartmentAddButton_Click(object sender, EventArgs e)
    {
        string addMe = DepartmentDropDownList.SelectedItem.Text;

        if (((List<string>)ViewState["deptAdded"]).Count == 0)
        {
            ((List<string>)ViewState["deptAdded"]).Add(addMe);
            DepartmentGridView.DataSource = ((List<string>)ViewState["deptAdded"]);
            DepartmentGridView.DataBind();
        }
        else
        {
            for (int i = 0; i < ((List<string>)ViewState["deptAdded"]).Count; i++)
            {
                if (((List<string>)ViewState["deptAdded"])[i].ToString() == addMe)
                {
                    Response.Write("<script>alert('" + Message.DepartmentAlreadyInList + "');</script>");
                    break;
                }
                if (i == ((List<string>)ViewState["deptAdded"]).Count - 1)
                {
                    ((List<string>)ViewState["deptAdded"]).Add(addMe);
                    DepartmentGridView.DataSource = ((List<string>)ViewState["deptAdded"]);
                    DepartmentGridView.DataBind();
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