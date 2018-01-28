using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;

/// <summary>
/// Summary description for EFBroker_Department
/// </summary>
public class EFBroker_Department
{
    public EFBroker_Department()
    {
    }

    public static List<string> GetAllDepartmentNames()
    {
        using (TransactionScope ts = new TransactionScope())
        {
            StationeryEntities SE = new StationeryEntities();
            List<string> allDepts = SE.Departments.Where(a => a.CollectionLocationID != null).Select(c => c.DeptName).ToList();
            ts.Complete();
            return allDepts;
        }
    }

    public static List<Department> GetDepartmentList()
    {
        using (TransactionScope ts = new TransactionScope())
        {
            StationeryEntities SE = new StationeryEntities();
            List<Department> allDepts = SE.Departments.ToList();
            ts.Complete();
            return allDepts;
        }
    }


}