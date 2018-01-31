using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DeptController
/// </summary>
public class DeptController
{

    //Employee List
    public List<Employee> GetEmployeeList()
    {
        return EFBroker_DeptEmployee.GetEmployeeList();
    }

    public Employee GetEmployeeEmailByEid(int eid)
    {
        return EFBroker_DeptEmployee.GetEmployeeEmailByEid(eid);
    }

    //Department List
    public List<Department> GetDepartList()
    {
        return EFBroker_DeptEmployee.GetDepartList();
    }

    public IList GetDepartDetailInfoList()
    {
        return EFBroker_DeptEmployee.GetDepartDetailInfoList();
    }

    public Department GetDepartByDepCode(string deptcode)
    {
        return EFBroker_DeptEmployee.GetDepartByDepCode(deptcode);
    }



    //Department Acting Head
    public int GetEmployeeListForActingDHeadSelectedCount(string deptcode)
    {
        return EFBroker_DeptEmployee.GetEmployeeListForActingDHeadSelectedCount(deptcode);
    }

    public Employee GetEmployeeListForActingDHeadSelected(string deptcode)
    {
        return EFBroker_DeptEmployee.GetEmployeeListForActingDHeadSelected(deptcode);
    }

    public void UpdateRevoke()
    {
        EFBroker_DeptEmployee.UpdateRevoke();
    }

    public void UpdateActingDHead(string deptcode, int cid, string sdate, string edate)
    {
        EFBroker_DeptEmployee.UpdateActingDHead(deptcode, cid, sdate, edate);
    }

    public List<Employee> GetEmployeeListForActingDHead(string deptcode, int id)
    {
        return EFBroker_DeptEmployee.GetEmployeeListForActingDHead(deptcode, id);
    }



    //Department Head


    public Employee GetDHeadByDeptCode(string deptcode)
    {
        return EFBroker_DeptEmployee.GetDHeadByDeptCode(deptcode);
    }

    //Department Rep
    public Employee GetEmployeeListForDRepSelected(string deptcode)
    {
        return EFBroker_DeptEmployee.GetEmployeeListForDRepSelected(deptcode);
    }

    public List<Employee> GetEmployeeListForDRep(string deptcode, int id)
    {
        return EFBroker_DeptEmployee.GetEmployeeListForDRep(deptcode, id);
    }

    public void UpdateDeptRep(string deptcode, int cid)
    {
        Employee oldDeptRep = EFBroker_DeptEmployee.GetEmployeeListForDRepSelected(deptcode);
        String oldEmail = oldDeptRep.Email;
        Utility.sendMail(oldEmail, "Change Department Rep", "Your Role have changed to Employee");
        EFBroker_DeptEmployee.UpdateDeptRep(deptcode, cid);
        Employee newDeptRep = EFBroker_DeptEmployee.GetEmployeeListForDRepSelected(deptcode);
        String newEmail = newDeptRep.Email;
        Utility.sendMail(newEmail, "Change Department Rep", "Your Role have changed to Department Rep");

    }

    //Collection Point
    public string GetDepartmentForCollectionPointSelected(string deptcode)
    {
        return EFBroker_DeptEmployee.GetDepartmentForCollectionPointSelected(deptcode);
    }

    public List<CollectionPoint> GetCollectionPointList()
    {
        return EFBroker_DeptEmployee.GetCollectionPointList();
    }

    public int GetCollectionidbyDeptCode(string deptcode)
    {
        return EFBroker_DeptEmployee.GetCollectionidbyDeptCode(deptcode);
    }

    public void UpdateCollectionPoint(string deptcode, int cid)
    {
        EFBroker_DeptEmployee.UpdateCollectionPoint(deptcode, cid);
        List<String> clerkEmails = EFBroker_Employee.getAllClerkMails();

        if (clerkEmails != null)
        {
            for (int i = 0; i < clerkEmails.Count; i++)
            {
                Utility.sendMail(clerkEmails[i].ToString(), "Change Collection Point", "New Collection Point is updated!");

            }
        }
    }
}