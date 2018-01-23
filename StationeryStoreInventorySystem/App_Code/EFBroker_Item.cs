using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for EFBroker_Item
/// </summary>
public class EFBroker_Item
{
    public EFBroker_Item()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public void AddItem(Item item)
    {
        using (StationeryEntities inventoryDB = new StationeryEntities())
        {
            inventoryDB.Items.Add(item);
            inventoryDB.SaveChanges();
        }
        return;
    }
    public Item GetItembyItemCode(string itemCode)
    {
        using (StationeryEntities inventoryDB = new StationeryEntities())
        {
            Item result = inventoryDB.Items.Where(x => x.ItemCode == itemCode).FirstOrDefault();
            return result;
        }
    }
    public Item GetActiveItembyItemCode(string itemCode)
    {
        using (StationeryEntities inventoryDB = new StationeryEntities())
        {
            Item result = inventoryDB.Items.Where(x => x.ItemCode == itemCode && x.ActiveStatus == "Y").FirstOrDefault();
            return result;
        }
    }
    public Item GetItembyDescription(string description)
    {
        Item result;
        using (StationeryEntities inventoryDB = new StationeryEntities())
        {
            result = inventoryDB.Items.Where(x => x.Description.Equals(description)).FirstOrDefault();
        }
        return result;
    }
    public void RemoveItem(string itemCode)
    {

        using (StationeryEntities inventoryDB = new StationeryEntities())
        {
            Item i = inventoryDB.Items.Where(x => x.ItemCode == itemCode).FirstOrDefault();
            i.ActiveStatus = "N";
            inventoryDB.SaveChanges();
        }
        return;
    }
    public List<Item> GetItemsbyCategoryID(int categoryID)
    {
        List<Item> itemList;
        using (StationeryEntities inventoryDB = new StationeryEntities())
        {
            itemList = inventoryDB.Items.Where(x => x.CategoryID == categoryID).ToList();
            return itemList;
        }
    }
    public List<Item> GetItemList()
    {
        List<Item> itemList;
        using (StationeryEntities inventoryDB = new StationeryEntities())
        {
            itemList = inventoryDB.Items.ToList();
        }
        return itemList;
    }
    public List<Item> GetActiveItemList()
    {
        List<Item> itemList;
        using (StationeryEntities inventoryDB = new StationeryEntities())
        {
            itemList = inventoryDB.Items.Where(x => x.ActiveStatus == "Y").OrderBy(x => x.ItemCode).ToList();
        }
        return itemList;
    }
    public List<Item> GetCatalogueList()
    {
        List<Item> catalogue;
        using (StationeryEntities inventoryDB = new StationeryEntities())
        {
            catalogue =
            inventoryDB.Items
            .Include("Category")
            .Where(db => db.ActiveStatus == "Y")
            .ToList();
        }
        return catalogue;
    }
    public List<string> GetDistinctUOMList()
    {
        List<string> uom;
        using (StationeryEntities inventoryDB = new StationeryEntities())
        {
            uom = inventoryDB.Items.Select(x => x.UnitOfMeasure).Distinct().ToList();
        }
        return uom;
    }
    public string GetUnitbyItemCode(string itemCode)
    {
        string unit;
        using (StationeryEntities inventoryDB = new StationeryEntities())
        {
            unit = inventoryDB.Items.Where(x => x.ItemCode == itemCode).Select(x => x.UnitOfMeasure).FirstOrDefault();
        }
        return unit;
    }
    public void UpdateItem(string itemCode, Category category, string description, int reorderLevel, int reorderQty, string unitOfMeasure, string bin)
    {
        Item i;
        using (StationeryEntities inventoryDB = new StationeryEntities())
        {
            i = GetItembyItemCode(itemCode);
            i.CategoryID = category.CategoryID;
            i.Description = description;
            i.ReorderLevel = reorderLevel;
            i.ReorderQty = reorderQty;
            i.UnitOfMeasure = unitOfMeasure;
            i.Bin = bin;
            inventoryDB.SaveChanges();
        }
        return;
    }
}