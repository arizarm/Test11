using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Transactions;

/// <summary>
/// Summary description for EFBroker_DeptEmployee
/// </summary>
public class EFBroker_DeptEmployee
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
    public static List<string> GetAllDepartmentNames()
    {
        using (TransactionScope ts = new TransactionScope())
        {
            StationeryEntities SE = new StationeryEntities();
            List<string> allDepts = SE.Departments.Where(a => a.CollectionLocationID != null).Select(c => c.DeptName).ToList();
            ts.Complete();
            return allDepts;
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
<<<<<<< HEAD:StationeryStoreInventorySystem/App_Code/DeptBusinessLogic.cs

    public static IList GetDepartDetailInfoList()
    {
        using (StationeryEntities smodel = new StationeryEntities())
        {

            return smodel.Departments.Join(smodel.CollectionPoints,
                d => d.CollectionLocationID, c => c.CollectionLocationID,
                (d, c) => new { Department = d, CollectionPoint = c }).
                Join(smodel.Employees.Where(e => e.Role == "DepartmentHead"),
                f => f.Department.DeptCode, e => e.DeptCode,
                (f, e) => new { f.Department, f.CollectionPoint, Employee = e }).
                Select(x => new
                {
                    x.Department.DeptCode,
                    x.Department.DeptName,
                    x.Employee.EmpName,
                    x.Department.DeptContactName,
                    x.CollectionPoint.CollectionPoint1,
                    x.Department.DeptTelephone,
                    x.Department.DeptFax
                }).ToList ();
            
        }

=======
    public static Department GetDepartByEmpID(int empID)
    {
        Department dep;
        using (StationeryEntities smodel = new StationeryEntities())
        {
            dep=smodel.Employees.Where(x => x.EmpID == empID).Select(x=>x.Department).FirstOrDefault();
        }
        return dep;
>>>>>>> 78d17ece77be94045f99549cd09a91f32f56e79d:StationeryStoreInventorySystem/App_Code/EFBroker/EFBroker_DeptEmployee.cs
    }
    public static string GetCollectionPointbyDeptCode(string depCode)
    {
        string collectionPoint;
        using (StationeryEntities context = new StationeryEntities())
        {
            collectionPoint = context.Departments.Include("CollectionPoint").Where(x => x.DeptCode.Equals(depCode)).Select(x => x.CollectionPoint.CollectionPoint1).FirstOrDefault();
        }
        return collectionPoint;
    }
    public static List<Employee> GetEmployeeListByRole(string role)
    {  //goes to employee broker
        List<Employee> e;
        using (StationeryEntities context = new StationeryEntities())
        {
             e = context.Employees.Where(x => x.Role == role).ToList();
        }
        return e;
    }
    public static Employee GetDHeadByDeptCode(string depCode)
    {
        using (StationeryEntities smodel = new StationeryEntities())
        {

            return smodel.Employees.Where(x => x.DeptCode == depCode && x.Role == "DepartmentHead").First();
        }

    }
    public static Employee GetDeptRepByDeptCode(string depName)
    {
        using (StationeryEntities smodel = new StationeryEntities())
        {
            return smodel.Employees.Where(x => x.Department.DeptName.Equals(depName) && x.Role.Equals("Representative")).First();
        }
    }
    public static Employee GetEmployeebyEmpID(int empID)
    {
        Employee e;
        using (StationeryEntities smodel = new StationeryEntities())
        {
            e = smodel.Employees.Where(x => x.EmpID == empID).FirstOrDefault();
        }
        return e;
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
                if (EFBroker_DeptEmployee.GetEmployeeListForActingDHeadSelectedCount(depcode) <= 0)
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