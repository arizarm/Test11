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
    public List<string> GetAllCategoryNames()
    {
        List<string> allCategoryNames = EFBroker_Category.GetAllCategoryNames();
        return allCategoryNames;
    }

    public List<string> GetAllDepartmentNames()
    {
        List<string> allDepts = EFBroker_Department.GetAllDepartmentNames();
        return allDepts;
    }

    public List<DateTime?> GetAllRequisitionMonths()
    {
        List<DateTime?> allMonths = EFBroker_Requisition.GetAllFinalisedRequisitionMonths();
        return allMonths;
    }

    //(1b) below will be in useCaseController
    public List<string> GetUniqueRequisitionMonths()
    {
        List<DateTime?> allMonths = GetAllRequisitionMonths();

        List<string> allMonthsInString = new List<string>();
        List<string> uniqueMonths = new List<string>();

        foreach (DateTime D in allMonths)
        {
            string month = D.ToString("MMMM");
            string year = D.Year.ToString();
            string entry = month + " " + month;

            allMonthsInString.Add(entry);
        }

        uniqueMonths = allMonthsInString.Distinct().ToList();

        return uniqueMonths;
    }

    //(1c) below will be in useCaseController
    public List<string> GetRequisitionsUpTo2MonthsAgo()
    {
        List<DateTime?> allMonths = GetAllRequisitionMonths();

        List<string> allMonthsInString = new List<string>();
        List<string> uniqueMonths = new List<string>();

        DateTime maxMonth = DateTime.Now.AddMonths(-2);

        foreach (DateTime D in allMonths)
        {
            if (D > maxMonth)
                continue;
            else
            {
                string Month = D.ToString("MMMM");
                string Year = D.Year.ToString();
                string Entry = Month + " " + Year;

                allMonthsInString.Add(Entry);
            }
        }

        uniqueMonths = allMonthsInString.Distinct().ToList();

        return uniqueMonths;
    }

    public List<string> Get3MonthsFromGivenMonth(string month)
    {
        List<string> listOfMth = new List<string>();

        string[] givenDate = month.Split(' ');
        //Format of date given is in 'Month Year'. Split to get each separately
        DateTime DT = new DateTime(int.Parse(givenDate[1]), DateTime.ParseExact(givenDate[0], "MMMM", CultureInfo.CurrentCulture).Month, 1);
        //Put it back together to get next 2 months data, then split again to get months in string
        listOfMth.Add(month);
        for (int i = 0; i < 2; i++)
        {
            DT = DT.AddMonths(1);
            string Month = DT.ToString("MMMM");
            string year = DT.Year.ToString();
            string monthYear = Month + " " + year;
            listOfMth.Add(monthYear);
        }
        return listOfMth;
    }

    public int GetTotalRequisitionByCategoryGivenMonth(string month, string dept, string cat)
    {
        List<DateTime> startEndDate = GetStartDateEndDateForGivenMonth(month);
        DateTime startDate = startEndDate[0];
        DateTime endDate = startEndDate[1];

        using (TransactionScope ts = new TransactionScope())
        {
            StationeryEntities SE = new StationeryEntities();

            var TotalR = from ri in SE.Requisition_Item
                         from r in SE.Requisitions
                         from i in SE.Items
                         from d in SE.Departments
                         from e in SE.Employees
                         from c in SE.Categories
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

            int? requisitionsForGivenMonth = TotalR.Sum();

            int returnedQ = 0;
            if (requisitionsForGivenMonth > 0)
                returnedQ = (int)requisitionsForGivenMonth;

            ts.Complete();
            return returnedQ;
        }
    }

    //getTotalRequisitionByCategoryGivenMonth() needs this function
    protected List<DateTime> GetStartDateEndDateForGivenMonth(string month)
    {
        List<DateTime> DTList = new List<DateTime>();
        string[] date = month.Split(' ');

        DateTime DTStartDate = new DateTime(int.Parse(date[1]), DateTime.ParseExact(date[0], "MMMM", CultureInfo.CurrentCulture).Month, 1);
        DateTime DTEndDate = DTStartDate.AddMonths(1).AddDays(-1);

        DTList.Add(DTStartDate); DTList.Add(DTEndDate);

        return DTList;
    }
}