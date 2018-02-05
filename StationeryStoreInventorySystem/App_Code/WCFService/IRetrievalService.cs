using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

//AUTHOR : CHOU MING SHENG
// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IRetrievalService" in both code and config file together.
[ServiceContract]
public interface IRetrievalService
{
    [OperationContract]
    [System.ServiceModel.Web.WebGet(UriTemplate = "/Retrieval", ResponseFormat = WebMessageFormat.Json)]
    List<WCFRetrieval> getAllRetrieval();

    [OperationContract]
    [WebGet(UriTemplate = "/Retrieval/{id}", ResponseFormat = WebMessageFormat.Json)]
    List<WCFRetrievalDetail> getRetrievalDetail(string id);

    [OperationContract]
    [WebInvoke(UriTemplate = "/RetrievalListDetailUpdate", Method = "POST",
        BodyStyle = WebMessageBodyStyle.Wrapped,
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json)]
    void Update(string RetrievalID, string ItemCode, string ItemQty);
    //void Update(WCFRetrievalListDetailUpdate RetrievalDetail);
    //
}

[Serializable]
[DataContract]
public class WCFRetrieval
{
    private int retrievalID;
    private int retrievedBy;
    private string retrievedDate;
    private string retrievalStatus;

    public static WCFRetrieval Make(int retrievalID, int retrievedBy, string retrievedDate, string retrievalStatus)
    {
        WCFRetrieval r = new WCFRetrieval();
        r.RetrievalID = retrievalID;
        r.RetrievedBy = retrievedBy;
        r.RetrievedDate = retrievedDate;
        r.RetrievalStatus = retrievalStatus;
        return r;
    }

    [DataMember]
    public int RetrievalID { get { return retrievalID; } set { retrievalID = value; } }

    [DataMember]
    public int RetrievedBy { get { return retrievedBy; } set { retrievedBy = value; } }

    [DataMember]
    public string RetrievedDate { get { return retrievedDate; } set { retrievedDate = value; } }

    [DataMember]
    public string RetrievalStatus { get { return retrievalStatus; } set { retrievalStatus = value; } }
}

[Serializable]
[DataContract]
public class WCFRetrievalDetail
{
    private string bin;
    private string description;
    private int totalRequestedQty;
    private string itemCode;
    private int retrievedQty;

    public static WCFRetrievalDetail Make(string bin, string description, int totalRequestedQty, string itemCode, int retrievedQty)
    {
        WCFRetrievalDetail rD = new WCFRetrievalDetail();
        rD.Bin = bin;
        rD.Description = description;
        rD.TotalRequestedQty = totalRequestedQty;
        rD.ItemCode = itemCode;
        rD.RetrievedQty = retrievedQty;
        return rD;
    }

    [DataMember]
    public string Bin { get { return bin; } set { bin = value; } }

    [DataMember]
    public string Description { get { return description; } set { description = value; } }

    [DataMember]
    public int TotalRequestedQty { get { return totalRequestedQty; } set { totalRequestedQty = value; } }

    [DataMember]
    public string ItemCode { get { return itemCode; } set { itemCode = value; } }

    [DataMember]///////////////////////int 
    public int RetrievedQty { get { return retrievedQty; } set { retrievedQty = value; } }
}


[Serializable]
[DataContract]
public class WCFRetrievalListDetailUpdate
{
    private string retrievalId;
    private string itemCode;
    string retrievedQty;
    //private Dictionary<String, int> retrievedData;

    public static WCFRetrievalListDetailUpdate Make(string retrievalId, string itemCode, string retrievedQty)
    {
        WCFRetrievalListDetailUpdate update = new WCFRetrievalListDetailUpdate();
        update.RetrievalId = retrievalId;
        update.ItemCode = itemCode;
        update.RetrievedQty = retrievedQty;
        return update;
    }

    [DataMember]
    public string RetrievalId { get { return retrievalId; } set { retrievalId = value; } }

    [DataMember]
    public string ItemCode { get { return itemCode; } set { itemCode = value; } }

    [DataMember]
    public string RetrievedQty { get { return retrievedQty; } set { retrievedQty = value; } }

}
