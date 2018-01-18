using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ReqBS
/// </summary>
public static class ReqBS
{
    static StationeryEntities context = new StationeryEntities();

    public static List<String> getItem()
    {
        return context.Items.Where(i => i.ActiveStatus.Equals("Y")).Select(i => i.Description).ToList();
    }

    public static String getUOM(string item)
    {
        return context.Items.Where(i => i.Description.Equals(item)).Select(i => i.UnitOfMeasure).FirstOrDefault();
    }

    public static String getLastReq()
    {
        return context.Requisitions.OrderByDescending(r=>r.RequisitionID).Select(r=>r.RequisitionID).FirstOrDefault().ToString();
    }

    public static String getCode(string item)
    {
        return context.Items.Where(i => i.Description.Equals(item)).Select(i => i.ItemCode).FirstOrDefault();
    }
    public static List<Requisition> getRequisitionList()
    {
        return context.Requisitions.Where(x => x.Status == "Pending").ToList<Requisition>();
    }

    public static Requisition getRequisition(int id)
    {
        return (context.Requisitions.Where(r => r.RequisitionID.Equals(id))).FirstOrDefault();
    }

    public static void getList(int id)
    {
        var q = from i in context.Items
                join ri in context.Requisition_Item
                on i.ItemCode equals ri.ItemCode
                join rt in context.Requisitions
                on ri.RequisitionID equals rt.RequisitionID
                where ri.RequisitionID == id
                select new
                {
                    i.Description,
                    ri.RequestedQty,
                    i.UnitOfMeasure,
                    rt.Status
                };
    }

    public static void cancelRejectRequisition(int id)
    {
        Requisition r = context.Requisitions.Where(x => x.RequisitionID == id).First();
        r.Status = "Rejected";
        context.SaveChanges();
    }

    public static List<Requisition> getRequisitionListByStatus(String status)
    {
        return (context.Requisitions.Where(x => x.Status == status).ToList<Requisition>());
    }

    public static Requisition_Item findRequisitionID(int id)
    {
        return context.Requisition_Item.Where(ri => ri.RequisitionID.Equals(id)).FirstOrDefault();
    }
    
    public static void addItemToRequisition(string code, int qty, int id)
    {
        using (StationeryEntities context = new StationeryEntities())
        {
            Requisition_Item ri = new Requisition_Item();
            ri.RequisitionID = id;
            ri.ItemCode = code;
            ri.RequestedQty = qty;
            context.Requisition_Item.Add(ri);
            context.SaveChanges();
        }

    }
}