using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DisbursementDetailListItems
/// </summary>
public class DisbursementDetailListItems
{
    private string itemCode;
    private string itemDesc;
    private int reqQty;
    private int actualQty;
    private string remarks;

    public DisbursementDetailListItems(string itemCode, string itemDesc, int reqQty, int actualQty, string remarks)
    {
        this.itemCode = itemCode;
        this.itemDesc = itemDesc;
        this.reqQty = reqQty;
        this.actualQty = actualQty;
        this.remarks = remarks;
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

    public string ItemDesc
    {
        get
        {
            return itemDesc;
        }

        set
        {
            itemDesc = value;
        }
    }

    public int ReqQty
    {
        get
        {
            return reqQty;
        }

        set
        {
            reqQty = value;
        }
    }

    public int ActualQty
    {
        get
        {
            return actualQty;
        }

        set
        {
            actualQty = value;
        }
    }

    public string Remarks
    {
        get
        {
            return remarks;
        }

        set
        {
            remarks = value;
        }
    }
}