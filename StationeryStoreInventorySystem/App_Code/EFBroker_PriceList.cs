using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;

/// <summary>
/// Summary description for EFBroker_PriceList
/// </summary>
public class EFBroker_PriceList
{
    static StationeryEntities dbInstance;

    public EFBroker_PriceList()
    {
        if (dbInstance == null)
            dbInstance = new StationeryEntities();
    }

    public static void AddPriceListItem(PriceList obj)
    {
        using (TransactionScope ts = new TransactionScope())
        {
            dbInstance.PriceLists.Add(obj);
            dbInstance.SaveChanges();
            ts.Complete();
        }
    }

    public static List<PriceList> GetCurrentYearSupplierPriceList(string supplierCode)
    {
        using (TransactionScope ts = new TransactionScope())
        {
            DateTime dt = DateTime.Now;
            string latestYear = dt.Year.ToString();
            List<PriceList> lpl = dbInstance.PriceLists.Where(x => x.SupplierCode == supplierCode).Where(y => y.TenderYear == latestYear).ToList();
            ts.Complete();
            return lpl;
        }
    }

    public static void RemovePriceListObject(string firstCPK, string secondCPK, string thirdCPK)
    {
        using (TransactionScope ts = new TransactionScope())
        {
            PriceList pl = dbInstance.PriceLists.Where(x => x.SupplierCode == firstCPK).Where(y => y.ItemCode == secondCPK).Where(z => z.TenderYear == thirdCPK).First();
            dbInstance.PriceLists.Remove(pl);
            dbInstance.SaveChanges();
            ts.Complete();
        }
    }

    public static void UpdatePriceListObject(string newPrice, string firstCPK, string secondCPK, string thirdCPK)
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
    public static List<PriceList> GetPriceListByItemCode(string itemCode)
    {   //goes to price list broker
        List<PriceList> prices;
        using (StationeryEntities context = new StationeryEntities())
        {
            prices = context.PriceLists.Where(x => x.ItemCode == itemCode).ToList();
        }
        return prices;
    }
}