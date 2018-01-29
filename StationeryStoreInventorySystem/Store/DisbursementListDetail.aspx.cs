    using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DisbursementListDetail : System.Web.UI.Page
{

    DisbursementCotrol disbCon = new DisbursementCotrol();

    protected void Page_Load(object sender, EventArgs e)
    {
        //populate grid view with disbursement details
        if (!IsPostBack)
        {
            populateGridView();
        }
    }

    protected void populateGridView()
    {
        List<DisbursementDetailListItems> retrievedItem = new List<DisbursementDetailListItems>();

        int disbId = (int)Session["SelectedDisb"];

        //get and display disbursement data 
        DisbursementListItems disb = disbCon.DisbursementListItemsObj(disbId);
        lblDate.Text = disb.CollectionDate.ToString();
        lblTime.Text = disb.CollectionTime.ToString();
        lblDepartment.Text = disb.DepName.ToString();
        lblColPoint.Text = disb.CollectionPoint.ToString();

        retrievedItem = disbCon.gvDisbursementDetailPopulate(disbId);
        gvDisbDetail.DataSource = retrievedItem;
        gvDisbDetail.DataBind();
    }

    protected void btnAck_Click(object sender, EventArgs e)
    {
        int disbId = (int)Session["SelectedDisb"];

        List<DisbursementDetailListItems> retrievedItem = new List<DisbursementDetailListItems>();
        retrievedItem = disbCon.gvDisbursementDetailPopulate(disbId);

        List<RequestedItem> shortfallItem = new List<RequestedItem>();
        List<int> actualQtyList = new List<int>();
        List<string> disbRemark = new List<string>();
        Dictionary<Item, int> discToUpdate = new Dictionary<Item, int>();
        RequestedItem reqItem;
        bool check = true;

        foreach (GridViewRow r in gvDisbDetail.Rows)
        {
            //get disbursement remarks for each items
            string remark;
            try
            {
                remark = (r.FindControl("remarks") as Label).Text;
            }
            catch
            {
                remark = "";
            }

            //add remarks to list to save to database
            disbRemark.Add(remark);


            //get retrieved item to compare
            int index = r.RowIndex;
            int retrievedQty = retrievedItem[index].ActualQty;

            //get actual qty to verify
            int reqQty = Convert.ToInt32((r.FindControl("lblreqQty") as Label).Text);

            //get actual qty to verify
            int actualQty = Convert.ToInt32((r.FindControl("txtactualQty") as TextBox).Text);

            //make actual qty list to update database
            actualQtyList.Add(actualQty);

            //check if any shortfall
            if (actualQty < reqQty)
            {
                //make short fall item list
                string iCode = (r.FindControl("hdnflditemCode") as HiddenField).Value;
                string iDesc = (r.FindControl("lblitemDesc") as Label).Text;
                int shortfallQty = reqQty - actualQty;
                string uom = RequisitionControl.getUOM(iDesc); //////to check
                reqItem = new RequestedItem(iCode, iDesc, shortfallQty, uom);
                shortfallItem.Add(reqItem);

                //check if any discrepancy
                if (actualQty < retrievedQty)
                {
                    //make discrepancy item list
                    int disQty = actualQty - retrievedQty;
                    Item disItem = EFBroker_Item.GetItembyItemCode(iCode);
                    string finalQty = (disItem.BalanceQty + disQty).ToString();
                    discToUpdate.Add(disItem, disQty);
                }                
            }
            else if (actualQty > retrievedQty)
            {
                check = false;
                (r.FindControl("lblActualError") as Label).Text = "Actual cannot be more than retrieved quantity";
            }
        }
        if (check)
        {
            //check access code
            if (disbCon.checkAccessCode(disbId, txtAccessCode.Text))
            {
                //update Disbursement table (actual qty + status)
                disbCon.UpdateDisbursement(disbId, actualQtyList, disbRemark);

                //add discrepancy item to session 
                Session["discrepancyList"] = discToUpdate;

                //redirect to Regenerate Request page if any shortfall
                if (shortfallItem.Count != 0)
                {
                    Session["RegenerateDate"] = disbCon.getRegenrateDate(disbId); ////to check
                    Session["RegenerateDep"] = lblDepartment.Text;
                    Session["RegrenerateItems"] = shortfallItem;
                    Response.Redirect("~/Store/RegenerateRequest.aspx");
                }
                //redirect back to Disbursement List page if no shortfall
                else
                {
                    Response.Redirect("~/Store/DisbursementList.aspx");
                }
            }
            else
            {
                string message = "Incorrect Access Code!";
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + "');", true);
            }
        }
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        populateGridView();
    }
}