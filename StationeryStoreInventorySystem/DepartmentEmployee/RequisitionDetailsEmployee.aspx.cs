﻿using System;
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
            Label3.Text = r.RequestDate.ToString();
            Label4.Text = r.Status.ToString();

            if (!IsPostBack)
            {

                showAllItems();
                if (r.Status != "Pending")
                {
                    Cancel.Visible = false;
                    Add.Visible = false;
                    Update.Visible = false;

                    if (!String.IsNullOrWhiteSpace(r.Remarks))
                        Label8.Text = r.Remarks.ToString();
                }

                DropDownList2.DataSource = RequisitionControl.getItem();
                DropDownList2.DataTextField = "Description";
                DropDownList2.DataValueField = "ItemCode";
                DropDownList2.DataBind();
            }

            code = DropDownList2.SelectedValue.ToString();
            Label6.Text = RequisitionControl.getUOM(code);
        }
        else
            Response.Redirect(LoginController.RequisitionListDepEmpURI);
    }

    protected void showAllItems()
    {
        GridView1.DataSource = RequisitionControl.getList(id);
        //GridView1.DataSource = q.ToList();
        GridView1.DataBind();

        GridView2.DataSource = RequisitionControl.getList(id);
        //GridView2.DataSource = q.ToList();
        GridView2.DataBind();
    }

    protected void Cancel_Click(object sender, EventArgs e)
    {
        try
        {
            id = Convert.ToInt32(Request.QueryString["requisitionNo"]);
            RequisitionControl.cancelRejectRequisition(id);

            Response.Redirect(LoginController.RequisitionListDepEmpURI);
            //Response.Write("<script language='javascript'>alert('Requisition has been cancelled');</script>");
        }
        catch (Exception)
        {
            Utility.DisplayAlertMessage("Error! Retry.");
        }
    }

    protected void Add_Click(object sender, EventArgs e)
    {
        Panel1.Visible = true;
    }

    protected void New_Click(object sender, EventArgs e)
    {
        id = Convert.ToInt32(Request.QueryString["requisitionNo"]);
        //string des = RequisitionControl.getDescription(code);
        int qty = Convert.ToInt32(TextBox1.Text);

        if (GridView1.Rows.Count <= 0)
        {
            RequisitionControl.addItemToRequisition(code, qty, id);
        }

        else
        {
            bool isEqual = false;
            string truCode = "";
            foreach (GridViewRow row in GridView1.Rows)
            {
                System.Web.UI.WebControls.Label labelDes = (System.Web.UI.WebControls.Label)row.FindControl("itemDes");
                string item = labelDes.Text;

                System.Web.UI.WebControls.Label labelCode = (System.Web.UI.WebControls.Label)row.FindControl("code");
                string iCode = labelCode.Text;

                if (code.Equals(iCode))
                {
                    isEqual = true;
                    truCode = iCode;
                }
            }
            if (isEqual)
            {
                RequiredFieldValidator2.Enabled = true;
                RangeValidator2.Enabled = true;
                RequisitionControl.editRequisitionItemQty(id, truCode, qty);
            }
            else
            {
                RequiredFieldValidator2.Enabled = true;
                RangeValidator2.Enabled = true;
                RequisitionControl.addItemToRequisition(code, qty, id);
            }
        }

        showAllItems();
    }

    protected void Close_Click(object sender, EventArgs e)
    {
        Panel1.Visible = false;
        Add.Visible = true;
        Close.Visible = false;
    }

    protected void Delete_Click(object sender, EventArgs e)
    {
        //LoadData();
        GridViewRow row = ((System.Web.UI.WebControls.Button)sender).Parent.Parent as GridViewRow;
        string itemDes = GridView1.DataKeys[row.RowIndex].Value.ToString();


        Requisition_Item rItem = RequisitionControl.findByReqIDItemCode(id, itemDes);
        string iCode = rItem.ItemCode;
        int rId = rItem.RequisitionID;
        RequisitionControl.removeRequisitionItem(rId, iCode);

        showAllItems();
    }

    protected void ReqRow_Updating(object sender, GridViewUpdateEventArgs e)
    {
        ValidationSummary1.Enabled = true;
        System.Web.UI.WebControls.TextBox qtyText = (System.Web.UI.WebControls.TextBox)GridView1.Rows[e.RowIndex].FindControl("qtyText");
        int newQty = Convert.ToInt32(qtyText.Text);

        System.Web.UI.WebControls.Label codeLabel = (System.Web.UI.WebControls.Label)GridView1.Rows[e.RowIndex].FindControl("code");
        string itemDesc = codeLabel.Text;

        Requisition_Item item = RequisitionControl.findByReqIDItemCode(id, itemDesc);
        string iCode = item.ItemCode;
        int rId = item.RequisitionID;

        RequisitionControl.updateRequisitionItem(rId, iCode, newQty);

        GridView1.EditIndex = -1;
        showAllItems();
    }

    protected void RowEdit(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        showAllItems();
    }

    protected void RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        showAllItems();
    }
    protected void Update_Click(object sender, EventArgs e)
    {
        Add.Visible = true;
        GridView1.Visible = true;
        GridView2.Visible = false;
        Update.Visible = false;
        Save.Visible = true;
    }
    protected void Save_Click(object sender, EventArgs e)
    {
        Save.Visible = false;
        Update.Visible = true;
        GridView2.Visible = true;
        GridView1.Visible = false;
        Add.Visible = false;
        Panel1.Visible = false;
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/DepartmentEmployee/RequisitionListDepEmp.aspx");
    }
}

