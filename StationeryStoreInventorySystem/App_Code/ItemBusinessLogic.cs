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
        categoryName = Utility.FirstUpperCase(categoryName);
        item.ItemCode = itemCode;
        item.Description = description;
        item.ReorderLevel = Convert.ToInt32(reorderLevel);
        item.ReorderQty = Convert.ToInt32(reorderQty);
        item.UnitOfMeasure = UOM;
        item.Bin = bin;
        item.ActiveStatus = "C";
        item.BalanceQty = 0;
        Category cat = EFBroker_Category.GetCategorybyName(categoryName);
        if (cat != null)
        {
            item.CategoryID = cat.CategoryID;
            EFBroker_Item.AddItem(item);
        }
        else
        {
            EFBroker_Item.AddItemAndCategory(item, categoryName);
            cat = EFBroker_Category.GetCategorybyName(categoryName);
        }
        item.Category = cat;
        return item;
    }
    public static bool ValidateNewItemfields(string itemCode, string categoryName, string description, string reorderLevel, string reorderQty, string UOM, string bin)
    {
        if (IsValidItemFields(itemCode, categoryName, description, reorderLevel, reorderQty, UOM, bin) && EFBroker_Item.GetItembyItemCode(itemCode.ToUpper()) == null)
        {
            return true;
        }
        else return false;
    }
    public static bool IsValidItemFields(string itemCode, string categoryName, string description, string reorderLevel, string reorderQty, string UOM, string bin)
    {
        bool sucesss = true;
        int level, qty;
        if (string.IsNullOrEmpty(itemCode) || string.IsNullOrEmpty(categoryName) || string.IsNullOrEmpty(description) || string.IsNullOrEmpty(UOM) || string.IsNullOrEmpty(reorderLevel) || string.IsNullOrEmpty(reorderQty))
        {
            sucesss = false;
        }
        else if (!(int.TryParse(reorderLevel, out level) && int.TryParse(reorderQty, out qty)))
        {
            sucesss = false;
        }
        else if (ValidatorUtil.IsInvalidfieldLength(itemCode, 10) || ValidatorUtil.IsInvalidfieldLength(categoryName, 10) || ValidatorUtil.IsInvalidfieldLength(description, 50) || ValidatorUtil.IsInvalidfieldLength(UOM, 10) || ValidatorUtil.IsInvalidfieldLength(bin, 10))
        {
            sucesss = false;
        }
        else
        {
            Item i = EFBroker_Item.GetItembyDescription(description);
            if ( i!= null && !itemCode.Equals(i.ItemCode))
            {
                sucesss = false;
            }
        }

        return sucesss;
    }
    public static void UpdateItem(string itemCode, string categoryName, string description, int reorderLevel, int reorderQty, string unitOfMeasure, string bin)
    {
        Category category = EFBroker_Category.GetCategorybyName(categoryName);
        Item i = EFBroker_Item.GetItembyItemCode(itemCode);
        if (i != null)
        {
            i.CategoryID = category.CategoryID;
            i.Description = description;
            i.ReorderLevel = reorderLevel;
            i.ReorderQty = reorderQty;
            i.UnitOfMeasure = unitOfMeasure;
            i.Bin = bin;
        }
        EFBroker_Item.UpdateItem(i);
        return;
    }
    public static void ActivateItem(string itemCode)
    {
        Item item = EFBroker_Item.GetItembyItemCode(itemCode);
        item.ActiveStatus = "Y";
        EFBroker_Item.UpdateItem(item);
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
    public static List<Item> GetCatalogueList()
    {
        return EFBroker_Item.GetCatalogueList();
    }
    public static List<InventoryReportItem> GetInventoryReportItemList()
    {
        List<InventoryReportItem> reportItemList = new List<InventoryReportItem>();
        List<Item> iList = EFBroker_Item.GetActiveItemList();
        foreach (Item i in iList)
        {
            InventoryReportItem rItem = new InventoryReportItem(i);
            reportItemList.Add(rItem);
        }
        return reportItemList;
    }
    public static List<InventoryReportItem> GetSelectedInventoryReportItemList(string search)
    {
        List<InventoryReportItem> reportItemList = new List<InventoryReportItem>();
        List<Item> iList = EFBroker_Item.SearchItemsByItemCodeOrDesc(search);
        foreach (Item i in iList)
        {
            InventoryReportItem rItem = new InventoryReportItem(i);
            reportItemList.Add(rItem);
        }
        return reportItemList;
    }
    public static List<Category> GetCategoryList()
    {
        return EFBroker_Category.GetCategoryList();
    }
    public static Category GetCategorybyID(int categoryID)
    {
        return EFBroker_Category.GetCategorybyID(categoryID);
    }
    public static List<string> GetDistinctUOMList()
    {
        return EFBroker_Item.GetDistinctUOMList();
    }

}