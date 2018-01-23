using System;
using System.Transactions;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;

/// <summary>
/// Summary description for GenerateReorderTrendController
/// </summary>
public class GenerateReorderTrendController
{
    public List<string> GetAllCategoryNames()
    {
        List<string> allCats = EFBroker_Category.GetAllCategoryNames();
        return allCats;
    }

    public List<string> GetAllSupplierNames()
    {
        List<string> allSupls = EFBroker_Supplier.ListAllSuppliers().Select(c => c.SupplierName).ToList();
        return allSupls;
    }

    public int GetTotalReorderByCategoryGivenMonth(string month, string supplier, string cat)
    {
        List<DateTime> startEndDate = GetStartDateEndDateForGivenMonth(month);
        DateTime startDate = startEndDate[0];
        DateTime endDate = startEndDate[1];

        int? reorderForGivenMonth = EFBroker_Report.GetReordersForGivenMonth(startDate, endDate, supplier, cat);

            int returnedQ = 0;
            if (reorderForGivenMonth > 0)
                returnedQ = (int)reorderForGivenMonth;

            return returnedQ;        
    }

    protected List<DateTime> GetStartDateEndDateForGivenMonth(string month)
    {
        List<DateTime> dtList = new List<DateTime>();
        string[] date = month.Split(' ');

        DateTime dtStartDate = new DateTime(int.Parse(date[1]), DateTime.ParseExact(date[0], "MMMM", CultureInfo.CurrentCulture).Month, 1);
        DateTime dtEndDate = dtStartDate.AddMonths(1).AddDays(-1);

        dtList.Add(dtStartDate); dtList.Add(dtEndDate);

        return dtList;
    }

}