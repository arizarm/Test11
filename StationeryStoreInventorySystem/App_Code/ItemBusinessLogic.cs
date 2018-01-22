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
    StationeryEntities inventoryDB;
    EFBroker_Item itemDB;
    public ItemBusinessLogic()
    {
        inventoryDB = new StationeryEntities();
        itemDB = new EFBroker_Item();
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
    public List<Category> getCategoryList()
    {
        List<Category> categories = inventoryDB.Categories.OrderBy(x => x.CategoryID).ToList();
        return categories;
    }
    public Category getCategorybyID(int categoryID)
    {
        Category cat = inventoryDB.Categories.Where(x => x.CategoryID == categoryID).FirstOrDefault();
        return cat;
    }
    public Category getCategorybyName(string categoryName)
    {
        string i = firstUpperCase(categoryName);
        Category cat = inventoryDB.Categories.Where(x => x.CategoryName == i).FirstOrDefault();
        return cat;
    }
    public void addCategory(string categoryName)
    {
        Category cat = new Category();
        cat.CategoryName = categoryName;
        inventoryDB.Categories.Add(cat);
        inventoryDB.SaveChanges();
    }
    public List<string> getDistinctUOMList()
    {
        List<string> uom = inventoryDB.Items.Select(x => x.UnitOfMeasure).Distinct().ToList();
        return uom;
    }
    public string firstUpperCase(string s)
    {
        return s.First().ToString().ToUpper() + s.Substring(1).ToLower();
    }
}