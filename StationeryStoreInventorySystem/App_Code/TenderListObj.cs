using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for TenderListObj
/// </summary>
public class TenderListObj
{
    string itemP, itemDesc, itemD;
    public TenderListObj(Item iD, string iP)
    {
        this.itemD = iD.ItemCode;
        this.itemP = iP;
        this.itemDesc = iD.Description;
    }

    public TenderListObj(Item iD)
    {
        this.itemD = iD.ItemCode;
        this.itemDesc = iD.Description;
    }

    public string ItemDescription
    {
        get { return itemDesc; }
        set { itemDesc = value; }
    }

    public string ItemPrice
    {
        get { return itemP; }
        set { itemP = value; }
    }

    public string ItemCode
    {
        get { return itemD; }
        set { itemD = value; }
    }
}