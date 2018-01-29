using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ItemService" in code, svc and config file together.
public class ItemService : IItemService
{
    public List<WCFCatalogueItem> GetAllItems()
    {
        List<Item> iList = EFBroker_Item.GetActiveItemList();
        List<WCFCatalogueItem> ciList = new List<WCFCatalogueItem>();
        foreach (Item i in iList)
        {
            if (i.BalanceQty != null)
            {
                int adjustments = GetAdjustmentSum(i);
                WCFCatalogueItem ci = WCFCatalogueItem.Make(i.ItemCode, i.Description, i.UnitOfMeasure, (int)i.BalanceQty, adjustments, i.Bin);
                ciList.Add(ci);
            }
        }
        return ciList;
    }
    public List<WCFCatalogueItem> SearchItems(string search)
    {
        List<Item> iList = EFBroker_Item.SearchItemsByItemCodeOrDesc(search);
        List<WCFCatalogueItem> ciList = new List<WCFCatalogueItem>();
        foreach (Item i in iList)
        {
            if (i.BalanceQty != null)
            {
                int adjustments = GetAdjustmentSum(i);
                WCFCatalogueItem ci = WCFCatalogueItem.Make(i.ItemCode, i.Description, i.UnitOfMeasure, (int)i.BalanceQty, adjustments, i.Bin);
                ciList.Add(ci);
            }
        }
        return ciList;
    }

    public WCFCatalogueItem GetItem(string itemCode)
    {
        Item i = EFBroker_Item.GetItembyItemCode(itemCode);
        WCFCatalogueItem ci = null;
        if (i.BalanceQty != null)
        {
            int adjustments = GetAdjustmentSum(i);
            ci = WCFCatalogueItem.Make(i.ItemCode, i.Description, i.UnitOfMeasure, (int)i.BalanceQty, adjustments, i.Bin);
        }
        return ci;
    }

    private int GetAdjustmentSum(Item i)
    {
        Discrepency dMonthly = EFBroker_Discrepancy.GetPendingMonthlyDiscrepancyByItemCode(i.ItemCode);
        List<Discrepency> dList = EFBroker_Discrepancy.GetPendingDiscrepanciesByItemCode(i.ItemCode);
        int adjustment = 0;
        if (dMonthly == null)
        {
            foreach (Discrepency d in dList)
            {
                adjustment += (int)d.AdjustmentQty;
            }
        }
        else
        {
            adjustment = (int)dMonthly.AdjustmentQty;

            foreach (Discrepency d in dList)
            {
                if ((int)d.DiscrepencyID > dMonthly.DiscrepencyID)
                {
                    adjustment += (int)d.AdjustmentQty;
                }
            }
        }
        return adjustment;
    }
}
