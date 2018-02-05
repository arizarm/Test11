using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;


// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service" in code, svc and config file together.
public class DeptService : IDeptService
{
    //AUTHOR : YIMON SOE
    // Start Login
    DeptController deptController = new DeptController();
    public WCFEmployee GetEmployeeByEmail(string email, string password)
    {
        Employee emp = LoginController.login(email, password);

        return WCFEmployee.Make(emp.EmpID, emp.DeptCode, emp.EmpName, emp.Role, emp.Password
        , emp.Email, emp.IsTempHead, emp.StartDate.GetValueOrDefault().ToShortDateString()
        , emp.EndDate.GetValueOrDefault().ToShortDateString());
    }
    //End Login

    //AUTHOR : KHIN MYO MYO SHWE
    //Start Employee Function Part
    public List<WCFEmployee> EmployeeList()
    {
        List<WCFEmployee> wlist = new List<WCFEmployee>();
        List<Employee> elist = deptController.GetEmployeeList();

        foreach (Employee e in elist)
        {
            wlist.Add(WCFEmployee.Make(e.EmpID, e.DeptCode, e.EmpName, e.Role, e.Password, e.Email
                , e.IsTempHead, e.StartDate.GetValueOrDefault().ToShortDateString()
                , e.EndDate.GetValueOrDefault().ToShortDateString()));
        }
        return wlist;
    }

    //AUTHOR : KHIN MYO MYO SHWE
    public WCFEmployee GetDeptHead(string dcode)
    {

        Department d = deptController.GetDepartByDepCode(dcode);
        Employee emp = deptController.GetDHeadByDeptCode(dcode);
        return WCFEmployee.Make(emp.EmpID, d.DeptName, emp.EmpName, emp.Role, emp.Password
         , emp.Email, emp.IsTempHead, emp.StartDate.GetValueOrDefault().ToShortDateString()
         , emp.EndDate.GetValueOrDefault().ToShortDateString());
    }

    //AUTHOR : KHIN MYO MYO SHWE
    public WCFEmployee GetActingDHead(string dcode)
    {
        int count = deptController.GetEmployeeListForActingDHeadSelectedCount(dcode);
        if (count > 0)
        {
            Department d = deptController.GetDepartByDepCode(dcode);
            Employee emp = deptController.GetEmployeeListForActingDHeadSelected(dcode);
            return WCFEmployee.Make(emp.EmpID, d.DeptName, emp.EmpName, emp.Role, emp.Password
             , emp.Email, emp.IsTempHead, emp.StartDate.GetValueOrDefault().ToShortDateString()
             , emp.EndDate.GetValueOrDefault().ToShortDateString());
        }
        else
        {
            return WCFEmployee.Make(0, null, null, null, null, null, null, null, null);
           
        }
    }

    //AUTHOR : KHIN MYO MYO SHWE
    public List<WCFEmployee> ListForActingDHead(string dcode, string id)
    {
        List<WCFEmployee> wlist = new List<WCFEmployee>();
        List<Employee> elist = deptController.GetEmployeeListForActingDHead(dcode, Convert.ToInt16(id));

        foreach (Employee e in elist)
        {
            wlist.Add(WCFEmployee.Make(e.EmpID, e.DeptCode, e.EmpName, e.Role, e.Password, e.Email
                , e.IsTempHead, e.StartDate.GetValueOrDefault().ToShortDateString()
                , e.EndDate.GetValueOrDefault().ToShortDateString()));
        }
        return wlist;
    }

    //AUTHOR : KHIN MYO MYO SHWE
    public WCFEmployee GetDeptRep(string dcode)
    {

        Department d = deptController.GetDepartByDepCode(dcode);
        Employee emp = deptController.GetEmployeeListForDRepSelected(dcode);
        return WCFEmployee.Make(emp.EmpID, d.DeptName, emp.EmpName, emp.Role, emp.Password
         , emp.Email, emp.IsTempHead, emp.StartDate.GetValueOrDefault().ToShortDateString()
         , emp.EndDate.GetValueOrDefault().ToShortDateString());
    }

    //AUTHOR : KHIN MYO MYO SHWE
    public List<WCFEmployee> ListForDeptRep(string dcode, string id)
    {
        List<WCFEmployee> wlist = new List<WCFEmployee>();
        List<Employee> elist = deptController.GetEmployeeListForDRep(dcode, Convert.ToInt16(id));

        foreach (Employee e in elist)
        {
            wlist.Add(WCFEmployee.Make(e.EmpID, e.DeptCode, e.EmpName, e.Role, e.Password, e.Email
                , e.IsTempHead, e.StartDate.GetValueOrDefault().ToShortDateString()
                , e.EndDate.GetValueOrDefault().ToShortDateString()));
        }
        return wlist;
    }

    //AUTHOR : KHIN MYO MYO SHWE
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
        deptController.UpdateActingDHead(e.DeptCode, e.Eid, e.StartDate, e.EndDate);
    }

    //AUTHOR : KHIN MYO MYO SHWE
    public void UpdateDeptRep(WCFEmployee e)
    {
        Employee emp = new Employee
        {
            EmpID = e.Eid,
            DeptCode = e.DeptCode,
            Role = e.Role,
        };
        deptController.UpdateDeptRep(e.DeptCode, e.Eid);
    }

    //End Employee Function Part


    //AUTHOR : KHIN MYO MYO SHWE
    //Start Department Function Part
    public List<WCFDept> DepartmentList()
    {
        List<WCFDept> wlist = new List<WCFDept>();
        List<Department> dlist = deptController.GetDepartList();

        foreach (Department d in dlist)
        {

            wlist.Add(WCFDept.Make(d.DeptCode, Convert.ToString(d.CollectionLocationID), d.DeptName, d.DeptContactName, d.DeptTelephone, d.DeptFax));
        }
        return wlist;
    }

    //AUTHOR : KHIN MYO MYO SHWE
    public WCFDept GetDeptInfo(string dcode)
    {
        string collectpoint = deptController.GetDepartmentForCollectionPointSelected(dcode);
        Department d = deptController.GetDepartByDepCode(dcode);

        return WCFDept.Make(d.DeptCode, collectpoint, d.DeptName, d.DeptContactName, d.DeptTelephone, d.DeptFax);
    }

    //End Department Function Part

    //Start Collection Function Part
    //AUTHOR : KHIN MYO MYO SHWE
    public List<WCFCollectPoint> CollectionPointList()
    {
        List<WCFCollectPoint> wlist = new List<WCFCollectPoint>();
        List<CollectionPoint> clist = deptController.GetCollectionPointList();

        foreach (CollectionPoint c in clist)
        {

            wlist.Add(WCFCollectPoint.Make(Convert.ToString(c.CollectionLocationID), c.CollectionPoint1, c.DefaultCollectionTime));
        }
        return wlist;
    }

    //AUTHOR : KHIN MYO MYO SHWE
    public string GetCollectionPointByDeptCode(string deptcode)
    {  
        return deptController.GetCollectionidbyDeptCode(deptcode).ToString();
    }

    //AUTHOR : KHIN MYO MYO SHWE
    public void UpdateCollect(WCFDept d)
    {
        Department dept = new Department
        {
            DeptCode = d.DeptCode,
            CollectionLocationID = Convert.ToInt32(d.Collectid)
        };
        deptController.UpdateCollectionPoint(dept.DeptCode,Convert.ToInt16(dept.CollectionLocationID));
    }

    //AUTHOR : APRIL SHAR
    //End Collection Function Part
    public IsTemp CheckIsTempHead(string id)
    {
        Employee e = EFBroker_Employee.GetEmployee(Convert.ToInt32(id));
        bool isTemp;
        isTemp = Utility.checkIsTempDepHead(e);
        IsTemp it = new IsTemp();
        it.IsTempHead = isTemp;
        return it;
    }

    //AUTHOR : APRIL SHAR
    public CheckHasTemp CheckHasTempHead(string deptCode)
    {
        bool checkHasTemp = EFBroker_Employee.isDeptHaveTempHead(deptCode);
        CheckHasTemp cht = new CheckHasTemp();
        cht.HasTemp = checkHasTemp;
        return cht;
    }
}
