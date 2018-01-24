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
    [System.ServiceModel.Web.WebGet(UriTemplate = "/Disbursement/{id}", ResponseFormat = WebMessageFormat.Json)]
    WCFDisbursement getDisbursement(string id);

    [OperationContract]
    [WebGet(UriTemplate = "/DisbursementDetail", ResponseFormat = WebMessageFormat.Json)]
    List<WCFDisbursementDetail> getDisbursementDetail();

}

[DataContract]
public class WCFDisbursement
{
    private string disbId;
    private string collectionDate;
    private string collectionTime;
    private string depName;
    private string collectionPoint;

    public static WCFDisbursement Make(string disbId, string collectionDate, string collectionTime, string depName, string collectionPoint)
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
    public string DisbId { get { return disbId; } set { disbId = value; } }

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

