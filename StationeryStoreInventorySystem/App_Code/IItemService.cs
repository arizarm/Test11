using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web;
using System.Web.Script.Services;
using System.Web.UI;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IItemService" in both code and config file together.
[ServiceContract]
public interface IItemService
{
    [OperationContract]
    [WebGet(UriTemplate = "/Items", ResponseFormat = WebMessageFormat.Json)]
    List<string> ListItem();
}

[DataContract]
public class WCFItem
{
    string itemCode;
    string description;
    int qty;
    string uom;

    public static WCFItem Make(string itemCode, string description, int qty, string uom)
    {
        WCFItem i = new WCFItem();
        i.itemCode = itemCode;
        i.description = description;
        i.qty = qty;
        i.uom = uom;
        return i;
    }

    [DataMember]
    public string ItemCode
    {
        get { return itemCode; }
        set { itemCode = value; }
    }

    [DataMember]
    public string Description
    {
        get { return description; }
        set { description = value; }
    }

    [DataMember]
    public int Quantity
    {
        get { return qty; }
        set { qty = value; }
    }

    [DataMember]
    public string Uom
    {
        get { return uom; }
        set { uom = value; }
    }
}
