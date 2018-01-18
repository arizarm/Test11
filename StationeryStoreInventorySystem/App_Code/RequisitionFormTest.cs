using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Class1
/// </summary>
public class RequisitionFormTest
{
    public string name { get; set; }
    public string needed { get; set; }
    public string date { get; set; }
    public RequisitionFormTest(string name, string needed)
    {
        this.name = name;
        this.needed = needed;
        //
        // TODO: Add constructor logic here
        //
    }

    public RequisitionFormTest(string name, string needed, string date)
    {
        this.name = name;
        this.needed = needed;
        this.date = date;
        //
        // TODO: Add constructor logic here
        //
    }
    public static List<RequisitionFormTest> getDepartmentRequsition()
    {
        List <RequisitionFormTest> a= new List<RequisitionFormTest>();
        a.Add(new RequisitionFormTest("REGR", "5", "11/01/2017"));
        a.Add(new RequisitionFormTest("ZOOL", "3", "13/01/2017"));
        a.Add(new RequisitionFormTest("COMM", "2", "14/01/2017"));
        return a;
    }

    public static List<RequisitionFormTest> getOrderList()
    {
        List<RequisitionFormTest> a = new List<RequisitionFormTest>();
        a.Add(new RequisitionFormTest("PR213459875", "123340985098"));
        a.Add(new RequisitionFormTest("PR182352580", "313548109580"));
        a.Add(new RequisitionFormTest("PR124789257", "958095832130"));
        return a;
    }
    public static List<RequisitionFormTest> getRequisitionList()
    {
        List<RequisitionFormTest> a = new List<RequisitionFormTest>();
        a.Add(new RequisitionFormTest("Diana Prince", "ZOOL/114/17"));
        a.Add(new RequisitionFormTest("Bruce Wayne", "ZOOL/115/17"));
        a.Add(new RequisitionFormTest("Clark Kent", "ZOOL/116/17"));
        return a;
    }
    public static List<RequisitionFormTest> getRequisitionItem()
    {
        List<RequisitionFormTest> a = new List<RequisitionFormTest>();
        a.Add(new RequisitionFormTest("Clips Double 2\"", "10"));
        a.Add(new RequisitionFormTest("Short Hand Book", "45"));
        a.Add(new RequisitionFormTest("Stapler No. 28", "55"));
        return a;
    }
}