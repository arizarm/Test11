using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Transactions;

/// <summary>
/// Summary description for EFBroker_Item
/// </summary>
/// 
//AUTHOR : TAN WEN SONG
public class EFBroker_Item
{
    public EFBroker_Item()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static void AddItem(Item item)
    {
        using (StationeryEntities inventoryDB = new StationeryEntities())
        {
            inventoryDB.Items.Add(item);
            inventoryDB.SaveChanges();
        }
        return;
    }
    public static void AddItemAndCategory(Item item, string categoryName)
    {
        Category cat = new Category();
        using (TransactionScope ts = new TransactionScope())
        {
            StationeryEntities context = new StationeryEntities();
            cat.CategoryName = categoryName;
            context.Categories.Add(cat);
            cat = context.Categories.Where(x => x.CategoryName.Equals(categoryName)).First();
            item.Category = cat;
            context.Items.Add(item);
            context.SaveChanges();
            ts.Complete();
        }
    }

    //AUTHOR : CHOU MING SHENG
    public static Item GetItembyItemCode(string itemCode)
    {
        using (StationeryEntities inventoryDB = new StationeryEntities())
        {
            Item result = inventoryDB.Items.Where(x => x.ItemCode == itemCode).FirstOrDefault();
            return result;
        }
    }
    public static Item GetItembyDescription(string description)
    {
        using (StationeryEntities inventoryDB = new StationeryEntities())
        {
            Item result = inventoryDB.Items.Where(x => x.Description.Replace(" ","").ToLower().Equals(description.Replace(" ","").ToLower())).FirstOrDefault();
            return result;
        }
    }
    public static Item GetActiveItembyItemCode(string itemCode)
    {
        using (StationeryEntities inventoryDB = new StationeryEntities())
        {
            Item result = inventoryDB.Items.Where(x => x.ItemCode == itemCode && x.ActiveStatus == "Y").FirstOrDefault();
            return result;
        }
    }
    public static List<Item> GetItemsbyCategoryID(int categoryID)
    {
        List<Item> itemList;
        using (StationeryEntities inventoryDB = new StationeryEntities())
        {
            itemList = inventoryDB.Items.Where(x => x.CategoryID == categoryID).ToList();
            return itemList;
        }
    }
    public static List<Item> GetItemList()
    {
        List<Item> itemList;
        using (StationeryEntities inventoryDB = new StationeryEntities())
        {
            itemList = inventoryDB.Items.ToList();
        }
        return itemList;
    }
    public static List<Item> GetActiveItemList()
    {
        List<Item> itemList;
        using (StationeryEntities inventoryDB = new StationeryEntities())
        {
            itemList = inventoryDB.Items.Where(x => x.ActiveStatus == "Y").OrderBy(x => x.ItemCode).ToList();
        }
        return itemList;
    }
    public static List<Item> GetActiveOrNewItemList()
    {
        List<Item> itemList;
        using (StationeryEntities inventoryDB = new StationeryEntities())
        {
            itemList = inventoryDB.Items.Where(x => x.ActiveStatus == "Y"|| x.ActiveStatus == "C").OrderBy(x => x.ItemCode).ToList();
        }
        return itemList;
    }
    public static List<Item> GetCatalogueList()
    {
        List<Item> catalogue;
        using (StationeryEntities inventoryDB = new StationeryEntities())
        {
            catalogue =
            inventoryDB.Items
            .Include("Category")
            .Where(db => db.ActiveStatus == "Y" || db.ActiveStatus =="C")
            .ToList();
        }
        return catalogue;
    }
    public static List<string> GetDistinctUOMList()
    {
        List<string> uom;
        using (StationeryEntities inventoryDB = new StationeryEntities())
        {
            uom = inventoryDB.Items.Select(x => x.UnitOfMeasure).Distinct().ToList();
        }
        return uom;
    }
    public static List<Item> GetActiveItemDescriptionList()
    {
        using (StationeryEntities context = new StationeryEntities())
        {
            return context.Items.Where(i => i.ActiveStatus.Equals("Y")).ToList();
        }
    }
    public static string GetUnitbyItemCode(string itemCode)
    {
        string unit;
        using (StationeryEntities inventoryDB = new StationeryEntities())
        {
            unit = inventoryDB.Items.Where(x => x.ItemCode.Equals(itemCode)).Select(x => x.UnitOfMeasure).FirstOrDefault();
        }
        return unit;
    }
    public static List<Item> SearchItemsByItemCodeOrDesc(string search)
    {
        List<Item> searchResults = new List<Item>();
        using (StationeryEntities inventoryDB = new StationeryEntities())
        {
            searchResults = inventoryDB.Items.Where(x => x.ItemCode.ToLower().Contains(search.ToLower()) || x.Description.ToLower().Contains(search.ToLower())).ToList();
        }
        return searchResults;
    }
    public static void UpdateItem(Item i)
    {
        using (StationeryEntities inventoryDB = new StationeryEntities())
        {
            inventoryDB.Entry(i).State = System.Data.Entity.EntityState.Modified;
            inventoryDB.SaveChanges();
        }
        return;
    }
    public static void RemoveItem(string itemCode)
    {
        using (StationeryEntities inventoryDB = new StationeryEntities())
        {
            Item i = inventoryDB.Items.Where(x => x.ItemCode == itemCode).FirstOrDefault();
            i.ActiveStatus = "N";
            inventoryDB.SaveChanges();
        }
        return;
    }
	public static List<ItemPrice> GetActiveItemWithPrice()
    {
        List<ItemPrice> activeitemList = null;
        using (StationeryEntities entities = new StationeryEntities())
        {
           
            activeitemList = (from item in entities.Items
                              join price in entities.PriceLists on item.ItemCode equals price.ItemCode into g
                              where item.ActiveStatus == "Y"
                              orderby item.ItemCode
                              select new ItemPrice
                              {
                                  ItemCode = item.ItemCode,
                                  Description = item.Description,
                                  SupplierCode = g.FirstOrDefault().SupplierCode,
                                  SupplierName = g.FirstOrDefault().Supplier.SupplierName,
                                  Price = g.FirstOrDefault().Price,

                              }).ToList();
        }
        return activeitemList;

    }
}