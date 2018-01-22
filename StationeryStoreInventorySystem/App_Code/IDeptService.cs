using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService" in both code and config file together.
[ServiceContract]
public interface IDeptService
{
    // Start Employee

    [OperationContract]
    [WebGet(UriTemplate = "/Employee", ResponseFormat = WebMessageFormat.Json)]
    List<WCFEmployee> EmployeeList();

    [OperationContract]
    [WebGet(UriTemplate = "/Employee/DeptHead/{deptCode}", ResponseFormat = WebMessageFormat.Json)]
    WCFEmployee GetDeptHead(string deptCode);

    [OperationContract]
    [WebGet(UriTemplate = "/Employee/ActingDHead/{deptCode}", ResponseFormat = WebMessageFormat.Json)]
    WCFEmployee GetActingDHead(string deptCode);

    [OperationContract]
    [WebGet(UriTemplate = "/Employee/DeptRep/{deptCode}", ResponseFormat = WebMessageFormat.Json)]
    WCFEmployee GetDeptRep(string deptCode);


    [OperationContract]
    [WebGet(UriTemplate = "/Employee/ForActingDHead/{deptCode}/{id}", ResponseFormat = WebMessageFormat.Json)]
    List<WCFEmployee> ListForActingDHead(string deptCode, string id);

    [OperationContract]
    [WebGet(UriTemplate = "/Employee/ForDeptRep/{deptCode}/{id}", ResponseFormat = WebMessageFormat.Json)]
    List<WCFEmployee> ListForDeptRep(string deptCode, string id);

    [OperationContract]
    [WebInvoke(UriTemplate = "/UpdateActingDHead", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    void UpdateActingDHead(WCFEmployee e);

    [OperationContract]
    [WebInvoke(UriTemplate = "/UpdateDeptRep", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    void UpdateDeptRep(WCFEmployee e);

    //End Employee

    //Start Department

    [OperationContract]
    [WebGet(UriTemplate = "/Dept", ResponseFormat = WebMessageFormat.Json)]
    List<WCFDept> DepartmentList();

    [OperationContract]
    [WebGet(UriTemplate = "/Dept/{deptCode}", ResponseFormat = WebMessageFormat.Json)]
    WCFDept GetDeptInfo(string deptCode);

    //End Department

    //Start Collection Point

    [OperationContract]
    [WebGet(UriTemplate = "/Collectpoint", ResponseFormat = WebMessageFormat.Json)]
    List<WCFCollectPoint> CollectionPointList();

    [OperationContract]
    [WebInvoke(UriTemplate = "/UpdateCollect", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    void UpdateCollect(WCFDept d);

    //End Collection Point

}

[DataContract]
public class WCFEmployee
{
    int eid;
    string deptCode;
    string ename;
    string role;
    string password;
    string email;
    string isTemphead;
    string startDate;
    string endDate;

    public static WCFEmployee Make(int eid, string deptCode, string ename, string role, string password, string email, string isTemphead, string startDate, string endDate)
    {
        WCFEmployee e = new WCFEmployee();
        e.eid = eid;
        e.deptCode = deptCode;
        e.ename = ename;
        e.role = role;
        e.password = password;
        e.email = email;
        e.isTemphead = isTemphead;
        e.startDate = startDate;
        e.endDate = endDate;
        return e;
    }

    [DataMember]
    public int Eid
    {
        get { return eid; }
        set { eid = value; }
    }

    [DataMember]
    public string DeptCode
    {
        get { return deptCode; }
        set { deptCode = value; }
    }

    [DataMember]
    public string Ename
    {
        get { return ename; }
        set { ename = value; }
    }


    [DataMember]
    public string Role
    {
        get { return role; }
        set { role = value; }
    }

    [DataMember]
    public string Password
    {
        get { return password; }
        set { password = value; }
    }

    [DataMember]
    public string Email
    {
        get { return email; }
        set { email = value; }
    }

    [DataMember]
    public string IsTemphead
    {
        get { return isTemphead; }
        set { isTemphead = value; }
    }

    [DataMember]
    public string StartDate
    {
        get { return startDate; }
        set { startDate = value; }
    }

    [DataMember]
    public string EndDate
    {
        get { return endDate; }
        set { endDate = value; }
    }
}

[DataContract]
public class WCFDept
{
    string deptCode;
    string collectid;
    string dname;
    string contactname;
    string telephone;
    string fax;

    public static WCFDept Make(string deptCode, string collectid, string dname, string contactname, string telephone, string fax)
    {
        WCFDept d = new WCFDept();
        d.deptCode = deptCode;
        d.collectid = collectid;
        d.dname = dname;
        d.contactname = contactname;
        d.telephone = telephone;
        d.fax = fax;
        return d;
    }


    [DataMember]
    public string DeptCode
    {
        get { return deptCode; }
        set { deptCode = value; }
    }

    [DataMember]
    public string Collectid
    {
        get { return collectid; }
        set { collectid = value; }
    }


    [DataMember]
    public string Dname
    {
        get { return dname; }
        set { dname = value; }
    }


    [DataMember]
    public string Contactname
    {
        get { return contactname; }
        set { contactname = value; }
    }

    [DataMember]
    public string Telephone
    {
        get { return telephone; }
        set { telephone = value; }
    }

    [DataMember]
    public string Fax
    {
        get { return fax; }
        set { fax = value; }
    }
}

[DataContract]
public class WCFCollectPoint
{

    string collectid;
    string collectionpoint;
    string defaulttime;


    public static WCFCollectPoint Make(string collectid, string collectionpoint, string defaulttime)
    {
        WCFCollectPoint c = new WCFCollectPoint();
        c.collectid = collectid;
        c.collectionpoint = collectionpoint;
        c.defaulttime = defaulttime;
        return c;
    }


    [DataMember]
    public string Collectid
    {
        get { return collectid; }
        set { collectid = value; }
    }


    [DataMember]
    public string Collectionpoint
    {
        get { return collectionpoint; }
        set { collectionpoint = value; }
    }


    [DataMember]
    public string Defaulttime
    {
        get { return defaulttime; }
        set { defaulttime = value; }
    }



}



