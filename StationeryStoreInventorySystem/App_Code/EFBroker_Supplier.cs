using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;

/// <summary>
/// Summary description for EFBroker_Supplier
/// </summary>
public class EFBroker_Supplier
{
    StationeryEntities dbInstance;

    public EFBroker_Supplier()
    {
        if (dbInstance == null)
            dbInstance = new StationeryEntities();
    }

    public List<Supplier> ListAllSuppliers()
    {
        using (TransactionScope ts = new TransactionScope())
        {
            List<Supplier> LS = dbInstance.Suppliers.ToList();
            ts.Complete();
            return LS;
        }
    }

    public Supplier GetSupplierGivenSupplierCode(string supplierCode)
    {
        using (TransactionScope ts = new TransactionScope())
        {
            Supplier s = dbInstance.Suppliers.Where(x => x.SupplierCode == supplierCode).First();
            ts.Complete();

            return s;
        }
    }

    public void UpdateSupplier(Supplier supplier)
    {
        using (TransactionScope ts = new TransactionScope())
        {
            dbInstance.Entry(supplier).State = System.Data.Entity.EntityState.Modified;
            dbInstance.SaveChanges();
            ts.Complete();
        }
    }

    public void DeleteSupplier(string supplierCode)
    {
        using (TransactionScope ts = new TransactionScope())
        {
            Supplier s = dbInstance.Suppliers.Where(x => x.SupplierCode == supplierCode).First();
            s.ActiveStatus = "N";
            dbInstance.Entry(s).State = System.Data.Entity.EntityState.Modified;
            dbInstance.SaveChanges();
            ts.Complete();
        }
    }

    public void CreateSupplier(Supplier supplier)
    {
        using (TransactionScope ts = new TransactionScope())
        {
            dbInstance.Suppliers.Add(supplier);
            dbInstance.SaveChanges();
            ts.Complete();
        }
    }

}