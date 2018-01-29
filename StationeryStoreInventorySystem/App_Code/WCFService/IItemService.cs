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
    public string ItemCode
    {
        get;

        set;
    }
    [DataMember]
    public string Description
    {
        get;

        set;
    }
    [DataMember]
    public string UnitOfMeasure
    {
        get;

        set;
    }
    [DataMember]
    public int BalanceQty
    {
        get;

        set;
    }
    [DataMember]
    public int Adjustments
    {
        get;

        set;
    }
    [DataMember]
    public string Bin
    {
        get;

        set;
    }

    public static WCFCatalogueItem Make(string itemCode, string description, string unitOfMeasure, int balanceQty, int adjustments, string bin)
    {
        WCFCatalogueItem wci = new WCFCatalogueItem();
        wci.ItemCode = itemCode;
        wci.Description = description;
        wci.UnitOfMeasure = unitOfMeasure;
        wci.BalanceQty = balanceQty;
        wci.Adjustments = adjustments;
        wci.Bin = bin;
        return wci;
    }
}