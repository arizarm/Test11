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

    List<ReorderItem> lowStockItemList = null;

    public PurchaseController()
    {
        //entities = new StationeryEntities();
    }

    public List<ReorderItem> GetReorderItemList()
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
    public List<ItemPrice> GetItemPriceList()
    {
        return EFBroker_PurchaseOrder.GetItemPriceList();
    }
    public List<ItemPrice> GetItemPriceByItemCode(string itemCode)
    {
        return EFBroker_PurchaseOrder.GetItemPriceByItemCode(itemCode);
    }

  
    public ReorderItem AddPurchaseItem(String itemCode)
    {
        Item item = EFBroker_Item.GetActiveItembyItemCode(itemCode);
        ReorderItem purchaseItem = new ReorderItem
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
    
    public void DeletePurchaseOrder(int orderID)
    {
        EFBroker_PurchaseOrder.DeletePurchaseOrder(orderID);
    }




}