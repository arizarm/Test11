using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for RetrievalShortfallItem
/// </summary>
public class RetrievalShortfallItem
{
    string description;
    int qty;
    string deptName;
    int requestedQty;

    public RetrievalShortfallItem(string description, int qty, string deptName, int requestedQty)
    {
        this.description = description;
        this.qty = qty;
        this.deptName = deptName;
        this.requestedQty = requestedQty;
    }

    public RetrievalShortfallItem(string description, int qty)
    {
        this.description = description;
        this.qty = qty;
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

    public int Qty
    {
        get
        {
            return qty;
        }

        set
        {
            qty = value;
        }
    }

    public string DeptName
    {
        get
        {
            return deptName;
        }

        set
        {
            deptName = value;
        }
    }

    public int RequestedQty
    {
        get
        {
            return requestedQty;
        }

        set
        {
            requestedQty = value;
        }
    }
}