using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service" in code, svc and config file together.
public class DeptService : IDeptService
{
    // Start Login

    public WCFEmployee GetEmployeeByEmail(string email)
    {
        Employee emp = EmployeeController.GetEmployeeByEmail(email);

        return WCFEmployee.Make(emp.EmpID, emp.DeptCode, emp.EmpName, emp.Role, emp.Password
        , emp.Email, emp.IsTempHead, emp.StartDate.GetValueOrDefault().ToShortDateString()
        , emp.EndDate.GetValueOrDefault().ToShortDateString());
    }

    //End Login

    //Start Employee Function Part
    public List<WCFEmployee> EmployeeList()
    {
        List<WCFEmployee> wlist = new List<WCFEmployee>();
        List<Employee> elist = EFBroker_DeptEmployee.GetEmployeeList();

        foreach (Employee e in elist)
        {
            wlist.Add(WCFEmployee.Make(e.EmpID, e.DeptCode, e.EmpName, e.Role, e.Password, e.Email
                , e.IsTempHead, e.StartDate.GetValueOrDefault().ToShortDateString()
                , e.EndDate.GetValueOrDefault().ToShortDateString()));
        }
        return wlist;
    }

    public WCFEmployee GetDeptHead(string dcode)
    {

        Department d = EFBroker_DeptEmployee.GetDepartByDepCode(dcode);
        Employee emp = EFBroker_DeptEmployee.GetDHeadByDeptCode(dcode);
        return WCFEmployee.Make(emp.EmpID, d.DeptName, emp.EmpName, emp.Role, emp.Password
         , emp.Email, emp.IsTempHead, emp.StartDate.GetValueOrDefault().ToShortDateString()
         , emp.EndDate.GetValueOrDefault().ToShortDateString());
    }

    public WCFEmployee GetActingDHead(string dcode)
    {

        Department d = EFBroker_DeptEmployee.GetDepartByDepCode(dcode);
        Employee emp = EFBroker_DeptEmployee.GetEmployeeListForActingDHeadSelected(dcode);
        return WCFEmployee.Make(emp.EmpID, d.DeptName, emp.EmpName, emp.Role, emp.Password
         , emp.Email, emp.IsTempHead, emp.StartDate.GetValueOrDefault().ToShortDateString()
         , emp.EndDate.GetValueOrDefault().ToShortDateString());
    }

    public List<WCFEmployee> ListForActingDHead(string dcode, string id)
    {
        List<WCFEmployee> wlist = new List<WCFEmployee>();
        List<Employee> elist = EFBroker_DeptEmployee.GetEmployeeListForActingDHead(dcode, Convert.ToInt16(id));

        foreach (Employee e in elist)
        {
            wlist.Add(WCFEmployee.Make(e.EmpID, e.DeptCode, e.EmpName, e.Role, e.Password, e.Email
                , e.IsTempHead, e.StartDate.GetValueOrDefault().ToShortDateString()
                , e.EndDate.GetValueOrDefault().ToShortDateString()));
        }
        return wlist;
    }

    public WCFEmployee GetDeptRep(string dcode)
    {

        Department d = EFBroker_DeptEmployee.GetDepartByDepCode(dcode);
        Employee emp = EFBroker_DeptEmployee.GetEmployeeListForDRepSelected(dcode);
        return WCFEmployee.Make(emp.EmpID, d.DeptName, emp.EmpName, emp.Role, emp.Password
         , emp.Email, emp.IsTempHead, emp.StartDate.GetValueOrDefault().ToShortDateString()
         , emp.EndDate.GetValueOrDefault().ToShortDateString());
    }

    public List<WCFEmployee> ListForDeptRep(string dcode, string id)
    {
        List<WCFEmployee> wlist = new List<WCFEmployee>();
        List<Employee> elist = EFBroker_DeptEmployee.GetEmployeeListForDRep(dcode, Convert.ToInt16(id));

        foreach (Employee e in elist)
        {
            wlist.Add(WCFEmployee.Make(e.EmpID, e.DeptCode, e.EmpName, e.Role, e.Password, e.Email
                , e.IsTempHead, e.StartDate.GetValueOrDefault().ToShortDateString()
                , e.EndDate.GetValueOrDefault().ToShortDateString()));
        }
        return wlist;
    }

    public void UpdateActingDHead(WCFEmployee e)
    {
        Employee emp = new Employee
        {
            EmpID = e.Eid,
            DeptCode = e.DeptCode,
            Role = e.Role,
            IsTempHead = e.IsTemphead,
            StartDate = Convert.ToDateTime(e.StartDate),
            EndDate = Convert.ToDateTime(e.EndDate)
        };
        EFBroker_DeptEmployee.UpdateActingDHead(e.DeptCode, e.Eid, e.StartDate, e.EndDate);
    }

    public void UpdateDeptRep(WCFEmployee e)
    {
        Employee emp = new Employee
        {
            EmpID = e.Eid,
            DeptCode = e.DeptCode,
            Role = e.Role,
        };
        EFBroker_DeptEmployee.UpdateDeptRep(e.DeptCode, e.Eid);
    }

    //End Employee Function Part


    //Start Department Function Part
    public List<WCFDept> DepartmentList()
    {
        List<WCFDept> wlist = new List<WCFDept>();
        List<Department> dlist = EFBroker_DeptEmployee.GetDepartList();

        foreach (Department d in dlist)
        {

            wlist.Add(WCFDept.Make(d.DeptCode, Convert.ToString(d.CollectionLocationID), d.DeptName, d.DeptContactName, d.DeptTelephone, d.DeptFax));
        }
        return wlist;
    }

    public WCFDept GetDeptInfo(string dcode)
    {
        string collectpoint = EFBroker_DeptEmployee.GetDepartmentForCollectionPointSelected(dcode);
        Department d = EFBroker_DeptEmployee.GetDepartByDepCode(dcode);

        return WCFDept.Make(d.DeptCode, collectpoint, d.DeptName, d.DeptContactName, d.DeptTelephone, d.DeptFax);
    }

    //End Department Function Part

    //Start Collection Function Part

    public List<WCFCollectPoint> CollectionPointList()
    {
        List<WCFCollectPoint> wlist = new List<WCFCollectPoint>();
        List<CollectionPoint> clist = EFBroker_DeptEmployee.GetCollectionPointList();

        foreach (CollectionPoint c in clist)
        {

            wlist.Add(WCFCollectPoint.Make(Convert.ToString(c.CollectionLocationID), c.CollectionPoint1, c.DefaultCollectionTime));
        }
        return wlist;
    }

    public void UpdateCollect(WCFDept d)
    {
        Department dept = new Department
        {
            DeptCode = d.DeptCode,
            CollectionLocationID = Convert.ToInt16(d.Collectid)
        };
        EFBroker_DeptEmployee.UpdateCollectionPoint(dept.DeptCode, dept.CollectionLocationID);
    }

    //End Collection Function Part

}
