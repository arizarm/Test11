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
        if((bool)Session["monthly"] == true)
        {
            Session["discrepancyList"] = new Dictionary<Item, String>();
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

        //bool alreadyInDiscrepancyList = false;
        //foreach (KeyValuePair<Item, String> kvp in discrepancies)
        //{
        //    if (kvp.Key.ItemCode == lblItemCode.Text)
        //    {
        //        alreadyInDiscrepancyList = true;
        //    }
        //}

        Item item = EFBroker_Item.GetItembyItemCode(lblItemCode.Text);

        int adjustment = 0;
        if (Int32.TryParse(txtAdj.Text, out adjustment))
        {
            if (adjustment != 0)
            {
                //int actualQuantity = (int)item.BalanceQty + adjustment;

                //if (alreadyInDiscrepancyList)
                //{
                string adjStr = "";

                if (adjustment > 0)
                {
                    adjStr = "+" + adjustment.ToString();
                }
                else
                {
                    adjStr = adjustment.ToString();
                }
                discrepancies[item] = adjStr.ToString();
                //}
                //else
                //{
                //    discrepancies.Add(item, actualQuantity.ToString());
                //}
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