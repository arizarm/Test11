using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IItemService" in both code and config file together.
[ServiceContract]
public interface IItemService
{
    [OperationContract]
    [WebGet(UriTemplate = "/CatalogueItems", ResponseFormat = WebMessageFormat.Json)]
    List<WCFCatalogueItem> GetAllItems();
    [OperationContract]
    [WebGet(UriTemplate = "/CatalogueItems/{search}", ResponseFormat = WebMessageFormat.Json)]
    List<WCFCatalogueItem> SearchItems(string search);
    [OperationContract]
    [WebGet(UriTemplate = "/GetItem/{itemCode}", ResponseFormat = WebMessageFormat.Json)]
    WCFCatalogueItem GetItem(string itemCode);
}

[DataContract]
public class WCFCatalogueItem
{
    [DataMember]
    string itemCode;
    [DataMember]
    string description;
    [DataMember]
    string unitOfMeasure;
    [DataMember]
    int balanceQty;
    [DataMember]
    int adjustments;
    
    public WCFCatalogueItem(string itemCode, string description, string unitOfMeasure, int balanceQty, int adjustments)
    {
        this.itemCode = itemCode;
        this.description = description;
        this.unitOfMeasure = unitOfMeasure;
        this.balanceQty = balanceQty;
        this.adjustments = adjustments;
    }
}