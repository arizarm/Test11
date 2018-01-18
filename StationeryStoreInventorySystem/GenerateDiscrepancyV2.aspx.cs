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
            List<InventoryItem> invItemList = new List<InventoryItem>();
            invItemList = GenerateDiscrepancyController.GetInventoryWithStock();

            GridView1.DataSource = invItemList;
            GridView1.DataBind();
            if (Session["itemError"] != null)
            {
                Session["itemError"] = null;
            }
        }
        else
        {
            if (Session["itemError"] != null)   //Retain the value of itemError when posting back
            {
                itemError = (bool)Session["itemError"];
            }
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
        Dictionary<InventoryItem, String> discrepancies = new Dictionary<InventoryItem, String>();
        GenerateDiscrepancyList();
        if (itemError == false)
        {
            //foreach (GridViewRow row in GridView2.Rows)
            for (int i = 0; i < GridView2.Rows.Count; i++)
            {
                GridViewRow row = GridView2.Rows[i];
                string itemCode = (row.FindControl("lblItemCode2") as Label).Text;
                string stock = (row.FindControl("lblStock") as Label).Text;
                string adj = (row.FindControl("lblAdj") as Label).Text;
                Item item = GenerateDiscrepancyController.GetItemByItemCode(itemCode);
                InventoryItem invItem = new InventoryItem(item, stock);

                discrepancies.Add(invItem, adj);
            }
            Session["discrepancyList"] = discrepancies;
            Response.Redirect("~/GenerateDiscrepancyAdhocV2.aspx");
        }
        else
        {
            Label1.Text = "Unable to finalise.";
        }
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
        Dictionary<InventoryItem, String> iList2 = new Dictionary<InventoryItem, String>();
        List<String> missed = new List<String>();
        itemError = false;
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            GridViewRow row = GridView1.Rows[i];
            bool ticked = (row.FindControl("CheckBox1") as CheckBox).Checked;
            string txtActual = (row.FindControl("txtActual") as TextBox).Text;
            string itemCode = (row.FindControl("lblItemCode1") as Label).Text;
            bool error = false;
            string mode = "";

            switch (RadioButtonList1.SelectedIndex)
            {
                case 0:
                    mode = "Monthly";
                    break;
                case 1:
                    mode = "Adhoc";
                    break;
            }

            bool monthlyCheckPass = true;    //Functions as ticked, but only if
                                             //the page mode is on Monthly
            if (mode == "Monthly")           //If adhoc, ticked is ignored
            {
                if (ticked)
                {
                    monthlyCheckPass = false;
                }
            }

            if (monthlyCheckPass)   //If a row is not checked
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
                        string quantity = holder.Text;
                        int adj = actualQuantity - Int32.Parse(quantity);
                        Item item = GenerateDiscrepancyController.GetItemByItemCode(itemCode);
                        InventoryItem invItem = new InventoryItem(item, quantity);
                        string adjustment = actualQuantity.ToString();
                        if (adj != 0)
                        {
                            //if (adj > 0)
                            //{
                            //    adjustment = "+" + adj.ToString();
                            //}
                            //else
                            //{
                            //    adjustment = adj.ToString();
                            //}
                            iList2.Add(invItem, adjustment);
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
                    if (mode == "Monthly")   // This code only applies to monthly
                    {
                        Label5.Text = "Some items have not been checked yet. ";
                        itemError = true;
                        error = true;
                    }

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
        GridView2.DataSource = iList2;
        GridView2.DataBind();
    }
}