﻿    using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//AUTHOR : KHIN MO MO ZIN
public partial class DisbursementListDetail : System.Web.UI.Page
{

    DisbursementCotrol disbCon = new DisbursementCotrol();

    protected void Page_Load(object sender, EventArgs e)
    {
        if(Session["SelectedDisb"] == null)
        {
            Response.Redirect(LoginController.DisbursementListURI);
        }
        else
        {//populate grid view with disbursement details
            if (!IsPostBack)
            {
                populateGridView();
            }
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

        retrievedItem = disbCon.GvDisbursementDetailPopulate(disbId);
        gvDisbDetail.DataSource = retrievedItem;
        gvDisbDetail.DataBind();
    }

    protected void btnAck_Click(object sender, EventArgs e)
    {
        int disbId = (int)Session["SelectedDisb"];

        List<DisbursementDetailListItems> retrievedItem = new List<DisbursementDetailListItems>();
        retrievedItem = disbCon.GvDisbursementDetailPopulate(disbId);

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
                remark = (r.FindControl("txtremarks") as TextBox).Text;
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
                string uom = EFBroker_Item.GetUnitbyItemCode(iCode); 
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
            if (disbCon.CheckAccessCode(disbId, txtAccessCode.Text))
            {
                //update Disbursement table (actual qty + status)
                disbCon.UpdateDisbursement(disbId, actualQtyList, disbRemark);

                //redirect to Regenerate Request page if any shortfall
                if (shortfallItem.Count != 0)
                {
                    Session["discrepancyList"] = discToUpdate;
                    Session["RegenerateDate"] = disbCon.GetRegenrateDate(disbId);
                    Session["RegenerateDep"] = lblDepartment.Text;
                    Session["RequestedByName"] = EFBroker_DeptEmployee.GetDeptRepByDeptCode(lblDepartment.Text);
                    Session["RegenerateItems"] = shortfallItem;
                   
                    Response.Redirect(LoginController.RegenerateRequestURI);
                }
                //redirect back to Disbursement List page if no shortfall
                else
                {
                    Session["SelectedDisb"] = null;
                    Session["disbItemsList"] = null;
                    Response.Redirect(LoginController.DisbursementListURI);
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