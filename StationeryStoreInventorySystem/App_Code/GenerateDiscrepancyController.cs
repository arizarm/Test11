using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for GenerateDiscrepancyController
/// </summary>
public class GenerateDiscrepancyController
{
    private static EFBroker_Item items = new EFBroker_Item();
    private static EFBroker_PriceList pricelists = new EFBroker_PriceList();
    private static EFBroker_Discrepancy discrepencies = new EFBroker_Discrepancy();
    private static EFBroker_Disbursement disbursements = new EFBroker_Disbursement();
    private static EFBroker_Disbursement_Item disbursement_Items = new EFBroker_Disbursement_Item();
    private static EFBroker_StockCard stockCards = new EFBroker_StockCard();
    private static StationeryEntities context = new StationeryEntities();
    public GenerateDiscrepancyController()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    //public static List<InventoryItem> GetInventoryWithStock()
    //{    //might be redundant
    //    List<Item> iList = new List<Item>();
    //    iList = GetAllItems();
    //    List<InventoryItem> invItemList = new List<InventoryItem>();
    //    foreach (Item i in iList)
    //    {
    //        List<StockCard> sc = GetStockCardsByItemCode(i.ItemCode);
    //        List<Discrepency> dList = GenerateDiscrepancyController.GetPendingDiscrepanciesByItemCode(i.ItemCode);
    //        int adj = 0;

    //        foreach (Discrepency d in dList)
    //        {
    //            adj += (int)d.AdjustmentQty;
    //        }

    //        string adjStr = "";

    //        if (adj > 0)
    //        {
    //            adjStr = "+" + adj.ToString();
    //        }
    //        else
    //        {
    //            adjStr = adj.ToString();
    //        }

    //        string stock = sc.Last().Balance.ToString() + " (" + adjStr + ")";

    //        InventoryItem iItem = new InventoryItem(i, stock);
    //        //InventoryItem iItem = new InventoryItem(i, i.StockCards.Last().Balance.ToString());
    //        invItemList.Add(iItem);
    //    }
    //    return invItemList;
    //}

    //public static void SubmitDiscrepancies(List<Discrepency> dList)
    //{
    //    foreach (Discrepency d in dList)
    //    {
    //        if (Math.Abs((decimal)d.TotalDiscrepencyAmount) < 250)
    //        {
    //            d.ApprovedBy = GetEmployeeByRole("Store Supervisor").EmpID;
    //        }
    //        else
    //        {
    //            d.ApprovedBy = GetEmployeeByRole("Store Manager").EmpID;
    //        }
    //    }
    //    SaveDiscrepancies(dList);
    //}

    public static List<Item> GetAllItems()
    {   //goes to item broker
        return items.GetActiveItemList();
    }

    public static Item GetItemByItemCode(string itemCode)
    {   //goes to item broker
        Item i = items.GetActiveItembyItemCode(itemCode);
        return i;
    }

    //public static List<Item> GetItemsByItemCodeOrDesc(string search)
    //{   //might be redundant
    //    List<Item> iList = context.Items.Where(x => x.ItemCode.ToLower() == search.ToLower() || x.Description.ToLower() == search.ToLower()).ToList();
    //    return iList;
    //}

    public static List<PriceList> GetPricesByItemCode(string itemCode)
    {   //goes to price list broker
        return pricelists.GetPriceListByItemCode(itemCode);
    }

    public static List<PriceList> GetPriceListsByItemCode(string itemCode)
    {   //goes to price list broker
        return pricelists.GetPriceListByItemCode(itemCode);
    }

    public static Employee GetEmployeeByRole(string role)
    {  //goes to employee broker
        Employee e = context.Employees.Where(x => x.Role == role).First();
        return e;
    }

    public static void SaveDiscrepancies(List<Discrepency> dList)
    {    //goes to discrepancy broker
        discrepencies.SaveDiscrepencies(dList);
    }

    public static int GetDiscrepancyID(Discrepency d)
    {   //goes to discrepancy broker
        return discrepencies.GetDiscrepancyID(d);
    }

    public static List<Discrepency> GetPendingDiscrepanciesByItemCode(string itemCode)
    {   //goes to discrepancy broker
        return discrepencies.GetPendingDiscrepanciesByItemCode(itemCode);
    }

    public static Discrepency GetPendingMonthlyDiscrepancyByItemCode(string itemCode)
    {   //goes to discrepancy broker
        return discrepencies.GetPendingMonthlyDiscrepancyByItemCode(itemCode);
    }

    public static Discrepency GetDiscrepancyById(int id)
    {   //goes to discrepancy broker
        return discrepencies.GetDiscrepancyById(id);
    }

    public static List<Discrepency> GetAllPendingDiscrepancies()
    {
        return discrepencies.GetPendingDiscrepancyList();
    }

    public static List<Discrepency> GetAllPendingMonthlyDiscrepancies()
    {
        return discrepencies.GetMonthlyDiscrepancyList();
    }

    public static Disbursement GetDisbursementById(int id)
    {   //goes to disbursement broker
        return disbursements.GetDisbursmentbyDisbID(id);
    }

    public static Disbursement_Item GetDisbursementItem(int id, string itemCode)
    {   //not needed
        return disbursement_Items.GetDisbursementItem(id, itemCode);
    }

    public static List<StockCard> GetStockCardsByItemCode(string itemCode)
    {   //goes to stock card broker
        return stockCards.GetStockCardsByItemCode(itemCode);
    }

    //public static void UpdateStockCards(List<Discrepency> dList)
    //{   //not needed
    //    foreach (Discrepency d in dList)
    //    {
    //        StockCard sc = new StockCard();
    //        sc.ItemCode = d.ItemCode;
    //        sc.TransactionType = "Discrepancy";
    //        sc.Qty = d.AdjustmentQty;
    //        sc.Balance = GetStockCardsByItemCode(d.ItemCode).Last().Balance + d.AdjustmentQty;
    //        sc.TransactionDetailID = GetDiscrepancyID(d);
    //        context.StockCards.Add(sc);
    //    }
    //    context.SaveChanges();
    //}

    public static PurchaseOrder GetPurchaseOrderById(int id)
    {   //goes to purchase order broker
        return context.PurchaseOrders.Where(x => x.PurchaseOrderID == id).First();
    }

    public static Item_PurchaseOrder GetPurchaseOrderItem(int poID, string itemCode)
    {   //goes to item_purchaseorder broker
        return context.Item_PurchaseOrder.Where(x => x.PurchaseOrderID == poID && x.ItemCode == itemCode).First();
    }

    public static Department GetDepartmentByDeptCode(string deptCode)
    {    //goes to department broker
        return context.Departments.Where(x => x.DeptCode == deptCode).First();
    }
    
}