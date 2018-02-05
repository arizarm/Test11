using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


//AUTHOR : CHOU MING SHENG
public partial class RetrievalDecision : System.Web.UI.Page
{
    RetrievalControl retCon = new RetrievalControl();

    int retrievalId;

    protected void Page_Load(object sender, EventArgs e)
    {
        //
        if (Session["RetrievalID"] == null)
        {
            Response.Redirect(LoginController.RequisitionListClerkURI);
        }
        else
        {
            //
            if (Request.UrlReferrer != null) // if previous page is not null
            {
                //
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
                        List<RetrievalShortfallItemSub> retrievalShortfallItemSubList = retCon.DisplayRetrievalShortfallSubGridView(retrievalId, (r.FindControl("hdfItemCode") as HiddenField).Value);

                        foreach (RetrievalShortfallItemSub i in retrievalShortfallItemSubList)
                        {
                            retrievalShortfallItemSubListOfList.Add(i);
                        }
                        gvSub.DataSource = retrievalShortfallItemSubList;
                        gvSub.DataBind();


                        // SET RangeValidator FOR MaximumValue
                        int retrievedQty = int.Parse((r.FindControl("lblRetrievedQuantity") as Label).Text);/// from lblAvailableQuantity to 
                        foreach (GridViewRow subR in gvSub.Rows)
                        {
                            int requestedQty = int.Parse((subR.FindControl("lblRequestedQty") as Label).Text);
                            int temp;
                            if (retrievedQty > requestedQty)
                            {
                                temp = requestedQty;
                            }
                            else
                            {
                                temp = retrievedQty;
                            }

                            RangeValidator rv = subR.FindControl("rng") as RangeValidator;
                            rv.MaximumValue = temp.ToString();
                        }
                        //

                        if (gvSub.Rows.Count == 1)
                        {
                            foreach (GridViewRow subR in gvSub.Rows)
                            {
                                (subR.FindControl("txtActualQuantity") as TextBox).Text = RetrievalShortfallItemList[j].Qty.ToString();
                                (subR.FindControl("txtActualQuantity") as TextBox).ReadOnly = true;
                            }
                        }
                        j++;
                    }

                    ViewState["retrievalShortfallItemSubListOfList"] = retrievalShortfallItemSubListOfList;
                }
            }
            else
            {
                Response.Redirect(LoginController.RequisitionListClerkURI);
            }

        }
    }

    protected void BtnFinalizeDisbursementList_Click(object sender, EventArgs e)
    {
        if (SaveActualQty())
        {
            //retCon.GenerateAccessCode(retrievalId);
            Response.Redirect("CollectionPointUpdate.aspx");
        }
    }

    protected bool SaveActualQty()
    {

        retrievalId = (int)Session["RetrievalID"];

        bool check = true;
        List<int> txtActualQuantityList = new List<int>();
        List<RetrievalShortfallItemSub> retrievalShortfallItemSubListOfList = new List<RetrievalShortfallItemSub>();
        retrievalShortfallItemSubListOfList = (List<RetrievalShortfallItemSub>)ViewState["retrievalShortfallItemSubListOfList"];
        int i = 0;

        foreach (GridViewRow row in gvMain.Rows)
        {
            //
            int totalActualQty = 0;
            //

            int actualQty;
            GridView gvSub = (GridView)row.FindControl("gvSub");
            foreach (GridViewRow subRow in gvSub.Rows)
            {
                actualQty = Convert.ToInt32((subRow.FindControl("txtActualQuantity") as TextBox).Text);
                retrievalShortfallItemSubListOfList[i].ActualQty = actualQty;
                i++;

                //
                totalActualQty += actualQty;
                //
            }

            if (gvSub.Rows.Count != 1)
            {
                if (totalActualQty != Convert.ToInt32((row.FindControl("lblRetrievedQuantity") as Label).Text))
                {
                    check = false;
                    (row.FindControl("lblTotalActualQuantityValidator") as Label).Text = "Total allocated quantity should be equal to retrieved quantity";
                }
            }
        }

        if (check)
        {
            retCon.SaveActualQtyBreakdownByDepartment(retrievalId, retrievalShortfallItemSubListOfList);
        }
        return check;
    }
}
