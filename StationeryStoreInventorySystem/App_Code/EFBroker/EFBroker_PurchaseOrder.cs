using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
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
            poItem = context.Item_PurchaseOrder.Where(x => x.PurchaseOrderID == poID && x.ItemCode.Equals(itemCode)).FirstOrDefault();
        }
        return poItem;
    }

    public static List<PurchaseOrder> GetPurchaseOrderList()
    {
        List<PurchaseOrder> rList;
        using (StationeryEntities context = new StationeryEntities())
        {
            rList = context.PurchaseOrders.ToList();
        }
        return rList;
    }
    public static List<PurchaseOrder> GetPurchaseOrderListByStatus(string status)
    {
        using (StationeryEntities entities = new StationeryEntities())
        {
            List<PurchaseOrder> purchaseorderList = (from pOrder in entities.PurchaseOrders
                                                     where pOrder.Status == status
                                                     select pOrder).ToList();
            return purchaseorderList;
        }
    }
    public static List<PurchaseOrder> GetPurchaseOrderListByOrderID(int orderID)
    {
        using (StationeryEntities entities = new StationeryEntities())
        {
            return entities.PurchaseOrders.Where(x => x.PurchaseOrderID == orderID).ToList();
        }
    }
    public static List<Item_PurchaseOrder> GetPurchaseOrderItemList()
    {
        List<Item_PurchaseOrder> rList;
        using (StationeryEntities context = new StationeryEntities())
        {
            rList = context.Item_PurchaseOrder.ToList();
        }
        return rList;
    }
    public static List<PurchaseItems> GetReorderItemList()
    {
        using (StationeryEntities entities = new StationeryEntities())
        {
            var itemList = (from Stock in entities.StockCards
                            group Stock by Stock.ItemCode into stck
                            join item in entities.Items on stck.Key equals item.ItemCode
                            where item.ReorderLevel >= stck.Min(x => x.Balance)
                            select new PurchaseItems
                            {
                                ItemCode = item.ItemCode,
                                Description = item.Description,
                                ReorderQty = item.ReorderQty,
                                ReorderLevel = item.ReorderLevel,
                                UnitOfMeasure = item.UnitOfMeasure,
                                Balance = stck.Min(x => x.Balance)
                            }).ToList<PurchaseItems>();
            return itemList;
        }

    }
    public static List<PurchaseOrderItemDetails> GetPurchaseOrderItemsDetailList(int orderID)
    {
        using(StationeryEntities entities = new StationeryEntities())
        {
            List<PurchaseOrderItemDetails> porderDetails = (from pOrderDetail in entities.Item_PurchaseOrder
                                                            join pOrder in entities.PurchaseOrders on pOrderDetail.PurchaseOrderID equals pOrder.PurchaseOrderID
                                                            where pOrderDetail.PurchaseOrderID == orderID
                                                            select new PurchaseOrderItemDetails
                                                            {
                                                                ItemCode = pOrderDetail.ItemCode,
                                                                Description = pOrderDetail.Item.Description,
                                                                OrderQty = pOrderDetail.OrderQty,
                                                                Price = pOrderDetail.Amount / pOrderDetail.OrderQty,
                                                                TotalAmount = pOrderDetail.Amount

                                                            }).ToList();

            return porderDetails;
        }
    }
    public static List<SupplierInfo> GetPurchaseSupplierInfoList()
    {
        using (StationeryEntities entities = new StationeryEntities())
        {
            var supplierList = (from item in entities.Items
                                join price in entities.PriceLists on item.ItemCode equals price.ItemCode
                                orderby price.SupplierRank ascending
                                select new SupplierInfo
                                {
                                    SupplierNameWithPrice = price.Supplier.SupplierName + " / " + price.Price + " / " + (price.Price * item.ReorderQty),
                                    SupplierCode = price.Supplier.SupplierCode,
                                    ItemCode = item.ItemCode,
                                    Amount = price.Price * item.ReorderQty,
                                }).Distinct().ToList<SupplierInfo>();
            return supplierList;
        }
    }
    public static List<SupplierInfo> GetPurchaseSupplierListByItemCode(string itemCode)
    {
        using (StationeryEntities entities = new StationeryEntities())
        {
            var supplierInfoList = (from item in entities.Items
                                    join price in entities.PriceLists on item.ItemCode equals price.ItemCode
                                    where item.ItemCode == itemCode
                                    orderby price.SupplierRank ascending
                                    select new SupplierInfo
                                    {
                                        SupplierNameWithPrice = price.Supplier.SupplierName + " / " + price.Price + " / " + (price.Price * item.ReorderQty),
                                        SupplierCode = price.Supplier.SupplierCode,
                                        ItemCode = item.ItemCode,
                                        Amount = price.Price * item.ReorderQty,
                                    }).Distinct().ToList<SupplierInfo>();
            return supplierInfoList;
        }
    }
    public static void AddPurchaseOrder(Dictionary<PurchaseOrder, List<Item_PurchaseOrder>> orderItems)
    {
        using (TransactionScope ts = new TransactionScope())
        {
            StationeryEntities entities = new StationeryEntities();
            decimal? totAmount = 0;
            foreach (var order in orderItems)
            {
                entities.PurchaseOrders.Add(order.Key);
                entities.SaveChanges();
                foreach (var items in order.Value)
                {
                    totAmount += items.Amount;

                    items.PurchaseOrderID = order.Key.PurchaseOrderID;
                    entities.Item_PurchaseOrder.Add(items);

                }

                PurchaseOrder porder = entities.PurchaseOrders.Where(x => x.PurchaseOrderID == order.Key.PurchaseOrderID).FirstOrDefault();
                porder.TotalAmount = totAmount;
                totAmount = 0;
                entities.SaveChanges();
            }
            ts.Complete();
        }
    }
    public static void UpdatePurchaseItem(Item_PurchaseOrder pItem)
    {
        using (TransactionScope ts = new TransactionScope())
        {
            StationeryEntities entities = new StationeryEntities();
            Item_PurchaseOrder item = entities.Item_PurchaseOrder.Where(x => x.ItemCode == pItem.ItemCode &&
                                        x.PurchaseOrderID == pItem.PurchaseOrderID).First();
            item.OrderQty = pItem.OrderQty;
            item.Amount = pItem.Amount;
            entities.SaveChanges();
            ts.Complete();
        }
    }
    public static void UpdatePurchaseOrder(PurchaseOrder pOrder)
    {
        using (StationeryEntities entities = new StationeryEntities())
        {

            PurchaseOrder order = entities.PurchaseOrders.Where(x => x.PurchaseOrderID == pOrder.PurchaseOrderID).FirstOrDefault();
            order.ApprovedByDate = pOrder.ApprovedByDate;
            order.Remarks = pOrder.Remarks;
            order.Status = pOrder.Status;
            order.ApprovedBy = pOrder.ApprovedBy;
            entities.SaveChanges();
        }
    }
    public static void ClosePurchaseOrder(PurchaseOrder pOrder)
    {
        using(TransactionScope ts = new TransactionScope())
        {
            StationeryEntities entities = new StationeryEntities();
            PurchaseOrder order = entities.PurchaseOrders.Where(x => x.PurchaseOrderID == pOrder.PurchaseOrderID).First();
            order.DONumber = pOrder.DONumber;
            order.Status = pOrder.Status;
            order.ExpectedDate = DateTime.Now.Date;

            List<Item_PurchaseOrder> itemList = entities.Item_PurchaseOrder.Where(x => x.PurchaseOrderID == order.PurchaseOrderID).ToList();
            foreach (Item_PurchaseOrder item in itemList)
            {
                StockCard itemStockCard = (from stck in entities.StockCards.OrderBy(x => x.Balance)
                                           where stck.ItemCode == item.ItemCode
                                           select stck).FirstOrDefault();
                itemStockCard.Qty = item.OrderQty;
                itemStockCard.Balance = itemStockCard.Balance + itemStockCard.Qty;
            }
            entities.SaveChanges();
            ts.Complete();
        }
        return;
    }
}