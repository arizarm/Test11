using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

//AUTHOR : CHOU MING SHENG
// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "RetrievalService" in code, svc and config file together.
public class RetrievalService : IRetrievalService
{
    RetrievalControl retCon = new RetrievalControl();

    //public void Update(WCFRetrievalListDetailUpdate RetrievalDetail)
    public void Update(string RetrievalID, string ItemCode, string ItemQty)
    {
        {
            int retrievalId = Convert.ToInt32(RetrievalID);
            string icode = ItemCode;
            int retrievedQty = Convert.ToInt32(ItemQty);

            Dictionary<String, int> retrievedData = new Dictionary<string, int>();
            retrievedData.Add(icode, retrievedQty);
            retCon.UpdateRetrieval(retrievalId, retrievedData);
        }
    }

    public List<WCFRetrieval> getAllRetrieval()
    {
        List<WCFRetrieval> wcfDisbList = new List<WCFRetrieval>();
        List<Retrieval> retList = retCon.DisplayRetrievalList();

        foreach (Retrieval r in retList)
        {
            wcfDisbList.Add(WCFRetrieval.Make(r.RetrievalID, (int)r.RetrievedBy, ((DateTime)r.RetrievedDate).ToLongDateString(), r.RetrievalStatus));
        }
        return wcfDisbList;
    }

    public List<WCFRetrievalDetail> getRetrievalDetail(string id)
    {
        List<WCFRetrievalDetail> wcfRetDetailList = new List<WCFRetrievalDetail>();
        List<RetrievalListDetailItem> retbDetailList = retCon.DisplayRetrievalListDetail(Convert.ToInt32(id));
        foreach (RetrievalListDetailItem rD in retbDetailList)
        {
            wcfRetDetailList.Add(WCFRetrievalDetail.Make(rD.Bin, rD.Description, rD.TotalRequestedQty, rD.ItemCode, rD.RetrievedQty));
        }
        return wcfRetDetailList;
    }
}
