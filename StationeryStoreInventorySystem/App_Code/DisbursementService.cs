using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "DisbursementService" in code, svc and config file together.
public class DisbursementService : IDisbursementService
{
    public List<WCFDisbursement> getAllDisbursement()
    {
        List<WCFDisbursement> wcfDisbList = new List<WCFDisbursement>();
        List<DisbursementListItems> disbList = DisbursementCotrol.gvDisbursementPopulate();

        foreach (DisbursementListItems d in disbList)
        {
            wcfDisbList.Add(WCFDisbursement.Make(d.DisbId, d.CollectionDate, d.CollectionTime, d.DepName, d.CollectionPoint));
        }
        return wcfDisbList;
    }

    public List<WCFDisbursementDetail> getDisbursementDetail(string id)
    {
        List<WCFDisbursementDetail> wcfDisbDetailList = new List<WCFDisbursementDetail>();
        DisbursementCotrol.DisbursementListItemsObj(id);
        List<DisbursementDetailListItems> disbDetailList = DisbursementCotrol.gvDisbursementDetailPopulate();
        foreach (DisbursementDetailListItems dI in disbDetailList)
        {
            wcfDisbDetailList.Add(WCFDisbursementDetail.Make(dI.ItemCode, dI.ItemDesc, dI.ReqQty, dI.ActualQty, dI.Remarks));
        }
        return wcfDisbDetailList;
    }
}
