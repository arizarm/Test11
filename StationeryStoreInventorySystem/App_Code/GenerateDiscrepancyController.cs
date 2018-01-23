﻿using System;
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
        foreach (Discrepency d in dList)
        {
            context.Discrepencies.Add(d);
        }
        context.SaveChanges();
    }

    public static int GetDiscrepancyID(Discrepency d)
    {   //goes to discrepancy broker
        return context.Discrepencies.Where(x => x.ItemCode == d.ItemCode && x.RequestedBy == d.RequestedBy && x.Date == d.Date && x.AdjustmentQty == d.AdjustmentQty && x.Remarks == d.Remarks).Select(x => x.DiscrepencyID).First();
    }

    public static List<Discrepency> GetPendingDiscrepanciesByItemCode(string itemCode)
    {   //goes to discrepancy broker
        List<Discrepency> dList = new List<Discrepency>();
        dList = context.Discrepencies.Where(x => x.ItemCode == itemCode && x.Status == "Pending").ToList();
        return dList;
    }

    public static Discrepency GetPendingMonthlyDiscrepancyByItemCode(string itemCode)
    {   //goes to discrepancy broker
        List<Discrepency> dList = context.Discrepencies.Where(x => x.ItemCode == itemCode && x.Status == "Monthly").ToList();
        if (dList.Count != 0)
        {
            return dList[0];
        }
        else
        {
            return null;
        }
    }

    public static Discrepency GetDiscrepancyById(int id)
    {   //goes to discrepancy broker
        return context.Discrepencies.Where(x => x.DiscrepencyID == id).First();
    }

    public static List<Discrepency> GetAllPendingDiscrepancies()
    {
        List<Discrepency> dList = context.Discrepencies.Where(x => x.Status == "Pending").ToList();
        return dList;
    }

    public static List<Discrepency> GetAllPendingMonthlyDiscrepancies()
    {
        List<Discrepency> dList = context.Discrepencies.Where(x => x.Status == "Monthly").ToList();
        return dList;
    }

    public static Disbursement GetDisbursementById(int id)
    {   //goes to disbursement broker
        return context.Disbursements.Where(x => x.DisbursementID == id).First();
    }

    public static Disbursement_Item GetDisbursementItem(int id, string itemCode)
    {   //not needed
        return context.Disbursement_Item.Where(x => x.DisbursementID == id && x.ItemCode == itemCode).First();
    }

    public static List<StockCard> GetStockCardsByItemCode(string itemCode)
    {   //goes to stock card broker
        return context.StockCards.Where(x => x.ItemCode == itemCode).ToList();
    }

    public static void UpdateStockCards(List<Discrepency> dList)
    {   //not needed
        foreach (Discrepency d in dList)
        {
            StockCard sc = new StockCard();
            sc.ItemCode = d.ItemCode;
            sc.TransactionType = "Discrepancy";
            sc.Qty = d.AdjustmentQty;
            sc.Balance = GetStockCardsByItemCode(d.ItemCode).Last().Balance + d.AdjustmentQty;
            sc.TransactionDetailID = GetDiscrepancyID(d);
            context.StockCards.Add(sc);
        }
        context.SaveChanges();
    }

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