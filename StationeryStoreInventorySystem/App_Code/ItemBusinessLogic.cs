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
    EFBroker_Item itemDB;
    EFBroker_Category categoryDB;
    public ItemBusinessLogic()
    {
        itemDB = new EFBroker_Item();
        categoryDB = new EFBroker_Category();
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
        else if (itemDB.GetItembyItemCode(itemCode) != null)
        {
            return null;
        }
        else
        {
            Category cat = categoryDB.GetCategorybyName(categoryName);
            if (cat == null)
            {
                AddCategory(categoryName);
                cat = categoryDB.GetCategorybyName(categoryName);
            }

            item.ItemCode = itemCode;
            item.Category = cat;
            item.Description = description;
            item.ReorderLevel = level;
            item.ReorderQty = qty;
            item.UnitOfMeasure = UOM;
            item.Bin = bin;
            item.ActiveStatus = "Y";
            item.BalanceQty = 0;
            itemDB.AddItem(item);
            //iList.Add(item);
            //Session["itemlist"] = iList;
        }
        return item;
    }
    public void UpdateItem(string itemCode, string categoryName, string description, int reorderLevel, int reorderQty, string unitOfMeasure, string bin)
    {
        Category category = GetCategorybyName(categoryName);
        itemDB.UpdateItem(itemCode, category, description, reorderLevel, reorderQty, unitOfMeasure, bin);
        return;
    }
    public Item GetItembyItemCode(string itemCode)
    {
        return itemDB.GetItembyItemCode(itemCode);
    }
    public void RemoveItem(string itemCode)
    {
        itemDB.RemoveItem(itemCode);
        return;
    }
    public List<Item> GetItemList()
    {
        return itemDB.GetActiveItemList();
    }
    public List<Item> GetCatalogueList()
    {
        return itemDB.GetCatalogueList();
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
        return categoryDB.GetCategoryList();
    }
    public Category GetCategorybyID(int categoryID)
    {
        return categoryDB.GetCategorybyID(categoryID);
    }
    public Category GetCategorybyName(string categoryName)
    {
        string i = FirstUpperCase(categoryName);
        return categoryDB.GetCategorybyName(i);
    }
    public void AddCategory(string categoryName)
    {
        string i = FirstUpperCase(categoryName);
        Category cat = new Category();
        cat.CategoryName = i;
        categoryDB.AddCategory(cat);
    }
    public List<string> GetDistinctUOMList()
    {
        return itemDB.GetDistinctUOMList();
    }
    public string FirstUpperCase(string s)
    {
        return s.First().ToString().ToUpper() + s.Substring(1).ToLower();
    }
}