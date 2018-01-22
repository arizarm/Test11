using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for EFBroker_Item
/// </summary>
public class EFBroker_Item
{
    StationeryEntities inventoryDB;
    public EFBroker_Item()
    {
        inventoryDB = new StationeryEntities();
        //
        // TODO: Add constructor logic here
        //
    }
    public void addItem(Item item)
    {
        inventoryDB.Items.Add(item);
        inventoryDB.SaveChanges();
        return;
    }
    public Item GetItembyItemCode(string itemCode)
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
        List<Item> itemList = inventoryDB.Items.ToList();
        return itemList;
    }
    public List<Item> getActiveItemList()
    {
        List<Item> itemList = inventoryDB.Items.Where(x=> x.ActiveStatus=="Y").ToList();
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
    public List<string> getDistinctUOMList()
    {
        List<string> uom = inventoryDB.Items.Select(x => x.UnitOfMeasure).Distinct().ToList();
        return uom;
    }
    public void updateItem(string itemCode, Category category, string description, int reorderLevel, int reorderQty, string unitOfMeasure, string bin)
    {
        Item i = GetItembyItemCode(itemCode);
        i.CategoryID = category.CategoryID;
        i.Description = description;
        i.ReorderLevel = reorderLevel;
        i.ReorderQty = reorderQty;
        i.UnitOfMeasure = unitOfMeasure;
        i.Bin = bin;
        inventoryDB.SaveChanges();
        return;
    }
}