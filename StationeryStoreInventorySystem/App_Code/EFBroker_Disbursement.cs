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
    public List<Disbursement> GetAllDisbursementList()
    {
        List<Disbursement> disbursements = new List<Disbursement>();
        using (StationeryEntities context = new StationeryEntities())
        {
            disbursements = context.Disbursements.Include("Department").ToList();
        }
        return disbursements;
    }
    public Disbursement GetDisbursmentbyDisbID(int disbID)
    {
        Disbursement disbursement;
        using (StationeryEntities context = new StationeryEntities())
        {
            disbursement = context.Disbursements.Where(x => x.DisbursementID == disbID).FirstOrDefault();
        }
        return disbursement;

    }
    public string GetAccessCodebyDisbID(int disbID)
    {
        string accessCode;
        using (StationeryEntities context = new StationeryEntities())
        {
            accessCode = context.Disbursements.Where(x => x.DisbursementID == disbID).Select(x => x.AccessCode).FirstOrDefault();
        }
        return accessCode;
    }
    public void UpdateDisbursementActualQty(int disbID, List<int> actualQty)
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
    public void UpdateDisbursementStatus(int disbID)
    {
        using (StationeryEntities context = new StationeryEntities())
        {
            Disbursement disbursement = GetDisbursmentbyDisbID(disbID);
            disbursement.Status = "Completed";
            context.SaveChanges();
        }
    }
}