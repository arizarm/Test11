using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IDiscrepancyService" in both code and config file together.
[ServiceContract]
public interface IDiscrepancyService
{
    [OperationContract]
    [WebInvoke(UriTemplate = "/SubmitDiscrepancies", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    void SubmitDiscrepancies(List<WCFDiscrepancy> wdList);
}

[DataContract]
public class WCFDiscrepancy
{
    [DataMember]
    public string ItemCode
    {
        get;

        set;
    }
    [DataMember]
    public string RequestedBy
    {
        get;

        set;
    }
    [DataMember]
    public string AdjustmentQty
    {
        get;

        set;
    }
    [DataMember]
    public string Remarks
    {
        get;

        set;
    }
    [DataMember]
    public string Status
    {
        get;

        set;
    }

    public static WCFDiscrepancy Make(string itemCode, string requestedBy, string adjustmentQty, string remarks, string status)
    {
        WCFDiscrepancy wd = new WCFDiscrepancy();
        wd.ItemCode = itemCode;
        wd.RequestedBy = requestedBy;
        wd.AdjustmentQty = adjustmentQty;
        wd.Remarks = remarks;
        wd.Status = status;
        return wd;
    }
}