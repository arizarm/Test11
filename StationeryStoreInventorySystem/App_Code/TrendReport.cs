using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for TrendReport
/// </summary>
public class TrendReport
{
    string supplanddeptName;
    string categoryName;
    string month1Name, month2Name, month3Name;
    int month1, month2, month3;

    public TrendReport(string suppldeptN, string catN, int month1, int month2, int month3, string month1Name, string month2Name, string month3Name)
    {
        this.Month1 = month1;
        this.Month2 = month2;
        this.Month3 = month3;
        supplanddeptName = suppldeptN;
        categoryName = catN;
        this.Month1Name = month1Name;
        this.Month2Name = month2Name;
        this.Month3Name = month3Name;
    }

    public TrendReport(string suppldeptN, string catN, int month1, int month2, string month1Name, string month2Name)
    {
        this.Month1 = month1;
        this.Month2 = month2;
        this.Month3 = 0;
        supplanddeptName = suppldeptN;
        categoryName = catN;
        this.Month1Name = month1Name;
        this.Month2Name = month2Name;
        this.Month3Name = "";
    }

    public TrendReport(string suppldeptN, string catN, int month1, string month1Name)
    {
        this.Month1 = month1;
        this.Month2 = 0;
        this.Month3 = 0;
        supplanddeptName = suppldeptN;
        categoryName = catN;
        this.Month1Name = month1Name;
        this.Month2Name = "";
        this.Month3Name = "";
    }

    public string DepartmentName
    {
        get
        {
            return supplanddeptName;
        }

        set
        {
            supplanddeptName = value;
        }
    }

    public string CategoryName
    {
        get
        {
            return categoryName;
        }

        set
        {
            categoryName = value;
        }
    }

    public int Month1
    {
        get
        {
            return month1;
        }

        set
        {
            month1 = value;
        }
    }

    public int Month2
    {
        get
        {
            return month2;
        }

        set
        {
            month2 = value;
        }
    }

    public int Month3
    {
        get
        {
            return month3;
        }

        set
        {
            month3 = value;
        }
    }

    public string Month1Name
    {
        get
        {
            return month1Name;
        }

        set
        {
            month1Name = value;
        }
    }

    public string Month2Name
    {
        get
        {
            return month2Name;
        }

        set
        {
            month2Name = value;
        }
    }

    public string Month3Name
    {
        get
        {
            return month3Name;
        }

        set
        {
            month3Name = value;
        }
    }
}