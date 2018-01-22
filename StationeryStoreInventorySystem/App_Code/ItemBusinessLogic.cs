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