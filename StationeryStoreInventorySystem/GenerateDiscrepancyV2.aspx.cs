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
            //Dictionary<InventoryItem, String> invItems = new Dictionary<InventoryItem, String>();

            //foreach (InventoryItem ii in invItemList)
            //{
            //    List<Discrepency> dList = GenerateDiscrepancyController.GetPendingDiscrepanciesByItemCode(ii.I.ItemCode);
            //    int adj = 0;

            //    foreach(Discrepency d in dList)
            //    {
            //        adj += (int) d.AdjustmentQty;
            //    }

            //    string adjStr = "";

            //    if (adj > 0)
            //    {
            //        adjStr = "+" + adj.ToString();
            //    }
            //    else
            //    {
            //        adjStr = adj.ToString();
            //    }

            //    string stockWithAdj = ii.Stock + " (" + adjStr + ")";

            //    invItems.Add(ii, stockWithAdj);
            //}


            Session["itemError"] = null;
        }
        else
        {
            if (Session["itemError"] != null)   //Retain the value of itemError when posting back
            {
                itemError = (bool)Session["itemError"];
            }
        }

        if (Session["discrepancyList"] != null)
        {
            Dictionary<Item, String> iList2 = (Dictionary<Item, String>)Session["discrepancyList"];
            GridView2.DataSource = iList2;
            GridView2.DataBind();
        }

        if (Request.UrlReferrer.ToString().Contains("AddItemDiscrepancy"))
        {    //Load full list after adding an item
            ShowAll();
        }

        Label1.Text = "";
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        GenerateDiscrepancyList();
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            GridViewRow row = GridView1.Rows[i];
            (row.FindControl("CheckBox1") as CheckBox).Checked = true;
        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {

        //GenerateDiscrepancyList();
        if (itemError == false)
        {
            //foreach (GridViewRow row in GridView2.Rows)
            //for (int i = 0; i < GridView2.Rows.Count; i++)
            //{
            //    GridViewRow row = GridView2.Rows[i];
            //    string itemCode = (row.FindControl("lblItemCode2") as Label).Text;
            //    //string stock = (row.FindControl("lblStock") as Label).Text;
            //    string actual = (row.FindControl("lblActual") as Label).Text;
            //    Item item = GenerateDiscrepancyController.GetItemByItemCode(itemCode);
            //    //InventoryItem invItem = new InventoryItem(item, stock);

            //    discrepancies.Add(item, actual);
            //}
            //Session["discrepancyList"] = discrepancies;

            if ((bool)Session["monthly"] == false)
            {
                Dictionary<Item, String> discrepancies = (Dictionary<Item, String>)Session["discrepancyList"];
                Dictionary<Item, String> discrepanciesOutput = new Dictionary<Item, String>();

                foreach (KeyValuePair<Item, String> kvp in discrepancies)
                {
                    string actual = "";
                    if (kvp.Value[0] == '+')
                    {
                        string adj = kvp.Value.Substring(1);
                        actual = (kvp.Key.BalanceQty + Int32.Parse(adj)).ToString();
                    }
                    else
                    {
                        string adj = kvp.Value;
                        actual = (kvp.Key.BalanceQty + Int32.Parse(adj)).ToString();
                    }
                    discrepanciesOutput.Add(kvp.Key, actual);
                }
                Session["discrepancyList"] = discrepanciesOutput;
            }
            Response.Redirect("~/GenerateDiscrepancyAdhocV2.aspx");
        }
        else
        {
            Label1.Text = "Unable to finalise.";
        }
    }

    protected void Button4_Click(object sender, EventArgs e)
    {
        List<Item> iList = new List<Item>();
        iList = GenerateDiscrepancyController.GetAllItems();

        Dictionary<Item, String> searchResults = new Dictionary<Item, String>();
        string search = txtSearch.Text.ToLower();

        foreach (Item i in iList)
        {
            if (i.ItemCode.ToLower().Contains(search) || i.Description.ToLower().Contains(search))
            {
                //If a monthly inventory check discrepancy is not yet approved, the sum of only
                //discrepancies starting from the monthly one will be displayed
                Discrepency dMonthly = GenerateDiscrepancyController.GetPendingMonthlyDiscrepancyByItemCode(i.ItemCode);
                List<Discrepency> dList = GenerateDiscrepancyController.GetPendingDiscrepanciesByItemCode(i.ItemCode);
                if (dMonthly == null)
                {

                    string adjStr = GetAdjustmentString(dList);

                    searchResults.Add(i, adjStr);
                }
                else
                {
                    string adjStr = GetPartialAdjustmentString(dList, dMonthly);
                    searchResults.Add(i, adjStr);
                }
            }
        }

        GridView1.DataSource = searchResults;
        GridView1.DataBind();
    }

    protected void Button5_Click(object sender, EventArgs e)
    {
        ShowAll();
    }
    protected void Button6_Click(object sender, EventArgs e)
    {
        Dictionary<Item, String> empty = new Dictionary<Item, String>();
        Session["discrepancyList"] = empty;
        GridView2.DataSource = empty;
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
        Dictionary<Item, String> iList2 = new Dictionary<Item, String>();
        List<String> missed = new List<String>();
        itemError = false;
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            GridViewRow row = GridView1.Rows[i];
            bool ticked = (row.FindControl("CheckBox1") as CheckBox).Checked;
            string txtActual = (row.FindControl("txtActual") as TextBox).Text;
            string itemCode = (row.FindControl("lblItemCode1") as Label).Text;
            bool error = false;
            //string mode = "Monthly";

            //switch (RadioButtonList1.SelectedIndex)
            //{
            //    case 0:
            //        mode = "Monthly";
            //        break;
            //    case 1:
            //        mode = "Adhoc";
            //        break;
            //}

            //bool monthlyCheckPass = true;    //Functions as ticked, but only if
            //                                 //the page mode is on Monthly
            //if (mode == "Monthly")           //If adhoc, ticked is ignored
            //{
            //    if (ticked)
            //    {
            //        monthlyCheckPass = false;
            //    }
            //}

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
                        Label holder = row.FindControl("lblStock") as Label;
                        string[] holderContents = holder.Text.Split(' ');
                        string quantity = holderContents[0];
                        int adj = actualQuantity - Int32.Parse(quantity);
                        Item item = GenerateDiscrepancyController.GetItemByItemCode(itemCode);
                        //InventoryItem invItem = new InventoryItem(item, quantity);
                        string actual = actualQuantity.ToString();
                        if (adj != 0)
                        {
                            iList2.Add(item, actual);
                        }
                    }
                    else
                    {
                        itemError = true;
                        error = true;
                    }
                }
                else    //If a row is neither checked nor has an actual quantity
                {
                    ErrorClear();
                    row.BackColor = Color.Transparent;
                    //if (mode == "Monthly")   // This code only applies to monthly
                    //{
                    Label5.Text = "Some items have not been checked yet. ";
                    itemError = true;
                    error = true;
                    //}

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

        }

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

        if (GridView1.Rows.Count == GenerateDiscrepancyController.GetAllItems().Count)
        {
            Session["monthly"] = true;
        }
        else
        {
            Session["monthly"] = false;
        }

        Session["discrepancyList"] = iList2;

        GridView2.DataSource = iList2;
        GridView2.DataBind();
    }

    private string GetAdjustmentString(List<Discrepency> dList)
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

    private string GetPartialAdjustmentString(List<Discrepency> dList, Discrepency dMonthly)
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
        iList = GenerateDiscrepancyController.GetAllItems();
        Dictionary<Item, String> displayItems = new Dictionary<Item, String>();

        foreach (Item i in iList)
        {
            //If a monthly inventory check discrepancy is not yet approved, the sum of only
            //discrepancies starting from the monthly one will be displayed
            Discrepency dMonthly = GenerateDiscrepancyController.GetPendingMonthlyDiscrepancyByItemCode(i.ItemCode);
            List<Discrepency> dList = GenerateDiscrepancyController.GetPendingDiscrepanciesByItemCode(i.ItemCode);
            if (dMonthly == null)
            {
                string adjStr = GetAdjustmentString(dList);
                displayItems.Add(i, adjStr);
            }
            else
            {
                string adjStr = GetPartialAdjustmentString(dList, dMonthly);
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
            link.NavigateUrl = "~/AddItemDiscrepancy.aspx?itemCode=" + itemCode;
        }
    }
}

