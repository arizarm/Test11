using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RetrievalForm : System.Web.UI.Page
{
    RetrievalControl retCon = new RetrievalControl();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) //first time 
        {
            int retrievalId = (int)Session["RetrievalID"];            

            List<RetrievalListDetailItem> RetrievalListDetailItemList = retCon.DisplayRetrievalListDetail(retrievalId);
            
            gvRe.DataSource = RetrievalListDetailItemList;
            gvRe.DataBind();          
        }
    }

    protected void Save_Click(object sender, EventArgs e)
    {    
        saveRetrievalQty();
    }

    public List<RetrievalShortfallItem> saveRetrievalQty()
    {
        int retrievalId = (int)Session["RetrievalID"];

        Dictionary<string, int> retrievedData = new Dictionary<string, int>(); // retrieved itemcode and quantity 

        foreach (GridViewRow row in gvRe.Rows)
        {
            string iCode = (row.FindControl("hdnflditemCode") as HiddenField).Value;
            int retrievedQty = Convert.ToInt32((row.FindControl("txtRetrieved") as TextBox).Text);
            retrievedData.Add(iCode, retrievedQty);
        }

        return retCon.UpdateRetrieval(retrievalId, retrievedData);
    }

    protected void FinalizeDisbursmentList_Click(object sender, EventArgs e)
    {        
        List<RetrievalShortfallItem> RetrievalShortfallItemList = new List<RetrievalShortfallItem>();

        Dictionary<Item, int> discToUpdate = new Dictionary<Item, int>();  //shortfall item + adjustment qty

        RetrievalShortfallItemList = saveRetrievalQty();

        if (RetrievalShortfallItemList.Count != 0)  //if there's shortfall
        {
            foreach (RetrievalShortfallItem r in RetrievalShortfallItemList)
            {
                Item i = EFBroker_Item.GetItembyItemCode(r.ItemCode);
                int balance = (int)i.BalanceQty;
                if (balance > r.Qty)
                {
                    int discQty = r.Qty - balance;
                    discToUpdate.Add(i, discQty);
                }
            }

            Session["discrepancyList"] = discToUpdate;
            Session["RetrievalShortfallItemList"] = RetrievalShortfallItemList;
            Response.Redirect(LoginController.RetrievalShortfallURI);
        }
        else //if there is no short fall go to collectionpoint
        {
            Session["discrepancyList"] = null;
            Session["RetrievalShortfallItemList"] = null;
            Response.Redirect(LoginController.CollectionPointUpdateURI);
        }
    }
}