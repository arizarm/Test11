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

    public List<PriceList> GetCurrentYearSupplierPriceList(string supplierCode)
    {
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

}