﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

public partial class GenerateDiscrepancyAdhocV2 : System.Web.UI.Page
{
    int maxChars = 100;
    protected void Page_Load(object sender, EventArgs e)
    {
        Dictionary<InventoryItem, String> discrepancies = new Dictionary<InventoryItem, String>();
        Dictionary<KeyValuePair<InventoryItem, String>, String> fullDiscrepancies = new Dictionary<KeyValuePair<InventoryItem, String>, String>();
        if (!IsPostBack)
        {
            if (Session["discrepancyList"] != null)
            {
                discrepancies = (Dictionary<InventoryItem, String>)Session["discrepancyList"];
                foreach (KeyValuePair<InventoryItem, String> kvp in discrepancies)
                {
                    string adjustment = "";
                    int stock = Int32.Parse(kvp.Key.Stock);
                    int actualQuantity = Int32.Parse(kvp.Value);
                    int adj = actualQuantity - stock;

                    if (adj > 0)
                    {
                        adjustment = "+" + adj.ToString();
                    }
                    else
                    {
                        adjustment = adj.ToString();
                    }

                    fullDiscrepancies.Add(kvp, adjustment);
                }
            }
            GridView1.DataSource = fullDiscrepancies;
            GridView1.DataBind();
        }
        Label1.Text = "";
        Label5.Text = "";
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        List<Discrepency> dList = new List<Discrepency>();
        bool complete = true;
        foreach (GridViewRow row in GridView1.Rows)
        {
            row.BackColor = Color.Transparent;
            string itemCode = (row.FindControl("lblItemCode") as Label).Text;
            string remarks = (row.FindControl("txtRemarks") as TextBox).Text;
            string stock = (row.FindControl("lblStock") as Label).Text;
            string actual = (row.FindControl("lblActual") as Label).Text;
            int adj = Int32.Parse(actual) - Int32.Parse(stock);
            if (!ValidatorUtil.isEmpty(remarks))
            {
                if (remarks.Length < maxChars)
                {
                    List<PriceList> itemPrices = GenerateDiscrepancyController.GetPricesByItemCode(itemCode);
                    decimal totalPrice = 0;

                    foreach (PriceList pl in itemPrices)
                    {
                        totalPrice += (decimal)pl.Price;
                    }

                    decimal averageUnitPrice = totalPrice / itemPrices.Count;

                    Discrepency d = new Discrepency();
                    d.ItemCode = itemCode;
                    //d.RequestedBy = (int)Session["empID"];
                    d.RequestedBy = 1005;     //note this
                    d.AdjustmentQty = adj;
                    d.Remarks = remarks;
                    d.Date = DateTime.Now;
                    d.Status = "Pending";
                    d.TotalDiscrepencyAmount = adj * averageUnitPrice;
                    dList.Add(d);
                }
                else
                {
                    Label1.Text = "Make sure all remarks are under 100 characters long";
                    row.BackColor = Color.Yellow;
                    complete = false;
                    break;
                }
            }
            else
            {
                Label5.Text = "Please state the cause of discrepancies for all items in Remarks";
                complete = false;
                break;
            }

        }

        if (complete)
        {
            GenerateDiscrepancyController.SubmitDiscrepancies(dList);

            bool informSupervisor = false;
            bool informManager = false;
            foreach(Discrepency d in dList)
            {
                if(Math.Abs((decimal)d.TotalDiscrepencyAmount) < 250)
                {
                    informSupervisor = true;
                }
                else
                {
                    informManager = true;
                }
            }

            //if (informSupervisor)
            //{
            //    string supervisorEmail = GenerateDiscrepancyController.GetEmployeeByRole("Store Supervisor").Email;
            //    Utility.sendMail(supervisorEmail, "New Discrepancies Notification", "New item discrepancies have been submitted. Please log in to the system to review them. Thank you.");
            //}
            //if (informManager)
            //{
            //    string managerEmail = GenerateDiscrepancyController.GetEmployeeByRole("Store Manager").Email;
            //    Utility.sendMail(managerEmail, "New Discrepancies Notification", "New item discrepancies (worth at least $250) have been submitted. Please log in to the system to review them. Thank you.");
            //}
            Utility.sendMail("etedwin123@gmail.com", "New Discrepancies Notification " + DateTime.Now.ToString(), "New item discrepancies have been submitted. Please log in to the system to review them. Thank you.");
            Response.Redirect("https://www.google.com.sg");
        }
    }
}