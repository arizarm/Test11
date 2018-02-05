using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for RetrievalListDetailItem
/// </summary>
/// 
//AUTHOR : CHOU MING SHENG

[Serializable]
public class RetrievalListDetailItem
{

    private string bin;
    private string description;
    private int totalRequestedQty;
    private string itemCode;
    private int retrievedQty;

    public RetrievalListDetailItem(string bin, string description, int totalRequestedQty, string itemCode, int retrievedQty)
    {
        this.bin = bin;
        this.description = description;
        this.totalRequestedQty = totalRequestedQty;
        this.itemCode = itemCode;
        this.retrievedQty = retrievedQty;
    }

    public string Bin
    {
        get
        {
            return bin;
        }

        set
        {
            bin = value;
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

    public int TotalRequestedQty
    {
        get
        {
            return totalRequestedQty;
        }

        set
        {
            totalRequestedQty = value;
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

    public int RetrievedQty
    {
        get
        {
            return retrievedQty;
        }

        set
        {
            retrievedQty = value;
        }
    }
}