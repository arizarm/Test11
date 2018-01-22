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
        EFBroker_Supplier EFBS = new EFBroker_Supplier();
        List<Supplier> LS = EFBS.ListAllSuppliers();
        return LS;
    }

    
    public Supplier GetSupplierGivenSupplierCode(string supplierCode)
    {
        EFBroker_Supplier EFBS = new EFBroker_Supplier();
        Supplier s = EFBS.GetSupplierGivenSupplierCode(supplierCode);
        return s;
    }


    public void UpdateSupplier(Supplier supplier)
    {
        EFBroker_Supplier EFBS = new EFBroker_Supplier();
        EFBS.UpdateSupplier(supplier);
    }

    //DAO
    public void DeleteSupplier(string supplierCode)
    {
        EFBroker_Supplier EFBS = new EFBroker_Supplier();
        EFBS.DeleteSupplier(supplierCode);
    }

    //DAO
    public void CreateSupplier(Supplier supplier)
    {
        EFBroker_Supplier EFBS = new EFBroker_Supplier();
        EFBS.CreateSupplier(supplier);
    }
}