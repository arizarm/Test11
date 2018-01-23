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

    public EFBroker_Supplier()
    {
    }

    public static List<Supplier> ListAllSuppliers()
    {
        using (TransactionScope ts = new TransactionScope())
        {
            StationeryEntities dbInstance = new StationeryEntities();
            List<Supplier> LS = dbInstance.Suppliers.ToList();
            ts.Complete();
            return LS;
        }
    }

    public static Supplier GetSupplierGivenSupplierCode(string supplierCode)
    {
        using (TransactionScope ts = new TransactionScope())
        {
            StationeryEntities dbInstance = new StationeryEntities();
            Supplier s = dbInstance.Suppliers.Where(x => x.SupplierCode == supplierCode).First();
            ts.Complete();

            return s;
        }
    }

    public static void UpdateSupplier(Supplier supplier)
    {
        using (TransactionScope ts = new TransactionScope())
        {
            StationeryEntities dbInstance = new StationeryEntities();
            dbInstance.Entry(supplier).State = System.Data.Entity.EntityState.Modified;
            dbInstance.SaveChanges();
            ts.Complete();
        }
    }

    public static void DeleteSupplier(string supplierCode)
    {
        using (TransactionScope ts = new TransactionScope())
        {
            StationeryEntities dbInstance = new StationeryEntities();
            Supplier s = dbInstance.Suppliers.Where(x => x.SupplierCode == supplierCode).First();
            s.ActiveStatus = "N";
            dbInstance.Entry(s).State = System.Data.Entity.EntityState.Modified;
            dbInstance.SaveChanges();
            ts.Complete();
        }
    }

    public static void CreateSupplier(Supplier supplier)
    {
        using (TransactionScope ts = new TransactionScope())
        {
            StationeryEntities dbInstance = new StationeryEntities();
            dbInstance.Suppliers.Add(supplier);
            dbInstance.SaveChanges();
            ts.Complete();
        }
    }

}