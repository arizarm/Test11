using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RetrievalDecision : System.Web.UI.Page
{ 
    RetrievalControl retCon = new RetrievalControl();

    int retrievalId;

    protected void Page_Load(object sender, EventArgs e)
    {     
        if (!IsPostBack)
        {
            retrievalId = (int)Session["RetrievalID"];

            List<RetrievalShortfallItem> RetrievalShortfallItemList = (List<RetrievalShortfallItem>)Session["RetrievalShortfallItemList"];
            
            gvMain.DataSource = RetrievalShortfallItemList;
            gvMain.DataBind();

            List<RetrievalShortfallItemSub> retrievalShortfallItemSubListOfList = new List<RetrievalShortfallItemSub>();

            int j = 0;
            foreach (GridViewRow r in gvMain.Rows)
            {
                GridView gvSub = (GridView)r.FindControl("gvSub");
                List<RetrievalShortfallItemSub> retrievalShortfallItemSubList = retCon.DisplayRetrievalShortfallSub(retrievalId,(r.FindControl("hdfItemCode") as HiddenField).Value);
                
                foreach (RetrievalShortfallItemSub i in retrievalShortfallItemSubList)
                {
                    retrievalShortfallItemSubListOfList.Add(i);
                }
                gvSub.DataSource = retrievalShortfallItemSubList;
                gvSub.DataBind();

                if (gvSub.Rows.Count == 1)
                {
                    foreach (GridViewRow subR in gvSub.Rows)
                    {
                        (subR.FindControl("txtActualQuantity") as TextBox).Text = RetrievalShortfallItemList[j].Qty.ToString();
                    }
                }
                j++;
            }

            ViewState["retrievalShortfallItemSubListOfList"] = retrievalShortfallItemSubListOfList;
        }
    }

    protected void BtnGenerateDisbursementList_Click(object sender, EventArgs e)
    {
        SaveActualQty();
        //retCon.GenerateAccessCode(retrievalId);
        Response.Redirect("CollectionPointUpdate.aspx");
    }

    protected void SaveActualQty()
    {
        List<int> txtActualQuantityList = new List<int>();
        List<RetrievalShortfallItem> shortfallSubList = new List<RetrievalShortfallItem>();
        List<RetrievalShortfallItemSub> retrievalShortfallItemSubListOfList = new List<RetrievalShortfallItemSub>();
        retrievalShortfallItemSubListOfList = (List<RetrievalShortfallItemSub>)ViewState["retrievalShortfallItemSubListOfList"];
        int i = 0;
        foreach (GridViewRow row in gvMain.Rows)
        {
            int actualQty;
            GridView gvSub = (GridView)row.FindControl("gvSub");
            foreach (GridViewRow subRow in gvSub.Rows)
            {
                actualQty = Convert.ToInt32((subRow.FindControl("txtActualQuantity") as TextBox).Text);
                retrievalShortfallItemSubListOfList[i].ActualQty = actualQty;
                i++;
            }
        }
        retCon.SaveActualQtyBreakdownByDepartment(retrievalId, retrievalShortfallItemSubListOfList);
    }
}