using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ReqisitionListItem
/// </summary>
/// 
//AUTHOR : KHIN MO MO ZIN
public class ReqisitionListItem
{
    private string date;
    private int requisitionNo;
    private string department;
    private string status;
    private string employeeName;

    public string Date
    {
        get
        {
            return date;
        }

        set
        {
            date = value;
        }
    }

    public int RequisitionNo
    {
        get
        {
            return requisitionNo;
        }

        set
        {
            requisitionNo = value;
        }
    }

    public string Department
    {
        get
        {
            return department;
        }

        set
        {
            department = value;
        }
    }

    public string Status
    {
        get
        {
            return status;
        }

        set
        {
            status = value;
        }
    }

    public string EmployeeName
    {
        get
        {
            return employeeName;
        }

        set
        {
            employeeName = value;
        }
    }

    public ReqisitionListItem(string date, int requisitionNo, string department, string status, string employeeName)
    {
        this.Date = date;
        this.RequisitionNo = requisitionNo;
        this.Department = department;
        this.Status = status;
        this.EmployeeName = employeeName;
    }

}