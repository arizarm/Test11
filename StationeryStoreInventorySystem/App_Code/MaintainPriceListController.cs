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
    public List<string> GetAllItemNames()
    {
            EFBroker_Item EFBI = new EFBroker_Item();
            List<string> items = EFBI.GetActiveItemList().Select(c => c.Description).ToList();
            return items;
    }

    public List<string> GetAllCategoryNames()
    {
        EFBroker_Category EFBC = new EFBroker_Category();
        List<string> categories = EFBC.GetAllCategoryNames();
        return categories;
    }

    //Break into 2. DAO(ItemDescAndCat) AND BizLogic(getItemDescForCat)
    public List<string> GetAllItemNamesForGivenCat(string cat)
    {
        using (TransactionScope ts = new TransactionScope())
        {
            StationeryEntities s = new StationeryEntities();
            int catID = s.Categories.Where(c => c.CategoryName == cat).Select(x => x.CategoryID).First();
            List<string> itemFGC = s.Items.Where(y => y.CategoryID == catID).Select(z => z.Description).ToList();
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
        EFBroker_Item EFBI = new EFBroker_Item();
        string itemCode = EFBI.GetItembyDescription(name).ItemCode;
        return itemCode;
    }

    //DAO
    public void AddPriceListItem(PriceList obj)
    {
        EFBroker_PriceList EFBPL = new EFBroker_PriceList();
        EFBPL.AddPriceListItem(obj);
    }

    public List<PriceList> GetCurrentYearSupplierPriceList(string supplierCode)
    {
        EFBroker_PriceList EFBPL = new EFBroker_PriceList();
        List<PriceList> lpl = EFBPL.GetCurrentYearSupplierPriceList(supplierCode);
        return lpl;
    }

    public string GetItemNameForGivenItemCode(string itemCode)
    {
        EFBroker_Item EFBI = new EFBroker_Item();
        string itemName = EFBI.GetItembyItemCode(itemCode).Description;
        return itemName;
    }

    public string GetUnitOfMeasureForGivenItemCode(string itemCode)
    {
        EFBroker_Item EFBI = new EFBroker_Item();
        string unitOfMeasure = EFBI.GetUnitbyItemCode(itemCode);
        return unitOfMeasure;
    }


    public PriceList GetPriceListObjForGivenDescNSupplier(string desc, string supplierCode)
    {
        EFBroker_Item EFBI = new EFBroker_Item();
        string itemCode = EFBI.GetItembyDescription(desc).ItemCode;

        EFBroker_PriceList EFBPL = new EFBroker_PriceList();
        PriceList pl = EFBPL.GetCurrentYearSupplierPriceList(supplierCode).Where(c => c.ItemCode == itemCode).FirstOrDefault();
        return pl;
    }

    public void RemovePriceListObject(string firstCPK, string secondCPK, string thirdCPK)
    {
        EFBroker_PriceList EFBPL = new EFBroker_PriceList();
        EFBPL.RemovePriceListObject(firstCPK, secondCPK, thirdCPK);
    }

    //can break into 2 DAO(updateEntry) BizLogic(UpdatePrice)
    public void UpdatePrice(string newPrice, string firstCPK, string secondCPK, string thirdCPK)
    {
        EFBroker_PriceList EFBPL = new EFBroker_PriceList();
        EFBPL.UpdatePriceListObject(newPrice, firstCPK, secondCPK, thirdCPK);
    }
}