using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "RequisitionListService" in code, svc and config file together.
public class RequisitionListService : IRequisitionListService
{
    List<WCFRequisitionListItem> IRequisitionListService.getRequisitionListByStatus()
    {
        List<WCFRequisitionListItem> wlist = new List<WCFRequisitionListItem>();
        List<ReqisitionListItem> rlist = RequisitionControl.getRequisitionListByStatus("Pending");
        
        foreach(ReqisitionListItem r in rlist)
        {
            wlist.Add(WCFRequisitionListItem.Make(r.Date, r.RequisitionNo.ToString(), r.Department, r.Status,r.EmployeeName));
            //wlist.Add(WCFRequisitionListItem.Make(r.RequisitionNo.ToString(), r.Department, r.Status, r.EmployeeName, r.Date ));
        }
        return wlist;
    }

    public List<WCFRequisition_ItemList> getList(string id)
    {
        List<WCFRequisition_ItemList> iList = new List<WCFRequisition_ItemList>();
        List<Requisition_ItemList> item = RequisitionControl.getList(Int32.Parse(id));
        foreach(Requisition_ItemList i in item)
        {
            iList.Add(WCFRequisition_ItemList.Make(i.Description, i.RequestedQty, i.UnitOfMeasure, i.Status));
        }
        return iList;
    }

    public void Approve(WCFRequisition requisition)
    {
        Requisition r = new Requisition()
        {
            RequisitionID = Convert.ToInt32(requisition.RequisitionNo),
            ApprovedBy = Convert.ToInt32(requisition.ApprovedBy),
            Remarks=requisition.Remarks,
        };

        RequisitionControl.approveRequisition(Convert.ToInt32(requisition.RequisitionNo),requisition.Remarks, r.ApprovedBy);
    }

    public void Reject(WCFRequisition requisition)
    {
        Requisition r = new Requisition()
        {
            RequisitionID = Convert.ToInt32(requisition.RequisitionNo),
            Status = "Rejected",
            Remarks = requisition.Remarks,
        };

        RequisitionControl.approveRequisition(Convert.ToInt32(requisition.RequisitionNo), requisition.Remarks,1);
    }
}
