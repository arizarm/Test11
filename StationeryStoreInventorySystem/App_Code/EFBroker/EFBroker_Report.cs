using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;

/// <summary>
/// Summary description for EFBroker_Report
/// </summary>
public class EFBroker_Report
{
    public static int? GetRequisitionsForGivenMonth(DateTime startDate, DateTime endDate, string dept, string cat)
    {
        using (TransactionScope ts = new TransactionScope())
        {
            StationeryEntities SE = new StationeryEntities();
            var totalR = from ri in SE.Requisition_Item 
                         from r in SE.Requisitions
                         from i in SE.Items
                         from d in SE.Departments
                         from e in SE.Employees 
                         from c in SE.Categories 
                         where ri.ItemCode == i.ItemCode
                         where ri.RequisitionID == r.RequisitionID
                         where r.ApprovedBy == e.EmpID
                         where e.DeptCode == d.DeptCode
                         where d.DeptName == dept
                         where i.CategoryID == c.CategoryID
                         where c.CategoryName == cat
                         where r.RequestDate >= startDate
                         where r.RequestDate <= endDate
                         select ri.RequestedQty;
            int? returnValue = totalR.Sum();
            ts.Complete();
            return returnValue;
        }
        }

    public static int? GetReordersForGivenMonth(DateTime startDate, DateTime endDate, string supplier, string cat)
    {
        using (TransactionScope ts = new TransactionScope())
        {
            StationeryEntities SE = new StationeryEntities();
            var totalR = from ip in SE.Item_PurchaseOrder
                         from po in SE.PurchaseOrders
                         from c in SE.Categories
                         from i in SE.Items
                         from s in SE.Suppliers
                         where ip.PurchaseOrderID == po.PurchaseOrderID
                         where ip.ItemCode == i.ItemCode
                         where po.OrderDate >= startDate
                         where po.OrderDate <= endDate
                         where po.SupplierCode == s.SupplierCode
                         where i.CategoryID == c.CategoryID
                         where c.CategoryName == cat
                         where s.SupplierName == supplier
                         select ip.OrderQty;

            int? returnValue = totalR.Sum();
            ts.Complete();
            return returnValue;
        }

    }

}