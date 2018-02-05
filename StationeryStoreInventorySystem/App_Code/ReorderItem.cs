using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ReorderItems
/// </summary>
/// 
//AUTHOR : KIRUTHIKA VENKATESH
public class ReorderItem
{
    private string itemCode;
    private string description;
    private int reorderQty;
    private string unitOfMeasure;
    private int? balance;
    private int reorderLevel;
    private decimal? price;
    private decimal? amount;

    public ReorderItem()
    {

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
    public String Description
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
    public int ReorderQty
    {
        get
        {
            return reorderQty;
        }
        set
        {
            reorderQty = value;
        }
    }
    public String UnitOfMeasure
    {
        get
        {
            return unitOfMeasure;
        }
        set
        {
            unitOfMeasure = value;
        }
    }
    public int? Balance
    {
        get
        {
            return balance;
        }
        set
        {
            balance = value;
        }
    }

    public int ReorderLevel
    {
        get
        {
            return reorderLevel;
        }
        set
        {
            reorderLevel = value;
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