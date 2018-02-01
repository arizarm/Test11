using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PurchaseOrderForm : System.Web.UI.Page
{


    PurchaseOrderComparer pComparer = new PurchaseOrderComparer();

    PurchaseController pCtrlr = new PurchaseController();

    String itemCode;
    List<ReorderItem> ritems;
    List<PurchaseOrder> pOrderList = null;
    static String exisitingItemsupplrName = null;
    int newItemcount = 0;
    int itemcount = 1;
    List<Dictionary<string, int>> itemSuplrdict = new List<Dictionary<string, int>>();
     Dictionary<string, int> dictnry = new Dictionary<string, int>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadData();
           
        }

    }
    private void LoadData()
    {
        //To populate Items dropDown List
        AddNewItemDropDown.DataSource =EFBroker_Item.GetActiveItemWithPrice();
        AddNewItemDropDown.DataTextField = "Description";
        AddNewItemDropDown.DataValueField = "ItemCode";
        AddNewItemDropDown.DataBind();

        //To populate Supervisor Name dropdown List
        supervisorNamesDropDown.DataSource = pCtrlr.GetSupervisorList();
        supervisorNamesDropDown.DataTextField = "EmpName";
        supervisorNamesDropDown.DataValueField = "EmpID";
        supervisorNamesDropDown.DataBind();


        if (Session["ReorderItem"] != null)
        {
            ritems = (List<ReorderItem>)Session["ReorderItem"];
            gvPurchaseItems.DataSource = ritems;
            gvPurchaseItems.DataBind();

        }
        else
        {
            //To add PurchaseItems to session if session is empty
            ritems = pCtrlr.GetReorderItemList();
            Session["ReorderItem"] = ritems;
            gvPurchaseItems.DataSource = ritems;
            gvPurchaseItems.DataBind();

        }

    }



    protected void reoderItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //To add supplierList to items that are below the reorderLevel
            DropDownList supplierList = (DropDownList)e.Row.FindControl("SupplierList");
            Label itemCodeLbl = (Label)e.Row.FindControl("ItemCode");
            String gvrowItemCode = itemCodeLbl.Text;
            List<ItemPrice> itemPriceList = pCtrlr.GetItemPriceList().Where(x => x.ItemCode == (string)DataBinder.Eval(e.Row.DataItem, "ItemCode")).ToList();
            // List<SupplierInfo> splrList = pCtrlr.GetSupplierListByItemCode(gvrowItemCode);
            supplierList.DataSource = itemPriceList;
            supplierList.DataTextField = "SupplierName";
            supplierList.DataValueField = "SupplierCode";
            supplierList.DataBind();


            //To check whether the same item with same supplier has been added to gridview
           // if so, then prepopulate with second supplier for that newly added item
               if(!dictnry.ContainsKey(gvrowItemCode))
                {

                    dictnry.Add(gvrowItemCode, 0);

                }
                else
                {
                    int value = dictnry[gvrowItemCode];
                    dictnry[gvrowItemCode] = value + 1;
                }
           
            if(itemPriceList.Count > dictnry[gvrowItemCode])
            {
                supplierList.SelectedValue = itemPriceList[dictnry[gvrowItemCode]].SupplierCode;
            }
            else
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "MessageBox",
            "<script language='javascript'>alert('" + "Item already exist!" + "');</script>");
            }
            
            Label price = (Label)e.Row.FindControl("Price");
            price.Text = Convert.ToString(itemPriceList.Where(x => x.SupplierCode == supplierList.SelectedValue).Select(x => x.FormattedPrice).First());
            Label amount = (Label)e.Row.FindControl("Amount");
            amount.Text = Convert.ToString(itemPriceList.Where(x => x.SupplierCode == supplierList.SelectedValue).Select(x => x.FormattedAmount).First());

        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {

        }

    }

    protected void AddItem_Click(object sender, EventArgs e)
    {
        //To add an item that is not under reorderLevel and append it to the gvPurchaseItems List
        String itemCode = AddNewItemDropDown.SelectedItem.Value;
        
        int count = 1;
        if (Session["ReorderItem"] != null)
        {
            List<ItemPrice> itemPriceList;
            ritems = (List<ReorderItem>)Session["ReorderItem"];
            if (ritems.Exists(x => x.ItemCode == itemCode))
            {
                itemPriceList = pCtrlr.GetItemPriceList().Where(x => x.ItemCode == itemCode).ToList();
                for (int i = 0; i < gvPurchaseItems.Rows.Count; i++)
                {
                    GridViewRow gvRow = gvPurchaseItems.Rows[i];

                    Label codeLbl = (Label)gvRow.FindControl("ItemCode");
                    string codeNo = codeLbl.Text;
                    //To check if an item with same supplier is already added. if exist,then item with 2nd supplier will be added .
                    if (codeNo == itemCode)
                    {
                        DropDownList splrControl = (DropDownList)gvRow.FindControl("SupplierList");                        
                        //ListItem item = splrControl.SelectedItem;
                        count++;
                        //exisitingItemsupplrName = item.Value;

                        //break;
                    }
                    else
                    {
                        continue;
                    }

                }
                if (itemPriceList.Count >= count)
                {

                    ritems.Add(pCtrlr.AddPurchaseItem(itemCode));
                    gvPurchaseItems.DataSource = ritems;
                    Session["ReorderItem"] = ritems;
                    gvPurchaseItems.DataBind();
                    ClientScript.RegisterStartupScript(Page.GetType(), "MessageBox",
                    "<script language='javascript'>alert('" + "Item Added!" + "');</script>");
                }
                else
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "MessageBox",
                    "<script language='javascript'>alert('" + "Item already added with all existing suppliers!" + "');</script>");
                }

            }
            else
            {
                ritems.Add(pCtrlr.AddPurchaseItem(itemCode));
                gvPurchaseItems.DataSource = ritems;
                Session["ReorderItem"] = ritems;
                gvPurchaseItems.DataBind();
                ClientScript.RegisterStartupScript(Page.GetType(), "MessageBox",
                "<script language='javascript'>alert('" + "Item Added!" + "');</script>");
            }
            
        }

    }


    protected void Reset_Click(object sender, EventArgs e)
    {

        //Update reorderItemList ie. gvPurchaseItems
        ritems = pCtrlr.GetReorderItemList();
        Session["ReorderItem"] = ritems;
        gvPurchaseItems.DataSource = ritems;
        gvPurchaseItems.DataBind();

        supervisorNamesDropDown.SelectedIndex = 0;
        AddNewItemDropDown.SelectedIndex = 0;
        ClientScript.RegisterStartupScript(Page.GetType(), "MessageBox",
            "<script language='javascript'>alert('" + "Reset done" + "');</script>");
    }

    protected void ProceedBtn_Click(object sender, EventArgs e)

    {
        if (Page.IsValid)
        {


            Dictionary<PurchaseOrder, List<Item_PurchaseOrder>> purchaseItemList = new Dictionary<PurchaseOrder, List<Item_PurchaseOrder>>(pComparer);

            for (int i = 0; i < gvPurchaseItems.Rows.Count; i++)
            {
                PurchaseOrder pOrder = new PurchaseOrder();

                GridViewRow gvRow = gvPurchaseItems.Rows[i];
                if(((CheckBox)gvRow.FindControl("CheckBox")).Checked)
                {
                    DropDownList splrControl = (DropDownList)gvRow.FindControl("SupplierList");
                    ListItem suplierInfo = splrControl.SelectedItem;
                    string[] str = suplierInfo.Text.ToString().Split('/');

                    pOrder.SupplierCode = suplierInfo.Value;
                    Label amntlbl = (Label)gvRow.FindControl("Amount");
                    if (!purchaseItemList.ContainsKey(pOrder))
                    {
                        pOrder.OrderDate = DateTime.Now.Date;
                        pOrder.ApprovedBy = Convert.ToInt32(supervisorNamesDropDown.SelectedItem.Value);
                        pOrder.ExpectedDate = DateTime.ParseExact(txtDate.Text, "d", CultureInfo.InvariantCulture);
                        pOrder.Status = "Pending";

                        pOrder.TotalAmount += decimal.Parse(amntlbl.Text, System.Globalization.NumberStyles.Currency);
                        pOrder.RequestedBy = (int)Session["empID"];
                        purchaseItemList.Add(pOrder, null);
                    }

                    Item_PurchaseOrder pItems = new Item_PurchaseOrder();
                    Label itemlbl = (Label)gvRow.FindControl("ItemCode");
                    pItems.ItemCode = itemlbl.Text;
                    pItems.PurchaseOrderID = pOrder.PurchaseOrderID;
                    TextBox qtyTxtBx = (TextBox)gvRow.FindControl("ReorderQty");
                    pItems.OrderQty = Convert.ToInt32(qtyTxtBx.Text);
                    Label priceLbl = (Label)gvRow.FindControl("Price");
                    pItems.ItemCode = itemlbl.Text;
                    pItems.Amount = pItems.OrderQty * decimal.Parse(priceLbl.Text, System.Globalization.NumberStyles.Currency);

                    List<Item_PurchaseOrder> ItemList = null;
                    if (purchaseItemList[pOrder] != null)
                    {
                        ItemList = purchaseItemList[pOrder];
                        ItemList.Add(pItems);
                        purchaseItemList[pOrder] = ItemList;
                    }
                    else
                    {
                        ItemList = new List<Item_PurchaseOrder>();
                        ItemList.Add(pItems);
                        purchaseItemList[pOrder] = ItemList;
                    }

                }
               
            }
            if(purchaseItemList.Count==0)
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "MessageBox",
                 "<script language='javascript'>alert('" + "Please select items first!" + "');</script>");
            }
            else
            {
                pCtrlr.AddPurchaseOrder(purchaseItemList);
                ClientScript.RegisterStartupScript(Page.GetType(), "MessageBox",
             "<script language='javascript'>alert('" + "Purchase Done, Awaiting Approval!" + "');</script>");

            }
        }
                
    }

    protected void DateValidator(object source, ServerValidateEventArgs args)
    {
       
        string date = txtDate.Text;
        string todayDate = Convert.ToString(DateTime.Today.Date.ToString("d"));
        //Console.WriteLine()
        DateTime d1 = DateTime.ParseExact(date,"d",CultureInfo.InvariantCulture);
        DateTime d2= DateTime.ParseExact(todayDate, "d/M/yyyy", CultureInfo.InvariantCulture);
        if (d1  > d2)
        {
            args.IsValid = true;

        }
        else
        {
            args.IsValid = false;

        }


    }

 

    protected void SupplierList_SelectedIndexChanged(object sender, EventArgs e)
    {

        DropDownList drpdLsit = (DropDownList)sender;
        GridViewRow row = (GridViewRow)drpdLsit.NamingContainer;
        string selected = (string)drpdLsit.SelectedItem.Value;
        //DropDownList splrControl = (DropDownList)gvPurchaseItems.SelectedRow.FindControl("SupplierList");
        //ListItem suplierInfo = splrControl.SelectedItem;
        Console.WriteLine(selected);
        ItemPrice item = pCtrlr.GetItemPriceList().Where(x => x.SupplierCode == selected).First();
        TextBox qty = (TextBox)row.FindControl("ReorderQty");       
        Label price = (Label)row.FindControl("Price");
        price.Text = Convert.ToString(item.FormattedPrice);
        Label amount = (Label)row.FindControl("Amount");
        item.Amount = Convert.ToDecimal(item.Price * Convert.ToInt32(qty.Text));
        amount.Text = item.FormattedAmount;
  
    }

    //protected void ReorderQtyValidation(object sender, ServerValidateEventArgs e)
    //{
       
    //    CustomValidator custval = new CustomValidator();
    //    custval = (CustomValidator)sender;
    //    GridViewRow row = custval.NamingContainer as GridViewRow;
    //     TextBox qty = row.FindControl("ReorderQty") as TextBox;        
    //        if (qty != null)
    //        {
    //            //GridViewRow row = textBox.NamingContainer as GridViewRow;
    //            Label itemLbl = (Label)row.FindControl("ItemCode");
    //            Item item = EFBroker_Item.GetActiveItembyItemCode(itemLbl.Text);
    //            if (item.ReorderQty <= Convert.ToInt32(qty.Text))
    //            {
    //                e.IsValid = true;
    //            }
    //            else
    //                e.IsValid = false;
    //        }
        
    //}
        

    protected void CheckAll_CheckedChanged(object sender, EventArgs e)
    {
        if (((CheckBox)gvPurchaseItems.HeaderRow.FindControl("CheckAll")).Checked)
        {
            foreach (GridViewRow row in gvPurchaseItems.Rows)
            {
                ((CheckBox)row.FindControl("CheckBox")).Checked = true;
            }
        }

        if (!((CheckBox)gvPurchaseItems.HeaderRow.FindControl("CheckAll")).Checked)
        {
            foreach (GridViewRow row in gvPurchaseItems.Rows)
            {
                ((CheckBox)row.FindControl("CheckBox")).Checked = false;
            }
        }
    }
    protected void orderQtyTxtBx_TextChanged(object sender, EventArgs e)
    {
        //Page.Validate();
        //if (Page.IsValid)
        //{
            TextBox qtytxt = sender as TextBox;            
            GridViewRow row = qtytxt.NamingContainer as GridViewRow;
            DropDownList drpdLsit = (DropDownList)row.FindControl("SupplierList");
            string selected = (string)drpdLsit.SelectedItem.Value;
            ItemPrice item = pCtrlr.GetItemPriceList().Where(x => x.SupplierCode == selected).First();
            Label price = (Label)row.FindControl("Price");            
            price.Text = Convert.ToString(item.FormattedPrice);
            Label amount = (Label)row.FindControl("Amount");
            item.Amount= Convert.ToDecimal(item.Price * Convert.ToInt32(qtytxt.Text));
            amount.Text = item.FormattedAmount;


        //}
   
    }  
}
