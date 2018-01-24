using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ReorderItems
/// </summary>
public class PurchaseItems
{
    private string itemCode;
    private string description;
    private int reorderQty;
    private string unitOfMeasure;
    private int? balance;
    private int reorderLevel;
    

    public PurchaseItems()
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
   




   

   
       
}