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
    List<ReorderItem> ritems;
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
        ddlAddNewItem.DataSource =EFBroker_Item.GetActiveItemWithPrice();
        ddlAddNewItem.DataTextField = "Description";
        ddlAddNewItem.DataValueField = "ItemCode";
        ddlAddNewItem.DataBind();

        //To populate Supervisor Name dropdown List
        ddlsupervisorNames.DataSource = pCtrlr.GetSupervisorList();
        ddlsupervisorNames.DataTextField = "EmpName";
        ddlsupervisorNames.DataValueField = "EmpID";
        ddlsupervisorNames.DataBind();


        if (Session["ReorderItem"] != null)
        {
            ritems = (List<ReorderItem>)Session["ReorderItem"];
            GvreorderItems.DataSource = ritems;
            GvreorderItems.DataBind();

        }
        else
        {
            //To add PurchaseItems to session if session is empty
            ritems = pCtrlr.GetReorderItemList();
            Session["ReorderItem"] = ritems;
            GvreorderItems.DataSource = ritems;
            GvreorderItems.DataBind();

        }

    }



    protected void GvreoderItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //To add supplierList to items that are below the reorderLevel
            DropDownList supplierList = (DropDownList)e.Row.FindControl("ddlSupplierList");
            Label itemCodeLbl = (Label)e.Row.FindControl("lblItemCode");
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
            
            Label price = (Label)e.Row.FindControl("lblPrice");
            price.Text = Convert.ToString(itemPriceList.Where(x => x.SupplierCode == supplierList.SelectedValue).Select(x => x.FormattedPrice).First());
            Label amount = (Label)e.Row.FindControl("lblAmount");
            amount.Text = Convert.ToString(itemPriceList.Where(x => x.SupplierCode == supplierList.SelectedValue).Select(x => x.FormattedAmount).First());

        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {

        }

    }

    protected void BtnAddItem_Click(object sender, EventArgs e)
    {
        //To add an item that is not under reorderLevel and append it to the gvPurchaseItems List
        String itemCode = ddlAddNewItem.SelectedItem.Value;
        
        int count = 1;
        if (Session["ReorderItem"] != null)
        {
            List<ItemPrice> itemPriceList;
            ritems = (List<ReorderItem>)Session["ReorderItem"];
            if (ritems.Exists(x => x.ItemCode == itemCode))
            {
                itemPriceList = pCtrlr.GetItemPriceList().Where(x => x.ItemCode == itemCode).ToList();
                for (int i = 0; i < GvreorderItems.Rows.Count; i++)
                {
                    GridViewRow gvRow = GvreorderItems.Rows[i];

                    Label codeLbl = (Label)gvRow.FindControl("lblItemCode");
                    string codeNo = codeLbl.Text;
                    //To check if an item with same supplier is already added. if exist,then item with 2nd supplier will be added .
                    if (codeNo == itemCode)
                    {
                        DropDownList splrControl = (DropDownList)gvRow.FindControl("ddlSupplierList");                        
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
                    GvreorderItems.DataSource = ritems;
                    Session["ReorderItem"] = ritems;
                    GvreorderItems.DataBind();
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
                GvreorderItems.DataSource = ritems;
                Session["ReorderItem"] = ritems;
                GvreorderItems.DataBind();
                ClientScript.RegisterStartupScript(Page.GetType(), "MessageBox",
                "<script language='javascript'>alert('" + "Item Added!" + "');</script>");
            }
            
        }

    }


    protected void BtnReset_Click(object sender, EventArgs e)
    {

        //Update reorderItemList ie. gvPurchaseItems
        ritems = pCtrlr.GetReorderItemList();
        Session["ReorderItem"] = ritems;
        GvreorderItems.DataSource = ritems;
        GvreorderItems.DataBind();
        txtDate.Text = "";
        ddlsupervisorNames.SelectedIndex = 0;
        ddlAddNewItem.SelectedIndex = 0;
        ClientScript.RegisterStartupScript(Page.GetType(), "MessageBox",
            "<script language='javascript'>alert('" + "Reset done" + "');</script>");
    }

    protected void BtnProceed_Click(object sender, EventArgs e)

    {
        if (Page.IsValid)
        {


            Dictionary<PurchaseOrder, List<Item_PurchaseOrder>> purchaseItemList = new Dictionary<PurchaseOrder, List<Item_PurchaseOrder>>(pComparer);

            for (int i = 0; i < GvreorderItems.Rows.Count; i++)
            {
                PurchaseOrder pOrder = new PurchaseOrder();

                GridViewRow gvRow = GvreorderItems.Rows[i];
                if(((CheckBox)gvRow.FindControl("CbxItem")).Checked)
                {
                    DropDownList splrControl = (DropDownList)gvRow.FindControl("ddlSupplierList");
                    ListItem suplierInfo = splrControl.SelectedItem;               
                    pOrder.SupplierCode = suplierInfo.Value;
                    Label amntlbl = (Label)gvRow.FindControl("lblAmount");
                    if (!purchaseItemList.ContainsKey(pOrder))
                    {
                        pOrder.OrderDate = DateTime.Now.Date;
                        pOrder.ApprovedBy = Convert.ToInt32(ddlsupervisorNames.SelectedItem.Value);
                        pOrder.ExpectedDate = DateTime.ParseExact(txtDate.Text, "d", CultureInfo.InvariantCulture);
                        pOrder.Status = "Pending";

                        pOrder.TotalAmount += decimal.Parse(amntlbl.Text, System.Globalization.NumberStyles.Currency);
                        pOrder.RequestedBy = (int)Session["empID"];
                        purchaseItemList.Add(pOrder, null);
                    }

                    Item_PurchaseOrder pItems = new Item_PurchaseOrder();
                    Label itemlbl = (Label)gvRow.FindControl("lblItemCode");
                    pItems.ItemCode = itemlbl.Text;
                    pItems.PurchaseOrderID = pOrder.PurchaseOrderID;
                    TextBox qtyTxtBx = (TextBox)gvRow.FindControl("txtReorderQty");
                    pItems.OrderQty = Convert.ToInt32(qtyTxtBx.Text);
                    Label priceLbl = (Label)gvRow.FindControl("lblPrice");
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
                Session["ReorderItem"] = null;
                ClientScript.RegisterStartupScript(Page.GetType(), "MessageBox",
             "<script language='javascript'>alert('" + "Purchase Done, Awaiting Approval!" + "');</script>");
                Response.Redirect(LoginController.PurchaseOrderListURI);
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
        if (d1  >= d2)
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
        TextBox qty = (TextBox)row.FindControl("txtReorderQty");       
        Label price = (Label)row.FindControl("lblPrice");
        price.Text = Convert.ToString(item.FormattedPrice);
        Label amount = (Label)row.FindControl("lblAmount");
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
        if (((CheckBox)GvreorderItems.HeaderRow.FindControl("CbxCheckAll")).Checked)
        {
            foreach (GridViewRow row in GvreorderItems.Rows)
            {
                ((CheckBox)row.FindControl("CbxItem")).Checked = true;
            }
        }

        if (!((CheckBox)GvreorderItems.HeaderRow.FindControl("CbxCheckAll")).Checked)
        {
            foreach (GridViewRow row in GvreorderItems.Rows)
            {
                ((CheckBox)row.FindControl("CbxItem")).Checked = false;
            }
        }
    }
    protected void OrderQtyTxtBx_TextChanged(object sender, EventArgs e)
    {
        //Page.Validate();
        //if (Page.IsValid)
        //{
            TextBox qtytxt = sender as TextBox;            
            GridViewRow row = qtytxt.NamingContainer as GridViewRow;
            DropDownList drpdLsit = (DropDownList)row.FindControl("ddlSupplierList");
            string selected = (string)drpdLsit.SelectedItem.Value;
            ItemPrice item = pCtrlr.GetItemPriceList().Where(x => x.SupplierCode == selected).First();
            Label price = (Label)row.FindControl("lblPrice");            
            price.Text = Convert.ToString(item.FormattedPrice);
            Label amount = (Label)row.FindControl("lblAmount");
            item.Amount= Convert.ToDecimal(item.Price * Convert.ToInt32(qtytxt.Text));
            amount.Text = item.FormattedAmount;


        //}
   
    }  
}
