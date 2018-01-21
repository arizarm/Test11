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
        Session["monthly"] = false;
        string itemCode = Request.QueryString["itemCode"];
        if (!ValidatorUtil.isEmpty(itemCode))
        {
            Item item = GenerateDiscrepancyController.GetItemByItemCode(itemCode);

            if (item != null)
            {
                lblItemCode.Text = item.ItemCode;
                lblItemName.Text = item.Description;
                lblUom.Text = item.UnitOfMeasure;
                lblStock.Text = item.BalanceQty.ToString();
            }
            else
            {
                Response.Redirect("~/GenerateDiscrepancyV2.aspx");
            }
        }
        else
        {
            Response.Redirect("~/GenerateDiscrepancyV2.aspx");
        }
        Label1.Text = "";      //Resetting error text field
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Dictionary<Item, String> discrepancies = null;

        if (Session["discrepancyList"] == null)
        {
            discrepancies = new Dictionary<Item, String>();
        }
        else
        {
            discrepancies = (Dictionary<Item, String>)Session["discrepancyList"];
        }

        bool alreadyInDiscrepancyList = false;
        foreach (KeyValuePair<Item, String> kvp in discrepancies)
        {
            if (kvp.Key.ItemCode == lblItemCode.Text)
            {
                alreadyInDiscrepancyList = true;
            }
        }

        Item item = GenerateDiscrepancyController.GetItemByItemCode(lblItemCode.Text);

        int adjustment = 0;
        if (Int32.TryParse(txtAdj.Text, out adjustment))
        {
            if (adjustment != 0)
            {
                int actualQuantity = (int)item.BalanceQty + adjustment;

                if (alreadyInDiscrepancyList)
                {
                    discrepancies[item] = actualQuantity.ToString();
                }
                else
                {
                    discrepancies.Add(item, actualQuantity.ToString());
                }
                Session["discrepancyList"] = discrepancies;
                Response.Redirect("~/GenerateDiscrepancyV2.aspx");
            }
            else
            {
                Label1.Text = "Please enter a non-zero integer for adjustment amount (either positive or negative)";
            }
        }
        else
        {
            Label1.Text = "Please enter an integer for adjustment amount (either positive or negative)";
        }
    }
}