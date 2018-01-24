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
    public static List<Retrieval> GetAllRetrievalList()
    {
        using (StationeryEntities context = new StationeryEntities())
        {
            return context.Retrievals.ToList();
        }
    }
    public static List<Disbursement> GetAllDisbursementList()
    {
        List<Disbursement> disbursements = new List<Disbursement>();
        using (StationeryEntities context = new StationeryEntities())
        {
            disbursements = context.Disbursements.Include("Department").ToList();
        }
        return disbursements;
    }
    public static Disbursement GetDisbursmentbyDisbID(int disbID)
    {
        Disbursement disbursement;
        using (StationeryEntities context = new StationeryEntities())
        {
            disbursement = context.Disbursements.Where(x => x.DisbursementID == disbID).FirstOrDefault();
        }
        return disbursement;

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
            disbursement.Status = "Completed";
            context.SaveChanges();
        }
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
}