using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IRequisitionListService" in both code and config file together.
[ServiceContract]
public interface IRequisitionListService
{
    [OperationContract]
    [WebGet(UriTemplate = "/Requisition", ResponseFormat = WebMessageFormat.Json)]
    List<WCFRequisition> getRequisitionListByStatus();

    [OperationContract]
    [WebGet(UriTemplate = "/Requisition/{id}", ResponseFormat = WebMessageFormat.Json)]
    List<WCFRequisition_ItemList> getList(string id);

    [OperationContract]
    [WebInvoke(UriTemplate = "/Approve", Method = "POST",
    RequestFormat = WebMessageFormat.Json,
    ResponseFormat = WebMessageFormat.Json)]
    void Approve(WCFRequisition requisition);

    [OperationContract]
    [WebInvoke(UriTemplate = "/Reject", Method = "POST",
    RequestFormat = WebMessageFormat.Json,
    ResponseFormat = WebMessageFormat.Json)]
    void Reject(WCFRequisition requisition);


}

[DataContract]
public class WCFRequisition
{
    private string date;
    private string requisitionNo;
    private string department;
    private string status;
    private string employeeName;
    private string remarks;

    public static WCFRequisition Make(string date, string requisitionNo, string department, string status, string employeeName/*,string remarks*/)
    {
        WCFRequisition r = new WCFRequisition();
        r.Date = date;
        r.RequisitionNo = requisitionNo;
        r.Department = department;
        r.Status = status;
        r.EmployeeName = employeeName;
        //r.remarks = remarks;
        return r;
    }

    [DataMember]
    public string Date { get { return date; } set { date = value; } }
    public string RequisitionNo { get { return requisitionNo; } set { requisitionNo = value; } }
    public string Department { get { return department; } set { department = value; } }
    public string Status { get { return status; } set { status = value; } }
    public string EmployeeName { get { return employeeName; } set { employeeName = value; } }
    public string Remarks { get { return remarks; } set { remarks = value; } }
} 

[DataContract]
public class WCFRequisition_ItemList
{
    string description;
    int? reqQty;
    string uom;
    string status;

    public static WCFRequisition_ItemList Make(string des, int? qty, string uom, string status)
    {
        WCFRequisition_ItemList item = new WCFRequisition_ItemList();
        item.description = des;
        item.reqQty = qty;
        item.uom = uom;
        item.status = status;
        return item;
    }


    public WCFRequisition_ItemList() { }

    public string Description { get { return description; } set { description = value; } }
    public int? ReqQty { get { return reqQty; } set { reqQty = value; } }
    public string Uom { get { return uom; } set { uom = value; } }
    public string Status { get { return status; } set { status = value; } }
}
