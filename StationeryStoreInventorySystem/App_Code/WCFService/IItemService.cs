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

    public string Description
    {
        get
        {
            return description;
        }

        set
        {
            description = value;
        }
    }

    public string UnitOfMeasure
    {
        get
        {
            return unitOfMeasure;
        }

        set
        {
            unitOfMeasure = value;
        }
    }

    public int BalanceQty
    {
        get
        {
            return balanceQty;
        }

        set
        {
            balanceQty = value;
        }
    }

    public int Adjustments
    {
        get
        {
            return adjustments;
        }

        set
        {
            adjustments = value;
        }
    }

    public WCFCatalogueItem(string itemCode, string description, string unitOfMeasure, int balanceQty, int adjustments)
    {
        this.ItemCode = itemCode;
        this.Description = description;
        this.UnitOfMeasure = unitOfMeasure;
        this.BalanceQty = balanceQty;
        this.Adjustments = adjustments;
    }
}