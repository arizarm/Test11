using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;

/// <summary>
/// Summary description for EFBroker_Requisition
/// </summary>
public class EFBroker_Requisition
{
    public EFBroker_Requisition()
    {
    }
    public static string GetEarliestReqDateTimebyDisbID(int disbID)
    {
        string earliest;
        using (StationeryEntities context = new StationeryEntities())
        {
            earliest = context.Requisitions.Where(x => x.DisbursementID == disbID).OrderByDescending(x => x.RequestDate).Select(x=> x.RequestDate.ToString() ).FirstOrDefault();
        }
        return earliest;
    }
    // alternative
    public static List<string> GetDateTimeListbyDisbID(int disbID)
    {
        List<string> dateStringList;
        using (StationeryEntities context = new StationeryEntities())
        {
            dateStringList = context.Requisitions.Where(x => x.DisbursementID == disbID).OrderBy(x => x.RequestDate).Select(x => x.RequestDate.ToString()).ToList();
        }
        return dateStringList;
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
    public static List<Requisition> GetAllApprovedOrPriorityReq()
    {
        List<Requisition> rList;
        rList = GetAllApprovedRequisitions();
        rList.AddRange(GetAllPriorityRequisitions());
        return rList;
    }
    public static List<Requisition> GetAllApprovedRequisitions()
    {
        List<Requisition> rList;
        using (StationeryEntities context = new StationeryEntities())
        {
            rList = context.Requisitions.Where(x => x.Status == "Approved").ToList();
        }
        return rList;
    }
    public static List<Requisition> GetAllPriorityRequisitions()
    {
        List<Requisition> rList;
        using (StationeryEntities context = new StationeryEntities())
        {
            rList = context.Requisitions.Where(x => x.Status == "Priority").ToList();
        }
        return rList;
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
}