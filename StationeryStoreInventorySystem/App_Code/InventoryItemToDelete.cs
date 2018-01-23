using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for InventoryItem
/// </summary>
public class InventoryItem
{
    Item i;
    string stock;
    public InventoryItem()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public InventoryItem(Item i, string stock)
    {
        this.i = i;
        this.stock = stock;
    }

    public Item I
    {
        get
        {
            return i;
        }

        set
        {
            i = value;
        }
    }

    public string Stock
    {
        get
        {
            return stock;
        }

        set
        {
            stock = value;
        }
    }
}