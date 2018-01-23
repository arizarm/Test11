using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for EFBroker_Disbursement_Item
/// </summary>
public class EFBroker_Disbursement_Item
{
    public EFBroker_Disbursement_Item()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public List<Disbursement_Item> GetDisbursement_ItemsbyDisbID(int disbID)
    {
        List<Disbursement_Item> disbursementDetail = new List<Disbursement_Item>();
        using (StationeryEntities context = new StationeryEntities())
        {
            disbursementDetail = context.Disbursement_Item.Include("Item").Where(x => x.DisbursementID == disbID).ToList();
        }
        return disbursementDetail;
    }
}