using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for EFBroker_Employee
/// </summary>
public class EFBroker_Employee
{
    public EFBroker_Employee()
    {
    }

    public static List<Employee> GetEmployeeList()
    {
        List<Employee> employeeList;
        using (StationeryEntities inventoryDB = new StationeryEntities())
        {
            employeeList = inventoryDB.Employees.ToList();
        }
        return employeeList;
    }

    public static Employee GetEmployee(int id)
    {
        Employee e = new Employee();
        using (StationeryEntities context = new StationeryEntities())
        {
            e = context.Employees.Where(em => em.EmpID == id).FirstOrDefault();
        }
        return e;
    }
}