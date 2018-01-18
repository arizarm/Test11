using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
}