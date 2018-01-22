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
    public List<Supplier> ListAllSuppliers()
    {
        using (TransactionScope ts = new TransactionScope())
        {
            StationeryEntities S = new StationeryEntities();
            List<Supplier> LS = S.Suppliers.ToList();
            ts.Complete();
            return LS;
        }
    }

    public Supplier GetSupplierGivenSupplierCode(string supplierCode)
    {
        using (TransactionScope ts = new TransactionScope())
        {
            StationeryEntities S = new StationeryEntities();
            Supplier s = S.Suppliers.Where(x => x.SupplierCode == supplierCode).First();
            ts.Complete();

            return s;
        }
    }

    public void UpdateSupplier(Supplier supplier)
    {
        using (TransactionScope ts = new TransactionScope())
        {
            StationeryEntities S = new StationeryEntities();
            S.Entry(supplier).State = System.Data.Entity.EntityState.Modified;
            S.SaveChanges();
            ts.Complete();
        }
    }

    public void DeleteSupplier(string supplierCode)
    {
        using (TransactionScope ts = new TransactionScope())
        {
            StationeryEntities S = new StationeryEntities();
            Supplier s = S.Suppliers.Where(x => x.SupplierCode == supplierCode).First();
            s.ActiveStatus = "N";
            S.Entry(s).State = System.Data.Entity.EntityState.Modified;
            S.SaveChanges();
            ts.Complete();
        }
    }

    public void CreateSupplier(Supplier supplier)
    {
        using (TransactionScope ts = new TransactionScope())
        {
            StationeryEntities S = new StationeryEntities();
            S.Suppliers.Add(supplier);
            S.SaveChanges();
            ts.Complete();
        }
    }

}