using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Item
/// </summary>
public class Samitem
{
    private String itemCode;
    private String quantity;
    private String reason;
    public Samitem()
    { 
        
    }
    public Samitem(String itemCode, String quantity, String reason)
    {
        this.itemCode = itemCode;
        this.quantity = quantity;
        this.reason = reason;
    }

    public String ItemCode
    {
        get
        {
            return itemCode;
        }
    }
    public String Quantity
    {
        get
        {
            return quantity;
        }
    }
    public String Reason
    {
        get
        {
            return reason;
        }
    }

}