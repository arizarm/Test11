using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for RetrievalShortfallItemSub
/// </summary>
public class RetrievalShortfallItemSub
{
    string deptName;
    int requestedQty;

    public RetrievalShortfallItemSub(string deptName, int requestedQty)
    {
        this.deptName = deptName;
        this.requestedQty = requestedQty;
    }

    public RetrievalShortfallItemSub(string deptName)
    {
        this.deptName = deptName;
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