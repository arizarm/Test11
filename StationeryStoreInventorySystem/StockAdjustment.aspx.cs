﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StockAdjustment : System.Web.UI.Page
{
    List<Discrepency> monthly;
    protected void Page_Load(object sender, EventArgs e)
    {
        monthly = GenerateDiscrepancyController.GetAllPendingMonthlyDiscrepancies();
        List<Discrepency> pending = GenerateDiscrepancyController.GetAllPendingDiscrepancies();

        Dictionary<Discrepency, Item> monthlySource = new Dictionary<Discrepency, Item>();
        Dictionary<Discrepency, Item> pendingSource = new Dictionary<Discrepency, Item>();

        foreach (Discrepency d in monthly)
        {
            Item i = GenerateDiscrepancyController.GetItemByItemCode(d.ItemCode);
            decimal discrepancyAmount = Math.Abs((decimal)d.TotalDiscrepencyAmount);
            if (Session["empRole"] != null)
            {
                string role = (string)Session["empRole"];
                if (Session["empRole"].ToString() == "Store Manager" && discrepancyAmount >= 250)
                {
                    monthlySource.Add(d, i);
                }
                else if (Session["empRole"].ToString() == "Store Supervisor" && discrepancyAmount < 250)
                {
                    monthlySource.Add(d, i);
                }
            }
        }
        GridView1.DataSource = monthlySource;
        GridView1.DataBind();

        foreach (Discrepency d in pending)
        {
            Item i = GenerateDiscrepancyController.GetItemByItemCode(d.ItemCode);
            decimal discrepancyAmount = Math.Abs((decimal)d.TotalDiscrepencyAmount);
            if (Session["empRole"] != null)
            {
                if (Session["empRole"].ToString() == "Store Manager" && discrepancyAmount >= 250)
                {
                    pendingSource.Add(d, i);
                }
                else if (Session["empRole"].ToString() == "Store Supervisor" && discrepancyAmount < 250)
                {
                    pendingSource.Add(d, i);
                }
            }
            else
            {
                Utility.logout();
            }
        }
        GridView2.DataSource = pendingSource;
        GridView2.DataBind();
    }

    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            KeyValuePair<Discrepency, Item> kvp = (KeyValuePair<Discrepency, Item>)e.Row.DataItem;

            foreach (Discrepency d in monthly)
            {
                if (kvp.Key.ItemCode == d.ItemCode && kvp.Key.DiscrepencyID < d.DiscrepencyID)
                {
                    RadioButtonList rbl = e.Row.FindControl("RadioButtonList1") as RadioButtonList;
                    rbl.Enabled = false;
                }
            }
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        GridView gdv = GridView1;
        ProcessApprovalAndRejections(gdv);
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        GridView gdv = GridView2;
        ProcessApprovalAndRejections(gdv);
    }

    private void ProcessApprovalAndRejections(GridView gdv)
    {
        Dictionary<KeyValuePair<Discrepency, Item>, bool> summary = new Dictionary<KeyValuePair<Discrepency, Item>, bool>();
        foreach (GridViewRow row in gdv.Rows)
        {
            RadioButtonList rbl = row.FindControl("RadioButtonList1") as RadioButtonList;

            if(rbl.SelectedIndex == 0 || rbl.SelectedIndex == 1)
            {
                string itemCode = (row.FindControl("lblItemCode") as Label).Text;
                string discID = (row.FindControl("lblDiscID") as Label).Text;
                //Item i = EFBroker_Item.GetItemByItemCode(itemCode);
                //Discrepency d = EFBroker_Discrepe
                KeyValuePair<Discrepency, Item> kvp = new KeyValuePair<Discrepency, Item>();
                if (rbl.SelectedIndex == 0)
                {
                    summary.Add(kvp, true);
                }
                else if (rbl.SelectedIndex == 1)
                {
                    summary.Add(kvp, false);
                }
            }
            
        }
        Session["discrepancySummary"] = summary;
        Response.Redirect("~/StockAdjustmentSummary.aspx");
    }
}

