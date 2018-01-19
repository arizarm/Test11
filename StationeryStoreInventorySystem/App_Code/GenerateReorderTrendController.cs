using System;
using System.Transactions;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for GenerateReorderTrendController
/// </summary>
public class GenerateReorderTrendController
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

    public List<string> getAllSupplierNames()
    {
        using (TransactionScope ts = new TransactionScope())
        {
            StationeryEntities se = new StationeryEntities();
            List<string> allSupls = se.Suppliers.Select(c => c.SupplierName).ToList();
            ts.Complete();
            return allSupls;
        }
    }

    // WRONG. CHG WITH ReOrder Data
    // WRONG. CHG WITH ReOrder Data
    // WRONG. CHG WITH ReOrder Data
    // WRONG. CHG WITH ReOrder Data
    // WRONG. CHG WITH ReOrder Data
    // WRONG. CHG WITH ReOrder Data

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

}