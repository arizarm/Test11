using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for StationeryItem
/// </summary>
public class StationeryItem
{
    private string itemCode;
    private string description;
    private int currentQty;
    private int orderQty;
    private double price;
    private double amount;

    public StationeryItem(string itemCode, string description, int currentQty, int orderQty, double price, double amount)
    {
        this.itemCode = itemCode;
        this.description = description;
        this.currentQty = currentQty;
        this.orderQty = orderQty;
        this.price = price;
        this.amount = amount;
    }
    public string ItemCode
    {
        get
        {

            return itemCode;
        }
    }
    public string Description
    {
        get
        {
            return description;
        }
    }
    public int CurrentQuantity
    {
        get
        {
            return currentQty;
        }
    }
    public int OrderQuantity
    {
        get
        {
            return orderQty;
        }
    }
    public double Price
    {
        get
        {

            return price;
        }
    }
public double Amount
{
    get
    {
        return amount;
    }
}
}