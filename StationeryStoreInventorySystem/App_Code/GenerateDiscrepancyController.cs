using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for GenerateDiscrepancyController
/// </summary>
public class GenerateDiscrepancyController
{
    public GenerateDiscrepancyController()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static List<InventoryItem> GetInventoryWithStock()
    {
        StationeryEntities context = new StationeryEntities();
        List<Item> iList = new List<Item>();
        iList = context.Items.Include("StockCards").OrderBy(x => x.ItemCode).ToList();
        //iList = context.Items.ToList();       //Commented are alt methods
        List<InventoryItem> invItemList = new List<InventoryItem>();
        foreach(Item i in iList)
        {
            //List<StockCard> sc = context.StockCards.Where(x => x.ItemCode == i.ItemCode).ToList();
            //InventoryItem iItem = new InventoryItem(i, sc[(sc.Count - 1)]);
            InventoryItem iItem = new InventoryItem(i, i.StockCards.Last().Balance.ToString());
            invItemList.Add(iItem);
        }
        return invItemList;
    }

    public static Item GetItemByItemCode(string itemCode)
    {
        StationeryEntities context = new StationeryEntities();
        Item i = context.Items.Where(x => x.ItemCode == itemCode).First();
        return i;
    }
}