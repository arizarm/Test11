using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;

/// <summary>
/// Summary description for PurchaseController
/// </summary>
public class PurchaseController
{
    StationeryEntities entities;

    List<PurchaseItems> lowStockItemList = null;

    public PurchaseController()
    {
        entities = new StationeryEntities();
    }

    public List<PurchaseItems> GetReorderItemList()
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

    //public List<ShortfallItems> GenerateReorderReportForPurchasedItems(DateTime startDate,DateTime endDate)
    //{     
    //  var lowStockItemList = (from Stock in entities.StockCards
    //                    group Stock by Stock.ItemCode into stck
    //                    join item in entities.Items on stck.Key equals item.ItemCode
    //                    where item.ReorderLevel >= stck.Min(x => x.Balance)
    //                    select new ShortfallItems
    //                    {
    //                        ItemCode = item.ItemCode,
    //                        Description = item.Description,
    //                        ReorderQuantity = item.ReorderQty,
    //                        ReorderLevel = item.ReorderLevel,
    //                        UnitOfMeasure = item.UnitOfMeasure,
    //                        Balance = stck.Min(x => x.Balance)
    //                    }).ToList<ShortfallItems>();

    //    List<Item_PurchaseOrder> itemList = entities.Item_PurchaseOrder.ToList();

    //    List <ShortfallItems>shortfallItemList = lowStockItemList.Join(itemList, a => a.ItemCode, b => b.ItemCode, (a, b) => new ShortfallItems
    //                                          {
    //                                                ItemCode = a.ItemCode,
    //                                                Description = a.Description,
    //                                                ReorderQuantity = a.ReorderQuantity,
    //                                                ReorderLevel = a.ReorderLevel,
    //                                                UnitOfMeasure = a.UnitOfMeasure,
    //                                                Balance = a.Balance,
    //                                                PurchaseOrderNo=b.PurchaseOrderID,
    //                                                ExpectedDate = b.PurchaseOrder.ExpectedDate,
    //                                                OrderDate = b.PurchaseOrder.OrderDate,
    //                                           }).Where(x=>x.OrderDate >=startDate && x.OrderDate <= endDate).ToList();



    //    return shortfallItemList;
    //}


    //public List<ShortfallItems> GenerateShortfallItemsReport()
    //{
    //    var lowStockItemList = (from Stock in entities.StockCards
    //                            group Stock by Stock.ItemCode into stck
    //                            join item in entities.Items on stck.Key equals item.ItemCode                              
    //                            where item.ReorderLevel >= stck.Min(x => x.Balance)
    //                            select new ShortfallItems
    //                            {
    //                                ItemCode = item.ItemCode,
    //                                Description = item.Description,
    //                                ReorderQuantity = item.ReorderQty,
    //                                ReorderLevel = item.ReorderLevel,
    //                                UnitOfMeasure = item.UnitOfMeasure,
    //                                Balance = stck.Min(x => x.Balance),
    //                                PurchaseOrderNo = 0,
    //                                ExpectedDate = null,
    //                            }).ToList<ShortfallItems>();

    //    return lowStockItemList;

    //}
    public List<SupplierInfo> GetSupplierList()
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
    public List<SupplierInfo> GetSupplierListByItemCode(String itemCode)
    {
        var supplierList = (from item in entities.Items
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
        return supplierList;
    }

    public List<PurchaseItems> AddItems(String itemCode)
    {
        var itemList = (from Stock in entities.StockCards
                        group Stock by Stock.ItemCode into stck
                        join item in entities.Items on stck.FirstOrDefault().ItemCode equals item.ItemCode
                        where item.ItemCode == itemCode
                        select new PurchaseItems
                        {
                            ItemCode = item.ItemCode,
                            Description = item.Description,
                            ReorderQty = item.ReorderQty,
                            ReorderLevel = item.ReorderLevel,
                            UnitOfMeasure = item.UnitOfMeasure,
                            Balance = stck.Min(x => x.Balance)
                        }).ToList<PurchaseItems>();
        if (itemList != null)
        {
            itemList = (from item in entities.Items
                        where item.ItemCode == itemCode
                        select new PurchaseItems
                        {
                            ItemCode = item.ItemCode,
                            Description = item.Description,
                            ReorderQty = item.ReorderQty,
                            ReorderLevel = item.ReorderLevel,
                            UnitOfMeasure = item.UnitOfMeasure,
                            Balance = 0
                        }).ToList<PurchaseItems>();
        }
        return itemList;
    }
    public List<Item> GetItemList()
    {
        List<Item> itemList = (from item in entities.Items
                               select item).ToList<Item>();
        return itemList;
    }

    public List<Employee> GetSupervisorList()
    {
        List<Employee> suprvsrList = (from suprvsr in entities.Employees
                                      select suprvsr).Where(x => x.Role == "Store Supervisor").ToList<Employee>();

        return suprvsrList;
    }

    public void AddPurchaseOrder(Dictionary<PurchaseOrder,List<Item_PurchaseOrder>> orderItems)
    {
        decimal? totAmount = 0;
        try
        {
            foreach(var order in orderItems)
            {
                entities.PurchaseOrders.Add(order.Key);
                entities.SaveChanges();
                foreach(var items in order.Value)
                {
                    totAmount += items.Amount;
                    
                    items.PurchaseOrderID = order.Key.PurchaseOrderID;
                    entities.Item_PurchaseOrder.Add(items);
                   
                }

               PurchaseOrder porder =  entities.PurchaseOrders.Where(x => x.PurchaseOrderID == order.Key.PurchaseOrderID).FirstOrDefault();
                porder.TotalAmount = totAmount;
                totAmount = 0;
                entities.SaveChanges();
            }
           
          //  Utility.sendMail("williams@logicuniversity.com", "Purchase order", "Please find the order for items and approve to proceed");
        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public List<PurchaseOrder> GetPurchaseOrderList()
    {
        List<PurchaseOrder> purchaseorderList = (from pOrder in entities.PurchaseOrders select pOrder).ToList();
        return purchaseorderList;
    }

    public List<PurchaseOrder> GetPurchaseOrderListByStatus(string status)
    {
        List<PurchaseOrder> purchaseorderList = (from pOrder in entities.PurchaseOrders
                                                 where pOrder.Status ==status
                                                 select pOrder ).ToList();
        return purchaseorderList;
    }

    public List<PurchaseOrder> SearchPurchaseOrderByID(int orderID)
    {
       
      
        return entities.PurchaseOrders.Where(x => x.PurchaseOrderID == orderID).ToList();
       
       
    }
    public PurchaseOrder GetPurchaseOrderByID(int orderID)
    {


        return entities.PurchaseOrders.Where(x => x.PurchaseOrderID == orderID).First();


    }
    //public List<Item_PurchaseOrder> GetPurchaseOrderItems(int orderID)
    //{
    //    return entities.Item_PurchaseOrder.Where(x => x.PurchaseOrderID == orderID).ToList();


    //}
    public List<PurchaseOrderItemDetails> GetPurchaseOrderItemsDetails(int orderID)
    {
        List<PurchaseOrderItemDetails> porderDetails = (from pOrderDetail in entities.Item_PurchaseOrder                                                       
                                                        join pOrder in entities.PurchaseOrders on pOrderDetail.PurchaseOrderID equals pOrder.PurchaseOrderID
                                                        where pOrderDetail.PurchaseOrderID ==orderID
                                                        select new PurchaseOrderItemDetails
                                                        {
                                                            ItemCode = pOrderDetail.ItemCode,
                                                            Description = pOrderDetail.Item.Description,
                                                            OrderQty = pOrderDetail.OrderQty,
                                                            Price = pOrderDetail.Amount/ pOrderDetail.OrderQty,
                                                            TotalAmount = pOrderDetail.Amount

                                                        }).ToList();

        return porderDetails;
                                                      


    }
    public void UpdatePurchaseOrder(PurchaseOrder pOrder)
    {
        PurchaseOrder order = entities.PurchaseOrders.Where(x => x.PurchaseOrderID == pOrder.PurchaseOrderID).First();
        order.ApprovedByDate = pOrder.ApprovedByDate;       
        order.Remarks = pOrder.Remarks;
        order.Status = pOrder.Status;
        order.ApprovedBy = pOrder.ApprovedBy;
        entities.SaveChanges();
      
    }
   
    public void ClosePurchaseOrder(PurchaseOrder pOrder)
    {
        PurchaseOrder order = entities.PurchaseOrders.Where(x => x.PurchaseOrderID == pOrder.PurchaseOrderID).First();        
        order.DONumber = pOrder.DONumber;
        order.Status = pOrder.Status;
        order.ExpectedDate = DateTime.Now.Date;

        List<Item_PurchaseOrder> itemList = entities.Item_PurchaseOrder.Where(x => x.PurchaseOrderID == order.PurchaseOrderID).ToList();
        foreach(Item_PurchaseOrder item in itemList)
        {
            StockCard itemStockCard = (from stck in entities.StockCards.OrderBy(x=>x.Balance)
                                       where stck.ItemCode ==item.ItemCode                                     
                                       select stck).FirstOrDefault();
            itemStockCard.Qty = item.OrderQty;
            itemStockCard.Balance = itemStockCard.Balance + itemStockCard.Qty;
        }
        entities.SaveChanges();

    }

    public void UpdatePurchaseItem(Item_PurchaseOrder pItem)
    {
        using (TransactionScope ts = new TransactionScope())
        {
            Item_PurchaseOrder item = entities.Item_PurchaseOrder.Where(x => x.ItemCode == pItem.ItemCode && 
                                        x.PurchaseOrderID == pItem.PurchaseOrderID).First();
            item.OrderQty = pItem.OrderQty;
            item.Amount = pItem.Amount;            
            entities.SaveChanges();
            ts.Complete();
            
        }

    }




}