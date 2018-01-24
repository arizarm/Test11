using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Transactions;

/// <summary>
/// Summary description for EFBroker_StockCard
/// </summary>
public class EFBroker_StockCard
{
    public EFBroker_StockCard()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static List<StockCard> GetStockCardsByItemCode(string itemCode)
    {   //goes to stock card broker
        List<StockCard> stockList;
        using (StationeryEntities context = new StationeryEntities())
        {
            stockList= context.StockCards.Where(x => x.ItemCode.Equals(itemCode)).ToList();
        }
        return stockList;
    }
    public static void AddStockTransaction(StockCard stockCard)
    {
        using (StationeryEntities context = new StationeryEntities())
        {
            context.StockCards.Add(stockCard);
            context.SaveChanges();
        }
    }
    public static void ResolveDiscrepancy(StockCard newEntry, string itemCode, int newBalance)
    {
        using (TransactionScope ts = new TransactionScope())
        {
            StationeryEntities context = new StationeryEntities();
            Item i = context.Items.Where(x => x.ItemCode == itemCode).First();
            i.BalanceQty = newBalance;
            context.StockCards.Add(newEntry);
            context.SaveChanges();
            ts.Complete();
        }
    }
}