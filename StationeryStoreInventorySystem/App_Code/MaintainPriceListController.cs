using System;
using System.Transactions;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for MaintainPriceListController
/// </summary>
public class MaintainPriceListController
{
    public List<string> getAllItemNames()
    {
        using (TransactionScope ts = new TransactionScope())
        {
            StationeryEntities S = new StationeryEntities();
            List<string> items = S.Items.Select(x => x.Description).ToList();
            ts.Complete();
            return items;
        }
    }

    public List<string> getAllCatNames()
    {
        using (TransactionScope ts = new TransactionScope())
        {
            StationeryEntities S = new StationeryEntities();
            List<string> categories = S.Categories.Select(c => c.CategoryName).ToList();
            ts.Complete();
            return categories;
        }
    }

    public List<string> getAllItemNamesForGivenCat(string cat)
    {
        using (TransactionScope ts = new TransactionScope())
        {
            StationeryEntities S = new StationeryEntities();
            int catID = S.Categories.Where(c => c.CategoryName == cat).Select(x => x.CategoryID).First();
            List<string> itemFGC = S.Items.Where(y => y.CategoryID == catID).Select(z => z.Description).ToList();
            ts.Complete();
            return itemFGC;
        }
    }

    public string getCatForGivenItem(string name)
    {
        using (TransactionScope ts = new TransactionScope())
        {
            StationeryEntities S = new StationeryEntities();
            int catFGI = S.Items.Where(x => x.Description == name).Select(c => c.CategoryID).First().Value;
            string catFGI2 = S.Categories.Where(z => z.CategoryID == catFGI).Select(d => d.CategoryName).First();
            ts.Complete();
            return catFGI2;
        }
    }

    public string getItemCodeForGivenItemName(string name)
    {
        using (TransactionScope ts = new TransactionScope())
        {
            StationeryEntities S = new StationeryEntities();
            string IC = S.Items.Where(x => x.Description == name).Select(c => c.ItemCode).First();
            ts.Complete();
            return IC;
        }
    }

    public void addPriceListItem(PriceList obj)
    {
        using (TransactionScope ts = new TransactionScope())
        {
            StationeryEntities S = new StationeryEntities();
            S.PriceLists.Add(obj);
            S.SaveChanges();
            ts.Complete();
        }
    }

    public List<PriceList> getSupplierPriceList(string supplierCode)
    {
        //only display pricelist for current year data
        using (TransactionScope ts = new TransactionScope())
        {
            StationeryEntities S = new StationeryEntities();
            DateTime dt = DateTime.Now;
            string latestYear = dt.Year.ToString();
            List<PriceList> lpl = S.PriceLists.Where(x => x.SupplierCode == supplierCode).Where(y => y.TenderYear == latestYear).ToList();
            ts.Complete();
            return lpl;
        }
    }

    public string getItemNameForGivenItemCode(string itemCode)
    {
        using (TransactionScope ts = new TransactionScope())
        {
            StationeryEntities S = new StationeryEntities();
            string IC = S.Items.Where(x => x.ItemCode == itemCode).Select(c => c.Description).First();
            ts.Complete();
            return IC;
        }
    }

    public string getUnitOfMeasureForGivenItemCode(string itemCode)
    {
        using (TransactionScope ts = new TransactionScope())
        {
            StationeryEntities S = new StationeryEntities();
            string IC = S.Items.Where(x => x.ItemCode == itemCode).Select(c => c.UnitOfMeasure).First();
            ts.Complete();
            return IC;
        }
    }

    public PriceList getPriceListObjForGivenDescNSupplier(string desc, string supplierCode)
    {
        using (TransactionScope ts = new TransactionScope())
        {
            StationeryEntities S = new StationeryEntities();
            string itemCode = S.Items.Where(b => b.Description == desc).Select(d => d.ItemCode).First();
            DateTime dt = DateTime.Now;
            PriceList pl = S.PriceLists.Where(x => x.ItemCode == itemCode).Where(e => e.SupplierCode == supplierCode).Where(f => f.TenderYear == dt.Year.ToString()).First();
            ts.Complete();
            return pl;
        }
    }

    public void removePriceListObject(string firstCPK, string secondCPK, string thirdCPK)
    {
        using (TransactionScope ts = new TransactionScope())
        {
            StationeryEntities S = new StationeryEntities();
            PriceList pl = S.PriceLists.Where(x => x.SupplierCode == firstCPK).Where(y => y.ItemCode == secondCPK).Where(z => z.TenderYear == thirdCPK).First();
            S.PriceLists.Remove(pl);
            S.SaveChanges();
            ts.Complete();
        }
    }

    public void updatePrice(string newPrice, string firstCPK, string secondCPK, string thirdCPK)
    {
        using (TransactionScope ts = new TransactionScope())
        {
            StationeryEntities S = new StationeryEntities();
            decimal price = Decimal.Parse(newPrice);
            PriceList pl = S.PriceLists.Where(x => x.SupplierCode == firstCPK).Where(y => y.ItemCode == secondCPK).Where(z => z.TenderYear == thirdCPK).First();
            pl.Price = price;
            S.Entry(pl).State = System.Data.Entity.EntityState.Modified;
            S.SaveChanges();
            ts.Complete();
        }
    }
}