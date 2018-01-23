﻿using System;
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
            List<string> items = EFBroker_Item.GetActiveItemList().Select(c => c.Description).ToList();
            return items;
    }

    public List<string> GetAllCategoryNames()
    {
        List<string> categories = EFBroker_Category.GetAllCategoryNames();
        return categories;
    }

    public List<string> GetAllItemNamesForGivenCat(string cat)
    {
        int catID = EFBroker_Category.GetCategoryList().Where(c => c.CategoryName == cat).Select(x => x.CategoryID).FirstOrDefault();
        List<string> itemFGC = EFBroker_Item.GetItemsbyCategoryID(catID).Select(z => z.Description).ToList();
        return itemFGC;
    }

    public string GetCatForGivenItem(string name)
    {
        int? catFGI = EFBroker_Item.GetItembyDescription(name).CategoryID;
        string catFGI2 = EFBroker_Category.GetCategoryList().Where(z => z.CategoryID == catFGI).Select(d => d.CategoryName).FirstOrDefault();
        return catFGI2;
    }

    public string GetItemCodeForGivenItemName(string name)
    {
        string itemCode = EFBroker_Item.GetItembyDescription(name).ItemCode;
        return itemCode;
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

    public string GetItemNameForGivenItemCode(string itemCode)
    {
        string itemName = EFBroker_Item.GetItembyItemCode(itemCode).Description;
        return itemName;
    }

    public string GetUnitOfMeasureForGivenItemCode(string itemCode)
    {
        string unitOfMeasure = EFBroker_Item.GetUnitbyItemCode(itemCode);
        return unitOfMeasure;
    }


    public PriceList GetPriceListObjForGivenDescNSupplier(string desc, string supplierCode)
    {
        string itemCode = EFBroker_Item.GetItembyDescription(desc).ItemCode;

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