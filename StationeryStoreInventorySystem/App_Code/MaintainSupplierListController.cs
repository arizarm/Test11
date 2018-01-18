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
    public List<Supplier> listAllSuppliers()
    {
        using (TransactionScope ts = new TransactionScope())
        {
            StationeryEntities S = new StationeryEntities();
            List<Supplier> LS = S.Suppliers.ToList();
            ts.Complete();

            return LS;
        }
    }

    public Supplier getSupplier(string supplierCode)
    {
        using (TransactionScope ts = new TransactionScope())
        {
            StationeryEntities S = new StationeryEntities();
            Supplier s = S.Suppliers.Where(x => x.SupplierCode == supplierCode).First();
            ts.Complete();

            return s;
        }
    }

    public void updateSupplier(Supplier supplier)
    {
        using (TransactionScope ts = new TransactionScope())
        {
            StationeryEntities S = new StationeryEntities();
            S.Entry(supplier).State = System.Data.Entity.EntityState.Modified;
            S.SaveChanges();
            ts.Complete();
        }
    }

    public void deleteSupplier(string supplierCode)
    {
        using (TransactionScope ts = new TransactionScope())
        {
            StationeryEntities S = new StationeryEntities();
            Supplier s = S.Suppliers.Where(x => x.SupplierCode == supplierCode).First();
            S.Suppliers.Remove(s);
            S.SaveChanges();
            ts.Complete();
        }
    }

    public void createSupplier(Supplier supplier)
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