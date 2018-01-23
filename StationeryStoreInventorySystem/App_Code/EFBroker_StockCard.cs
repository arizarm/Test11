using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
    public List<StockCard> GetStockCardsByItemCode(string itemCode)
    {   //goes to stock card broker
        List<StockCard> stockList;
        using (StationeryEntities context = new StationeryEntities())
        {
            stockList= context.StockCards.Where(x => x.ItemCode.Equals(itemCode)).ToList();
        }
        return stockList;
    }
    public void AddStockTransaction(StockCard stockCard)
    {
        using (StationeryEntities context = new StationeryEntities())
        {
            context.StockCards.Add(stockCard);
            context.SaveChanges();
        }
    }
}