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
    public Item AddItem(string itemCode, string categoryName, string description, string reorderLevel, string reorderQty, string UOM, string bin)
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
        else if (EFBroker_Item.GetItembyItemCode(itemCode) != null)
        {
            return null;
        }
        else
        {
            categoryName = FirstUpperCase(categoryName);
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
    public void UpdateItem(string itemCode, string categoryName, string description, int reorderLevel, int reorderQty, string unitOfMeasure, string bin)
    {
        Category category = EFBroker_Category.GetCategorybyName(categoryName);
        EFBroker_Item.UpdateItem(itemCode, category, description, reorderLevel, reorderQty, unitOfMeasure, bin);
        return;
    }
    public Item GetItembyItemCode(string itemCode)
    {
        return EFBroker_Item.GetItembyItemCode(itemCode);
    }
    public void RemoveItem(string itemCode)
    {
        EFBroker_Item.RemoveItem(itemCode);
        return;
    }
    public List<Item> GetItemList()
    {
        return EFBroker_Item.GetActiveItemList();
    }
    public List<Item> GetCatalogueList()
    {
        return EFBroker_Item.GetCatalogueList();
    }
    public List<InventoryReportItem> GetInventoryReportItemList()
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
    public List<Category> GetCategoryList()
    {
        return EFBroker_Category.GetCategoryList();
    }
    public Category GetCategorybyID(int categoryID)
    {
        return EFBroker_Category.GetCategorybyID(categoryID);
    }
    public List<string> GetDistinctUOMList()
    {
        return EFBroker_Item.GetDistinctUOMList();
    }
    public string FirstUpperCase(string s)
    {
        return s.First().ToString().ToUpper() + s.Substring(1).ToLower();
    }
}