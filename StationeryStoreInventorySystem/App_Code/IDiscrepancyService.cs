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
    string itemCode;
    [DataMember]
    int requestedBy;
    [DataMember]
    int adjustmentQty;
    [DataMember]
    string remarks;
    [DataMember]
    string status;

    public string ItemCode
    {
        get
        {
            return itemCode;
        }

        set
        {
            itemCode = value;
        }
    }

    public int RequestedBy
    {
        get
        {
            return requestedBy;
        }

        set
        {
            requestedBy = value;
        }
    }

    public int AdjustmentQty
    {
        get
        {
            return adjustmentQty;
        }

        set
        {
            adjustmentQty = value;
        }
    }

    public string Remarks
    {
        get
        {
            return remarks;
        }

        set
        {
            remarks = value;
        }
    }

    public string Status
    {
        get
        {
            return status;
        }

        set
        {
            status = value;
        }
    }

    public WCFDiscrepancy(string itemCode, int requestedBy, int adjustmentQty, string remarks, string status)
    {
        this.ItemCode = itemCode;
        this.RequestedBy = requestedBy;
        this.AdjustmentQty = adjustmentQty;
        this.Remarks = remarks;
        this.Status = status;
    }
}