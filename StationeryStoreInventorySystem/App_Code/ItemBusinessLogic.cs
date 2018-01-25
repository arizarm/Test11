using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Reflection;

/// <summary>
/// Summary description for ItemBusinessLogic
/// </summary>
public class ItemBusinessLogic
{
    //EFBroker_Item EFBroker_Item;
    //EFBroker_Category categoryDB;
    public ItemBusinessLogic()
    {
        //EFBroker_Item = new EFBroker_Item();
        //categoryDB = new EFBroker_Category();
        //
        // TODO: Add constructor logic here
        //
    }
    public static Item AddItem(string itemCode, string categoryName, string description, string reorderLevel, string reorderQty, string UOM, string bin)
    {
        Item item = new Item();
        int level, qty;
        if (string.IsNullOrEmpty(itemCode) || string.IsNullOrEmpty(categoryName) || string.IsNullOrEmpty(description) || string.IsNullOrEmpty(UOM) || string.IsNullOrEmpty(reorderLevel) || string.IsNullOrEmpty(reorderQty))
        {
            return null;
        }
        else if (!int.TryParse(reorderLevel, out level) || !int.TryParse(reorderQty, out qty))
        {
            return null;
        }
        else if (EFBroker_Item.GetItembyItemCode(itemCode.ToUpper()) != null)
        {
            return null;
        }
        else
        {
            categoryName = Utility.FirstUpperCase(categoryName);
            item.ItemCode = itemCode;
            item.Description = description;
            item.ReorderLevel = level;
            item.ReorderQty = qty;
            item.UnitOfMeasure = UOM;
            item.Bin = bin;
            item.ActiveStatus = "Y";
            item.BalanceQty = 0;
            Category cat = EFBroker_Category.GetCategorybyName(categoryName);
            if (cat != null)
            {
                item.Category = cat;
                EFBroker_Item.AddItem(item);
            }
            else
            {
                EFBroker_Item.AddItemAndCategory(item, categoryName);
                cat = EFBroker_Category.GetCategorybyName(categoryName);
                item.Category = cat;
            }
        }
        return item;
    }
    public static void UpdateItem(string itemCode, string categoryName, string description, int reorderLevel, int reorderQty, string unitOfMeasure, string bin)
    {
        Category category = EFBroker_Category.GetCategorybyName(categoryName);
        Item i =EFBroker_Item.GetItembyItemCode(itemCode);
        if (i != null)
        {
            i.Category = category;
            i.Description = description;
            i.ReorderLevel = reorderLevel;
            i.ReorderQty = reorderQty;
            i.UnitOfMeasure = unitOfMeasure;
            i.Bin = bin;
        }
        EFBroker_Item.UpdateItem(i);
        return;
    }
    public static Item GetItembyItemCode(string itemCode)
    {
        return EFBroker_Item.GetItembyItemCode(itemCode);
    }
    public static void RemoveItem(string itemCode)
    {
        EFBroker_Item.RemoveItem(itemCode);
        return;
    }
    public static List<Item> GetItemList()
    {
        return EFBroker_Item.GetActiveItemList();
    }
    public static List<Item> GetCatalogueList()
    {
        return EFBroker_Item.GetCatalogueList();
    }
    public static List<InventoryReportItem> GetInventoryReportItemList()
    {
        List<InventoryReportItem> reportItemList = new List<InventoryReportItem>();
        List<Item> iList = GetItemList();
        foreach (Item i in iList)
        {
            InventoryReportItem rItem = new InventoryReportItem(i);
            reportItemList.Add(rItem);
        }
        return reportItemList;
    }
    public static  List<Category> GetCategoryList()
    {
        return EFBroker_Category.GetCategoryList();
    }
    public static  Category GetCategorybyID(int categoryID)
    {
        return EFBroker_Category.GetCategorybyID(categoryID);
    }
    public static List<string> GetDistinctUOMList()
    {
        return EFBroker_Item.GetDistinctUOMList();
    }

}