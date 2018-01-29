using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for EFBroker_Disbursement
/// </summary>
public class EFBroker_Disbursement
{
    public EFBroker_Disbursement()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static int GetRequestedQtybyDisbursementIDItemCode(int disbID, string itemCode)
    {
        int qty;
        using (StationeryEntities context = new StationeryEntities())
        {
            qty = context.Disbursement_Item.Where(x => x.DisbursementID == disbID && x.ItemCode.ToString().Equals(itemCode)).Select(x => x.TotalRequestedQty).FirstOrDefault() ?? 0;
        }
        return qty;
    }
    public static List<Retrieval> GetAllRetrievalList()
    {
        using (StationeryEntities context = new StationeryEntities())
        {
            return context.Retrievals.ToList();
        }
    }
    public static List<Retrieval> GetPendingAndProgressRetrievalList()
    {
        List<Retrieval> rList = new List<Retrieval>();
        using (StationeryEntities context = new StationeryEntities())
        {
            context.Retrievals.Where(x => x.RetrievalStatus.Equals("Pending") || x.RetrievalStatus.Equals("InProgress")).ToList();////
        }
        return rList;
    }
    public static List<Disbursement> GetAllDisbursementList()
    {
        List<Disbursement> disbursements = new List<Disbursement>();
        using (StationeryEntities context = new StationeryEntities())
        {
            disbursements = context.Disbursements.Include("Department").Where(x => x.Status.Equals("Ready")).ToList();
        }
        return disbursements;
    }
    public static List<Disbursement> GetDisbursmentListbyRetrievalID(int retrievalID)
    {
        List<Disbursement> dList;
        using (StationeryEntities context = new StationeryEntities())
        {
            dList = context.Disbursements.Include("Retrieval").Include("Department").Include("Disbursement_Item").Where(x => x.RetrievalID == retrievalID).ToList();
        }
        return dList;

    }
    public static Disbursement GetDisbursmentbyDisbID(int disbID)
    {
        Disbursement disbursement;
        using (StationeryEntities context = new StationeryEntities())
        {
            disbursement = context.Disbursements.Include("Department").Include("Disbursement_Item").Where(x => x.DisbursementID == disbID).FirstOrDefault();
        }
        return disbursement;

    }
    public static Disbursement_Item GetDisbursementItem(int id, string itemCode)
    {   //not needed
        Disbursement_Item disbursementItem;
        using (StationeryEntities context = new StationeryEntities())
        {
            disbursementItem = context.Disbursement_Item.Where(x => x.DisbursementID == id && x.ItemCode == itemCode).FirstOrDefault();
        }
        return disbursementItem;
    }
    public static List<Disbursement_Item> GetDisbursement_ItemsbyDisbID(int disbID)
    {
        List<Disbursement_Item> disbursementDetail = new List<Disbursement_Item>();
        using (StationeryEntities context = new StationeryEntities())
        {
            disbursementDetail = context.Disbursement_Item.Include("Item").Where(x => x.DisbursementID == disbID).ToList();
        }
        return disbursementDetail;
    }
    public static int AddNewRetrieval(int empID)
    {
        int retrievalID;
        using (StationeryEntities context = new StationeryEntities())
        {
            Retrieval r = new Retrieval();
            r.RetrievedBy = empID;     //base on user session
            r.RetrievedDate = DateTime.Today;
            r.RetrievalStatus = "Pending";
            context.Retrievals.Add(r);
            context.SaveChanges();
            retrievalID = r.RetrievalID; // get auto increasement data after SaveChanges        
        }
        return retrievalID;
    }
    public static int AddNewDisbursment(Disbursement disbursment)
    {
        using (StationeryEntities context = new StationeryEntities())
        {
            context.Disbursements.Add(disbursment);
            context.SaveChanges();
            //saving changes get ID for disbursement
            return disbursment.DisbursementID;
        }
    }
    public static void AddNewDisbursementItem(Disbursement_Item disitem)
    {
        using (StationeryEntities context = new StationeryEntities())
        {
            context.Disbursement_Item.Add(disitem);
            context.SaveChanges();
        }
        return;

    }
    public static void AddNewDisbursementItemList(List<Disbursement_Item> disitems)
    {
        using (StationeryEntities context = new StationeryEntities())
        {
            context.Disbursement_Item.AddRange(disitems);
            context.SaveChanges();
        }
        return;

    }
    public static string GetAccessCodebyDisbID(int disbID)
    {
        string accessCode;
        using (StationeryEntities context = new StationeryEntities())
        {
            accessCode = context.Disbursements.Where(x => x.DisbursementID == disbID).Select(x => x.AccessCode).FirstOrDefault();
        }
        return accessCode;
    }
    public static void UpdateRetrievalStatus(int rId, string status)
    {
        using (StationeryEntities context = new StationeryEntities())
        {
            Retrieval retrieval = context.Retrievals.Where(x => x.RetrievalID == rId).FirstOrDefault();
            retrieval.RetrievalStatus = status;
            context.SaveChanges();
        }
    }
    public static void UpdateDisbursementActualQty(int disbID, List<int> actualQty)
    {
        int i = 0;
        using (StationeryEntities context = new StationeryEntities())
        {
            Disbursement disbursement = GetDisbursmentbyDisbID(disbID);
            foreach (Disbursement_Item di in disbursement.Disbursement_Item)
            {
                di.ActualQty = actualQty[i];
                i++;
            }
            context.SaveChanges();
        }
    }
    public static void UpdateDisbursementStatus(int disbID)
    {
        using (StationeryEntities context = new StationeryEntities())
        {
            Disbursement disbursement = GetDisbursmentbyDisbID(disbID);
            disbursement.Status = "Closed";
            UpdateDisbursement(disbursement);
        }
    }
    public static void UpdateDisbursement(Disbursement disbursement)
    {
        using (StationeryEntities context = new StationeryEntities())
        {
            context.Entry(disbursement).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }
    }
    public static void UpdateDisbursementItem(Disbursement_Item disItem)
    {
        using (StationeryEntities context = new StationeryEntities())
        {
            context.Entry(disItem).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }

    }


}