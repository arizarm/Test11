using System;
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
        Dictionary<Item, String> discrepancies = new Dictionary<Item, String>();
        Dictionary<KeyValuePair<Item, String>, String> fullDiscrepancies = new Dictionary<KeyValuePair<Item, String>, String>();
        if (!IsPostBack)
        {
            if (Session["discrepancyList"] != null)
            {
                discrepancies = (Dictionary<Item, String>)Session["discrepancyList"];
                foreach (KeyValuePair<Item, String> kvp in discrepancies)
                {
                    string adjustment = "";
                    int stock = (int)kvp.Key.BalanceQty;
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
            else
            {
                Response.Redirect("~/GenerateDiscrepancyV2.aspx");
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
                if (remarks.Length <= maxChars)
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
                    if(Session["empID"] != null)
                    {
                        int empID = (int)Session["empID"];
                        d.RequestedBy = empID;
                    }
                    else
                    {
                        Utility.logout();
                    }
                    d.AdjustmentQty = adj;
                    d.Remarks = remarks;
                    d.Date = DateTime.Now;
                    if(Session["monthly"] != null)
                    {
                        if ((bool)Session["monthly"] == true)
                        {
                            d.Status = "Monthly";
                        }
                        else
                        {
                            d.Status = "Pending";
                        }
                    }
                    else
                    {
                        d.Status = "Pending";
                    }
                    d.TotalDiscrepencyAmount = adj * averageUnitPrice;
                    if(d.TotalDiscrepencyAmount < 250)
                    {
                        d.ApprovedBy = GenerateDiscrepancyController.GetEmployeeByRole("Store Supervisor").EmpID;
                    }
                    else
                    {
                        d.ApprovedBy = GenerateDiscrepancyController.GetEmployeeByRole("Store Manager").EmpID;
                    }
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
            GenerateDiscrepancyController.SaveDiscrepancies(dList);

            Session["discrepancyList"] = null;
            Session["monthly"] = null;

            bool informSupervisor = false;
            bool informManager = false;
            foreach (Discrepency d in dList)
            {
                if (Math.Abs((decimal)d.TotalDiscrepencyAmount) < 250)
                {
                    informSupervisor = true;
                }
                else
                {
                    informManager = true;
                }
            }

            if (informSupervisor)
            {
                string supervisorEmail = GenerateDiscrepancyController.GetEmployeeByRole("Store Supervisor").Email;
                Utility.sendMail(supervisorEmail, "New Discrepancies Notification " + DateTime.Now.ToString(), "New item discrepancies have been submitted. Please log in to the system to review them. Thank you.");
            }
            if (informManager)
            {
                string managerEmail = GenerateDiscrepancyController.GetEmployeeByRole("Store Manager").Email;
                Utility.sendMail(managerEmail, "New Discrepancies Notification " + DateTime.Now.ToString(), "New item discrepancies (worth at least $250) have been submitted. Please log in to the system to review them. Thank you.");
            }
            
            Response.Redirect("https://www.google.com.sg");
        }
    }
}