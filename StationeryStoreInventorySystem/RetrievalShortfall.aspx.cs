using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RetrievalDecision : System.Web.UI.Page
{
    static List<RetrievalShortfallItemSub> retrievalShortfallItemSubList;
    static List<RetrievalShortfallItemSub> retrievalShortfallItemSubListOfList;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<RetrievalShortfallItem> RetrievalShortfallItemList = (List<RetrievalShortfallItem>)Session["RetrievalShortfallItemList"];
            //string retrievalId = (string)Session["RetrievalID"];
            gvMain.DataSource = RetrievalShortfallItemList;
            gvMain.DataBind();

            retrievalShortfallItemSubListOfList = new List<RetrievalShortfallItemSub>();

            int j = 0;
            foreach (GridViewRow r in gvMain.Rows)
            {
                GridView gvSub = (GridView)r.FindControl("gvSub");
                retrievalShortfallItemSubList = RetrievalControl.DisplayRetrievalShortfallSub((r.FindControl("hdfItemCode") as HiddenField).Value);

                //	Available Quantity	in main gv
                //foreach (RetrievalShortfallItem rs in RetrievalShortfallItemList)
                //{
                //    rs.Qty;
                //}
                //
                foreach (RetrievalShortfallItemSub i in retrievalShortfallItemSubList)
                {
                    retrievalShortfallItemSubListOfList.Add(i);
                }
                gvSub.DataSource = retrievalShortfallItemSubList;
                gvSub.DataBind();

                if (gvSub.Rows.Count == 1)
                {
                    //string qty = RetrievalShortfallItemList[j].Qty.ToString();
                    foreach (GridViewRow subR in gvSub.Rows)
                    {
                        (subR.FindControl("txtActualQuantity") as TextBox).Text = RetrievalShortfallItemList[j].Qty.ToString();
                    }
                }
                j++;
            }
        }
    }

    //protected void BtnResetAll_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect(Request.RawUrl);
    //}

    //protected void BtnSave_Click(object sender, EventArgs e)
    //{
    //    SaveActualQty();
    //}

    protected void BtnGenerateDisbursementList_Click(object sender, EventArgs e)
    {
        SaveActualQty();
        RetrievalControl.GenerateAccessCode();
        Response.Redirect("CollectionPointUpdate.aspx");
    }

    protected void SaveActualQty()
    {
        List<int> txtActualQuantityList = new List<int>();
        List<RetrievalShortfallItem> shortfallSubList = new List<RetrievalShortfallItem>();

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
        RetrievalControl.SaveActualQtyBreakdownByDepartment(retrievalShortfallItemSubListOfList);
    }


}