﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.Transactions;

public partial class RequisitionForm : System.Web.UI.Page
{
    //ReqBS bs = new ReqBS();
    ArrayList reqItem =new ArrayList();
    RequestedItem ri;
    string des;
    protected void Page_Load(object sender, EventArgs e)
    {
        Label1.Text = DateTime.Now.ToLongDateString();
        if(!IsPostBack)
        {
            int id = Convert.ToInt32(RequisitionControl.getLastReq()) + 1;
            Label3.Text = "Form:ENGL/" + id;
            ViewState["list"] = reqItem;
            DropDownList1.DataSource = RequisitionControl.getItem();
            DropDownList1.DataBind();
        }
        reqItem = (ArrayList)ViewState["list"];
        des = DropDownList1.SelectedItem.ToString();
        Label2.Text = RequisitionControl.getUOM(des);
        Label4.Text = RequisitionControl.getCode(des);
        Label4.Visible = false;
    }

    protected void Add_Click(object sender, EventArgs e)
    {
        //reqItem = new ArrayList();
        des = DropDownList1.SelectedItem.ToString();
        int qty = Convert.ToInt32(TextBox4.Text);

        if (GridView1.Rows.Count <= 0)
        {
            ri = new RequestedItem(Label4.Text, des, qty, Label2.Text);
            reqItem = (ArrayList)ViewState["list"];
            reqItem.Add(ri);
            ViewState["list"] = reqItem;
        }
        else
        {
            bool isEqual = false;
            foreach(GridViewRow row in GridView1.Rows)
            {
               if(Label4.Text.Equals(row.Cells[0].Text))
                {
                    isEqual = true;
                }
            }
            if(isEqual)
            {
                Response.Write("<script>alert('Item is already in the form.');</script>");
            }
            else
            {
                ri = new RequestedItem(Label4.Text, des, qty, Label2.Text);
                reqItem = (ArrayList)ViewState["list"];
                reqItem.Add(ri);
                ViewState["list"] = reqItem;
            }
        }

        GridView1.DataSource = reqItem;
        GridView1.DataBind();
    }

    protected void Submit_Click(object sender, EventArgs e)
    {
        if(GridView1.Rows.Count<=0)
        {
            Response.Write("<script>alert('You have not requested any item yet!');</script>");
        }
        using (TransactionScope ts = new TransactionScope())
        {
            StationeryEntities context = new StationeryEntities();
            Requisition r = new Requisition();
            r.RequestDate = DateTime.Now;
            r.Status = "Pending";
            r.RequestedBy = 1028;

            context.Requisitions.Add(r);
            context.SaveChanges();

            foreach (GridViewRow row in GridView1.Rows)
            {
                Requisition_Item ri = new Requisition_Item();
                ri.RequisitionID=r.RequisitionID;
                //string code = row.Cells[0].Text;
                ri.ItemCode = row.Cells[0].Text;
                ri.RequestedQty = Convert.ToInt32(row.Cells[2].Text);
                context.Requisition_Item.Add(ri);
                context.SaveChanges();
            }
            
            ts.Complete();
        }
        //Response.Write("<script language='javascript'>alert('Requisition Submitted');</script>");
        //Server.Transfer("RequisitionListDepartment.aspx", true);
        //Response.Redirect("ReqisitionListDepartment.aspx");

    }
}

[Serializable]
public class RequestedItem
{
    private string code;
    private string description;
    private int quantity;
    private string uom;

    public RequestedItem(string code, string description,int quantity, string uom)
    {
        this.code=code;
        this.description = description;
        this.quantity = quantity;
        this.uom = uom;
    }

    public string Code { get { return code; } set { code = value; } }
    public string Description { get { return description; } set { description = value; } }
    public int Quantity { get { return quantity; } set { quantity = value; } }
    public string Uom { get { return uom; } set { uom = value; } }
}
