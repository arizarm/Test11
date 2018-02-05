using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for EmployeeController
/// </summary>
/// 
//AUTHOR : YIMON SOE
public class EmployeeController
{
    public EmployeeController()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static Employee GetDeptHeadTempHeadEmail(Employee em)
    {
        return EFBroker_Employee.GetHeadEmail(em);
    }

    public static List<String> getAllClerkMails()
    {
        return EFBroker_Employee.getAllClerkMails();
    }


    public static bool isDeptHaveTempHead(string deptCode)
    {
        return EFBroker_Employee.isDeptHaveTempHead(deptCode);
    }
}