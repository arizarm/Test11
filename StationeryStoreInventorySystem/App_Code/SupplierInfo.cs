using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SupplierInfo
/// </summary>
public class SupplierInfo
{
    private string supplierNameWithPrice;
    private string supplierCode;
    private string itemCode;
    private decimal? price;
    private decimal? amount;
    public String SupplierNameWithPrice
    {
        get
        {
            return supplierNameWithPrice;
        }
        set
        {
            supplierNameWithPrice = value;
        }
    }
    public String SupplierCode
    {
        get
        {
            return supplierCode;
        }
        set
        {
            supplierCode = value;
        }
    }
    public String ItemCode
    {
        get
        {
            return itemCode;
        }
        set
        {
            itemCode = value;
        }
    }
    public decimal? Price
    {
        get
        {
            return price;
        }
        set
        {
            price = value;
        }
    }
    public decimal? Amount
    {
        get
        {
            return amount;
        }
        set
        {
            amount = value;
        }
    }
}