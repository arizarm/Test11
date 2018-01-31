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
            GridView2.DataSource = iList2;
            GridView2.DataBind();
        }

        if (Request.UrlReferrer != null)
        {
            if (Request.UrlReferrer.ToString().Contains("AddItemDiscrepancy"))
            {    //Load full list after adding an item
                ShowAll();
            }
        }

        Label1.Text = "";
    }

    protected void Button1_Click(object sender, EventArgs e)
    {      //Generate discrepancy button
        GenerateDiscrepancyList();
    }

    protected void Button3_Click(object sender, EventArgs e)
    {        //Check all button
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            GridViewRow row = GridView1.Rows[i];
            (row.FindControl("CheckBox1") as CheckBox).Checked = true;
        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {     //Finalise discrepancy button
        if (itemError == false)
        {
            Response.Redirect(LoginController.GenerateDiscrepancyAdhocV2URI);
        }
        else
        {
            Label1.Text = "Unable to finalise.";
        }
    }

    protected void Button4_Click(object sender, EventArgs e)
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
            //}
        }

        GridView1.DataSource = searchResults;
        GridView1.DataBind();

        foreach (GridViewRow row in GridView1.Rows)
        {
            HyperLink link = row.FindControl("lnkItem") as HyperLink;
            Label lbl = row.FindControl("lblItemCode1") as Label;
            string itemCode = lbl.Text;
            link.NavigateUrl = LoginController.AddItemDiscrepancyURI + "?itemCode=" + itemCode;
        }
    }

    protected void Button5_Click(object sender, EventArgs e)
    {       //Display all button
        ShowAll();
    }
    protected void Button6_Click(object sender, EventArgs e)
    {       //Clear list button
        Dictionary<Item, int> emptyList = new Dictionary<Item, int>();
        Dictionary<Item, String> emptyDisplay = new Dictionary<Item, string>();
        Session["discrepancyList"] = emptyList;
        Session["discrepancyDisplay"] = emptyDisplay;
        GridView2.DataSource = emptyDisplay;
        GridView2.DataBind();
    }
    private void ErrorClear()
    {
        if (itemError == false)
        {
            Label5.Text = "";
            Label7.Text = "";
            Label8.Text = "";
        }
    }

    private void GenerateDiscrepancyList()
    {
        Dictionary<Item, int> discrepancyList = new Dictionary<Item, int>();
        Dictionary<Item, String> discrepancyDisplay = new Dictionary<Item, String>();
        List<String> missed = new List<String>();
        itemError = false;      //Whether there are any rows with errors in the whole page
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            GridViewRow row = GridView1.Rows[i];
            bool ticked = (row.FindControl("CheckBox1") as CheckBox).Checked;
            string txtActual = (row.FindControl("txtActual") as TextBox).Text;
            string itemCode = (row.FindControl("lblItemCode1") as Label).Text;
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
                        //Calculate the adjustment needed, then add to GridView2
                        string quantity = (row.FindControl("lblStock") as Label).Text;
                        int adj = actualQuantity - Int32.Parse(quantity);
                        Item item = EFBroker_Item.GetItembyItemCode(itemCode);
                        string actual = actualQuantity.ToString();
                        if (adj != 0)
                        {
                            discrepancyList.Add(item, adj);
                            discrepancyDisplay.Add(item, actual);
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
                    Label5.Text = "Some items have not been checked yet. ";
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
                Label7.Text = "Please double-check the highlighted items.";
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
            Label8.Text = missedMessage;
        }
        Session["itemError"] = itemError;

        //Indicate monthly inventory check mode if the number of items in GridView1
        //when generating discrepancies matches the number of active items in the database
        if (GridView1.Rows.Count == EFBroker_Item.GetActiveItemList().Count)
        {
            Session["monthly"] = true;
        }
        else
        {
            Session["monthly"] = false;
        }

        Session["discrepancyList"] = discrepancyList;
        Session["discrepancyDisplay"] = discrepancyDisplay;

        GridView2.DataSource = discrepancyDisplay;
        GridView2.DataBind();
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

        GridView1.DataSource = displayItems;
        GridView1.DataBind();

        foreach (GridViewRow row in GridView1.Rows)
        {
            HyperLink link = row.FindControl("lnkItem") as HyperLink;
            Label lbl = row.FindControl("lblItemCode1") as Label;
            string itemCode = lbl.Text;
            link.NavigateUrl = LoginController.AddItemDiscrepancyURI + "?itemCode=" + itemCode;
        }
    }
}

