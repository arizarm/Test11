using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PurchaseOrderItemDetails
/// </summary>
/// 
//AUTHOR : KIRUTHIKA VENKATESH
public class PurchaseOrderItemDetails
{
    private int orderID;
    private string itemCode;
    private string description;
    private int? orderQty;
    private decimal? price;
    private decimal? totalAmount;
    private string formattedPrice;
    private string formattedAmount;

    public PurchaseOrderItemDetails()
    {

    }
    public int OrderID
    {
        get
        {
            return orderID;
        }

        set
        {
            orderID = value;
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

    public int? OrderQty
    {
        get
        {
            return orderQty;
        }

        set
        {
            orderQty = value;
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

    public decimal? TotalAmount
    {
        get
        {
            return totalAmount;
        }

        set
        {
            totalAmount = value;
        }
    }

    public string ItemCode
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

    public string FormattedPrice
    {
        get
        {
            string newPrice=string.Format("{0:C}", Price);
            return newPrice;
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
            string newAmount = string.Format("{0:C}", TotalAmount);
            return newAmount;
        }

        set
        {
            formattedAmount = value;
        }
    }
}