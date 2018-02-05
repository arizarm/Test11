using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SupplierInfo
/// </summary>
/// 
//AUTHOR : KIRUTHIKA VENKATESH
public class ItemPrice
{
   // private string supplierNameWithPrice;
    private string supplierCode;
    private string itemCode;
    private decimal? price;
    private decimal? amount;
    private string supplierName;
    private string description;
    private string formattedPrice;
    private string formattedAmount;
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

    public string SupplierName
    {
        get
        {
            return supplierName;
        }

        set
        {
            supplierName = value;
        }
    }

    public string Description
    {
        get
        {
            return description;
        }

        set
        {
            description = value;
        }
    }

    public string FormattedPrice
    {
        get
        {
            return string.Format("{0:C}",price);
        }

        set
        {
            formattedPrice = value;
        }
    }

    public string FormattedAmount
    {
        get
        {
            return string.Format("{0:C}", amount);
        }

        set
        {
            formattedAmount = value;
        }
    }
}