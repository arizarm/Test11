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
            order = context.PurchaseOrders.Include("Employee").Include("Employee1").Include("Item_PurchaseOrder").Include("Supplier").Where(x => x.PurchaseOrderID == id).FirstOrDefault();
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
            rList = context.PurchaseOrders.Include("Supplier").Include("Employee").Include("Employee1").Include("Item_PurchaseOrder").OrderByDescending(x=>x.OrderDate).OrderByDescending(x=>x.PurchaseOrderID).ToList();
        }
        return rList;
    }  

    public static List<PurchaseOrder> GetPurchaseOrderListByStatus(string status)
    {
        using (StationeryEntities entities = new StationeryEntities())
        {
            List<PurchaseOrder> purchaseorderList = (from pOrder in entities.PurchaseOrders.Include("Supplier").Include("Employee").Include("Employee1").Include("Item_PurchaseOrder")
                                                     where pOrder.Status == status
                                                     select pOrder).OrderByDescending(x=>x.OrderDate).OrderByDescending(x => x.PurchaseOrderID).ToList();
            return purchaseorderList;
        }
    }
    public static List<PurchaseOrder> SearchPurchaseOrder(string searchTxt)
    {
        //int id = Convert.ToInt32(searchTxt);
        using (StationeryEntities entities = new StationeryEntities())
        {
            return entities.PurchaseOrders.Include("Supplier").Include("Employee").Include("Employee1").Include("Item_PurchaseOrder").Where(x=>
            x.Employee1.EmpName.ToString().ToLower().Contains(searchTxt.ToLower())|| x.PurchaseOrderID.ToString().ToLower().Contains(searchTxt.ToLower())||  x.Supplier.SupplierName.ToString().ToLower().Contains(searchTxt.ToLower())
            || x.Status.ToString().ToLower().Contains(searchTxt.ToLower())).OrderByDescending(x=>x.OrderDate).OrderByDescending(x => x.PurchaseOrderID).ToList();
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
    public static List<ReorderItem> GetReorderItemList()
    {
        using (StationeryEntities entities = new StationeryEntities())
        {
            var itemList = (from item in entities.Items
                            where item.ReorderLevel >= item.BalanceQty && item.ActiveStatus=="Y"
                            select new ReorderItem
                            {
                                ItemCode = item.ItemCode,
                                Description = item.Description,
                                ReorderQty = item.ReorderQty,
                                ReorderLevel = item.ReorderLevel,
                                UnitOfMeasure = item.UnitOfMeasure,
                                Balance = item.BalanceQty
                            }).Distinct().ToList<ReorderItem>();
            return itemList;
        }

    }
    public static List<PurchaseOrderItemDetails> GetPurchaseOrderItemsDetailList(int orderID)
    {
        using (StationeryEntities entities = new StationeryEntities())
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
    public static List<ItemPrice> GetItemPriceList()
    {
        String currentyear = Convert.ToString(DateTime.Now.Date.Year);

        using (StationeryEntities entities = new StationeryEntities())
        {
            var itemPriceList = (from item in entities.Items
                                join price in entities.PriceLists on item.ItemCode equals price.ItemCode
                                where  price.TenderYear==currentyear
                                orderby price.SupplierRank ascending
                                select new ItemPrice
                                {
                                    //SupplierNameWithPrice = price.Supplier.SupplierName + " / " + price.Price + " / " + (price.Price * item.ReorderQty),
                                    SupplierName = price.Supplier.SupplierName,
                                    SupplierCode = price.Supplier.SupplierCode,
                                    ItemCode = item.ItemCode,
                                    Price =price.Price,
                                    Amount = price.Price * item.ReorderQty,
                                }).Distinct().ToList<ItemPrice>();
            return itemPriceList;
        }
    }
    public static List<ItemPrice> GetItemPriceByItemCode(string itemCode)
    {
        String currentyear = Convert.ToString(DateTime.Now.Date.Year);
        using (StationeryEntities entities = new StationeryEntities())
        {
            var itemPriceList = (from item in entities.Items
                                    join price in entities.PriceLists on item.ItemCode equals price.ItemCode
                                    where item.ItemCode == itemCode && price.TenderYear == currentyear
                                    orderby price.SupplierRank ascending
                                    select new ItemPrice
                                    {
                                        //SupplierNameWithPrice = price.Supplier.SupplierName + " / " + price.Price + " / " + (price.Price * item.ReorderQty),
                                        SupplierCode = price.Supplier.SupplierCode,
                                        ItemCode = item.ItemCode,
                                        Price =price.Price,
                                        Amount = price.Price * item.ReorderQty,
                                    }).Distinct().ToList<ItemPrice>();
            return itemPriceList;
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
        using (TransactionScope ts = new TransactionScope())
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

    public static void DeletePurchaseOrder(int orderID)
    {
        using (TransactionScope ts = new TransactionScope())
        {
            StationeryEntities entities = new StationeryEntities();
            PurchaseOrder pOrder = entities.PurchaseOrders.Where(x => x.PurchaseOrderID == orderID).FirstOrDefault();
            List<Item_PurchaseOrder> pitemList = entities.Item_PurchaseOrder.Where(x => x.PurchaseOrderID == orderID).ToList();
            entities.Item_PurchaseOrder.RemoveRange(pitemList);
            entities.PurchaseOrders.Attach(pOrder);
            entities.PurchaseOrders.Remove(pOrder);
            entities.SaveChanges();
            ts.Complete();
        }
    }

    public static List<ShortfallItems> GenerateReorderReportForPurchasedItems(DateTime startDate, DateTime endDate)
    {
        StationeryEntities entities = new StationeryEntities();
    
        var itemList = (from item in entities.Items
                        join pItems in entities.Item_PurchaseOrder on item.ItemCode equals pItems.ItemCode
                        where item.ReorderLevel >= item.BalanceQty && (pItems.PurchaseOrder.OrderDate >= startDate && endDate >= pItems.PurchaseOrder.OrderDate)
                        select new ShortfallItems
                        {
                            ItemCode = item.ItemCode,
                            Description = item.Description,
                            ReorderQuantity = item.ReorderQty,
                            ReorderLevel = item.ReorderLevel,
                            UnitOfMeasure = item.UnitOfMeasure,
                            Balance = item.BalanceQty,
                            PurchaseOrderNo = pItems.PurchaseOrderID,
                            ExpectedDate = pItems.PurchaseOrder.ExpectedDate
                        }).ToList<ShortfallItems>();
        return itemList;
    }
    public static List<ShortfallItems> GenerateShortfallItemsReport(DateTime startDate, DateTime endDate)
    {
        StationeryEntities entities = new StationeryEntities();
        var lowStockItemList = (from i in entities.Item_PurchaseOrder
                                where !(entities.Items.Any(x => x.ItemCode == i.ItemCode) && (i.PurchaseOrder.OrderDate >= startDate && endDate >= i.PurchaseOrder.OrderDate))
                                join item in entities.Items on i.ItemCode equals item.ItemCode
                                where item.ReorderLevel>=item.BalanceQty
                                select new ShortfallItems
                                {
                                    ItemCode = item.ItemCode,
                                    Description = item.Description,
                                    ReorderQuantity = item.ReorderQty,
                                    ReorderLevel = item.ReorderLevel,
                                    UnitOfMeasure = item.UnitOfMeasure,
                                    Balance = item.BalanceQty,
                                    NullablePurchaseOrderNo = "-",
                                    ExpectedDate = null,
                                }).ToList();

        return lowStockItemList;
    }

    public static List<DateTime?> GetAllFinalisedReorderMonths()
    {
        using (TransactionScope ts = new TransactionScope())
        {
            StationeryEntities dbInstance = new StationeryEntities();
            List<DateTime?> allMonths = dbInstance.PurchaseOrders.Where(o => o.Status.Equals("Closed")).OrderByDescending(c => c.OrderDate).Select(o => o.OrderDate).ToList();

            ts.Complete();
            return allMonths;
        }
    }
}
