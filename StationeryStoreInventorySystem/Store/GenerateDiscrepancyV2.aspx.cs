using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

public partial class GenerateDiscrepancyV2 : System.Web.UI.Page
{
    bool itemError = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["itemError"] = null;
            ShowAll();
        }
        else
        {
            if (Session["itemError"] != null)   //Retain the value of itemError when posting back
            {
                itemError = (bool)Session["itemError"];
            }
        }

        if (Session["discrepancyDisplay"] != null && Session["discrepancyList"] != null)
        {
            Dictionary<Item, String> iList2 = (Dictionary<Item, String>)Session["discrepancyDisplay"];
            gvDiscrepancyList.DataSource = iList2;
            gvDiscrepancyList.DataBind();
        }
        
        lblErrorFinalise.Text = "";
    }

    protected void BtnGenerateDiscrepancy_Click(object sender, EventArgs e)
    {      //Generate discrepancy button
        Dictionary<Item, int> discrepancyList = new Dictionary<Item, int>();
        Dictionary<Item, String> discrepancyDisplay = new Dictionary<Item, String>();
        List<String> missed = new List<String>();
        itemError = false;      //Whether there are any rows with errors in the whole page
        for (int i = 0; i < gvItemList.Rows.Count; i++)
        {
            GridViewRow row = gvItemList.Rows[i];
            bool ticked = (row.FindControl("cbxCorrect") as CheckBox).Checked;
            string txtActual = (row.FindControl("txtActual") as TextBox).Text;
            string itemCode = (row.FindControl("lblItemCodeItem") as Label).Text;
            bool error = false;       //Whether a row has an error

            if (!ticked)   //If a row is not checked
            {
                if (!(txtActual == "" || txtActual == null))    //Check whether actual quantity is blank
                {

                    row.BackColor = Color.Transparent;
                    ErrorClear();

                    int actualQuantity = 0;
                    if (Int32.TryParse(txtActual, out actualQuantity))
                    {
                        //Calculate the adjustment needed, then add to gvDiscrepancyList
                        string quantity = (row.FindControl("lblStockItem") as Label).Text;
                        int adj = actualQuantity - Int32.Parse(quantity);
                        Item item = EFBroker_Item.GetItembyItemCode(itemCode);
                        string actual = actualQuantity.ToString();
                        if (adj != 0 && actualQuantity >= 0)
                        {
                            discrepancyList.Add(item, adj);
                            discrepancyDisplay.Add(item, actual);
                        }
                        else
                        {
                            itemError = true;
                            error = true;
                        }
                    }
                    else   //If a row is not checked but has a non integer actual quantity
                    {
                        itemError = true;
                        error = true;
                    }
                }
                else    //If a row is neither checked nor has an actual quantity
                {
                    ErrorClear();
                    row.BackColor = Color.Transparent;
                    lblErrorMissed.Text = "Some items have not been checked yet. ";
                    itemError = true;
                    error = true;
                }
            }
            else
            {
                ErrorClear();
                row.BackColor = Color.Transparent;
                if (!(txtActual == "" || txtActual == null))   //if a row is checked and has an actual quantity
                {
                    itemError = true;
                    error = true;
                }
            }

            if (error)
            {
                row.BackColor = Color.Yellow;
                lblErrorBase.Text = "Please double-check the highlighted items.";
                missed.Add(itemCode);
            }
        }   //end of iterating through gridview rows

        string missedMessage = "Items with issues: ";
        if (missed.Count > 0)     //Create message listing items with issues
        {
            for (int i = 0; i < missed.Count; i++)
            {
                if (i > 0)
                {
                    missedMessage += ", ";
                }
                missedMessage += missed[i];
            }
            lblErrorMissedItems.Text = missedMessage;
        }
        Session["itemError"] = itemError;

        //Indicate monthly inventory check mode if the number of items in gvItemList
        //when generating discrepancies matches the number of active items in the database
        if (gvItemList.Rows.Count == EFBroker_Item.GetActiveItemList().Count)
        {
            Session["monthly"] = true;
        }
        else
        {
            Session["monthly"] = false;
        }

        Session["discrepancyList"] = discrepancyList;
        Session["discrepancyDisplay"] = discrepancyDisplay;

        gvDiscrepancyList.DataSource = discrepancyDisplay;
        gvDiscrepancyList.DataBind();
    }

    protected void BtnCheckAll_Click(object sender, EventArgs e)
    {        //Check all button, for testing purpose
        for (int i = 0; i < gvItemList.Rows.Count; i++)
        {
            GridViewRow row = gvItemList.Rows[i];
            (row.FindControl("cbxCorrect") as CheckBox).Checked = true;
        }
    }

    protected void BtnUncheckAll_Click(object sender, EventArgs e)
    {        //Uncheck all button, for testing purpose
        for (int i = 0; i < gvItemList.Rows.Count; i++)
        {
            GridViewRow row = gvItemList.Rows[i];
            (row.FindControl("cbxCorrect") as CheckBox).Checked = false;
        }

    }

    protected void BtnFinalise_Click(object sender, EventArgs e)
    {     //Finalise discrepancy button
        if (itemError == false)
        {
            Response.Redirect(LoginController.GenerateDiscrepancyAdhocV2URI);
        }
        else
        {
            lblErrorFinalise.Text = "Unable to finalise.";
        }
    }

    protected void BtnSearch_Click(object sender, EventArgs e)
    {        //Search button

        Dictionary<Item, String> searchResults = new Dictionary<Item, String>();
        string search = txtSearch.Text.ToLower();
        List<Item> iList = EFBroker_Item.SearchItemsByItemCodeOrDesc(search);

        foreach (Item i in iList)
        {
            //If a monthly inventory check discrepancy is not yet approved, the sum of only
            //discrepancies starting from the monthly one will be displayed
            Discrepency dMonthly = EFBroker_Discrepancy.GetPendingMonthlyDiscrepancyByItemCode(i.ItemCode);
            List<Discrepency> dList = EFBroker_Discrepancy.GetPendingDiscrepanciesByItemCode(i.ItemCode);
            if (dMonthly == null)
            {
                string adjStr = GetAdjustmentsString(dList);

                searchResults.Add(i, adjStr);
            }
            else
            {
                string adjStr = GetPartialAdjustmentsString(dList, dMonthly);
                searchResults.Add(i, adjStr);
            }
        }

        gvItemList.DataSource = searchResults;
        gvItemList.DataBind();

        foreach (GridViewRow row in gvItemList.Rows)
        {
            HyperLink link = row.FindControl("hlkDesc") as HyperLink;
            Label lbl = row.FindControl("lblItemCodeItem") as Label;
            string itemCode = lbl.Text;
            link.NavigateUrl = LoginController.AddItemDiscrepancyURI + "?itemCode=" + itemCode;
        }
    }

    protected void BtnDisplayAll_Click(object sender, EventArgs e)
    {       //Display all button
        ShowAll();
    }
    protected void BtnClearList_Click(object sender, EventArgs e)
    {       //Clear list button
        Dictionary<Item, int> emptyList = new Dictionary<Item, int>();
        Dictionary<Item, String> emptyDisplay = new Dictionary<Item, string>();
        Session["discrepancyList"] = emptyList;
        Session["discrepancyDisplay"] = emptyDisplay;
        gvDiscrepancyList.DataSource = emptyDisplay;
        gvDiscrepancyList.DataBind();
    }
    private void ErrorClear()
    {
        if (itemError == false)
        {
            lblErrorMissed.Text = "";
            lblErrorBase.Text = "";
            lblErrorMissedItems.Text = "";
        }
    }

    private string GetAdjustmentsString(List<Discrepency> dList)
    {
        int adj = 0;

        foreach (Discrepency d in dList)
        {
            adj += (int)d.AdjustmentQty;
        }

        string adjStr = "";

        if (adj > 0)
        {
            adjStr = "+" + adj.ToString();
        }
        else
        {
            adjStr = adj.ToString();
        }
        return adjStr;
    }

    private string GetPartialAdjustmentsString(List<Discrepency> dList, Discrepency dMonthly)
    {
        int adj = (int)dMonthly.AdjustmentQty;

        foreach (Discrepency d in dList)
        {
            if (d.DiscrepencyID > dMonthly.DiscrepencyID)
            {
                adj += (int)d.AdjustmentQty; ;
            }
        }

        string adjStr = "";

        if (adj > 0)
        {
            adjStr = "+" + adj.ToString();
        }
        else
        {
            adjStr = adj.ToString();
        }
        return adjStr;
    }

    private void ShowAll()
    {
        List<Item> iList = new List<Item>();
        iList = EFBroker_Item.GetActiveItemList();
        Dictionary<Item, String> displayItems = new Dictionary<Item, String>();

        foreach (Item i in iList)
        {
            //If a monthly inventory check discrepancy is not yet approved, the sum of only
            //discrepancies starting from the monthly one will be displayed
            Discrepency dMonthly = EFBroker_Discrepancy.GetPendingMonthlyDiscrepancyByItemCode(i.ItemCode);
            List<Discrepency> dList = EFBroker_Discrepancy.GetPendingDiscrepanciesByItemCode(i.ItemCode);
            if (dMonthly == null)
            {
                string adjStr = GetAdjustmentsString(dList);
                displayItems.Add(i, adjStr);
            }
            else
            {
                string adjStr = GetPartialAdjustmentsString(dList, dMonthly);
                displayItems.Add(i, adjStr);
            }
        }

        gvItemList.DataSource = displayItems;
        gvItemList.DataBind();

        foreach (GridViewRow row in gvItemList.Rows)
        {
            HyperLink link = row.FindControl("hlkDesc") as HyperLink;
            Label lbl = row.FindControl("lblItemCodeItem") as Label;
            string itemCode = lbl.Text;
            link.NavigateUrl = LoginController.AddItemDiscrepancyURI + "?itemCode=" + itemCode;
        }
    }

    
}

