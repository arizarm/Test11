using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Summary description for EmployeeController
/// </summary>
public static class EmployeeController
{
    public static bool verifyLogin(string email, string password)
    {
        using (StationeryEntities context = new StationeryEntities())
        {
            if (context.Employees.Where(x => x.Email == email).Count() == 1)
            {    //Check if email exists
                if (context.Employees.Where(x => x.Email == email).Select(y => y.Password).First() == password)
                {      //Check if the password is correct

                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }

    public static Employee GetEmployeeByEmail(string email)
    {
        using (StationeryEntities context = new StationeryEntities())
        {
            return context.Employees.Where(x => x.Email == email).First();
        }

    }

    public static string getEmployeeDepartment(int id)
    {
        using (StationeryEntities context = new StationeryEntities())
        {
            string dep= context.Employees.Where(e => e.EmpID.Equals(id)).Select(e => e.DeptCode).FirstOrDefault();
            return context.Departments.Where(d => d.DeptCode.Equals(dep)).Select(d => d.DeptName).FirstOrDefault();
        }
    }

    //EMPLOYEE NAME
    public static string getEmployee(int id)
    {
        using (StationeryEntities context = new StationeryEntities())
        {
            return context.Employees.Where(e => e.EmpID.Equals(id)).Select(e => e.EmpName).FirstOrDefault();
        }
    }

    public static List<String> getAllClerkMails()
    {
        using (StationeryEntities context = new StationeryEntities())
        {
            return context.Employees.Where(e => e.Role.Equals("Store Clerk")).Select(e => e.Email).ToList();
        }
    }

    public static Department GetDepartByDepCode(string DepCode)
    {
        return EFBroker_DeptEmployee.GetDepartByDepCode(DepCode);
    }

    public static Employee GetDeptHeadTempHeadEmail(Employee em)
    {
        return EFBroker_DeptEmployee.GetHeadEmail(em);
    }

    public static Employee GetEmployee(int id)
    {
        return EFBroker_Employee.GetEmployee(id);
    }
}
