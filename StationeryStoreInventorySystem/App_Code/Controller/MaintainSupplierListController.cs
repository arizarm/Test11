using System;
using System.Transactions;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for MaintainSupplierListController
/// </summary>
public class SupplierListController
{

    public List<Supplier> ListAllSuppliers()
    {
        List<Supplier> LS = EFBroker_Supplier.ListAllSuppliers();
        return LS;
    }

    
    public Supplier GetSupplierGivenSupplierCode(string supplierCode)
    {
        Supplier s = EFBroker_Supplier.GetSupplierGivenSupplierCode(supplierCode);
        return s;
    }


    public void UpdateSupplier(Supplier supplier)
    {
        EFBroker_Supplier.UpdateSupplier(supplier);
    }

    public void DeleteSupplier(string supplierCode)
    {
        EFBroker_Supplier.DeleteSupplier(supplierCode);
    }

    public void CreateSupplier(Supplier supplier)
    {
        EFBroker_Supplier.CreateSupplier(supplier);
    }

    public void MakeSupplierActive(string code)
    {
        EFBroker_Supplier.MakeSupplierActive(code);
    }
}