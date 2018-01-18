using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ItemLogic
/// </summary>
public class ItemLogic
{
    StationeryEntities inventoryDB;
    public ItemLogic()
    {
        inventoryDB=new StationeryEntities();
        //
        // TODO: Add constructor logic here
        //
    }
    public void updateItem(string itemCode, string categoryID,string description,string reorderLevel,string reorderQty,string unitOfMeasure)
    {
        Item i = getItem(itemCode);
        i.CategoryID = Convert.ToInt32(categoryID);
        i.Description = description;
        i.ReorderLevel = Convert.ToInt32(reorderLevel);
        i.ReorderQty = Convert.ToInt32(reorderQty);
        i.UnitOfMeasure = unitOfMeasure;
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
        Item result=inventoryDB.Items.Where(x => x.ItemCode == itemCode).First();
        return result;
    }
    public void removeItem(string itemCode)
    {
        Item i = inventoryDB.Items.Where(x => x.ItemCode == itemCode).First();
        i.ActiveStatus = "N";
        inventoryDB.SaveChanges();
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
        List<Category> categories= inventoryDB.Categories.OrderBy(x => x.CategoryID).ToList();
        return categories;
    }
    public List<string> getDistinctUOMList()
    {
        List<string> uom = inventoryDB.Items.Select(x => x.UnitOfMeasure).Distinct().ToList();
        return uom;
    }
}