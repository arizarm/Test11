using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Transactions;

/// <summary>
/// Summary description for ReqBS
/// </summary>
public static class ReqBS
{

    //GET ITEM DESCRIPTION
    public static List<String> getItem()
    {
        using (StationeryEntities context = new StationeryEntities())
        {
            return context.Items.Where(i => i.ActiveStatus.Equals("Y")).Select(i => i.Description).ToList();

        }
    }

    //GET ITEM UOM
    public static String getUOM(string item)
    {
        using (StationeryEntities context = new StationeryEntities())
        {
            return context.Items.Where(i => i.Description.Equals(item)).Select(i => i.UnitOfMeasure).FirstOrDefault();
        }
    }

    //GET LAST REQUISITION
    public static String getLastReq()
    {
        using (StationeryEntities context = new StationeryEntities())
        {
            return context.Requisitions.OrderByDescending(r => r.RequisitionID).Select(r => r.RequisitionID).FirstOrDefault().ToString();
        }
    }

    //GET ITEM DESCRIPTION BY ITEM CODE
    public static String getCode(string item)
    {
        using (StationeryEntities context = new StationeryEntities())
        {
            return context.Items.Where(i => i.Description.Equals(item)).Select(i => i.ItemCode).FirstOrDefault();
        }
    }

    //FIND REQUISITION WITH PENDING STATUS
    public static List<Requisition> getRequisitionList()
    {
        using (StationeryEntities context = new StationeryEntities())
        {
            return context.Requisitions.Where(x => x.Status == "Pending").ToList<Requisition>();
        }
    }

    //FIND REQUISITION BY ID
    public static Requisition getRequisition(int id)
    {
        using (StationeryEntities context = new StationeryEntities())
        {
            return (context.Requisitions.Where(r => r.RequisitionID.Equals(id))).FirstOrDefault();
        }
    }

    public static void getList(int id)
    {
        using (StationeryEntities context = new StationeryEntities())
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
    }

    //CANCEL REQUISITION
    public static void cancelRejectRequisition(int id)
    {
        using (StationeryEntities context = new StationeryEntities())
        {
            Requisition r = context.Requisitions.Where(x => x.RequisitionID == id).First();
            r.Status = "Rejected";
            r.Remarks = "Request cancelled";
            context.SaveChanges();
        }
    }

    //SEARCH REQUISITION BY STATUS
    public static List<Requisition> getRequisitionListByStatus(String status)
    {
        using (StationeryEntities context = new StationeryEntities())
        {
            return (context.Requisitions.Where(x => x.Status == status).ToList<Requisition>());
        }
    }

    //FIND REQUISITION ITEM BY REQUISITION ID
    public static Requisition_Item findRequisitionID(int id)
    {
        using (StationeryEntities context = new StationeryEntities())
        {
            return context.Requisition_Item.Where(ri => ri.RequisitionID.Equals(id)).FirstOrDefault();
        }
    }

    //FIND REQUISITION ITEM BY REQUISITION ID AND ITEM CODE
    public static Requisition_Item findByReqIDItemCode(int id, string des)
    {
        using (StationeryEntities context = new StationeryEntities())
        {
            string code = context.Items.Where(i => i.Description.Equals(des)).Select(i => i.ItemCode).FirstOrDefault();
            return context.Requisition_Item.Where(ri => ri.ItemCode.Equals(code)).Where(ri => ri.RequisitionID.Equals(id)).FirstOrDefault();
        }
    }

    //REMOVE REQUISITION
    public static void removeRequisitionItem(int id, string code)
    {
        using (TransactionScope ts = new TransactionScope())
        {
            StationeryEntities context = new StationeryEntities();
            Requisition_Item ri = context.Requisition_Item.Where(r => r.RequisitionID.Equals(id)).Where(r => r.ItemCode.Equals(code)).FirstOrDefault();
            context.Requisition_Item.Remove(ri);
            context.SaveChanges();
            ts.Complete();
        }
    }

    //UPDATE REQUISITION ITEM
    public static void updateRequisitionItem(int id, string code, int qty)
    {
        using (TransactionScope ts = new TransactionScope())
        {
            StationeryEntities context = new StationeryEntities();
            Requisition_Item ri = context.Requisition_Item.Where(r=>r.RequisitionID.Equals(id)).Where(r=>r.ItemCode.Equals(code)).FirstOrDefault();
            ri.RequestedQty = qty;
            context.Entry(ri).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
            ts.Complete();
        }
    }
    
    //ADD REQUISITION ITEM
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

    //CHANGE REQUISITION STATUS
    public static void approveRequisition(int id,string reason)
    {
        using (StationeryEntities context = new StationeryEntities())
        {
            Requisition r = context.Requisitions.Where(x => x.RequisitionID == id).First();
            r.Remarks = reason;
            r.RequisitionID = id;
            r.Status = "Approved";          
            context.SaveChanges();  
        }
    }
    public static void rejectRequisition(int id, string reason)
    {
        using (StationeryEntities context = new StationeryEntities())
        {
            Requisition r = context.Requisitions.Where(x => x.RequisitionID == id).First();
            r.Remarks = reason;
            r.RequisitionID = id;
            r.Status = "Rejected";
            r.Remarks = "Rejected By Head";
            context.SaveChanges();
        }
    }
}