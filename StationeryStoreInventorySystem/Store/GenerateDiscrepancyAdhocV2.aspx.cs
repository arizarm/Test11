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
        Dictionary<Item, int> discrepancies = new Dictionary<Item, int>();
        Dictionary<KeyValuePair<Item, String>, String> fullDiscrepancies = new Dictionary<KeyValuePair<Item, String>, String>();
        if (!IsPostBack)
        {
            if (Session["discrepancyList"] != null)
            {
                discrepancies = (Dictionary<Item, int>)Session["discrepancyList"];
                foreach (KeyValuePair<Item, int> kvp in discrepancies)
                {
                    string adjustment = "";
                    int stock = (int)kvp.Key.BalanceQty;
                    int adj = kvp.Value;
                    int actualQuantity = stock + adj;

                    if (adj > 0)
                    {
                        adjustment = "+" + adj.ToString();
                    }
                    else
                    {
                        adjustment = adj.ToString();
                    }
                    KeyValuePair<Item, String> displayKvp = new KeyValuePair<Item, String>(kvp.Key, actualQuantity.ToString());
                    fullDiscrepancies.Add(displayKvp, adjustment);
                }
            }
            else
            {
                Response.Redirect("~/Store/GenerateDiscrepancyV2.aspx");
            }
            GridView1.DataSource = fullDiscrepancies;
            GridView1.DataBind();

            if (Session["monthly"] != null)
            {
                if ((bool)Session["monthly"] == false)
                {
                    GridView1.Columns[4].Visible = false;
                }
            }
        }
        Label1.Text = "";
        Label5.Text = "";
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        List<Discrepency> dList = new List<Discrepency>();
        bool complete = true;
        bool informSupervisor = false;
        bool informManager = false;

        foreach (GridViewRow row in GridView1.Rows)
        {
            string itemCode = (row.FindControl("lblItemCode") as Label).Text;
            string stock = (row.FindControl("lblStock") as Label).Text;
            string actual = (row.FindControl("lblActual") as Label).Text;
            int adj = Int32.Parse(actual) - Int32.Parse(stock);
            string remarks = (row.FindControl("txtRemarks") as TextBox).Text;
            if (!ValidatorUtil.isEmpty(remarks))
            {
                if (remarks.Length <= maxChars)
                {
                    //update item table if any adjustment at disubrsement point 
                    if (Session["ItemToUpdate"] != null)
                    {
                        if ((bool)Session["ItemToUpdate"])
                        {
                            StationeryEntities context = new StationeryEntities();
                            Item i = EFBroker_Item.GetItembyItemCode(itemCode);
                            i.BalanceQty = (adj * -1) + i.BalanceQty;
                            EFBroker_Item.UpdateItem(i);
                        }
                    }

                    //Code to determine the discrepancy amount
                    List<PriceList> plHistory = EFBroker_PriceList.GetPriceListByItemCode(itemCode);
                    List<PriceList> itemPrices = new List<PriceList>();

                    foreach (PriceList pl in plHistory)
                    {    //Get only currently active suppliers for an item
                        if (pl.TenderYear == DateTime.Now.Year.ToString())
                        {
                            itemPrices.Add(pl);
                        }
                    }

                    decimal totalPrice = 0;

                    foreach (PriceList pl in itemPrices)
                    {
                        totalPrice += (decimal)pl.Price;
                    }

                    decimal averageUnitPrice = totalPrice / itemPrices.Count;

                    Discrepency d = new Discrepency();
                    d.ItemCode = itemCode;
                    if (Session["empID"] != null)
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
                    if (Session["monthly"] != null)
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

                    //Set the approver based on discrepancy amount, and email notify them
                    if (d.TotalDiscrepencyAmount < 250)
                    {
                        d.ApprovedBy = EFBroker_DeptEmployee.GetEmployeeListByRole("Store Supervisor")[0].EmpID;
                        informSupervisor = true;
                    }
                    else
                    {
                        d.ApprovedBy = EFBroker_DeptEmployee.GetEmployeeListByRole("Store Manager")[0].EmpID;
                        informManager = true;
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
            try
            {
                EFBroker_Discrepancy.SaveDiscrepencies(dList);
            }
            catch (Exception ex)
            {
                Label5.Text = "Discrepancy report submission failed, please try again.";
            }

            Session["discrepancyList"] = null;
            Session["monthly"] = null;
            Session["ItemToUpdate"] = null;

            
            //foreach (Discrepency d in dList)
            //{
            //    if (Math.Abs((decimal)d.TotalDiscrepencyAmount) < 250)
            //    {
            //        informSupervisor = true;
            //    }
            //    else
            //    {
            //        informManager = true;
            //    }
            //}

            if (informSupervisor)
            {
                string supervisorEmail = EFBroker_DeptEmployee.GetEmployeeListByRole("Store Supervisor")[0].Email;
                Utility.sendMail(supervisorEmail, "New Discrepancies Notification " + DateTime.Now.ToString(), "New item discrepancies have been submitted. Please log in to the system to review them. Thank you.");
            }
            if (informManager)
            {
                string managerEmail = EFBroker_DeptEmployee.GetEmployeeListByRole("Store Manager")[0].Email;
                Utility.sendMail(managerEmail, "New Discrepancies Notification " + DateTime.Now.ToString(), "New item discrepancies (worth at least $250) have been submitted. Please log in to the system to review them. Thank you.");
            }

            Response.Redirect("~/Store/GenerateDiscrepancyV2.aspx");

        }
    }
}