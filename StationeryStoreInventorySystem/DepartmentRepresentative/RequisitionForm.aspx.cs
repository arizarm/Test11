using System;
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
    List<RequestedItem> rItem = new List<RequestedItem>();
    //ArrayList reqItem =new ArrayList();
    static RequestedItem ri;
    string code;
    Employee emp;
    string des;

    protected void Page_Load(object sender, EventArgs e)
    {
        Label1.Text = DateTime.Now.ToLongDateString();
        emp = (Employee)Session["emp"];
        if (!IsPostBack)
        {
            int id = Convert.ToInt32(RequisitionControl.getLastReq()) + 1;
            //Label3.Text = "Form: "+emp.DeptCode+"/" + id;
            Label3.Text = "Form: " + "/ " + id;

            ViewState["list"] = rItem;
            DropDownList1.DataSource = RequisitionControl.getItem();
            DropDownList1.DataTextField = "Description";
            DropDownList1.DataValueField = "ItemCode";
            DropDownList1.DataBind();
        }
        rItem = (List<RequestedItem>)ViewState["list"];
        des = DropDownList1.SelectedItem.ToString();
        code = DropDownList1.SelectedValue.ToString();
        Label2.Text = RequisitionControl.getUOM(code);
        //Label4.Text = RequisitionControl.getCode(des);
        Label4.Visible = false;
    }

    protected void Add_Click(object sender, EventArgs e)
    {
        //reqItem = new ArrayList();
        des = DropDownList1.SelectedItem.ToString();
        int qty = Convert.ToInt32(TextBox4.Text);

        if (GridView2.Rows.Count <= 0)
        {
            ri = new RequestedItem(code, des, qty, Label2.Text);
            rItem = (List<RequestedItem>)ViewState["list"];
            rItem.Add(ri);
            //reqItem.Add(ri);
            ViewState["list"] = rItem;
        }

        else
        {
            bool isEqual = false;
            foreach (GridViewRow row in GridView2.Rows)
            {
                System.Web.UI.WebControls.Label labelDes = (System.Web.UI.WebControls.Label)row.FindControl("code");
                string item = labelDes.Text;

                if (code.Equals(item))
                {
                    isEqual = true;
                }
            }
            if (isEqual)
            {
                Response.Write("<script>alert('Item is already in the form.');</script>");
            }
            else
            {
                ri = new RequestedItem(code, des, qty, Label2.Text);
                rItem = (List<RequestedItem>)ViewState["list"];
                rItem.Add(ri);
                ViewState["list"] = rItem;
            }
        }
        bindGrid();
    }

    public void bindGrid()
    {
        GridView2.DataSource = rItem;
        GridView2.DataBind();
    }

    protected void Submit_Click(object sender, EventArgs e)
    {
        if (Session["emp"] != null)
        {
            emp = (Employee)Session["emp"];
            int RequestedBy = emp.EmpID;
            string DeptCode = emp.DeptCode;
            if (GridView2.Rows.Count <= 0)
            {
                Response.Write("<script>alert('You have not requested any item yet!');</script>");
            }
            else
            {
                RequisitionControl.addNewRequisitionItem(rItem, DateTime.Now, "Pending", RequestedBy, DeptCode);
                //string receiver = "@gmail.com";
                //string subject = "New Requisition";
                //string body = "Dear Department Head,\nOne of your employees has made a new requisition. Please check and see for more information.";
                //Utility.sendMail(receiver, subject, body);
                Response.Redirect(LoginController.RequisitionListTempDepRedURI);
            }
            //Response.Write("<script language='javascript'>alert('Requisition Submitted');</script>");
            //Server.Transfer("RequisitionListDepartment.aspx", true);            
        }
        else
        {
            Utility.logout();
        }
    }

    protected void Delete_Click(object sender, EventArgs e)
    {
        GridViewRow row = ((System.Web.UI.WebControls.Button)sender).Parent.Parent as GridViewRow;
        string itemDes = GridView2.DataKeys[row.RowIndex].Value.ToString();

        RequestedItem i = rItem.Find(r => r.Code.Equals(itemDes));

        rItem = (List<RequestedItem>)ViewState["list"];
        rItem.Remove(i);
        ViewState["list"] = rItem;

        bindGrid();
    }

    protected void ReqRow_Updating(object sender, GridViewUpdateEventArgs e)
    {
        ValidationSummary1.Enabled = true;
        System.Web.UI.WebControls.TextBox qtyText = (System.Web.UI.WebControls.TextBox)GridView2.Rows[e.RowIndex].FindControl("qtyText");
        int newQty = Convert.ToInt32(qtyText.Text);

        System.Web.UI.WebControls.Label itemDescLabel = (System.Web.UI.WebControls.Label)GridView2.Rows[e.RowIndex].FindControl("code");
        string code = itemDescLabel.Text;

        RequestedItem i = rItem.Find(r => r.Code.Equals(code));
        rItem = (List<RequestedItem>)ViewState["list"];

        i.Quantity = Convert.ToInt32(newQty);
        rItem[e.RowIndex].Quantity = i.Quantity;
        ViewState["list"] = rItem;

        GridView2.EditIndex = -1;
        bindGrid();
    }

    protected void RowEdit(object sender, GridViewEditEventArgs e)
    {
        GridView2.EditIndex = e.NewEditIndex;
        bindGrid();
    }

    protected void RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView2.EditIndex = -1;
        bindGrid();
    }
}
