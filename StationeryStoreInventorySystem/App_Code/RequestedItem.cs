using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for RequestedItem
/// </summary>
/// 
//AUTHOR : APRIL SHAR
[Serializable]
public class RequestedItem
{
    private string code;
    private string description;
    private int quantity;
    private string uom;

    public RequestedItem(string code, string description, int quantity, string uom)
    {
        this.code = code;
        this.description = description;
        this.quantity = quantity;
        this.uom = uom;
    }

    public string Code { get { return code; } set { code = value; } }
    public string Description { get { return description; } set { description = value; } }
    public int Quantity { get { return quantity; } set { quantity = value; } }
    public string Uom { get { return uom; } set { uom = value; } }

    public override bool Equals(object obj)
    {
        if (obj == null) return false;
        RequestedItem objAsPart = obj as RequestedItem;
        if (objAsPart == null) return false;
        else return Equals(objAsPart);
    }

    public bool Equals(RequestedItem other)
    {
        if (other == null) return false;
        return (this.code.Equals(other.code));
    }
}
