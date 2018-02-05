using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


//AUTHOR : ARIZ ARMAND BIN ABDUL RAHMAN
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
                    LblDepartment.Visible = true;
                    RBtnLDepartment.Visible = true;
                    RBtnLSplit.Visible = true;
                    LblHeader.Text = Message.RTReport;
                    break;
                case "ROR":
                    DisplayCommonComponents();
                    SupplierLabel.Visible = true;
                    RBtnLSupplier.Visible = true;
                    RBtnLSplitROR.Visible = true;
                    LblHeader.Text = Message.ROReport;
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
        TblMain.Visible = true;
        LblDuration.Visible = true;
        LblSelectCategory.Visible = true;
        RBtnLDuration.Visible = true;
        RBtnLCategory.Visible = true;
        LblSplit.Visible = true;
        BtnGenerate.Visible = true;
    }

    protected void RBtnLDuration_SelectedIndexChanged(object sender, EventArgs e)
    {
        string type = Request.QueryString["type"];
        int selectedIndex = RBtnLDuration.SelectedIndex;
        if (type.Equals("RTR"))
        {
            switch (selectedIndex)
            {
                case 0:
                    LblFrom.Visible = false;
                    DDLFrom.Visible = false;
                    DDLDuration.Visible = false;
                    BtnDurationAdd.Visible = false;
                    GVDuration.Visible = false;
                    LblDurAlert.Visible = false;
                    ViewState["durSel"] = 0;
                    break;
                case 1:
                    LblFrom.Visible = true;
                    DDLFrom.Visible = true;
                    DDLDuration.Visible = false;
                    BtnDurationAdd.Visible = false;
                    GVDuration.Visible = false;
                    LblDurAlert.Visible = false;
                    ViewState["durSel"] = 1;
                    GenerateRequisitionTrendController grtc = new GenerateRequisitionTrendController();
                    List<string> allMonths = grtc.GetRequisitionsUpTo2MonthsAgo();
                    DDLFrom.DataSource = allMonths;
                    DDLFrom.DataBind();
                    ViewState["fromMonth"] = DDLFrom.SelectedValue;
                    break;
                case 2:
                    LblFrom.Visible = false;
                    DDLFrom.Visible = false;
                    DDLDuration.Visible = true;
                    BtnDurationAdd.Visible = true;
                    GVDuration.Visible = true;
                    ViewState["durSel"] = 2;
                    GenerateRequisitionTrendController Grtc1 = new GenerateRequisitionTrendController();
                    List<string> FromMths = Grtc1.GetUniqueRequisitionMonths();
                    DDLDuration.DataSource = FromMths;
                    DDLDuration.DataBind();
                    break;
            }
        }
        else
        {
            switch (selectedIndex)
            {
                case 0:
                    LblFrom.Visible = false;
                    DDLFrom.Visible = false;
                    DDLDuration.Visible = false;
                    BtnDurationAdd.Visible = false;
                    GVDuration.Visible = false;
                    LblDurAlert.Visible = false;
                    ViewState["durSel"] = 0;
                    break;
                case 1:
                    LblFrom.Visible = true;
                    DDLFrom.Visible = true;
                    DDLDuration.Visible = false;
                    BtnDurationAdd.Visible = false;
                    GVDuration.Visible = false;
                    LblDurAlert.Visible = false;
                    ViewState["durSel"] = 1;
                    GenerateReorderTrendController grtc = new GenerateReorderTrendController();
                    List<string> allMonths = grtc.GetRequisitionsUpTo2MonthsAgo();
                    DDLFrom.DataSource = allMonths;
                    DDLFrom.DataBind();
                    ViewState["fromMonth"] = DDLFrom.SelectedValue;
                    break;
                case 2:
                    LblFrom.Visible = false;
                    DDLFrom.Visible = false;
                    DDLDuration.Visible = true;
                    BtnDurationAdd.Visible = true;
                    GVDuration.Visible = true;
                    ViewState["durSel"] = 2;
                    GenerateReorderTrendController Grtc1 = new GenerateReorderTrendController();
                    List<string> FromMths = Grtc1.GetUniqueRequisitionMonths();
                    DDLDuration.DataSource = FromMths;
                    DDLDuration.DataBind();
                    break;
            }
        }
    }

    protected void RBtnLCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        int selectedIndex = RBtnLCategory.SelectedIndex;
        string type = Request.QueryString["type"];
        if (type.Equals("RTR"))
        {
            switch (selectedIndex)
            {
                case 0:
                    DDLCategory.Visible = false;
                    BtnCategoryAdd.Visible = false;
                    GVCategory.Visible = false;
                    LblCatAlert.Visible = false;
                    ViewState["catSel"] = 0;
                    break;
                case 1:
                    DDLCategory.Visible = true;
                    BtnCategoryAdd.Visible = true;
                    GVCategory.Visible = true;
                    ViewState["catSel"] = 1;
                    GenerateRequisitionTrendController grtc = new GenerateRequisitionTrendController();
                    List<string> CatNames = grtc.GetAllCategoryNames();
                    DDLCategory.DataSource = CatNames;
                    DDLCategory.DataBind();
                    break;
            }
        }
        else
        {
            switch (selectedIndex)
            {
                case 0:
                    DDLCategory.Visible = false;
                    BtnCategoryAdd.Visible = false;
                    GVCategory.Visible = false;
                    LblCatAlert.Visible = false;
                    ViewState["catSel"] = 0;
                    break;
                case 1:
                    DDLCategory.Visible = true;
                    BtnCategoryAdd.Visible = true;
                    GVCategory.Visible = true;
                    ViewState["catSel"] = 1;
                    GenerateReorderTrendController grtc = new GenerateReorderTrendController();
                    List<string> CatNames = grtc.GetAllCategoryNames();
                    DDLCategory.DataSource = CatNames;
                    DDLCategory.DataBind();
                    break;
            }
        }
    }

    protected void RBtnLDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        int SelectedIndex = RBtnLDepartment.SelectedIndex;
        switch (SelectedIndex)
        {
            case 0:
                DDLShared.Visible = false;
                BtnSharedAdd.Visible = false;
                GVShared.Visible = false;
                LblSharedAlert.Visible = false;
                ViewState["sharedListSel"] = 0;
                break;
            case 1:
                DDLShared.Visible = true;
                BtnSharedAdd.Visible = true;
                GVShared.Visible = true;
                ViewState["sharedListSel"] = 1;
                GenerateRequisitionTrendController grtc = new GenerateRequisitionTrendController();
                List<string> deptNames = grtc.GetAllDepartmentNames();
                DDLShared.DataSource = deptNames;
                DDLShared.DataBind();
                break;
        }

    }

    protected void RBtnLSupplier_SelectedIndexChanged(object sender, EventArgs e)
    {
        int SelectedIndex = RBtnLSupplier.SelectedIndex;
        switch (SelectedIndex)
        {
            case 0:
                DDLShared.Visible = false;
                BtnSharedAdd.Visible = false;
                GVShared.Visible = false;
                LblSharedAlert.Visible = false;
                ViewState["sharedListSel"] = 0;
                break;
            case 1:
                DDLShared.Visible = true;
                BtnSharedAdd.Visible = true;
                GVShared.Visible = true;
                ViewState["sharedListSel"] = 1;
                GenerateReorderTrendController grtc = new GenerateReorderTrendController();
                List<string> supplierNames = grtc.GetAllSupplierNames();
                DDLShared.DataSource = supplierNames;
                DDLShared.DataBind();
                break;
        }

    }


    protected void BtnCategoryAdd_Click(object sender, EventArgs e)
    {
        string addMe = DDLCategory.SelectedItem.Text;

        if (((List<string>)ViewState["catAdded"]).Count == 0)
        {
            ((List<string>)ViewState["catAdded"]).Add(addMe);
            GVCategory.DataSource = ((List<string>)ViewState["catAdded"]);
            GVCategory.DataBind();
            LblCatAlert.Visible = false;
        }
        else
        {
            for (int i = 0; i < ((List<string>)ViewState["catAdded"]).Count; i++)
            {
                if (((List<string>)ViewState["catAdded"])[i].ToString() == addMe)
                {
                    Utility.DisplayAlertMessage(Message.CategoryAlreadyInList);
                    break;
                }
                if (i == ((List<string>)ViewState["catAdded"]).Count - 1)
                {
                    ((List<string>)ViewState["catAdded"]).Add(addMe);
                    GVCategory.DataSource = ((List<string>)ViewState["catAdded"]);
                    GVCategory.DataBind();
                    LblCatAlert.Visible = false;
                    break;
                }
            }
        }
    }

    protected void BtnRemoveCategory_Click(object sender, EventArgs e)
    {
        //get row position of item
        GridViewRow row = ((System.Web.UI.WebControls.Button)sender).Parent.Parent as GridViewRow;

        ((List<string>)ViewState["catAdded"]).RemoveAt(row.RowIndex);
        GVCategory.DataSource = ((List<string>)ViewState["catAdded"]);
        GVCategory.DataBind();
    }

    protected void BtnRemoveShared_Click(object sender, EventArgs e)
    {
        GridViewRow row = ((System.Web.UI.WebControls.Button)sender).Parent.Parent as GridViewRow;

        ((List<string>)ViewState["sharedListAdded"]).RemoveAt(row.RowIndex);
        GVShared.DataSource = ((List<string>)ViewState["sharedListAdded"]);
        GVShared.DataBind();
    }

    protected void BtnSharedAdd_Click(object sender, EventArgs e)
    {
        string addMe = DDLShared.SelectedItem.Text;

        if (((List<string>)ViewState["sharedListAdded"]).Count == 0)
        {
            ((List<string>)ViewState["sharedListAdded"]).Add(addMe);
            GVShared.DataSource = ((List<string>)ViewState["sharedListAdded"]);
            GVShared.DataBind();
            LblSharedAlert.Visible = false;
        }
        else
        {
            for (int i = 0; i < ((List<string>)ViewState["sharedListAdded"]).Count; i++)
            {
                if (((List<string>)ViewState["sharedListAdded"])[i].ToString() == addMe)
                {
                    if (ReportToSelect == "RTR")
                        Utility.DisplayAlertMessage(Message.DepartmentAlreadyInList);
                    else if (ReportToSelect == "ROR")
                        Utility.DisplayAlertMessage(Message.SupplierAlreadyInList);
                    break;
                }
                if (i == ((List<string>)ViewState["sharedListAdded"]).Count - 1)
                {
                    ((List<string>)ViewState["sharedListAdded"]).Add(addMe);
                    GVShared.DataSource = ((List<string>)ViewState["sharedListAdded"]);
                    GVShared.DataBind();
                    LblSharedAlert.Visible = false;
                    break;
                }
            }
        }
    }


    protected void BtnDurationAdd_Click(object sender, EventArgs e)
    {
        string addMe = DDLDuration.SelectedItem.Text;

        if (((List<string>)ViewState["dateAdded"]).Count == 0)
        {
            ((List<string>)ViewState["dateAdded"]).Add(addMe);
            GVDuration.DataSource = ((List<string>)ViewState["dateAdded"]);
            GVDuration.DataBind();
            LblDurAlert.Visible = false;
        }
        else if (((List<string>)ViewState["dateAdded"]).Count == 1 || (((List<string>)ViewState["dateAdded"]).Count == 2))
        {
            for (int i = 0; i < ((List<string>)ViewState["dateAdded"]).Count; i++)
            {
                if (((List<string>)ViewState["dateAdded"])[i].ToString() == addMe)
                {
                    Utility.DisplayAlertMessage(Message.DateAlreadyInList);
                    break;
                }
                if (i == ((List<string>)ViewState["dateAdded"]).Count - 1)
                {
                    ((List<string>)ViewState["dateAdded"]).Add(addMe);
                    GVDuration.DataSource = ((List<string>)ViewState["dateAdded"]);
                    GVDuration.DataBind();
                    LblDurAlert.Visible = false;
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
                    Utility.DisplayAlertMessage(Message.DateAlreadyInList);
                    break;
                }
                if (i == ((List<string>)ViewState["dateAdded"]).Count - 1)
                {
                    ((List<string>)ViewState["dateAdded"]).RemoveAt(0);
                    ((List<string>)ViewState["dateAdded"]).Add(addMe);
                    GVDuration.DataSource = ((List<string>)ViewState["dateAdded"]);
                    GVDuration.DataBind();
                    LblDurAlert.Visible = false;
                    break;
                }
            }
        }

    }

    protected void BtnRemoveDuration_Click(object sender, EventArgs e)
    {
        GridViewRow row = ((System.Web.UI.WebControls.Button)sender).Parent.Parent as GridViewRow;

        ((List<string>)ViewState["dateAdded"]).RemoveAt(row.RowIndex);
        GVDuration.DataSource = ((List<string>)ViewState["dateAdded"]);
        GVDuration.DataBind();
    }

    protected void BtnGenerate_Click(object sender, EventArgs e)
    {
        try
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

                    int splitBySelected = RBtnLSplit.SelectedIndex;
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
                    //Utility.DisplayAlertMessage(Message.CustomFieldsNotSelected);
                    if ((int)ViewState["catSel"] == 1 && GVCategory.Rows.Count == 0)
                        LblCatAlert.Visible = true;
                    if ((int)ViewState["durSel"] == 2 && GVDuration.Rows.Count == 0)
                        LblDurAlert.Visible = true;
                    if ((int)ViewState["sharedListSel"] == 1 && GVShared.Rows.Count == 0)
                        LblSharedAlert.Visible = true;
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

                    int splitBySelected = RBtnLSplitROR.SelectedIndex;
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
                    //Utility.DisplayAlertMessage(Message.CustomFieldsNotSelected);
                    if ((int)ViewState["catSel"] == 1 && GVCategory.Rows.Count == 0)
                        LblCatAlert.Visible = true;
                    if ((int)ViewState["durSel"] == 2 && GVDuration.Rows.Count == 0)
                        LblDurAlert.Visible = true;
                    if ((int)ViewState["sharedListSel"] == 1 && GVShared.Rows.Count == 0)
                        LblSharedAlert.Visible = true;
                }
            }
        }
        catch (Exception)
        {
            Utility.DisplayAlertMessage(Message.GeneralError);
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
            if (GVDuration.Rows.Count == 0)
                areAllFieldsValid = false;
        }

        if ((int)ViewState["catSel"] == 1)
        {
            if (GVCategory.Rows.Count == 0)
                areAllFieldsValid = false;
        }

        if ((int)ViewState["sharedListSel"] == 1)
        {
            if (GVShared.Rows.Count == 0)
                areAllFieldsValid = false;
        }

        return areAllFieldsValid;
    }


    protected void DDLFrom_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["fromMonth"] = DDLFrom.SelectedValue;
    }
}