using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//AUTHOR : CHOU MING SHENG
public partial class RetrievalForm : System.Web.UI.Page
{
    RetrievalControl retCon = new RetrievalControl();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.UrlReferrer != null) // if previous page is not null
        {
            if (!IsPostBack) //first time 
            {
                if (Session["RetrievalID"] != null)
                {
                    int retrievalId = (int)Session["RetrievalID"];

                    List<RetrievalListDetailItem> RetrievalListDetailItemList = retCon.DisplayRetrievalListDetail(retrievalId);

                    gvRe.DataSource = RetrievalListDetailItemList;
                    gvRe.DataBind();

                    //RangeValidator
                    foreach (GridViewRow r in gvRe.Rows)
                    {
                        int totalRequestedQty = Convert.ToInt32((r.FindControl("lblTotalRequestedQty") as Label).Text);

                        RangeValidator rv = r.FindControl("rng") as RangeValidator;
                        rv.MaximumValue = totalRequestedQty.ToString();
                    }
                    //
                }
                else
                {
                    Response.Redirect(LoginController.RequisitionListClerkURI);
                }
            }
        }
        else
        {
            Response.Redirect(LoginController.RequisitionListClerkURI);
        }
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        SaveRetrievalQty();
    }

    public List<RetrievalShortfallItem> SaveRetrievalQty()
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

    protected void BtnFinalizeDisbursmentList_Click(object sender, EventArgs e)
    {
        int retrievalId = (int)Session["RetrievalID"];

        List<RetrievalShortfallItem> RetrievalShortfallItemList = new List<RetrievalShortfallItem>();

        Dictionary<Item, int> discToUpdate = new Dictionary<Item, int>();  //shortfall item + adjustment qty

        RetrievalShortfallItemList = SaveRetrievalQty();

        if (retCon.CheckInvalidDisbursement(retrievalId))
        {
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
                Response.Redirect("RetrievalShortfall.aspx");
            }
            else //if there is no short fall go to collectionpoint
            {
                //update all actual qty to be same as requested qty if no shortfall
                retCon.UpdateAllActaulQty(retrievalId);

                Session["discrepancyList"] = null;
                Session["RetrievalShortfallItemList"] = null;
                Response.Redirect("CollectionPointUpdate.aspx");
            }
        }
        else
        {
            Response.Redirect("RetrievalListDetailErrorPage.aspx");
        }
    }
}