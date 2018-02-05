using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Transactions;

public partial class RequisitionDetails : System.Web.UI.Page
{
    StationeryEntities context = new StationeryEntities();
    Requisition r = new Requisition();
    int id = 0;
    string code;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (RequisitionControl.getRequisition(int.Parse((string)Request.QueryString["requisitionNo"])) != null)
        {
            id = Convert.ToInt32(Request.QueryString["requisitionNo"]);
            //int id = 24;

            Requisition r = RequisitionControl.getRequisition(id);
            int empid = Convert.ToInt32(r.RequestedBy);
            lblDate.Text = r.RequestDate.ToString();
            lblStatus.Text = r.Status.ToString();
            if (lblStatus.Text.Equals("Approved") || lblStatus.Text.Equals("approved") || lblStatus.Text.Equals("InProgress"))
            {
                lblStatus.ForeColor = System.Drawing.Color.Green;
            }
            else if (lblStatus.Text.Equals("Pending"))
            {
                lblStatus.ForeColor = System.Drawing.Color.Blue;
            }
            else if (lblStatus.Text.Equals("Priority"))
            {
                lblStatus.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                lblStatus.ForeColor = System.Drawing.Color.Black;
            }

            if (!IsPostBack)
            {

                showAllItems();
                if (r.Status != "Pending")
                {
                    btnCancel.Visible = false;
                    btnAdd.Visible = false;
                    btnUpdate.Visible = false;

                    if (!String.IsNullOrWhiteSpace(r.Remarks))
                        lblRemarks.Text = r.Remarks.ToString();
                }

                ddlItem.DataSource = RequisitionControl.getItem();
                ddlItem.DataTextField = "Description";
                ddlItem.DataValueField = "ItemCode";
                ddlItem.DataBind();
            }

            code = ddlItem.SelectedValue.ToString();
            lblUom.Text = RequisitionControl.getUOM(code);
        }
        else
            Response.Redirect(LoginController.RequisitionListDepEmpURI);
    }

    protected void showAllItems()
    {
        gvItemList.DataSource = RequisitionControl.getList(id);
        //GridView1.DataSource = q.ToList();
        gvItemList.DataBind();

        gvItemListView.DataSource = RequisitionControl.getList(id);
        //GridView2.DataSource = q.ToList();
        gvItemListView.DataBind();
    }

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            id = Convert.ToInt32(Request.QueryString["requisitionNo"]);
            RequisitionControl.cancelRejectRequisition(id);

            Response.Redirect(LoginController.RequisitionListDepRepURI);
            //Response.Write("<script language='javascript'>alert('Requisition has been cancelled');</script>");
        }
        catch (Exception)
        {
            Utility.DisplayAlertMessage("Error! Retry.");
        }
    }

    protected void BtnAdd_Click(object sender, EventArgs e)
    {
        pnlAddNew.Visible = true;
        btnAdd.Visible = false;
        btnHide.Visible = true;
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        id = Convert.ToInt32(Request.QueryString["requisitionNo"]);
        //string des = RequisitionControl.getDescription(code);
        int qty = Convert.ToInt32(txtQty.Text);

        if (gvItemList.Rows.Count <= 0)
        {
            RequisitionControl.addItemToRequisition(code, qty, id);
        }

        else
        {
            bool isEqual = false;
            string truCode = "";
            foreach (GridViewRow row in gvItemList.Rows)
            {
                System.Web.UI.WebControls.Label labelDes = (System.Web.UI.WebControls.Label)row.FindControl("lblItemDes");
                string item = labelDes.Text;

                System.Web.UI.WebControls.Label labelCode = (System.Web.UI.WebControls.Label)row.FindControl("lblCode");
                string iCode = labelCode.Text;

                if (code.Equals(iCode))
                {
                    isEqual = true;
                    truCode = iCode;
                }
            }
            if (isEqual)
            {
                rfvQty.Enabled = true;
                rvQty.Enabled = true;
                RequisitionControl.editRequisitionItemQty(id, truCode, qty);
            }
            else
            {
                rfvQty.Enabled = true;
                rvQty.Enabled = true;
                RequisitionControl.addItemToRequisition(code, qty, id);
            }
        }

        showAllItems();
    }

    protected void BtnHide_Click(object sender, EventArgs e)
    {
        pnlAddNew.Visible = false;
        btnAdd.Visible = true;
        btnHide.Visible = false;
    }

    protected void BtnDelete_Click(object sender, EventArgs e)
    {
        //LoadData();
        GridViewRow row = ((System.Web.UI.WebControls.Button)sender).Parent.Parent as GridViewRow;
        string itemDes = gvItemList.DataKeys[row.RowIndex].Value.ToString();


        Requisition_Item rItem = RequisitionControl.findByReqIDItemCode(id, itemDes);
        string iCode = rItem.ItemCode;
        int rId = rItem.RequisitionID;
        RequisitionControl.removeRequisitionItem(rId, iCode);

        showAllItems();
    }

    protected void ReqRow_Updating(object sender, GridViewUpdateEventArgs e)
    {
        vsQty.Enabled = true;
        System.Web.UI.WebControls.TextBox qtyText = (System.Web.UI.WebControls.TextBox)gvItemList.Rows[e.RowIndex].FindControl("txtQuantity");
        int newQty = Convert.ToInt32(qtyText.Text);

        System.Web.UI.WebControls.Label codeLabel = (System.Web.UI.WebControls.Label)gvItemList.Rows[e.RowIndex].FindControl("lblCode");
        string itemDesc = codeLabel.Text;

        Requisition_Item item = RequisitionControl.findByReqIDItemCode(id, itemDesc);
        string iCode = item.ItemCode;
        int rId = item.RequisitionID;

        RequisitionControl.updateRequisitionItem(rId, iCode, newQty);

        gvItemList.EditIndex = -1;
        showAllItems();
    }

    protected void RowEdit(object sender, GridViewEditEventArgs e)
    {
        gvItemList.EditIndex = e.NewEditIndex;
        showAllItems();
    }

    protected void RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvItemList.EditIndex = -1;
        showAllItems();
    }
    protected void BtnUpdate_Click(object sender, EventArgs e)
    {
        btnAdd.Visible = true;
        gvItemList.Visible = true;
        gvItemListView.Visible = false;
        btnUpdate.Visible = false;
        btnSave.Visible = true;
    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        btnSave.Visible = false;
        btnUpdate.Visible = true;
        gvItemListView.Visible = true;
        gvItemList.Visible = false;
        btnAdd.Visible = false;
        pnlAddNew.Visible = false;
    }
    protected void BtnBack_Click(object sender, EventArgs e)
    {
            Response.Redirect("~/DepartmentRepresentative/RequisitionListDepRep.aspx");
    }
}

