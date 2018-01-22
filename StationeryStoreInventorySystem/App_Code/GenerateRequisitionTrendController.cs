using System;
using System.Transactions;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
}