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
    private static EFBroker_Supplier DbInstance;

    private EFBroker_Supplier()
    {
        StationeryEntities SE = new StationeryEntities();
    }

    public static EFBroker_Supplier getInstance()
    {
        if (DbInstance == null)
            DbInstance = new EFBroker_Supplier();
        return DbInstance;
    }

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

}