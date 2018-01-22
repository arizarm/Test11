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
    public ItemBusinessLogic()
    {
        inventoryDB = new StationeryEntities();
        //
        // TODO: Add constructor logic here
        //
    }
    public void updateItem(string itemCode, string categoryName, string description, int reorderLevel, int reorderQty, string unitOfMeasure, string bin)
    {
        Item i = getItem(itemCode);
        Category category = getCategorybyName(categoryName);
        i.CategoryID = category.CategoryID;
        i.Description = description;
        i.ReorderLevel = reorderLevel;
        i.ReorderQty = reorderQty;
        i.UnitOfMeasure = unitOfMeasure;
        i.Bin = bin;
        inventoryDB.SaveChanges();
        return;
    }
    public void addItem(Item item)
    {
        inventoryDB.Items.Add(item);
        inventoryDB.SaveChanges();
        return;
    }
    public Item getItem(string itemCode)
    {
        Item result = inventoryDB.Items.Where(x => x.ItemCode == itemCode).FirstOrDefault();
        return result;
    }
    public void removeItem(string itemCode)
    {
        Item i = inventoryDB.Items.Where(x => x.ItemCode == itemCode).FirstOrDefault();
        i.ActiveStatus = "N";
        inventoryDB.SaveChanges();
    }
    public List<Item> getItemList()
    {
        List<Item> itemList = inventoryDB.Items
            .Where(db => db.ActiveStatus == "Y")
            .ToList();
        return itemList;
    }
    public List<Item> getCatalogueList()
    {
        List<Item> catalogue =
            inventoryDB.Items
            .Include("Category")
            .Where(db => db.ActiveStatus == "Y")
            .ToList();
        return catalogue;
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