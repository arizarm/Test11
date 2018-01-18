using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for EmployeeController
/// </summary>
public class EmployeeController
{
    public EmployeeController()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public bool verifyLogin(string email, string password)
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

    public Employee GetEmployeeByEmail(string email)
    {
        using (StationeryEntities context = new StationeryEntities())
        {
            return context.Employees.Where(x => x.Email == email).First();
        }

    }
}
