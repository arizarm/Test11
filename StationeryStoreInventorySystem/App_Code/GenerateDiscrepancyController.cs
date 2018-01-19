﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for GenerateDiscrepancyController
/// </summary>
public class GenerateDiscrepancyController
{
    private static StationeryEntities context = new StationeryEntities();
    public GenerateDiscrepancyController()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static List<InventoryItem> GetInventoryWithStock()
    {
        List<Item> iList = new List<Item>();
        iList = GetAllItems();
        List<InventoryItem> invItemList = new List<InventoryItem>();
        foreach (Item i in iList)
        {
            List<StockCard> sc = GetStockCardsByItemCode(i.ItemCode);
            InventoryItem iItem = new InventoryItem(i, sc.Last().Balance.ToString());
            //InventoryItem iItem = new InventoryItem(i, i.StockCards.Last().Balance.ToString());
            invItemList.Add(iItem);
        }
        return invItemList;
    }

    public static void SubmitDiscrepancies(List<Discrepency> dList)
    {
        foreach (Discrepency d in dList)
        {
            if (Math.Abs((decimal)d.TotalDiscrepencyAmount) < 250)
            {
                d.ApprovedBy = GetEmployeeByRole("Store Supervisor").EmpID;
            }
            else
            {
                d.ApprovedBy = GetEmployeeByRole("Store Manager").EmpID;
            }
        }
        SaveDiscrepancies(dList);
    }

    public static List<Item> GetAllItems()
    {   //goes to item broker
        return context.Items.OrderBy(x => x.ItemCode).ToList();
    }

    public static Item GetItemByItemCode(string itemCode)
    {   //goes to item broker
        Item i = context.Items.Where(x => x.ItemCode == itemCode).First();
        return i;
    }

    public static List<PriceList> GetPricesByItemCode(string itemCode)
    {   //goes to price list broker
        List<PriceList> prices = context.PriceLists.Where(x => x.ItemCode == itemCode).ToList();
        return prices;
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
    {   //goes to discrepancy id
        return context.Discrepencies.Where(x => x.ItemCode == d.ItemCode && x.RequestedBy == d.RequestedBy && x.Date == d.Date && x.AdjustmentQty == d.AdjustmentQty && x.Remarks == d.Remarks).Select(x => x.DiscrepencyID).First();
    }

    public static List<StockCard> GetStockCardsByItemCode(string itemCode)
    {   //goes to stock card broker
        return context.StockCards.Where(x => x.ItemCode == itemCode).ToList();
    }

    public static void UpdateStockCards(List<Discrepency> dList)
    {   //goes to stock card broker
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
}