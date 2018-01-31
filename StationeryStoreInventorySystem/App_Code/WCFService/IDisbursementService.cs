using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IDisbursementService" in both code and config file together.
[ServiceContract]
public interface IDisbursementService
{

    [OperationContract]
    [System.ServiceModel.Web.WebGet(UriTemplate = "/Disbursement", ResponseFormat = WebMessageFormat.Json)]
    List<WCFDisbursement> getAllDisbursement();

    [OperationContract]
    [WebGet(UriTemplate = "/Disbursement/{id}", ResponseFormat = WebMessageFormat.Json)]
    List<WCFDisbursementDetail> getDisbursementDetail(string id);

    [OperationContract]
    [WebInvoke(UriTemplate = "/AccessCodeValidate", Method = "POST",
        BodyStyle = WebMessageBodyStyle.Wrapped,
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json)]
    string AccessCodeValidate(string disbId, string accessCode);

    [OperationContract]
    [WebInvoke(UriTemplate = "/UpdateDisbursement", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    void UpdateDisbursement(List<WCFUpdateDisbursement> qtyList);


    [OperationContract]
    [WebGet(UriTemplate = "/GetRegenerateDate/{id}", ResponseFormat = WebMessageFormat.Json)]
    WCFRegenerateRequest GetRegenerateDate(string id);


    [OperationContract]
    [WebInvoke(UriTemplate = "/RegenerateRequest", Method = "POST",
    RequestFormat = WebMessageFormat.Json,
    ResponseFormat = WebMessageFormat.Json)]
    void RegenerateRequisition(List<WCFRequestedItem> regenList);
}

[DataContract]
public class WCFDisbursement
{
    private int disbId;
    private string collectionDate;
    private string collectionTime;
    private string depName;
    private string collectionPoint;

    public static WCFDisbursement Make(int disbId, string collectionDate, string collectionTime, string depName, string collectionPoint)
    {
        WCFDisbursement d = new WCFDisbursement();
        d.DisbId = disbId;
        d.CollectionDate = collectionDate;
        d.CollectionTime = collectionTime;
        d.DepName = depName;
        d.CollectionPoint = collectionPoint;
        return d;
    }

    [DataMember]
    public int DisbId { get { return disbId; } set { disbId = value; } }

    [DataMember]
    public string CollectionDate { get { return collectionDate; } set { collectionDate = value; } }

    [DataMember]
    public string CollectionTime { get { return collectionTime; } set { collectionTime = value; } }

    [DataMember]
    public string DepName { get { return depName; } set { depName = value; } }

    [DataMember]
    public string CollectionPoint { get { return collectionPoint; } set { collectionPoint = value; } }
}

[DataContract]
public class WCFDisbursementDetail
{
    private string itemCode;
    private string itemDesc;
    private int reqQty;
    private int actualQty;
    private string remarks;

    public static WCFDisbursementDetail Make(string itemCode, string itemDesc, int reqQty, int actualQty, string remarks)
    {
        WCFDisbursementDetail d = new WCFDisbursementDetail();
        d.ItemCode = itemCode;
        d.ItemDesc = itemDesc;
        d.ReqQty = reqQty;
        d.ActualQty = actualQty;
        d.Remarks = remarks;
        return d;
    }

    [DataMember]
    public string ItemCode { get { return itemCode; } set { itemCode = value; } }

    [DataMember]
    public string ItemDesc { get { return itemDesc; } set { itemDesc = value; } }

    [DataMember]
    public int ReqQty { get { return reqQty; } set { reqQty = value; } }

    [DataMember]
    public int ActualQty { get { return actualQty; } set { actualQty = value; } }

    [DataMember]
    public string Remarks { get { return remarks; } set { remarks = value; } }
}



[DataContract]
public class WCFUpdateDisbursement
{
    private string disbId;
    private string actualQty;
    private string remark;

    public static WCFUpdateDisbursement Make(string disbId, string actualQty, string remark)
    {
        WCFUpdateDisbursement d = new WCFUpdateDisbursement();
        d.DisbId = disbId;
        d.ActualQty = actualQty;
        d.Remark = remark;
        return d;
    }

    [DataMember]
    public string DisbId { get { return disbId; } set { disbId = value; } }

    [DataMember]
    public string ActualQty { get { return actualQty; } set { actualQty = value; } }

    [DataMember]
    public string Remark { get { return remark; } set { remark = value; } }
}

[DataContract]
public class WCFRegenerateRequest
{
    private string reqDate;
    private string reqBy;
    private string depName;

    public static WCFRegenerateRequest Make(string reqDate, string reqBy, string depName)
    {
        WCFRegenerateRequest d = new WCFRegenerateRequest();
        d.ReqBy = reqBy;
        d.ReqDate = reqDate;
        d.DepName = depName;
        return d;
    }

    [DataMember]
    public string ReqDate { get { return reqDate; } set { reqDate = value; } }

    [DataMember]
    public string ReqBy { get { return reqBy; } set { reqBy = value; } }

    [DataMember]
    public string DepName { get { return depName; } set { depName = value; } }

}

[DataContract]
public class WCFRequestedItem
{
    private string code;
    private string description;
    private string shortfallQty;
    private int disbId;

    public static WCFRequestedItem Make(string code, string description, string shortfallQty, int disbId)
    {
        WCFRequestedItem item = new WCFRequestedItem();
        item.Code = code;
        item.Description = description;
        item.ShortfallQty = shortfallQty;
        item.DisbId = disbId;
        return item;
    }

    [DataMember]
    public string Code { get { return code; } set { code = value; } }
    [DataMember]
    public string Description { get { return description; } set { description = value; } }
    [DataMember]
    public string ShortfallQty { get { return shortfallQty; } set { shortfallQty = value; } }
    [DataMember]
    public int DisbId { get { return disbId; } set { disbId = value; } }

}

