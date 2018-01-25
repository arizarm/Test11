using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "DisbursementService" in code, svc and config file together.
public class DisbursementService : IDisbursementService
{
    DisbursementCotrol disbCon = new DisbursementCotrol();

    public List<WCFDisbursement> getAllDisbursement()
    {
        List<WCFDisbursement> wcfDisbList = new List<WCFDisbursement>();
        List<DisbursementListItems> disbList = disbCon.gvDisbursementPopulate();

        foreach (DisbursementListItems d in disbList)
        {
            wcfDisbList.Add(WCFDisbursement.Make(d.DisbId, d.CollectionDate, d.CollectionTime, d.DepName, d.CollectionPoint));
        }
        return wcfDisbList;
    }

    public WCFDisbursement getDisbursement(int id)
    {
        WCFDisbursement wcfDisb = new WCFDisbursement();
        DisbursementListItems d = disbCon.DisbursementListItemsObj(id);
        wcfDisb = WCFDisbursement.Make(d.DisbId, d.CollectionDate, d.CollectionTime, d.DepName, d.CollectionPoint);
        return wcfDisb;
    }

    public List<WCFDisbursementDetail> getDisbursementDetail()
    {
        List<WCFDisbursementDetail> wcfDisbDetailList = new List<WCFDisbursementDetail>();
        List<DisbursementDetailListItems> disbDetailList = disbCon.gvDisbursementDetailPopulate();
        foreach (DisbursementDetailListItems dI in disbDetailList)
        {
            wcfDisbDetailList.Add(WCFDisbursementDetail.Make(dI.ItemCode, dI.ItemDesc, dI.ReqQty, dI.ActualQty, dI.Remarks));
        }
        return wcfDisbDetailList;
    }
}
