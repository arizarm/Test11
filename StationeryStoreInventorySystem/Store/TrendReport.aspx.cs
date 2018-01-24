using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RequisitionTrend : System.Web.UI.Page
{

    string ReportToSelect;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ReportToSelect = Request.QueryString["type"];
            switch (ReportToSelect)
            {
                case "RTR":
                    DisplayCommonComponents();
                    DepartmentLabel.Visible = true;
                    DepartmentRadioButtonList.Visible = true;
                    SplitReportRadioButtonList.Visible = true;
                    HeaderLabel.Text = Message.RTReport;
                    break;
                case "ROR":
                    DisplayCommonComponents();
                    SupplierLabel.Visible = true;
                    SupplierRadioButtonList.Visible = true;
                    SplitRORReportRadioButtonList.Visible = true;
                    HeaderLabel.Text = Message.ROReport;
                    break;
            }
            List<string> catAdded = new List<string>();
            ViewState["catAdded"] = catAdded;
            ViewState["catSel"] = 0;

            List<string> dateAdded = new List<string>();
            ViewState["dateAdded"] = dateAdded;
            ViewState["durSel"] = 0;

            List<string> sharedListAdded = new List<string>();
            ViewState["sharedListAdded"] = sharedListAdded;
            ViewState["sharedListSel"] = 0;
        }
    }

    protected void DisplayCommonComponents()
    {
        MainTable.Visible = true;
        DurationLabel.Visible = true;
        SelectCategoryLabel.Visible = true;
        DurationRadioButtonList.Visible = true;
        CategoryRadioButtonList.Visible = true;
        SplitLabel.Visible = true;
        GenerateButton.Visible = true;
    }

    protected void DurationRadioButtonList_SelectedIndexChanged(object sender, EventArgs e)
    {
        int selectedIndex = DurationRadioButtonList.SelectedIndex;
        switch (selectedIndex)
        {
            case 0:
                FromLabel.Visible = false;
                FromDropDownList.Visible = false;
                DurationDropDownList.Visible = false;
                DurationAddButton.Visible = false;
                DurationGridView.Visible = false;
                ViewState["durSel"] = 0;
                break;
            case 1:
                FromLabel.Visible = true;
                FromDropDownList.Visible = true;
                DurationDropDownList.Visible = false;
                DurationAddButton.Visible = false;
                DurationGridView.Visible = false;
                ViewState["durSel"] = 1;
                GenerateRequisitionTrendController grtc = new GenerateRequisitionTrendController();
                List<string> allMonths = grtc.GetRequisitionsUpTo2MonthsAgo();
                FromDropDownList.DataSource = allMonths;
                FromDropDownList.DataBind();
                break;
            case 2:
                FromLabel.Visible = false;
                FromDropDownList.Visible = false;
                DurationDropDownList.Visible = true;
                DurationAddButton.Visible = true;
                DurationGridView.Visible = true;
                ViewState["durSel"] = 2;
                GenerateRequisitionTrendController Grtc1 = new GenerateRequisitionTrendController();
                List<string> FromMths = Grtc1.GetUniqueRequisitionMonths();
                DurationDropDownList.DataSource = FromMths;
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
                CategoryGridView.Visible = false;
                ViewState["catSel"] = 0;
                break;
            case 1:
                CategoryDropDownList.Visible = true;
                CategoryAddButton.Visible = true;
                CategoryGridView.Visible = true;
                ViewState["catSel"] = 1;
                GenerateRequisitionTrendController grtc = new GenerateRequisitionTrendController();
                List<string> CatNames = grtc.GetAllCategoryNames();
                CategoryDropDownList.DataSource = CatNames;
                CategoryDropDownList.DataBind();
                break;
        }
    }

    protected void DepartmentRadioButtonList_SelectedIndexChanged(object sender, EventArgs e)
    {
        int SelectedIndex = DepartmentRadioButtonList.SelectedIndex;
        switch (SelectedIndex)
        {
            case 0:
                SharedDropDownList.Visible = false;
                SharedAddButton.Visible = false;
                SharedGridView.Visible = false;
                ViewState["sharedListSel"] = 0;
                break;
            case 1:
                SharedDropDownList.Visible = true;
                SharedAddButton.Visible = true;
                SharedGridView.Visible = true;
                ViewState["sharedListSel"] = 1;
                GenerateRequisitionTrendController grtc = new GenerateRequisitionTrendController();
                List<string> deptNames = grtc.GetAllDepartmentNames();
                SharedDropDownList.DataSource = deptNames;
                SharedDropDownList.DataBind();
                break;
        }

    }

    protected void SupplierRadioButtonList_SelectedIndexChanged(object sender, EventArgs e)
    {
        int SelectedIndex = SupplierRadioButtonList.SelectedIndex;
        switch (SelectedIndex)
        {
            case 0:
                SharedDropDownList.Visible = false;
                SharedAddButton.Visible = false;
                SharedGridView.Visible = false;
                ViewState["sharedListSel"] = 0;
                break;
            case 1:
                SharedDropDownList.Visible = true;
                SharedAddButton.Visible = true;
                SharedGridView.Visible = true;
                ViewState["sharedListSel"] = 1;
                GenerateReorderTrendController grtc = new GenerateReorderTrendController();
                List<string> supplierNames = grtc.GetAllSupplierNames();
                SharedDropDownList.DataSource = supplierNames;
                SharedDropDownList.DataBind();
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

    protected void RemoveSharedBtn_Click(object sender, EventArgs e)
    {
        GridViewRow row = ((System.Web.UI.WebControls.Button)sender).Parent.Parent as GridViewRow;

        ((List<string>)ViewState["sharedListAdded"]).RemoveAt(row.RowIndex);
        SharedGridView.DataSource = ((List<string>)ViewState["sharedListAdded"]);
        SharedGridView.DataBind();
    }

    protected void SharedAddButton_Click(object sender, EventArgs e)
    {
        string addMe = SharedDropDownList.SelectedItem.Text;

        if (((List<string>)ViewState["sharedListAdded"]).Count == 0)
        {
            ((List<string>)ViewState["sharedListAdded"]).Add(addMe);
            SharedGridView.DataSource = ((List<string>)ViewState["sharedListAdded"]);
            SharedGridView.DataBind();
        }
        else
        {
            for (int i = 0; i < ((List<string>)ViewState["sharedListAdded"]).Count; i++)
            {
                if (((List<string>)ViewState["sharedListAdded"])[i].ToString() == addMe)
                {
                    if (ReportToSelect == "RTR")
                        Response.Write("<script>alert('" + Message.DepartmentAlreadyInList + "');</script>");
                    else if (ReportToSelect == "ROR")
                        Response.Write("<script>alert('" + Message.SupplierAlreadyInList + "');</script>");
                    break;
                }
                if (i == ((List<string>)ViewState["sharedListAdded"]).Count - 1)
                {
                    ((List<string>)ViewState["sharedListAdded"]).Add(addMe);
                    SharedGridView.DataSource = ((List<string>)ViewState["sharedListAdded"]);
                    SharedGridView.DataBind();
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
        else if (((List<string>)ViewState["dateAdded"]).Count == 1 || (((List<string>)ViewState["dateAdded"]).Count == 2))
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
                    ((List<string>)ViewState["dateAdded"]).RemoveAt(0);
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

    protected void GenerateButton_Click(object sender, EventArgs e)
    {
        bool areAllFieldsValid = VerifyAllCustomFieldsAreValid();
        ReportToSelect = Request.QueryString["type"];
        if (ReportToSelect == "RTR")
        {

            if (areAllFieldsValid)
            {
                GenerateRequisitionTrendController genRTC = new GenerateRequisitionTrendController();
                List<string> deptSelected = GetSelectedDepartments();
                List<string> catSelected = GetSelectedCategories();
                List<string> duratnSelected = GetSelectedDuration();

                List<TrendReport> basicReportObjects = new List<TrendReport>();

                int splitBySelected = SplitReportRadioButtonList.SelectedIndex;
                if (splitBySelected == 0)
                {
                    foreach (string dept in deptSelected)
                    {

                        string categoryName;
                        string departmentName = dept;
                        int month1 = 0, month2 = 0, month3 = 0;

                        foreach (string cat in catSelected)
                        {
                            categoryName = cat;
                            TrendReport input;

                            if (duratnSelected.Count == 1)
                            {
                                month1 = genRTC.GetTotalRequisitionByCategoryGivenMonth(duratnSelected[0], dept, cat);
                                input = new TrendReport(departmentName, categoryName, month1, duratnSelected[0]);
                                basicReportObjects.Add(input);
                            }
                            else if (duratnSelected.Count == 2)
                            {
                                month1 = genRTC.GetTotalRequisitionByCategoryGivenMonth(duratnSelected[0], dept, cat);
                                month2 = genRTC.GetTotalRequisitionByCategoryGivenMonth(duratnSelected[1], dept, cat);
                                input = new TrendReport(departmentName, categoryName, month1, month2, duratnSelected[0], duratnSelected[1]);
                                basicReportObjects.Add(input);
                            }
                            else
                            {
                                month1 = genRTC.GetTotalRequisitionByCategoryGivenMonth(duratnSelected[0], dept, cat);
                                month2 = genRTC.GetTotalRequisitionByCategoryGivenMonth(duratnSelected[1], dept, cat);
                                month3 = genRTC.GetTotalRequisitionByCategoryGivenMonth(duratnSelected[2], dept, cat);
                                input = new TrendReport(departmentName, categoryName, month1, month2, month3, duratnSelected[0], duratnSelected[1], duratnSelected[2]);
                                basicReportObjects.Add(input);
                            }
                        }
                    }
                }
                else
                {
                    foreach (string cat in catSelected)
                    {
                        string categoryName = cat;
                        string departmentName = "";
                        int month1 = 0, month2 = 0, month3 = 0;

                        foreach (string dept in deptSelected)
                        {
                            departmentName = dept;
                            TrendReport input;
                            if (duratnSelected.Count == 1)
                            {
                                month1 = genRTC.GetTotalRequisitionByCategoryGivenMonth(duratnSelected[0], dept, cat);
                                input = new TrendReport(departmentName, categoryName, month1, duratnSelected[0]);
                                basicReportObjects.Add(input);
                            }
                            else if (duratnSelected.Count == 2)
                            {
                                month1 = genRTC.GetTotalRequisitionByCategoryGivenMonth(duratnSelected[0], dept, cat);
                                month2 = genRTC.GetTotalRequisitionByCategoryGivenMonth(duratnSelected[1], dept, cat);
                                input = new TrendReport(departmentName, categoryName, month1, month2, duratnSelected[0], duratnSelected[1]);
                                basicReportObjects.Add(input);
                            }
                            else
                            {
                                month1 = genRTC.GetTotalRequisitionByCategoryGivenMonth(duratnSelected[0], dept, cat);
                                month2 = genRTC.GetTotalRequisitionByCategoryGivenMonth(duratnSelected[1], dept, cat);
                                month3 = genRTC.GetTotalRequisitionByCategoryGivenMonth(duratnSelected[2], dept, cat);
                                input = new TrendReport(departmentName, categoryName, month1, month2, month3, duratnSelected[0], duratnSelected[1], duratnSelected[2]);
                                basicReportObjects.Add(input);
                            }

                        }
                    }
                }
                Session["reportsToDisplay"] = basicReportObjects;
                Session["typeOfReport"] = splitBySelected;
                Response.Redirect("~/Store/TrendReportDisplay.aspx");
            }
            else
            {
                Response.Write("<script>alert('" + Message.CustomFieldsNotSelected + "');</script>");
            }
        }
        else if (ReportToSelect == "ROR")
        {

            if (areAllFieldsValid)
            {
                GenerateReorderTrendController genRTC = new GenerateReorderTrendController();
                List<string> duratnSelected = GetSelectedDuration();
                List<string> catSelected = GetSelectedCategories();
                List<string> supplierSelected = GetSelectedSuppliers();

                List<TrendReport> basicReportObjects = new List<TrendReport>();

                int splitBySelected = SplitRORReportRadioButtonList.SelectedIndex;
                if (splitBySelected == 0)
                {
                    foreach (string suppl in supplierSelected)
                    {

                        string categoryName;
                        string supplierName = suppl;
                        int month1 = 0, month2 = 0, month3 = 0;

                        foreach (string cat in catSelected)
                        {
                            categoryName = cat;
                            TrendReport input;

                            if (duratnSelected.Count == 1)
                            {
                                month1 = genRTC.GetTotalReorderByCategoryGivenMonth(duratnSelected[0], suppl, cat);
                                input = new TrendReport(supplierName, categoryName, month1, duratnSelected[0]);
                                basicReportObjects.Add(input);
                            }
                            else if (duratnSelected.Count == 2)
                            {
                                month1 = genRTC.GetTotalReorderByCategoryGivenMonth(duratnSelected[0], suppl, cat);
                                month2 = genRTC.GetTotalReorderByCategoryGivenMonth(duratnSelected[1], suppl, cat);
                                input = new TrendReport(supplierName, categoryName, month1, month2, duratnSelected[0], duratnSelected[1]);
                                basicReportObjects.Add(input);
                            }
                            else
                            {
                                month1 = genRTC.GetTotalReorderByCategoryGivenMonth(duratnSelected[0], suppl, cat);
                                month2 = genRTC.GetTotalReorderByCategoryGivenMonth(duratnSelected[1], suppl, cat);
                                month3 = genRTC.GetTotalReorderByCategoryGivenMonth(duratnSelected[2], suppl, cat);
                                input = new TrendReport(supplierName, categoryName, month1, month2, month3, duratnSelected[0], duratnSelected[1], duratnSelected[2]);
                                basicReportObjects.Add(input);
                            }
                        }
                    }
                    Session["typeOfReport"] = 2;
                }
                else
                {
                    foreach (string cat in catSelected)
                    {
                        string categoryName = cat;
                        string supplierName = "";
                        int month1 = 0, month2 = 0, month3 = 0;

                        foreach (string suppl in supplierSelected)
                        {
                            supplierName = suppl;
                            TrendReport input;
                            if (duratnSelected.Count == 1)
                            {
                                month1 = genRTC.GetTotalReorderByCategoryGivenMonth(duratnSelected[0], suppl, cat);
                                input = new TrendReport(supplierName, categoryName, month1, duratnSelected[0]);
                                basicReportObjects.Add(input);
                            }
                            else if (duratnSelected.Count == 2)
                            {
                                month1 = genRTC.GetTotalReorderByCategoryGivenMonth(duratnSelected[0], suppl, cat);
                                month2 = genRTC.GetTotalReorderByCategoryGivenMonth(duratnSelected[1], suppl, cat);
                                input = new TrendReport(supplierName, categoryName, month1, month2, duratnSelected[0], duratnSelected[1]);
                                basicReportObjects.Add(input);
                            }
                            else
                            {
                                month1 = genRTC.GetTotalReorderByCategoryGivenMonth(duratnSelected[0], suppl, cat);
                                month2 = genRTC.GetTotalReorderByCategoryGivenMonth(duratnSelected[1], suppl, cat);
                                month3 = genRTC.GetTotalReorderByCategoryGivenMonth(duratnSelected[2], suppl, cat);
                                input = new TrendReport(supplierName, categoryName, month1, month2, month3, duratnSelected[0], duratnSelected[1], duratnSelected[2]);
                                basicReportObjects.Add(input);
                            }

                        }
                    }
                    Session["typeOfReport"] = 3;
                }
                Session["reportsToDisplay"] = basicReportObjects;

                Response.Redirect("~/Store/TrendReportDisplay.aspx");
            }
            else
            {
                Response.Write("<script>alert('" + Message.CustomFieldsNotSelected + "');</script>");
            }
        }
    }

    private List<string> GetSelectedSuppliers()
    {
        int selectedSuppl = (int)ViewState["sharedListSel"];
        List<string> supplNames = new List<string>();
        GenerateReorderTrendController grtc = new GenerateReorderTrendController();
        switch (selectedSuppl)
        {
            case 0:
                supplNames = grtc.GetAllSupplierNames();
                return supplNames;
            case 1:
                foreach (string gvr in (List<string>)ViewState["sharedListAdded"])
                {
                    supplNames.Add(gvr);
                }
                return supplNames;
            default:
                return supplNames;
        }
    }

    protected List<string> GetSelectedDepartments()
    {
        int selectedDept = (int)ViewState["sharedListSel"];
        List<string> deptNames = new List<string>();
        GenerateRequisitionTrendController grtc = new GenerateRequisitionTrendController();
        switch (selectedDept)
        {
            case 0:
                deptNames = grtc.GetAllDepartmentNames();
                return deptNames;
            case 1:
                foreach (string gvr in (List<string>)ViewState["sharedListAdded"])
                {
                    deptNames.Add(gvr);
                }
                return deptNames;
            default:
                return deptNames;
        }
    }

    protected List<string> GetSelectedCategories()
    {
        int selectedCat = (int)ViewState["catSel"];
        List<string> catNames = new List<string>();
        GenerateRequisitionTrendController grtc = new GenerateRequisitionTrendController();
        switch (selectedCat)
        {
            case 0:
                catNames = grtc.GetAllCategoryNames();
                return catNames;
            case 1:
                foreach (string gvr in (List<string>)ViewState["catAdded"])
                {
                    catNames.Add(gvr);
                }
                return catNames;
            default:
                return catNames;
        }
    }

    protected List<string> GetSelectedDuration()
    {
        int selectedDuration = (int)ViewState["durSel"];
        List<string> months = new List<string>();
        GenerateRequisitionTrendController grtc = new GenerateRequisitionTrendController();
        switch (selectedDuration)
        {
            case 0:
                DateTime dt = DateTime.Now.AddMonths(-2);
                for (int i = 0; i < 3; i++)
                {
                    string mthYr = dt.ToString("MMMM") + " " + dt.Year;
                    months.Add(mthYr);
                    dt = dt.AddMonths(1);
                }
                return months;
            case 1:
                string fromMonth = (string)ViewState["fromMonth"];
                months = grtc.Get3MonthsFromGivenMonth(fromMonth);
                return months;
            case 2:
                List<DateTime> period = new List<DateTime>();
                foreach (string m in (List<string>)ViewState["dateAdded"])
                {
                    string[] bm = m.Trim().Split(' ');
                    DateTime datte = new DateTime(int.Parse(bm[1]), DateTime.ParseExact(bm[0], "MMMM", CultureInfo.CurrentCulture).Month, 1);
                    period.Add(datte);
                }
                period = period.OrderBy(e => e).ToList();
                foreach (DateTime dtime in period)
                {
                    string month = dtime.ToString("MMMM");
                    string yr = dtime.Year.ToString();
                    string mYr = month + " " + yr;
                    months.Add(mYr);
                }
                return months;
            default:
                return months;
        }
    }

    protected bool VerifyAllCustomFieldsAreValid()
    {
        bool areAllFieldsValid = true;

        if ((int)ViewState["durSel"] == 2)
        {
            if (DurationGridView.Rows.Count == 0)
                areAllFieldsValid = false;
        }

        if ((int)ViewState["catSel"] == 1)
        {
            if (CategoryGridView.Rows.Count == 0)
                areAllFieldsValid = false;
        }

        if ((int)ViewState["sharedListSel"] == 1)
        {
            if (SharedGridView.Rows.Count == 0)
                areAllFieldsValid = false;
        }

        return areAllFieldsValid;
    }


    protected void FromDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["fromMonth"] = FromDropDownList.SelectedValue;
    }
}