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
    public List<Item> GetActiveItemList()
    {
            List<Item> items = EFBroker_Item.GetActiveItemList().ToList();
            return items;
    }

    public List<string> GetAllCategoryNames()
    {
        List<string> categories = EFBroker_Category.GetAllCategoryNames();
        return categories;
    }

    public List<Item> GetAllItemsForGivenCat(string cat)
    {
        int catID = EFBroker_Category.GetCategoryList().Where(c => c.CategoryName == cat).Select(x => x.CategoryID).FirstOrDefault();
        List<Item> itemFGC = EFBroker_Item.GetItemsbyCategoryID(catID).ToList();
        return itemFGC;
    }

    public string GetCatForGivenItemCode(string code)
    {
        int? catFGI = EFBroker_Item.GetItembyItemCode(code).CategoryID;
        string catFGI2 = EFBroker_Category.GetCategoryList().Where(z => z.CategoryID == catFGI).Select(d => d.CategoryName).FirstOrDefault();
        return catFGI2;
    }

    public void AddPriceListItem(PriceList obj)
    {
        EFBroker_PriceList.AddPriceListItem(obj);
    }

    public List<PriceList> GetCurrentYearSupplierPriceList(string supplierCode)
    {
        List<PriceList> lpl = EFBroker_PriceList.GetCurrentYearSupplierPriceList(supplierCode);
        return lpl;
    }

    public Item GetItemForGivenItemCode(string itemCode)
    {
        Item itemName = EFBroker_Item.GetItembyItemCode(itemCode);
        return itemName;
    }

    public string GetUnitOfMeasureForGivenItemCode(string itemCode)
    {
        string unitOfMeasure = EFBroker_Item.GetUnitbyItemCode(itemCode);
        return unitOfMeasure;
    }


    public PriceList GetPriceListObjForGivenItemCodeNSupplier(string itemCode, string supplierCode)
    {
        PriceList pl = EFBroker_PriceList.GetCurrentYearSupplierPriceList(supplierCode).Where(c => c.ItemCode == itemCode).FirstOrDefault();
        return pl;
    }

    public void RemovePriceListObject(string firstCPK, string secondCPK, string thirdCPK)
    {
        EFBroker_PriceList.RemovePriceListObject(firstCPK, secondCPK, thirdCPK);
    }

    public void UpdatePrice(string newPrice, string firstCPK, string secondCPK, string thirdCPK)
    {
        EFBroker_PriceList.UpdatePriceListObject(newPrice, firstCPK, secondCPK, thirdCPK);
    }
}