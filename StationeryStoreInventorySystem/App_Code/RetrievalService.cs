using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "RetrievalService" in code, svc and config file together.
public class RetrievalService : IRetrievalService
{
    public List<WCFRetrieval> getAllRetrieval()
    {
        List<WCFRetrieval> wcfDisbList = new List<WCFRetrieval>();
        List<Retrieval> retList = RetrievalControl.DisplayRetrievalList();

        foreach (Retrieval r in retList)
        {
            wcfDisbList.Add(WCFRetrieval.Make(r.RetrievalID, (int)r.RetrievedBy, (DateTime)r.RetrievedDate, r.RetrievalStatus));
        }
        return wcfDisbList;
    }

    public List<WCFRetrievalDetail> getRetrievalDetail(string id)
    {
        List<WCFRetrievalDetail> wcfRetDetailList = new List<WCFRetrievalDetail>();        
        List<RetrievalListDetailItem> retbDetailList = RetrievalControl.DisplayRetrievalListDetail(id);
        foreach (RetrievalListDetailItem rD in retbDetailList)
        { 
            wcfRetDetailList.Add(WCFRetrievalDetail.Make(rD.Bin, rD.Description, rD.TotalRequestedQty, rD.ItemCode));
        }
        return wcfRetDetailList;
    }
}
