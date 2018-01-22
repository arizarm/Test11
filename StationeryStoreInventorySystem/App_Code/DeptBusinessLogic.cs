﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DeptBusinessLogic
/// </summary>
public class DeptBusinessLogic
{
<<<<<<< HEAD
=======
   
        //
        // TODO: Add constructor logic here
        //
        public static List<Department> getDepartList()
         {
             using (StationeryEntities smodel = new StationeryEntities())
                {
                    
                    return smodel.Departments.ToList<Department>();
                }
>>>>>>> 1821b3f2585af8eb28971f23e40bd9d9c081c68b

    //
    // TODO: Add constructor logic here
    //
    public static List<Department> GetDepartList()
    {
        using (StationeryEntities smodel = new StationeryEntities())
        {

<<<<<<< HEAD
            return smodel.Departments.ToList<Department>();
        }

    }

    public static List<Employee> GetEmployeeList()
    {
        using (StationeryEntities smodel = new StationeryEntities())
        {

            return smodel.Employees.ToList<Employee>();
        }

    }

    public static Department GetDepartByDepCode(string depCode)
=======
    public static Department getDepartByDepCode(string depCode)
>>>>>>> 1821b3f2585af8eb28971f23e40bd9d9c081c68b
    {
        using (StationeryEntities smodel = new StationeryEntities())
        {

            return smodel.Departments.Where(x => x.DeptCode == depCode).First();
        }

    }

    public static Employee getEmployeeByDeptCode(string depCode)
    {
        using (StationeryEntities smodel = new StationeryEntities())
        {

            return smodel.Employees.Where(x => x.DeptCode == depCode && x.Role == "DepartmentHead").First();
        }

    }
<<<<<<< HEAD



    public static List<Employee> GetEmployeeListForActingDHead(string deptcode, int a)
=======
    public static List<Employee> getEmployeeListForActingDHead(string deptcode, int a)
>>>>>>> 1821b3f2585af8eb28971f23e40bd9d9c081c68b
    {
        using (StationeryEntities smodel = new StationeryEntities())
        {

            return smodel.Employees.Where
                    (p => p.DeptCode == deptcode && p.Role == "Employee" && p.EmpID != a).ToList<Employee>();
        }

    }

<<<<<<< HEAD

    public static Employee GetEmployeeListForActingDHeadSelected(string deptcode)
=======
    public static Employee getEmployeeListForActingDHeadSelected(string deptcode)
>>>>>>> 1821b3f2585af8eb28971f23e40bd9d9c081c68b
    {
        using (StationeryEntities smodel = new StationeryEntities())
        {

            Employee e = smodel.Employees.Where
                    (p => p.DeptCode == deptcode && p.IsTempHead == "Y").First();

            return e;

        }

    }

    public static int GetEmployeeListForActingDHeadSelectedCount(string deptcode)
    {
        using (StationeryEntities smodel = new StationeryEntities())
        {

            return smodel.Employees.Where
                    (p => p.DeptCode == deptcode && p.IsTempHead == "Y").Count();

            

        }

    }

<<<<<<< HEAD
    public static List<Employee> GetEmployeeListForDRep(string deptcode, int a)
=======
    public static List<Employee> getEmployeeListForDRep(string deptcode,int a)
>>>>>>> 1821b3f2585af8eb28971f23e40bd9d9c081c68b
    {
        using (StationeryEntities smodel = new StationeryEntities())
        {

            return smodel.Employees.Where
                    (p => p.DeptCode == deptcode && p.IsTempHead == "N" && p.Role != "DepartmentHead" && p.EmpID != a).ToList<Employee>();
        }

    }

    public static Employee getEmployeeListForDRepSelected(string deptcode)
    {
        using (StationeryEntities smodel = new StationeryEntities())
        {

            Employee e = smodel.Employees.Where
                    (p => p.DeptCode == deptcode && p.Role == "Representative").First();
            return e;

        }

    }

    public static List<CollectionPoint> getCollectionPointList()
    {
        using (StationeryEntities smodel = new StationeryEntities())
        {

            return smodel.CollectionPoints.ToList<CollectionPoint>();

        }

    }
    public static string getDepartmentForCollectionPointSelected(string deptcode)
    {
        using (StationeryEntities smodel = new StationeryEntities())
        {

            var d = smodel.Departments.Where(p => p.DeptCode == deptcode)
                            .Join(smodel.CollectionPoints, p => p.CollectionLocationID, c => c.CollectionLocationID, (p, c) => new { Department = p, CollectionPoint = c })
                            .Select(a => new { a.CollectionPoint.CollectionPoint1 }).First();
            return d.CollectionPoint1;



        }

    }

<<<<<<< HEAD
    public static void UpdateCollectionPoint(string depcode, int? collectpoint)
=======
    public static void UpdateCollectionPoint(string depcode,int collectpoint)
>>>>>>> 1821b3f2585af8eb28971f23e40bd9d9c081c68b
    {
        using (StationeryEntities smodel = new StationeryEntities())
        {
            Department dept = smodel.Departments.Where(p => p.DeptCode == depcode).First<Department>();
            dept.CollectionLocationID = collectpoint;
            smodel.SaveChanges();
        }
    }

    public static void UpdateDeptRep(string depcode, int empid)
    {
        using (StationeryEntities smodel = new StationeryEntities())
        {
            try
            {
                Employee orgemp = smodel.Employees.Where(q => q.DeptCode == depcode && q.Role == "Representative").First<Employee>();
                orgemp.Role = "Employee";
                Employee emp = smodel.Employees.Where(p => p.DeptCode == depcode && p.EmpID == empid).First<Employee>();
                emp.Role = "Representative";
                smodel.SaveChanges();
            }
            catch (Exception e)
            {

            }
        }
    }

    public static void UpdateActingDHead(string depcode, int empid, string sdate, string edate)
    {
        using (StationeryEntities smodel = new StationeryEntities())
        {
            try
            {
                if (DeptBusinessLogic.GetEmployeeListForActingDHeadSelectedCount(depcode) <= 0)
                {
                    Employee emp = smodel.Employees.Where(p => p.DeptCode == depcode && p.EmpID == empid).First<Employee>();
                    emp.IsTempHead = "Y";

                    emp.StartDate = Convert.ToDateTime(sdate);
                    emp.EndDate = Convert.ToDateTime(edate);
                    smodel.SaveChanges();
                }
                else
                {


                    Employee orgemp = smodel.Employees.Where(q => q.DeptCode == depcode && q.IsTempHead == "Y").First<Employee>();
                    orgemp.IsTempHead = "N";
                    orgemp.StartDate = null;
                    orgemp.EndDate = null;
                    Employee emp = smodel.Employees.Where(p => p.DeptCode == depcode && p.EmpID == empid).First<Employee>();
                    emp.IsTempHead = "Y";

                    emp.StartDate = Convert.ToDateTime(sdate);
                    emp.EndDate = Convert.ToDateTime(edate);
                    smodel.SaveChanges();
                }
            }
            catch (Exception e)
            {

            }
        }
    }

    public static void UpdateRevoke()
    {
        StationeryEntities context = new StationeryEntities();
        Employee emp = context.Employees.Where(em => em.IsTempHead.Equals("Y")).FirstOrDefault();
        emp.IsTempHead = "N";
        emp.StartDate = null;
        emp.EndDate = null;
        context.SaveChanges();
       
    }
}