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
    //DAO
    public List<string> GetAllItemNames()
    {
        using (TransactionScope ts = new TransactionScope())
        {
            StationeryEntities S = new StationeryEntities();
            List<string> items = S.Items.Select(x => x.Description).ToList();
            ts.Complete();
            return items;
        }
    }

    //DAO
    public List<string> GetAllCatNames()
    {
        using (TransactionScope ts = new TransactionScope())
        {
            StationeryEntities S = new StationeryEntities();
            List<string> categories = S.Categories.Select(c => c.CategoryName).ToList();
            ts.Complete();
            return categories;
        }
    }

    //Break into 2. DAO(ItemDescAndCat) AND BizLogic(getItemDescForCat)
    public List<string> GetAllItemNamesForGivenCat(string cat)
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

    //DAO(CombinedEntities) and BizLogic(passing method)
    public string GetCatForGivenItem(string name)
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

    //DAO
    public string GetItemCodeForGivenItemName(string name)
    {
        using (TransactionScope ts = new TransactionScope())
        {
            StationeryEntities S = new StationeryEntities();
            string IC = S.Items.Where(x => x.Description == name).Select(c => c.ItemCode).First();
            ts.Complete();
            return IC;
        }
    }

    //DAO
    public void AddPriceListItem(PriceList obj)
    {
        using (TransactionScope ts = new TransactionScope())
        {
            StationeryEntities S = new StationeryEntities();
            S.PriceLists.Add(obj);
            S.SaveChanges();
            ts.Complete();
        }
    }

    public List<PriceList> GetSupplierPriceList(string supplierCode)
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

    public string GetItemNameForGivenItemCode(string itemCode)
    {
        using (TransactionScope ts = new TransactionScope())
        {
            StationeryEntities S = new StationeryEntities();
            string IC = S.Items.Where(x => x.ItemCode == itemCode).Select(c => c.Description).First();
            ts.Complete();
            return IC;
        }
    }

    public string GetUnitOfMeasureForGivenItemCode(string itemCode)
    {
        using (TransactionScope ts = new TransactionScope())
        {
            StationeryEntities S = new StationeryEntities();
            string IC = S.Items.Where(x => x.ItemCode == itemCode).Select(c => c.UnitOfMeasure).First();
            ts.Complete();
            return IC;
        }
    }


    public PriceList GetPriceListObjForGivenDescNSupplier(string desc, string supplierCode)
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

    //DAO
    public void RemovePriceListObject(string firstCPK, string secondCPK, string thirdCPK)
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

    //can break into 2 DAO(updateEntry) BizLogic(UpdatePrice)
    public void UpdatePrice(string newPrice, string firstCPK, string secondCPK, string thirdCPK)
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