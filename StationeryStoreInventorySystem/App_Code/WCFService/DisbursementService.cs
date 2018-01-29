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

    public List<WCFDisbursementDetail> getDisbursementDetail(string id)
    {
        List<WCFDisbursementDetail> wcfDisbDetailList = new List<WCFDisbursementDetail>();
        List<DisbursementDetailListItems> disbDetailList = disbCon.gvDisbursementDetailPopulate(Convert.ToInt32(id));
        foreach (DisbursementDetailListItems dI in disbDetailList)
        {
            wcfDisbDetailList.Add(WCFDisbursementDetail.Make(dI.ItemCode, dI.ItemDesc, dI.ReqQty, dI.ActualQty, dI.Remarks));
        }
        return wcfDisbDetailList;
    }

    public string AccessCodeValidate(string disbId, string accessCode)
    {
        return (disbCon.checkAccessCode(Convert.ToInt32(disbId), accessCode)).ToString();
    }

    public void UpdateDisbursement(List<WCFUpdateDisbursement> qtyList)
    {
        int disbId = 0;
        List<int> actualQty = new List<int>();
        List<string> remark = new List<string>();
        foreach (WCFUpdateDisbursement u in qtyList)
        {
            actualQty.Add(Convert.ToInt32(u.ActualQty));
            disbId = Convert.ToInt32(u.DisbId);
            remark.Add(u.Remark);
        }
        disbCon.UpdateDisbursement(disbId, actualQty, remark);
    }
}
