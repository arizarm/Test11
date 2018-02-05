using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for EFBroker_Employee
/// </summary>
/// 
//AUTHOR : TAN WEN SONG
//AUTHOR : YIMON SOE
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

    public static Employee GetHeadEmail(Employee e)
    {
        Employee currentHead = new Employee();
        StationeryEntities context = new StationeryEntities();
        currentHead = context.Employees.Where(em => em.DeptCode.Equals(e.DeptCode) && em.IsTempHead.Equals("Y")).FirstOrDefault();
        if (currentHead != null)
        {
            if (!Utility.checkIsTempDepHead(currentHead))
            {
                currentHead = context.Employees.Where(em => em.DeptCode.Equals(e.DeptCode) && em.Role.Equals("DepartmentHead")).FirstOrDefault();
            }
        }
       
        return currentHead;
    }
    public static bool verifyLogin(string email, string password)
    {
        using (StationeryEntities context = new StationeryEntities())
        {
            if (context.Employees.Where(x => x.Email == email).Count() == 1)
            {    //Check if email exists
                string passHash = context.Employees.Where(x => x.Email == email).Select(y => y.Password).First();
                if (DevOne.Security.Cryptography.BCrypt.BCryptHelper.CheckPassword(password, passHash))
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
            string dep = context.Employees.Where(e => e.EmpID.Equals(id)).Select(e => e.DeptCode).FirstOrDefault();
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

    public static bool isDeptHaveTempHead(string deptCode)
    {
        using (StationeryEntities context = new StationeryEntities())
        {
            List<Employee> deptEmpList = context.Employees.Where(x => x.DeptCode.Equals(deptCode) && x.IsTempHead == "Y").ToList<Employee>();
            if(deptEmpList.Count >=1)
            {
                for (int i = 0; i < deptEmpList.Count; i++)
                {
                    Employee emp = (Employee)deptEmpList[i];
                    if (Utility.checkIsTempDepHead(emp))
                    {
                        return true;
                    }
                }
                return false;
            }
            else
            {
                return false;
            }    
        }
    }

}