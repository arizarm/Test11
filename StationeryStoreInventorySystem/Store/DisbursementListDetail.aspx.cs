using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DisbursementListDetail : System.Web.UI.Page
{

    DisbursementCotrol disbCon = new DisbursementCotrol();

    List<RequestedItem> shortfallItem = new List<RequestedItem>();
    static List<DisbursementDetailListItems> retrievedItem;
    Dictionary<Item, int> discToUpdate = new Dictionary<Item, int>();

    protected void Page_Load(object sender, EventArgs e)
    {   
        //populate grid view with disbursement details
        if (!IsPostBack)
        {
            int disbId = (int) Session["SelectedDisb"];
           
            //get and display disbursement data 
            DisbursementListItems disb = disbCon.DisbursementListItemsObj(disbId);
            lblDate.Text = disb.CollectionDate.ToString();
            lblTime.Text = disb.CollectionTime.ToString();
            lblDepartment.Text = disb.DepName.ToString();
            lblColPoint.Text = disb.CollectionPoint.ToString();

            retrievedItem = disbCon.gvDisbursementDetailPopulate();
            gvDisbDetail.DataSource = retrievedItem;
            gvDisbDetail.DataBind();
        }
    }

    protected void btnAck_Click(object sender, EventArgs e)
    {
        string message;

        int disbId = (int)Session["SelectedDisb"];

        //Verify access code
        if (disbCon.checkAccessCode(disbId, txtAccessCode.Text))
        {
            //message = "Disbursement Acknowledgement Successful!";
            //ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + "');", true);

            RequestedItem reqItem;

            List<int> actualQtyList = new List<int>();

            bool check = true;

            foreach (GridViewRow r in gvDisbDetail.Rows)
            {           
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
                    string uom = RequisitionControl.getUOM(iDesc);
                    reqItem = new RequestedItem(iCode, iDesc, shortfallQty, uom);
                    shortfallItem.Add(reqItem);

                    //get retrieved item to compare
                    int index = r.RowIndex;
                    int retrievedQty = retrievedItem[index].ActualQty;

                    //check if any discrepancy
                    if (actualQty< retrievedQty)
                    {
                        //make discrepancy item list
                        int disQty = actualQty - retrievedQty;
                        Item disItem = GenerateDiscrepancyController.GetItemByItemCode(iCode);
                        string finalQty = (disItem.BalanceQty + disQty).ToString();
                        discToUpdate.Add(disItem, disQty);
                    }
                    else if(actualQty > retrievedQty)
                    {
                        check = false;
                        (r.FindControl("lblActualError") as Label).Text = "Actual cannot be more than retrieved quantity";                        
                    }
                }
            }

            if(check)
            {
                //update Disbursement table (actual qty + status)
                disbCon.UpdateDisbursementActualQty(actualQtyList);
                disbCon.UpdateDisbursementStatus();

                //Add disbursement transaction to Stockcard   
                disbCon.AddStockCardTransaction();

                //add discrepancy item to session 
                Session["discrepancyList"] = discToUpdate;

                //redirect to Regenerate Request page if any shortfall
                if (shortfallItem.Count != 0)
                {
                    Session["RegenerateDate"] = disbCon.getRegenrateDate();
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
        }
        else
        {
            message = "Incorrect Access Code!";
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + "');", true);
        }
    }
}    