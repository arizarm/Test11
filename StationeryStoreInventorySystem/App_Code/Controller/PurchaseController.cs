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
    //StationeryEntities entities;

    List<PurchaseItems> lowStockItemList = null;

    public PurchaseController()
    {
        //entities = new StationeryEntities();
    }

    public List<PurchaseItems> GetReorderItemList()
    {

        return EFBroker_PurchaseOrder.GetReorderItemList();
    }

    public List<ShortfallItems> GenerateReorderReportForPurchasedItems(DateTime startDate, DateTime endDate)
    {
        return EFBroker_PurchaseOrder.GenerateReorderReportForPurchasedItems(startDate, endDate);
    }


    public List<ShortfallItems> GenerateShortfallItemsReport(DateTime startDate, DateTime endDate)
    {
        return EFBroker_PurchaseOrder.GenerateShortfallItemsReport(startDate, endDate);

    }
    public List<SupplierInfo> GetSupplierList()
    {
        return EFBroker_PurchaseOrder.GetPurchaseSupplierInfoList();
    }
    public List<SupplierInfo> GetSupplierListByItemCode(string itemCode)
    {
        return EFBroker_PurchaseOrder.GetPurchaseSupplierListByItemCode(itemCode);
    }

    //public List<PurchaseItems> AddItems(String itemCode)
    //{
    //    var itemList = (from Stock in entities.StockCards
    //                    group Stock by Stock.ItemCode into stck
    //                    join item in entities.Items on stck.FirstOrDefault().ItemCode equals item.ItemCode
    //                    where item.ItemCode == itemCode
    //                    select new PurchaseItems
    //                    {
    //                        ItemCode = item.ItemCode,
    //                        Description = item.Description,
    //                        ReorderQty = item.ReorderQty,
    //                        ReorderLevel = item.ReorderLevel,
    //                        UnitOfMeasure = item.UnitOfMeasure,
    //                        Balance = stck.Min(x => x.Balance)
    //                    }).ToList<PurchaseItems>();
    //    if (itemList != null)
    //    {
    //        itemList = (from item in entities.Items
    //                    where item.ItemCode == itemCode
    //                    select new PurchaseItems
    //                    {
    //                        ItemCode = item.ItemCode,
    //                        Description = item.Description,
    //                        ReorderQty = item.ReorderQty,
    //                        ReorderLevel = item.ReorderLevel,
    //                        UnitOfMeasure = item.UnitOfMeasure,
    //                        Balance = 0
    //                    }).ToList<PurchaseItems>();
    //    }
    //    return itemList;
    //}
    public PurchaseItems AddPurchaseItem(String itemCode)
    {
        Item item = EFBroker_Item.GetActiveItembyItemCode(itemCode);
        PurchaseItems purchaseItem = new PurchaseItems
        {
            ItemCode = item.ItemCode,
            Description = item.Description,
            ReorderQty = item.ReorderQty,
            ReorderLevel = item.ReorderLevel,
            UnitOfMeasure = item.UnitOfMeasure,
        };
        if (item.BalanceQty == null || item.BalanceQty == 0)
        {
            purchaseItem.Balance = 0;
        }
        else
        {
            purchaseItem.Balance = item.BalanceQty;
        }
        return purchaseItem;
    }
    public List<Item> GetItemList()
    {
        //Active or All items?
        return EFBroker_Item.GetItemList();
    }

    public List<Employee> GetSupervisorList()
    {
        return EFBroker_DeptEmployee.GetEmployeeListByRole("Store Supervisor");
    }

    public void AddPurchaseOrder(Dictionary<PurchaseOrder, List<Item_PurchaseOrder>> orderItems)
    {
        try
        {
            EFBroker_PurchaseOrder.AddPurchaseOrder(orderItems);
            Utility.sendMail("williams@logicuniversity.com", "Purchase order", "Please find the order for items and approve to proceed");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public List<PurchaseOrder> GetPurchaseOrderList()
    {
        return EFBroker_PurchaseOrder.GetPurchaseOrderList();
    }

    public List<PurchaseOrder> GetPurchaseOrderListByStatus(string status)
    {
        return EFBroker_PurchaseOrder.GetPurchaseOrderListByStatus(status);
    }

    public List<PurchaseOrder> SearchPurchaseOrderByID(int orderID)
    {
        return EFBroker_PurchaseOrder.GetPurchaseOrderListByOrderID(orderID);
    }
    public PurchaseOrder GetPurchaseOrderByID(int orderID)
    {
        return EFBroker_PurchaseOrder.GetPurchaseOrderById(orderID);
    }
 
    public List<PurchaseOrderItemDetails> GetPurchaseOrderItemsDetails(int orderID)
    {
        return EFBroker_PurchaseOrder.GetPurchaseOrderItemsDetailList(orderID);

    }
    public void UpdatePurchaseOrder(PurchaseOrder pOrder)
    {
        EFBroker_PurchaseOrder.UpdatePurchaseOrder(pOrder);
        return;
    }

    public void ClosePurchaseOrder(PurchaseOrder pOrder)
    {
        EFBroker_PurchaseOrder.ClosePurchaseOrder(pOrder);
    }

    public void UpdatePurchaseItem(Item_PurchaseOrder pItem)
    {
        EFBroker_PurchaseOrder.UpdatePurchaseItem(pItem);
        return;
    }




}