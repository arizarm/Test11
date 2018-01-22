using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DeptBusinessLogic
/// </summary>
public class DeptBusinessLogic
{

    //
    // TODO: Add constructor logic here
    //
    public static List<Department> GetDepartList()
    {
        using (StationeryEntities smodel = new StationeryEntities())
        {

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
    {
        using (StationeryEntities smodel = new StationeryEntities())
        {

            return smodel.Departments.Where(x => x.DeptCode == depCode).First();
        }

    }

    public static Employee GetDHeadByDeptCode(string depCode)
    {
        using (StationeryEntities smodel = new StationeryEntities())
        {

            return smodel.Employees.Where(x => x.DeptCode == depCode && x.Role == "DepartmentHead").First();
        }

    }



    public static List<Employee> GetEmployeeListForActingDHead(string deptcode, int a)
    {
        using (StationeryEntities smodel = new StationeryEntities())
        {

            return smodel.Employees.Where
                    (p => p.DeptCode == deptcode && p.Role == "Employee" && p.EmpID != a).ToList<Employee>();
        }

    }


    public static Employee GetEmployeeListForActingDHeadSelected(string deptcode)
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

    public static List<Employee> GetEmployeeListForDRep(string deptcode, int a)
    {
        using (StationeryEntities smodel = new StationeryEntities())
        {

            return smodel.Employees.Where
                    (p => p.DeptCode == deptcode && p.IsTempHead == "N" && p.Role != "DepartmentHead" && p.EmpID != a).ToList<Employee>();
        }

    }

    public static Employee GetEmployeeListForDRepSelected(string deptcode)
    {
        using (StationeryEntities smodel = new StationeryEntities())
        {

            Employee e = smodel.Employees.Where
                    (p => p.DeptCode == deptcode && p.Role == "Representative").First();
            return e;

        }

    }

    public static List<CollectionPoint> GetCollectionPointList()
    {
        using (StationeryEntities smodel = new StationeryEntities())
        {

            return smodel.CollectionPoints.ToList<CollectionPoint>();

        }

    }
    public static string GetDepartmentForCollectionPointSelected(string deptcode)
    {
        using (StationeryEntities smodel = new StationeryEntities())
        {

            var d = smodel.Departments.Where(p => p.DeptCode == deptcode)
                            .Join(smodel.CollectionPoints, p => p.CollectionLocationID, c => c.CollectionLocationID, (p, c) => new { Department = p, CollectionPoint = c })
                            .Select(a => new { a.CollectionPoint.CollectionPoint1 }).First();
            return d.CollectionPoint1;



        }

    }

    public static void UpdateCollectionPoint(string depcode, int? collectpoint)
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