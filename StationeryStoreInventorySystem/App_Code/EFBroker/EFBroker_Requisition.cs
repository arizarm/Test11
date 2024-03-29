﻿ using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;

/// <summary>
/// Summary description for EFBroker_Requisition
/// </summary>
/// 
//AUTHOR : TAN WEN SONG
//AUTHOR : APRIL SHAR
//AUTHOR : CHOU MING SHENG
public class EFBroker_Requisition
{
    public EFBroker_Requisition()
    {
    }
    public static string GetLatestRequisitionID()
    {
        string req;
        using (StationeryEntities context = new StationeryEntities())
        {
            req = context.Requisitions.OrderByDescending(r => r.RequisitionID).Select(r => r.RequisitionID).FirstOrDefault().ToString();
        }
        return req;
    }
    public static string GetDepartmentCodebyRequisitionID(int requisitionID)
    {
        string depCode;
        using (StationeryEntities context = new StationeryEntities())
        {
            depCode = context.Requisitions.OrderByDescending(x => x.RequestDate).Where(r => r.RequisitionID == requisitionID).Select(r => r.DeptCode).FirstOrDefault();
        }
        return depCode;
    }
    public static Requisition GetRequisitionByID(int id)
    {
        Requisition req;
        using (StationeryEntities context = new StationeryEntities())
        {
            req = context.Requisitions.Where(x => x.RequisitionID.Equals(id)).FirstOrDefault();
        }
        return req;
    }
    public static Requisition_Item FindReqItemsByReqIDItemID(int reqid, string itemcode)
    {
        Requisition_Item item;
        using (StationeryEntities context = new StationeryEntities())
        {
            item = context.Requisition_Item.Where(ri => ri.Item.ItemCode.Equals(itemcode) && ri.RequisitionID.Equals(reqid)).FirstOrDefault();
        }
        return item;
    }
    public static DateTime? GetEarliestReqDateTimebyDisbID(int disbID)
    {
        DateTime? earliest;
        using (StationeryEntities context = new StationeryEntities())
        {
            earliest = context.Requisitions.Where(x => x.DisbursementID == disbID).OrderByDescending(x => x.RequestDate).Select(x => x.RequestDate).FirstOrDefault();
        }
        return earliest;
    }
    // alternative
    public static List<DateTime?> GetDateTimeListbyDisbID(int disbID)
    {
        List<DateTime?> dateList;
        using (StationeryEntities context = new StationeryEntities())
        {
            dateList = context.Requisitions.Where(x => x.DisbursementID == disbID).OrderBy(x => x.RequestDate).Select(x => x.RequestDate).ToList();
        }
        return dateList;
    }
    public static List<DateTime?> GetAllFinalisedRequisitionMonths()
    {
        using (TransactionScope ts = new TransactionScope())
        {
            StationeryEntities dbInstance = new StationeryEntities();
            List<DateTime?> allMonths = dbInstance.Requisitions.Where(b => b.Status == "Closed" || b.Status == "Approved").Select(c => c.RequestDate).ToList();

            ts.Complete();
            return allMonths;
        }
    }
    public static List<int> GetRequisitionIDListbyDisbID(int disbID)
    {
        using (StationeryEntities context = new StationeryEntities())
        {
            return context.Requisitions.Where(x => x.DisbursementID == disbID).Select(x => x.RequisitionID).ToList();
        }
    }
    public static List<Requisition> GetAllApprovedOrPriorityReq()
    {
        List<Requisition> rList;
        rList = GetAllRequisitionsByStatus("Priority");
        rList.AddRange(GetAllRequisitionsByStatus("Approved"));
        return rList;
    }
    public static List<Requisition> GetAllRequisitionsByStatus(string status)
    {
        List<Requisition> rlist;
        using (StationeryEntities context = new StationeryEntities())
        {
            rlist = context.Requisitions.OrderBy(x=>x.RequestDate).Where(x => x.Status == status).ToList();
        }
        return rlist;
    }
    public static List<Requisition> getRequisitionListByEmpIDAndStatus(int empID, string status)
    {
        List<Requisition> rlist;
        using (StationeryEntities context = new StationeryEntities())
        {
            rlist = context.Requisitions.OrderBy(x => x.RequestDate).Where(x => x.Status == status && x.RequestedBy == empID).ToList();
        }
        return rlist;
    }
    public static List<Requisition> getRequisitionListByStatusAndDepCode(string status, string depCode)
    {
        List<Requisition> rlist;
        using (StationeryEntities context = new StationeryEntities())
        {
            rlist = context.Requisitions.OrderBy(x => x.RequestDate).Where(x => x.Status.Equals(status) && x.DeptCode.Equals(depCode)).ToList();
        }
        return rlist;
    }

    public static List<Requisition> GetRequisitionListByDisbursementID(int disbursementID)
    {
        List<Requisition> rlist;
        using (StationeryEntities context = new StationeryEntities())
        {
            rlist = context.Requisitions.OrderBy(x => x.RequestDate).Where(x => x.DisbursementID == disbursementID).ToList();
        }
        return rlist;
    }

    public static List<Requisition> displayAllByDeptCode(string deptCode)
    {
        List<Requisition> rlist;
        using (StationeryEntities context = new StationeryEntities())
        {
            rlist = context.Requisitions.OrderByDescending(x => x.RequestDate).Where(x => x.DeptCode.Equals(deptCode)).ToList();
        }
        return rlist;
    }

    public static List<Requisition> GetAllRequisitionList()
    {
        List<Requisition> rList;
        using (StationeryEntities context = new StationeryEntities())
        {
            rList = context.Requisitions.ToList();
        }
        return rList;
    }
    public static List<Requisition> GetRequisitionListByRequestorID(int empID)
    {
        List<Requisition> rList;
        using (StationeryEntities context = new StationeryEntities())
        {
            rList = context.Requisitions.OrderByDescending(x => x.RequestDate).Where(x => x.RequestedBy == empID).OrderByDescending(c => c.RequestDate).ToList();
        }
        return rList;
    }

    public static List<Requisition_Item> GetRequisitionItemListbyReqID(int id)
    {
        List<Requisition_Item> tlist = new List<Requisition_Item>();
        using (StationeryEntities context = new StationeryEntities())
        {
            tlist = context.Requisition_Item.Include("Item").Include("Requisition").Where(x => x.RequisitionID.Equals(id)).ToList();
        }
        return tlist;
    }
    public static List<Requisition_Item> FindReqItemsByReqID(int id)
    {
        List<Requisition_Item> list;
        using (StationeryEntities context = new StationeryEntities())
        {
            list = context.Requisition_Item.Where(ri => ri.RequisitionID.Equals(id)).ToList();
        }
        return list;
    }
    public static List<Requisition_Item> GetRequisitionItemList()
    {
        List<Requisition_Item> rList;
        using (StationeryEntities context = new StationeryEntities())
        {
            rList = context.Requisition_Item.ToList();
        }
        return rList;
    }
    public static void AddItemToRequisition(Requisition_Item item)
    {
        using (StationeryEntities context = new StationeryEntities())
        {
            context.Requisition_Item.Add(item);
            context.SaveChanges();
        }
        return;
    }
    public static void EditRequisitionItemQty(int id, string code, int qty)
    {
        using (TransactionScope ts = new TransactionScope())
        {
            StationeryEntities context = new StationeryEntities();
            Requisition_Item ri = context.Requisition_Item.Where(i => i.RequisitionID == id).Where(i => i.ItemCode.Equals(code)).FirstOrDefault();
            ri.RequestedQty += qty;
            context.SaveChanges();
            ts.Complete();
        }
    }
    public static void UpdateRequisitionItem(int id, string code, int qty)
    {
        using (TransactionScope ts = new TransactionScope())
        {
            StationeryEntities context = new StationeryEntities();
            Requisition_Item ri = context.Requisition_Item.Where(r => r.RequisitionID == id && r.ItemCode.Equals(code)).FirstOrDefault();
            ri.RequestedQty = qty;
            context.Entry(ri).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
            ts.Complete();
        }
    }
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
    public static void AddNewRequisition(List<RequestedItem> item, DateTime date, string status, int empID, string DeptCode)
    {
        using (TransactionScope ts = new TransactionScope())
        {
            StationeryEntities context = new StationeryEntities();
            Requisition r = new Requisition();
            List<Requisition_Item> list = new List<Requisition_Item>();
            //to pass from previous form
            r.RequestDate = date;
            r.Status = status;
            r.RequestedBy = empID;
            r.DeptCode = DeptCode;
            context.Requisitions.Add(r);
            context.SaveChanges();

            foreach (RequestedItem i in item)
            {
                int qty = i.Quantity;

                string code = i.Code;

                Requisition_Item ri = new Requisition_Item();
                ri.RequisitionID = r.RequisitionID;
                ri.ItemCode = code;
                ri.RequestedQty = qty;
                list.Add(ri);

            }
            context.Requisition_Item.AddRange(list);
            context.SaveChanges();
            ts.Complete();
        }
    }
    public static void ApproveRequisition(int id, string reason, int? empID)
    {
        using (StationeryEntities context = new StationeryEntities())
        {
            Requisition r = context.Requisitions.Where(x => x.RequisitionID == id).First();
            r.Remarks = reason;
            r.RequisitionID = id;
            r.Status = "Approved";
            r.ApprovedBy = empID;

            context.Entry(r).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }
    }
    public static void RejectRequisition(int id, string reason, int empID)
    {
        using (StationeryEntities context = new StationeryEntities())
        {
            Requisition r = context.Requisitions.Where(x => x.RequisitionID == id).First();
            r.Remarks = reason;
            r.RequisitionID = id;
            r.ApprovedBy = empID;
            r.Status = "Rejected";
            //r.Remarks = "Rejected By Head";
            context.SaveChanges();
        }
    }
    public static void CancelRejectRequisition(int id)
    {
        using (StationeryEntities context = new StationeryEntities())
        {
            Requisition r = context.Requisitions.Where(x => x.RequisitionID == id).FirstOrDefault();
            r.Status = "Rejected";
            r.Remarks = "Request cancelled";
            context.SaveChanges();
        }
        return;
    }
    public static void UpdateRequisition(Requisition requisition)
    {
        using (StationeryEntities context = new StationeryEntities())
        {
            context.Entry(requisition).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }
        return;
    }
    public static void UpdateRequisitionList(List<Requisition> requisitions)
    {
        using (StationeryEntities context = new StationeryEntities())
        {
            requisitions.ForEach(requisition => context.Entry(requisition).State = System.Data.Entity.EntityState.Modified);
            context.SaveChanges();
        }
        return;
    }
    public static void UpdateRequisitionStatus(int reqID, string status)
    {
        Requisition r = EFBroker_Requisition.GetRequisitionByID(reqID);
        r.Status = status;
        EFBroker_Requisition.UpdateRequisition(r);
    }
    public static List<Requisition> getCollectionList(string deptCode)
    {
        using (StationeryEntities context = new StationeryEntities())
        {
            return context.Requisitions.OrderByDescending(x => x.RequestDate).Where(x => x.Status.Equals("InProgress") && x.DeptCode == deptCode).ToList();
        }
    }

    public static List<Requisition> SearchForCollectionList(string deptCode)
    {
        using (StationeryEntities context = new StationeryEntities())
        {
            return context.Requisitions.OrderByDescending(x => x.RequestDate).Where(x => x.DeptCode.Equals(deptCode) && x.Status.Equals("InProgress")).ToList();
        }
    }

    public static List<Requisition> SearchForReqHeadWithoutStatus(string deptCode)
    {
        using (StationeryEntities context = new StationeryEntities())
        {
            return context.Requisitions.OrderByDescending(x => x.RequestDate).Where(x => x.DeptCode.Equals(deptCode)).ToList();
        }
    }

    public static List<Requisition> SearchForReqHeadWithStatus(string deptCode, string status)
    {
        using (StationeryEntities context = new StationeryEntities())
        {
            return context.Requisitions.OrderByDescending(x => x.RequestDate).Where(x => x.DeptCode.Equals(deptCode) && x.Status.Equals(status)).ToList();
        }
    }

    public static List<Requisition> DisplayReqByDeptCodeEmpID(string deptCode, int empID)
    {
        using (StationeryEntities context = new StationeryEntities())
        {
            return context.Requisitions.OrderByDescending(x => x.RequestDate).Where(x => x.DeptCode.Equals(deptCode) && x.RequestedBy == (empID)).ToList();
        }
    }

    public static Requisition GetRequisitionByDisbID(int disbId)
    {
        using (StationeryEntities context = new StationeryEntities())
        {
            return context.Requisitions.OrderByDescending(x => x.RequestDate).Where(x => x.DisbursementID == disbId).FirstOrDefault();
        }
    }

    public static int CountPending(string deptCode)
    {
        using (StationeryEntities context = new StationeryEntities())
        {
            return context.Requisitions.Where(x => x.DeptCode.Equals(deptCode) && x.Status.Equals("Pending")).Count();
        }
    }
}