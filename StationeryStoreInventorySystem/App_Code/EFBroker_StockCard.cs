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
    public void AddStockTransaction(StockCard stockCard)
    {
        using (StationeryEntities context = new StationeryEntities())
        {
            context.StockCards.Add(stockCard);
            context.SaveChanges();
        }
    }
}