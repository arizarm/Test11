using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AddItemDiscrepancy : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["monthly"] != null)
        {
            if ((bool)Session["monthly"] == true)
            {
                Session["discrepancyList"] = new Dictionary<Item, int>();
                Session["discrepancyDisplay"] = new Dictionary<Item, String>();
            }
        }

        Session["monthly"] = false;
        string itemCode = Request.QueryString["itemCode"];
        if (!ValidatorUtil.isEmpty(itemCode))
        {
            Item item = EFBroker_Item.GetItembyItemCode(itemCode);

            if (item != null)
            {
                lblItemCode.Text = item.ItemCode;
                lblItemName.Text = item.Description;
                lblUom.Text = item.UnitOfMeasure;
                lblStock.Text = item.BalanceQty.ToString();
            }
            else
            {
                Response.Redirect(LoginController.GenerateDiscrepancyV2URI);
            }
        }
        else
        {
            Response.Redirect(LoginController.GenerateDiscrepancyV2URI);
        }
        lblErrorMsg.Text = "";      //Resetting error text field
    }

    protected void btnAddItem_Click(object sender, EventArgs e)
    {
        Dictionary<Item, int> discrepancies = null;
        Dictionary<Item, String> discrepancyDisplay = null;

        if (Session["discrepancyList"] == null)
        {
            discrepancies = new Dictionary<Item, int>();
        }
        else
        {
            discrepancies = (Dictionary<Item, int>)Session["discrepancyList"];
        }

        if (Session["discrepancyDisplay"] == null)
        {
            discrepancyDisplay = new Dictionary<Item, String>();
        }
        else
        {
            discrepancyDisplay = (Dictionary<Item, String>)Session["discrepancyDisplay"];
        }

        bool alreadyInDiscrepancyList = false;
        KeyValuePair<Item, int> toBeReplaced = new KeyValuePair<Item, int>();
        foreach (KeyValuePair<Item, int> kvp in discrepancies)
        {
            if (kvp.Key.ItemCode == lblItemCode.Text)
            {
                alreadyInDiscrepancyList = true;
                toBeReplaced = kvp;
                break;
            }
        }

        Item item = EFBroker_Item.GetItembyItemCode(lblItemCode.Text);

        int adjustment = 0;
        if (Int32.TryParse(txtAdj.Text, out adjustment))
        {
            if (adjustment != 0)
            {
                if (item.BalanceQty + adjustment >= 0)
                {
                    int actualQuantity = (int)item.BalanceQty + adjustment;
                    string adjStr = "";

                    if (adjustment > 0)
                    {
                        adjStr = "+" + adjustment.ToString();
                    }
                    else
                    {
                        adjStr = adjustment.ToString();
                    }

                    if (alreadyInDiscrepancyList)
                    {
                        //discrepancies.Remove(toBeReplaced.Key);
                        //discrepancyDisplay.Remove(toBeReplaced.Key);
                        discrepancies[toBeReplaced.Key] = adjustment;
                        discrepancyDisplay[toBeReplaced.Key] = adjStr;
                    }
                    else
                    {
                        discrepancies.Add(item, adjustment);
                        discrepancyDisplay.Add(item, adjStr);
                    }

                    Session["discrepancyList"] = discrepancies;
                    Session["discrepancyDisplay"] = discrepancyDisplay;
                    Response.Redirect(LoginController.GenerateDiscrepancyV2URI);
                }
                else
                {
                    lblErrorMsg.Text = "Adjustment cannot reduce stock to less than 0";
                }
            }
            else
            {
                lblErrorMsg.Text = "Please enter a non-zero integer for adjustment amount (either positive or negative)";
            }
        }
        else
        {
            lblErrorMsg.Text = "Please enter an integer for adjustment amount (either positive or negative)";
        }
    }
}