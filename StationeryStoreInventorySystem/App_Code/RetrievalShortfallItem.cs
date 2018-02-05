using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for RetrievalShortfallItem
/// </summary>
/// 
//AUTHOR : CHOU MING SHENG
[Serializable]
public class RetrievalShortfallItem
{
    //string deptName;
    //int requestedQty;

    string description;
    int qty;
    string itemCode;

    public RetrievalShortfallItem(string description, int qty, string itemCode)
    {
        this.description = description;
        this.qty = qty;
        this.itemCode = itemCode;
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