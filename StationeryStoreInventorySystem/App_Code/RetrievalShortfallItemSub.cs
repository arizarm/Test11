using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for RetrievalShortfallItemSub
/// </summary>
[Serializable]
public class RetrievalShortfallItemSub
{
    DateTime requestDate;
    string deptName;
    string deptCode;
    int requestedQty;
    int actualQty;
    string itemCode;

    public RetrievalShortfallItemSub(DateTime requestDate, string deptName, string deptCode, int requestedQty, int actualQty, string itemCode)
    {
        this.requestDate = requestDate;
        this.deptName = deptName;
        this.deptCode = deptCode;
        this.requestedQty = requestedQty;
        this.actualQty = actualQty;
        this.itemCode = itemCode;
    }

    public DateTime RequestDate
    {
        get
        {
            return requestDate;
        }

        set
        {
            requestDate = value;
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

    public string DeptCode
    {
        get
        {
            return deptCode;
        }

        set
        {
            deptCode = value;
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