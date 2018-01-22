using System;
using System.Transactions;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;

/// <summary>
/// Summary description for GenerateRequisitionTrendController
/// </summary>
public class GenerateRequisitionTrendController
{
    public List<string> getAllCategoryNames()
    {
        using (TransactionScope ts = new TransactionScope())
        {
            StationeryEntities se = new StationeryEntities();
            List<string> allCats = se.Categories.Select(c => c.CategoryName).ToList();
            ts.Complete();
            return allCats;
        }
    }

    public List<string> getAllDepartmentNames()
    {
        using (TransactionScope ts = new TransactionScope())
        {
            StationeryEntities se = new StationeryEntities();
            List<string> allDepts = se.Departments.Where(a => a.CollectionLocationID != null).Select(c => c.DeptName).ToList();
            ts.Complete();
            return allDepts;
        }
    }

    //(1a) below will be in DAO/ bizcontroller
    public List<DateTime?> getAllRequisitionMonths()
    {
        using (TransactionScope ts = new TransactionScope())
        {
            StationeryEntities se = new StationeryEntities();

            List<DateTime?> allMonths = se.Requisitions.Where(b => b.Status == "Closed" || b.Status == "Approved").Select(c => c.RequestDate).ToList();

            ts.Complete();
            return allMonths;
        }
    }

    //(1b) below will be in useCaseController
    public List<string> getUniqueRequisitionMonths()
    {
        List<DateTime?> allMonths = getAllRequisitionMonths();

        List<string> allMonthsInString = new List<string>();
        List<string> uniqueMonths = new List<string>();

        foreach (DateTime d in allMonths)
        {
            string month = d.ToString("MMMM");
            string year = d.Year.ToString();
            string entry = month + " " + year;

            allMonthsInString.Add(entry);
        }

        uniqueMonths = allMonthsInString.Distinct().ToList();

        return uniqueMonths;
    }

    //(1c) below will be in useCaseController
    public List<string> getRequisitionsUpTo2MonthsAgo()
    {
        List<DateTime?> allMonths = getAllRequisitionMonths();

        List<string> allMonthsInString = new List<string>();
        List<string> uniqueMonths = new List<string>();

        DateTime maxMonth = DateTime.Now.AddMonths(-2);

        foreach (DateTime d in allMonths)
        {
            if (d > maxMonth)
                continue;
            else
            {
                string month = d.ToString("MMMM");
                string year = d.Year.ToString();
                string entry = month + " " + year;

                allMonthsInString.Add(entry);
            }
        }

        uniqueMonths = allMonthsInString.Distinct().ToList();

        return uniqueMonths;
    }

    public List<string> get3MonthsFromGivenMonth(string month)
    {
        List<string> listOfMth = new List<string>();

        string[] givenDate = month.Split(' ');
        //Format of date given is in 'Month Year'. Split to get each separately
        DateTime dt = new DateTime(int.Parse(givenDate[1]), DateTime.ParseExact(givenDate[0], "MMMM", CultureInfo.CurrentCulture).Month, 1);
        //Put it back together to get next 2 months data, then split again to get months in string
        listOfMth.Add(month);
        for (int i = 0; i < 2; i++)
        {
            dt = dt.AddMonths(1);
            string mth = dt.ToString("MMMM");
            string yr = dt.Year.ToString();
            string mthyr = mth + " " + yr;
            listOfMth.Add(mthyr);
        }
        return listOfMth;
    }

    public int getTotalRequisitionByCategoryGivenMonth(string month, string dept, string cat)
    {
        List<DateTime> startEndDate = getStartDateEndDateForGivenMonth(month);
        DateTime startDate = startEndDate[0];
        DateTime endDate = startEndDate[1];

        using (TransactionScope ts = new TransactionScope())
        {
            StationeryEntities se = new StationeryEntities();

            var totalR = from ri in se.Requisition_Item
                         from r in se.Requisitions
                         from i in se.Items
                         from d in se.Departments
                         from e in se.Employees
                         from c in se.Categories
                         where ri.ItemCode == i.ItemCode
                         where ri.RequisitionID == r.RequisitionID
                         where r.ApprovedBy == e.EmpID
                         where e.DeptCode == d.DeptCode
                         where d.DeptName == dept
                         where i.CategoryID == c.CategoryID
                         where c.CategoryName == cat
                         where r.RequestDate >= startDate
                         where r.RequestDate <= endDate
                         select ri.RequestedQty;

            int? requisitionsForGivenMonth = totalR.Sum();

            int returnedQ = 0;
            if (requisitionsForGivenMonth > 0)
                returnedQ = (int)requisitionsForGivenMonth;

            ts.Complete();
            return returnedQ;
        }
    }

    //getTotalRequisitionByCategoryGivenMonth() needs this function
    protected List<DateTime> getStartDateEndDateForGivenMonth(string month)
    {
        List<DateTime> dtList = new List<DateTime>();
        string[] date = month.Split(' ');

        DateTime dtStartDate = new DateTime(int.Parse(date[1]), DateTime.ParseExact(date[0], "MMMM", CultureInfo.CurrentCulture).Month, 1);
        DateTime dtEndDate = dtStartDate.AddMonths(1).AddDays(-1);

        dtList.Add(dtStartDate); dtList.Add(dtEndDate);

        return dtList;
    }
}