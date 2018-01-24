using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for EFBroker_Discrepancy
/// </summary>
public class EFBroker_Discrepancy
{
    public EFBroker_Discrepancy()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static void SaveDiscrepencies(List<Discrepency> dList)
    {
        using (StationeryEntities context = new StationeryEntities())
        {
            foreach (Discrepency d in dList)
            {
                context.Discrepencies.Add(d);
            }
            context.SaveChanges();
        }
        return;
    }
    public static int GetDiscrepancyID(Discrepency d)
    {
        int id;
        using (StationeryEntities context = new StationeryEntities())
        {
            id = context.Discrepencies.Where(x => x.ItemCode == d.ItemCode && x.RequestedBy == d.RequestedBy && x.Date == d.Date && x.AdjustmentQty == d.AdjustmentQty && x.Remarks == d.Remarks).Select(x => x.DiscrepencyID).FirstOrDefault();
        }
        return id;
    }
    public static Discrepency GetDiscrepancyById(int id)
    {   //goes to discrepancy broker
        Discrepency d;
        using (StationeryEntities context = new StationeryEntities())
        {
            d = context.Discrepencies.Where(x => x.DiscrepencyID == id).FirstOrDefault();
        }
        return d;
    }
    public static Discrepency GetPendingMonthlyDiscrepancyByItemCode(string itemCode)
    {   //goes to discrepancy broker
        Discrepency d;
        using (StationeryEntities context = new StationeryEntities())
        {
            d = context.Discrepencies.Where(x => x.ItemCode == itemCode && x.Status == "Monthly").OrderByDescending(x => x.Date).FirstOrDefault();
        }
        return d;
    }
    public static List<Discrepency> GetPendingDiscrepanciesByItemCode(string itemCode)
    {   //goes to discrepancy broker
        List<Discrepency> dList = new List<Discrepency>();
        using (StationeryEntities context = new StationeryEntities())
        {
            dList = context.Discrepencies.Where(x => x.ItemCode == itemCode && x.Status == "Pending").ToList();
        }
        return dList;
    }
    public static List<Discrepency> GetPendingDiscrepancyList()
    {
        List<Discrepency> dList = new List<Discrepency>();
        using (StationeryEntities context = new StationeryEntities())
        {
            dList = context.Discrepencies.Where(x => x.Status == "Pending").ToList();
        }
        return dList;
    }
    public static List<Discrepency> GetMonthlyDiscrepancyList()
    {
        List<Discrepency> dList = new List<Discrepency>();
        using (StationeryEntities context = new StationeryEntities())
        {
            dList = context.Discrepencies.Where(x => x.Status == "Monthly").ToList();
        }
        return dList;
    }

    public static void ProcessDiscrepancy(int id, string action)
    {
        using (StationeryEntities context = new StationeryEntities())
        {
            Discrepency d = context.Discrepencies.Where(x => x.DiscrepencyID == id).First();
            d.Status = action;
            context.SaveChanges();
        }
    }
}