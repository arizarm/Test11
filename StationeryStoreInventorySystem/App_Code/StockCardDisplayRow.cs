using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for StockCardDisplayRow
/// </summary>
/// 
//AUTHOR : EDWIN TAN
public class StockCardDisplayRow
{
    string transDate;
    string transDetails;
    string quantity;
    int balance;

    public string TransDate
    {
        get
        {
            return transDate;
        }

        set
        {
            transDate = value;
        }
    }

    public string TransDetails
    {
        get
        {
            return transDetails;
        }

        set
        {
            transDetails = value;
        }
    }

    public string Quantity
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

    public int Balance
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

    public StockCardDisplayRow()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public StockCardDisplayRow(string transDate, string transDetails, string quantity, int balance)
    {
        this.transDate = transDate;
        this.transDetails = transDetails;
        this.quantity = quantity;
        this.balance = balance;
    }
}