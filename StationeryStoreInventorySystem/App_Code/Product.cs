using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Product
/// </summary>

public class Product
{
    string itemName;
    int quantity;

    public Product(string itemName, int quantity)
    {
        this.ItemName = itemName;
        this.Quantity = quantity;
    }

   
    public string ItemName
    {
        get
        {
            return itemName;
        }

        set
        {
            itemName = value;
        }
    }

    public int Quantity
    {
        get
        {
            return quantity;
        }

        set
        {
            quantity = value;
        }
    }
}