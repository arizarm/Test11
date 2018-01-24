using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

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
}

[DataContract]
public class WCFRetrieval
{
    private int retrievalID;
    private int retrievedBy;
    private DateTime retrievedDate;
    private string retrievalStatus;

    public static WCFRetrieval Make(int retrievalID, int retrievedBy, DateTime retrievedDate, string retrievalStatus)
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
    public DateTime RetrievedDate { get { return retrievedDate; } set { retrievedDate = value; } }

    [DataMember]
    public string RetrievalStatus { get { return retrievalStatus; } set { retrievalStatus = value; } }    
}

[DataContract]
public class WCFRetrievalDetail
{
    private string bin;
    private string description;
    private int totalRequestedQty;
    private string itemCode;

    public static WCFRetrievalDetail Make(string bin, string description, int totalRequestedQty, string itemCode)
    {
        WCFRetrievalDetail rD = new WCFRetrievalDetail();
        rD.Bin = bin;
        rD.Description = description;
        rD.TotalRequestedQty = totalRequestedQty;
        rD.ItemCode = itemCode;
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
}
