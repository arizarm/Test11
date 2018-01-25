using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ItemService" in code, svc and config file together.
public class ItemService : IItemService
{
    public List<WCFCatalogueItem> GetAllItems()
    {
        List<Item> iList = EFBroker_Item.GetActiveItemList();
        List<WCFCatalogueItem> ciList = new List<WCFCatalogueItem>();
        foreach(Item i in iList)
        {
            if(i.BalanceQty != null)
            {
                WCFCatalogueItem ci = new WCFCatalogueItem(i.ItemCode, i.Description, i.UnitOfMeasure, (int)i.BalanceQty);
                ciList.Add(ci);
            }
        }
        return ciList;
    }
}
