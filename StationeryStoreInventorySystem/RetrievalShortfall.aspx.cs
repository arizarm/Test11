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
                List<RetrievalShortfallItemSub> retrievalShortfallItemSubList = retCon.DisplayRetrievalShortfallSub(retrievalId, (r.FindControl("hdfItemCode") as HiddenField).Value);

                foreach (RetrievalShortfallItemSub i in retrievalShortfallItemSubList)
                {
                    retrievalShortfallItemSubListOfList.Add(i);
                }
                gvSub.DataSource = retrievalShortfallItemSubList;
                gvSub.DataBind();

                int availableQty = int.Parse((r.FindControl("availableQuantity") as Label).Text);

                foreach (GridViewRow subR in gvSub.Rows)
                {
                    int requestedQty = int.Parse((subR.FindControl("requestedQty") as Label).Text);
                    int temp;
                    if (availableQty > requestedQty)
                    {
                        temp = requestedQty;
                    }
                    else
                    {
                        temp = availableQty;
                    }

                    RangeValidator rv = subR.FindControl("RangeValidator1") as RangeValidator;
                    rv.MaximumValue = temp.ToString();
                    string actualQty;////////////////
                    actualQty = (subR.FindControl("txtActualQuantity") as TextBox).Text;

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

    protected void BtnGenerateDisbursementList_Click(object sender, EventArgs e)
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
                if (totalActualQty != Convert.ToInt32((row.FindControl("availableQuantity") as Label).Text))
                {
                    check = false;
                    (row.FindControl("totalActualQuantityValidator") as Label).Text = "Total quantity should be equal to available quantity";
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
