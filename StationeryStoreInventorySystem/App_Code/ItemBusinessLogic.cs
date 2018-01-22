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
    public void updateItem(string itemCode, string categoryName, string description, int reorderLevel, int reorderQty, string unitOfMeasure, string bin)
    {
        Category category = getCategorybyName(categoryName);
        itemDB.updateItem(itemCode, category, description, reorderLevel, reorderQty, unitOfMeasure, bin);
        return;
    }
    public Item GetItembyItemCode(string itemCode)
    {
        return itemDB.GetItembyItemCode(itemCode);
    }
    public void removeItem(string itemCode)
    {
        itemDB.removeItem(itemCode);
        return;
    }
    public List<Item> getItemList()
    {
        return itemDB.getActiveItemList();
    }
    public List<Item> getCatalogueList()
    {
        return itemDB.getCatalogueList();
    }
    public List<Category> GetCategoryList()
    {
        return categoryDB.GetCategoryList();
    }
    public Category GetCategorybyID(int categoryID)
    {
        return categoryDB.GetCategorybyID(categoryID);
    }
    public Category getCategorybyName(string categoryName)
    {
        string i = firstUpperCase(categoryName);
        return categoryDB.getCategorybyName(i);
    }
    public void addCategory(string categoryName)
    {
        Category cat = new Category();
        cat.CategoryName = categoryName;
        categoryDB.addCategory(cat);
    }
    public List<string> getDistinctUOMList()
    {
        return itemDB.getDistinctUOMList();
    }
    public string firstUpperCase(string s)
    {
        return s.First().ToString().ToUpper() + s.Substring(1).ToLower();
    }
}