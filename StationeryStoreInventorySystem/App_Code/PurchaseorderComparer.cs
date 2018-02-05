using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Purchaseorder
/// </summary>
/// 
//AUTHOR : KIRUTHIKA VENKATESH
public class PurchaseOrderComparer : IEqualityComparer<PurchaseOrder>
{

    public string SupplierCode { get; set; }
    public string PurchaseOrderID { get; set; }
    public PurchaseOrderComparer()
    {

    }

    public bool Equals(PurchaseOrder x, PurchaseOrder y)
    {
        if (x.SupplierCode == y.SupplierCode)
            return true;
        else
            return false;
    }

    public int GetHashCode(PurchaseOrder obj)
    {
        if(this.SupplierCode !=null && obj.SupplierCode ==null)
        {
            return this.SupplierCode.GetHashCode() + obj.SupplierCode.GetHashCode();
        }
        return 0;
    }
}