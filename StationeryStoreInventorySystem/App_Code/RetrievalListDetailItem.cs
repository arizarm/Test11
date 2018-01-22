using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for RetrievalListDetailItem
/// </summary>
public class RetrievalListDetailItem
{

    private string bin;
    private string description;
    private int totalRequestedQty;
    private string itemCode;

    public RetrievalListDetailItem(string bin, string description, int totalRequestedQty, string itemCode)
    {
        this.bin = bin;
        this.description = description;
        this.totalRequestedQty = totalRequestedQty;
        this.itemCode = itemCode;
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
}