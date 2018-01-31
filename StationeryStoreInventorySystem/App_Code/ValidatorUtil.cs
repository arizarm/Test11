using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for ValidatorUtil
/// </summary>
public static class ValidatorUtil
{
    public static bool isEmpty(String s)
    {
        if (string.IsNullOrEmpty(s))
            return true;
        else
            return false;
    }
    public static bool ValidateNewItem(CustomValidator control, string itemCode)
    {
        Item item = EFBroker_Item.GetItembyItemCode(itemCode.ToUpper());
        if (item == null)
        {
            return true;
        }
        else
        {
            if (item.ActiveStatus == "Y")
            {
                control.ErrorMessage = "ItemCode is in use by existing item";
            }
            else
            {
                control.ErrorMessage = "ItemCode is used for archived item";
            }
            return false;
        }
    }
    public static bool IsInvalidfieldLength(string field, int length)
    {
        if (field.Length > length)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}