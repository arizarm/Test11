using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DisbursementListDetail : System.Web.UI.Page
{

    List<RequestedItem> shortfallItem = new List<RequestedItem>();
    static List<DisbursementDetailListItems> retrievedItem;
    Dictionary<Item, String> discrepanciesOutput = new Dictionary<Item, String>();

    protected void Page_Load(object sender, EventArgs e)
    {
        string disbId = Session["SelectedDisb"].ToString();

        //get and display disbursement data 
        DisbursementListItems disb = DisbursementCotrol.DisbursementListItemsObj(disbId);
        lblDate.Text = disb.CollectionDate.ToString();
        lblTime.Text = disb.CollectionTime.ToString();
        lblDepartment.Text = disb.DepName.ToString();
        lblColPoint.Text = disb.CollectionPoint.ToString();

        //populate grid view with disbursement details
        if (!IsPostBack)
        {
            retrievedItem = DisbursementCotrol.gvDisbursementDetailPopulate();
            gvDisbDetail.DataSource = retrievedItem;
            gvDisbDetail.DataBind();
        }
    }

    protected void btnAck_Click(object sender, EventArgs e)
    {
        string message;

        //Verify access code
        if (DisbursementCotrol.checkAccessCode(txtAccessCode.Text))
        {
            //message = "Disbursement Acknowledgement Successful!";
            //ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + "');", true);

            RequestedItem reqItem;

            List<int> actualQtyList = new List<int>();

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
                        discrepanciesOutput.Add(disItem, finalQty);
                    }
                    else if(actualQty > retrievedQty)
                    {
                        //display error
                    }
                }
            }

            //update Disbursement table (actual qty + status)
            DisbursementCotrol.UpdateDisbursementActualQty(actualQtyList);            
            DisbursementCotrol.UpdateDisbursementStatus();

            //Add disbursement transaction to Stockcard   
            DisbursementCotrol.AddStockCardTransaction(); 

            //add discrepancy item to session 
            Session["discrepancyList"] = discrepanciesOutput;

            //redirect to Regenerate Request page if any shortfall
            if (shortfallItem.Count != 0)
            {
                Session["RegenerateDate"] = DisbursementCotrol.getRegenrateDate();
                Session["RegenerateDep"] = lblDepartment.Text;
                Session["RegrenerateItems"] = shortfallItem;                
                Response.Redirect("~/RegenerateRequest.aspx");
            }
            //redirect back to Disbursement List page if no shortfall
            else
            {
                Response.Redirect("~/DisbursementList.aspx");
            }
        }
        else
        {
            message = "Incorrect Access Code!";
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + "');", true);
        }
    }
}    