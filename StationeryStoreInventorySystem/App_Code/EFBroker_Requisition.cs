using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for EFBroker_Requisition
/// </summary>
public class EFBroker_Requisition
{
    public EFBroker_Requisition()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public DateTime GetEarliestReqDateTimebyDisbID(int disbID)
    {
        DateTime earliest;
        using (StationeryEntities context = new StationeryEntities())
        {
            earliest = context.Requisitions.Where(x => x.DisbursementID == disbID).OrderBy(x => x.RequestDate).Select(x = x.RequestDate).FirstOrDefault();
        }
        return earliest;

    }
    // alternative
    public List<DateTime> GetDateTimeListbyDisbID(int disbID)
    {
        List<DateTime> dateList;
        using (StationeryEntities context = new StationeryEntities())
        {
            dateList = context.Requisitions.Where(x => x.Disbursement == disbID).OrderBy(x => x.RequestDate).Select(x = x.RequestDate).ToList();
        }
        return dateList;
    }
    public void AddItemToRequisition(Requisition_Item item)
    {
        using (StationeryEntities context = new StationeryEntities())
        {
            context.Requisition_Item.Add(item);
            context.SaveChanges();
        }
        return;
    }
}