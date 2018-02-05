using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

//AUTHOR : KHIN MO MO ZIN
// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "DisbursementService" in code, svc and config file together.
public class DisbursementService : IDisbursementService
{
    DisbursementCotrol disbCon = new DisbursementCotrol();

    public List<WCFDisbursement> getAllDisbursement()
    {
        List<WCFDisbursement> wcfDisbList = new List<WCFDisbursement>();
        List<DisbursementListItems> disbList = disbCon.GvDisbursementPopulate();

        foreach (DisbursementListItems d in disbList)
        {
            wcfDisbList.Add(WCFDisbursement.Make(d.DisbId, d.CollectionDate, d.CollectionTime, d.DepName, d.CollectionPoint));
        }
        return wcfDisbList;
    }

    public List<WCFDisbursementDetail> getDisbursementDetail(string id)
    {
        List<WCFDisbursementDetail> wcfDisbDetailList = new List<WCFDisbursementDetail>();
        List<DisbursementDetailListItems> disbDetailList = disbCon.GvDisbursementDetailPopulate(Convert.ToInt32(id));
        foreach (DisbursementDetailListItems dI in disbDetailList)
        {
            wcfDisbDetailList.Add(WCFDisbursementDetail.Make(dI.ItemCode,dI.ItemCode,dI.Remarks,dI.ReqQty,dI.ActualQty,dI.ActualQty));
        }
        return wcfDisbDetailList;
    }

    public string AccessCodeValidate(string disbId, string accessCode)
    {
        return (disbCon.CheckAccessCode(Convert.ToInt32(disbId), accessCode)).ToString();
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
            remark.Add(u.Remarks);
        }
        disbCon.UpdateDisbursement(disbId, actualQty, remark);
    }
    

    public WCFRegenerateRequest GetRegenerateDate(string disbId)
    {
        string reqDate = (disbCon.GetRegenrateDate(Convert.ToInt32(disbId))).ToLongDateString();
        string depName = EFBroker_Disbursement.GetDisbursmentbyDisbID(Convert.ToInt32(disbId)).Department.DeptName;
        string reqBy = EFBroker_DeptEmployee.GetDeptRepByDeptCode(depName);
        
        WCFRegenerateRequest r = new WCFRegenerateRequest();
        r = WCFRegenerateRequest.Make(reqDate, reqBy, depName);
        return r;
    }

    public void RegenerateRequisition(List<WCFRequestedItem> regenList)
    {
        int disbId = 0;

        List<RequestedItem> requItemList = new List<RequestedItem>();

        foreach(WCFRequestedItem r in regenList)
        {
            RequestedItem rItem = new RequestedItem(r.Code, r.Description, Convert.ToInt32(r.ShortfallQty), RequisitionControl.getUOM(r.Code));
            disbId = r.DisbId;
            requItemList.Add(rItem);
        }

        DateTime date = (disbCon.GetRegenrateDate(disbId));
        string depName = EFBroker_Disbursement.GetDisbursmentbyDisbID(Convert.ToInt32(disbId)).Department.DeptName;
        string reqBy = EFBroker_DeptEmployee.GetDeptRepByDeptCode(depName);
        int empID = EFBroker_DeptEmployee.GetDeptRepEmpIDByDeptCode(depName);
        string depCode = EFBroker_DeptEmployee.GetDepartByEmpID(empID).DeptCode;
        string status = "Priority";

        RequisitionControl.addNewRequisitionItem(requItemList, date, status, empID, depCode);
    }
}
