using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PurchaseOrderForm : System.Web.UI.Page
{


    PurchaseOrderComparer pComparer = new PurchaseOrderComparer();

    PurchaseController pCtrlr = new PurchaseController();
    String itemCode;
    List<PurchaseItems> ritems;
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
        AddNewItemDropDown.DataSource = pCtrlr.GetItemList();
        AddNewItemDropDown.DataTextField = "Description";
        AddNewItemDropDown.DataValueField = "ItemCode";
        AddNewItemDropDown.DataBind();

        //To populate Supervisor Name dropdown List
        supervisorNamesDropDown.DataSource = pCtrlr.GetSupervisorList();
        supervisorNamesDropDown.DataTextField = "EmpName";
        supervisorNamesDropDown.DataValueField = "EmpID";
        supervisorNamesDropDown.DataBind();


        if (Session["PurchaseItems"] != null)
        {
            ritems = (List<PurchaseItems>)Session["PurchaseItems"];
            gvPurchaseItems.DataSource = ritems;
            gvPurchaseItems.DataBind();

        }
        else
        {
            //To add PurchaseItems to session if session is empty
            ritems = pCtrlr.GetReorderItemList();
            Session["PurchaseItems"] = ritems;
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
            List<SupplierInfo> splrList = pCtrlr.GetSupplierList().Where(x => x.ItemCode == (string)DataBinder.Eval(e.Row.DataItem, "ItemCode")).ToList();
            // List<SupplierInfo> splrList = pCtrlr.GetSupplierListByItemCode(gvrowItemCode);
            supplierList.DataSource = splrList;
            supplierList.DataTextField = "SupplierNameWithPrice";
            supplierList.DataValueField = "SupplierCode";
            supplierList.DataBind();

            //To check whether the same item with same supplier has been added to gridview 
            //if so, then prepopulate with second supplier for that newly added item
            if (!dictnry.ContainsKey(gvrowItemCode))
            {

                dictnry.Add(gvrowItemCode, 1);

            }
            else
            {
                int value = dictnry[gvrowItemCode];
                dictnry[gvrowItemCode] = value + 1;
            }
            if (dictnry[gvrowItemCode] == 1)
                supplierList.SelectedValue = splrList[0].SupplierCode;
            else if (dictnry[gvrowItemCode] == 2)
                supplierList.SelectedValue = splrList[1].SupplierCode;
            else
                supplierList.SelectedValue = splrList[2].SupplierCode;


        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {

        }


    }

    protected void AddItem_Click(object sender, EventArgs e)
    {
        //To add an item that is not under reorderLevel and append it to the gvPurchaseItems List
        String itemCode = AddNewItemDropDown.SelectedItem.Value;
        if (Session["PurchaseItems"] != null)
        {
            ritems = (List<PurchaseItems>)Session["PurchaseItems"];
            if (ritems.Exists(x => x.ItemCode == itemCode))
            {
                for (int i = 0; i < gvPurchaseItems.Rows.Count; i++)
                {
                    GridViewRow gvRow = gvPurchaseItems.Rows[i];

                    Label codeLbl = (Label)gvRow.FindControl("ItemCode");
                    string codeNo = codeLbl.Text;
                    if (codeNo == itemCode)
                    {
                        DropDownList splrControl = (DropDownList)gvRow.FindControl("SupplierList");
                        ListItem item = splrControl.SelectedItem;
                        exisitingItemsupplrName = item.Value;
                        break;
                    }
                    else
                    {
                        continue;
                    }

                }

            }
            ritems.Add(pCtrlr.AddPurchaseItem(itemCode));
            gvPurchaseItems.DataSource = ritems;
            Session["PurchaseItems"] = ritems;
            gvPurchaseItems.DataBind();
            ClientScript.RegisterStartupScript(Page.GetType(), "MessageBox",
  "<script language='javascript'>alert('" + "Item Added!" + "');</script>");
        }

    }

    //protected void gvreoderItems_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    // deleting a item row from gvPurchaseItems
    //    if (e.CommandName == "Delete")
    //    {
    //        int index = Convert.ToInt32(e.CommandArgument);
    //        GridViewRow gvRow = gvPurchaseItems.Rows[index];
    //        List<PurchaseItems> reorderItems = (List<PurchaseItems>)Session["PurchaseItems"];
    //        reorderItems.RemoveAt(index);
    //       Session["PurchaseItems"] = reorderItems;

    //    }
    //}

    //protected void gvreoderItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
    //{
    //    //Update gvPurchaseItems after deleting a row
    //    List<PurchaseItems> reorderItems = (List<PurchaseItems>)Session["PurchaseItems"];
    //    gvPurchaseItems.DataSource = reorderItems;
    //    gvPurchaseItems.DataBind();
    //}

    protected void Reset_Click(object sender, EventArgs e)
    {

        //Update reorderItemList ie. gvPurchaseItems
        ritems = pCtrlr.GetReorderItemList();
        Session["PurchaseItems"] = ritems;
        gvPurchaseItems.DataSource = ritems;
        gvPurchaseItems.DataBind();

        supervisorNamesDropDown.SelectedIndex = 0;
        AddNewItemDropDown.SelectedIndex = 0;
    }

    protected void ProceedBtn_Click(object sender, EventArgs e)

    {
        if (Page.IsValid)
        {


            decimal totalAmount = 0;

            Dictionary<PurchaseOrder, List<Item_PurchaseOrder>> purchaseItemList = new Dictionary<PurchaseOrder, List<Item_PurchaseOrder>>(pComparer);

            for (int i = 0; i < gvPurchaseItems.Rows.Count; i++)
            {
                PurchaseOrder pOrder = new PurchaseOrder();

                GridViewRow gvRow = gvPurchaseItems.Rows[i];

                DropDownList splrControl = (DropDownList)gvRow.FindControl("SupplierList");
                ListItem suplierInfo = splrControl.SelectedItem;
                string[] str = suplierInfo.Text.ToString().Split('/');

                pOrder.SupplierCode = suplierInfo.Value;
                if (!purchaseItemList.ContainsKey(pOrder))
                {
                    pOrder.OrderDate = DateTime.Now.Date;
                    pOrder.ApprovedBy = Convert.ToInt32(supervisorNamesDropDown.SelectedItem.Value);
                    pOrder.ExpectedDate = DateTime.Parse(txtDate.Text);
                    pOrder.Status = "Pending";
                    pOrder.TotalAmount += Convert.ToDecimal(str[2]);
                    pOrder.RequestedBy = (int)Session["empID"];
                    purchaseItemList.Add(pOrder, null);
                }

                Item_PurchaseOrder pItems = new Item_PurchaseOrder();
                Label itemlbl = (Label)gvRow.FindControl("ItemCode");
                pItems.ItemCode = itemlbl.Text;
                pItems.PurchaseOrderID = pOrder.PurchaseOrderID;
                Label qtyTxtBx = (Label)gvRow.FindControl("ReorderQty");
                pItems.OrderQty = Convert.ToInt32(qtyTxtBx.Text);

                pItems.Amount = pItems.OrderQty * Convert.ToDecimal(str[1]);

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
            pCtrlr.AddPurchaseOrder(purchaseItemList);
            ClientScript.RegisterStartupScript(Page.GetType(), "MessageBox",
  "<script language='javascript'>alert('" + "Purchase Done, Awaiting Approval!" + "');</script>");
        }
    }

    protected void DateValidator(object source, ServerValidateEventArgs args)
    {
        string date = txtDate.Text;
        string todayDate = Convert.ToString(DateTime.Today.Date);
        if (Convert.ToDateTime(date) > Convert.ToDateTime(todayDate))
        {
            args.IsValid = true;

        }
        else
        {
            args.IsValid = false;

        }


    }

    protected void DeleteItem_Click(object sender, EventArgs e)
    {
        //int index = Convert.ToInt32(e.CommandArgument);
        // GridViewRow gvRow = gvPurchaseItems.Rows[index];
        List<PurchaseItems> reorderItems = (List<PurchaseItems>)Session["PurchaseItems"];
        List<PurchaseItems> newReorderList = new List<PurchaseItems>();
        foreach (GridViewRow gvrow in gvPurchaseItems.Rows)
        {
            CheckBox chkbx = (CheckBox)gvrow.FindControl("DeleteChkBx");
            if (!chkbx.Checked)
            {


                int index = Convert.ToInt32(gvrow.RowIndex);
                newReorderList.Add(reorderItems.ElementAt(index));                
            }
        }

         
         Session["PurchaseItems"] = newReorderList;
         gvPurchaseItems.DataSource = newReorderList;
         gvPurchaseItems.DataBind();
        ClientScript.RegisterStartupScript(Page.GetType(), "MessageBox",
        "<script language='javascript'>alert('" + "Item Deleted!" + "');</script>");

    }

}
