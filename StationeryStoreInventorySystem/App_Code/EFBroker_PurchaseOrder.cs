using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for EFBroker_PurchaseOrder
/// </summary>
public class EFBroker_PurchaseOrder
{
    public EFBroker_PurchaseOrder()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static PurchaseOrder GetPurchaseOrderById(int id)
    {   //goes to purchase order broker
        PurchaseOrder order;
        using (StationeryEntities context = new StationeryEntities())
        {
            order = context.PurchaseOrders.Where(x => x.PurchaseOrderID == id).FirstOrDefault();
        }
        return order;
    }
    public static Item_PurchaseOrder GetPurchaseOrderItem(int poID, string itemCode)
    {   //goes to item_purchaseorder broker
        Item_PurchaseOrder poItem;
        using (StationeryEntities context = new StationeryEntities())
        {
            poItem =context.Item_PurchaseOrder.Where(x => x.PurchaseOrderID == poID && x.ItemCode.Equals(itemCode)).FirstOrDefault();
        }
        return poItem;
    }
}