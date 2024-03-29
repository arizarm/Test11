﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Requisition_ItemList
/// </summary>
/// 
//AUTHOR : APRIL SHAR
public class Requisition_ItemList
{
    string description;
    int? reqQty;
    string uom;
    string status;
    string code;

    public Requisition_ItemList(string des, int? qty, string uom, string status)
    {
        this.description = des;
        this.reqQty = qty;
        this.uom = uom;
        this.status = status;
    }

    public Requisition_ItemList(string code, string des, int? qty, string uom, string status)
    {
        this.code = code;
        this.description = des;
        this.reqQty = qty;
        this.uom = uom;
        this.status = status;
    }

    public Requisition_ItemList() { }

    public string Code { get { return code; } set { code = value; } }
    public string Description { get { return description; } set { description = value; } }
    public int? RequestedQty { get { return reqQty; } set { reqQty = value; } }
    public string UnitOfMeasure { get { return uom; } set { uom = value; } }
    public string Status { get { return status; } set { status = value; } }
}