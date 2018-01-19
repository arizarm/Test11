using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for RegenerateRequestItems
/// </summary>
public class RegenerateRequestItems
{
    string itemcode;
    string description;
    int shortfallQty;

    public RegenerateRequestItems(string itemcode, string description, int shortfallQty)
    {
        this.itemcode = itemcode;
        this.description = description;
        this.shortfallQty = shortfallQty;
    }

    public string Itemcode
    {
        get
        {
            return itemcode;
        }

        set
        {
            itemcode = value;
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

    public int ShortfallQty
    {
        get
        {
            return shortfallQty;
        }

        set
        {
            shortfallQty = value;
        }
    }
}