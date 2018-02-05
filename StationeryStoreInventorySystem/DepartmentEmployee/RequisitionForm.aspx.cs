using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.Transactions;
using System.Threading;


//AUTHOR : APRIL SHAR
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
        lblDate.Text = DateTime.Now.ToLongDateString();
        emp = (Employee)Session["emp"];
        if (!IsPostBack)
        {
            int id = Convert.ToInt32(RequisitionControl.getLastReq()) + 1;
            //Label3.Text = "Form: "+emp.DeptCode+"/" + id;
            lblFormTitle.Text = "Form: " + emp.DeptCode+"/ " + id;
            
            ViewState["list"] = rItem;
            ddlItem.DataSource = RequisitionControl.getItem();
            ddlItem.DataTextField = "Description";
            ddlItem.DataValueField = "ItemCode";
            ddlItem.DataBind();
        }
        rItem = (List<RequestedItem>)ViewState["list"];
        des = ddlItem.SelectedItem.ToString();
        code = ddlItem.SelectedValue.ToString();
        lblUom.Text = RequisitionControl.getUOM(code);
    }

    protected void BtnAdd_Click(object sender, EventArgs e)
    {
        //reqItem = new ArrayList();
        des = ddlItem.SelectedItem.ToString();
        int qty = Convert.ToInt32(txtQuantity.Text);

        if (gvItemList.Rows.Count <= 0)
        {
            ri = new RequestedItem(code, des, qty, lblUom.Text);
            rItem = (List<RequestedItem>)ViewState["list"];
            rItem.Add(ri);
            //reqItem.Add(ri);
            ViewState["list"] = rItem;
        }

        else
        {
            bool isEqual = false;
            foreach (GridViewRow row in gvItemList.Rows)
            {
                System.Web.UI.WebControls.Label labelDes = (System.Web.UI.WebControls.Label)row.FindControl("lblCode");
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
                ri = new RequestedItem(code, des, qty, lblUom.Text);
                rItem = (List<RequestedItem>)ViewState["list"];
                rItem.Add(ri);
                ViewState["list"] = rItem;
            }
        }
        BindGrid();
    }

    public void BindGrid()
    {
        gvItemList.DataSource = rItem;
        gvItemList.DataBind();
    }

    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        if (Session["emp"] != null)
        {
            emp = (Employee)Session["emp"];
            int RequestedBy = emp.EmpID;
            string DeptCode = emp.DeptCode;
            if (gvItemList.Rows.Count <= 0)
            {
                Response.Write("<script>alert('You have not requested any item yet!');</script>");
            }
            else
            {
                RequisitionControl.addNewRequisitionItem(rItem, DateTime.Now, "Pending", RequestedBy, DeptCode);

                DeptController dc = new DeptController();
                Employee tempHead = EmployeeController.GetDeptHeadTempHeadEmail(emp);
                Employee deptHead = dc.GetDHeadByDeptCode(emp.DeptCode);

                if(tempHead != null)
                {
                    string mail = tempHead.Email;
                    string receiver = mail;
                    Thread emailThreadWithParam = new Thread(() => TempMailNotification(receiver));
                    emailThreadWithParam.Start();
                    
                }

               
                if (deptHead != null)
                {
                    string mail1 = deptHead.Email;
                    string receiver1 = mail1;
                    Thread emailThreadWithParam1 = new Thread(() => HeadMailNotification(receiver1));
                    emailThreadWithParam1.Start();
                   
                }
                Response.Redirect(LoginController.RequisitionListDepEmpURI);
            }
            //Response.Write("<script language='javascript'>alert('Requisition Submitted');</script>");
            //Server.Transfer("RequisitionListDepartment.aspx", true);            
        }
        else
        {
            Utility.logout();
        }
    }

    protected void BtnDelete_Click(object sender, EventArgs e)
    {
        GridViewRow row = ((System.Web.UI.WebControls.Button)sender).Parent.Parent as GridViewRow;
        string itemDes = gvItemList.DataKeys[row.RowIndex].Value.ToString();

        RequestedItem i = rItem.Find(r => r.Code.Equals(itemDes));

        rItem = (List<RequestedItem>)ViewState["list"];
        rItem.Remove(i);
        ViewState["list"] = rItem;

        BindGrid();
    }

    protected void ReqRow_Updating(object sender, GridViewUpdateEventArgs e)
    {
        vsQuantity.Enabled = true;
        System.Web.UI.WebControls.TextBox qtyText = (System.Web.UI.WebControls.TextBox)gvItemList.Rows[e.RowIndex].FindControl("txtQty");
        int newQty = Convert.ToInt32(qtyText.Text);

        System.Web.UI.WebControls.Label itemDescLabel = (System.Web.UI.WebControls.Label)gvItemList.Rows[e.RowIndex].FindControl("lblCode");
        string code = itemDescLabel.Text;

        RequestedItem i = rItem.Find(r => r.Code.Equals(code));
        rItem = (List<RequestedItem>)ViewState["list"];

        i.Quantity = Convert.ToInt32(newQty);
        rItem[e.RowIndex].Quantity = i.Quantity;
        ViewState["list"] = rItem;

        gvItemList.EditIndex = -1;
        BindGrid();
    }

    protected void RowEdit(object sender, GridViewEditEventArgs e)
    {
        gvItemList.EditIndex = e.NewEditIndex;
        BindGrid();
    }

    protected void RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvItemList.EditIndex = -1;
        BindGrid();
    }

    private void TempMailNotification(String receiver)
    {
        string subject = "New Requisition";
        string body = "Dear Acting Department Head,\nOne of your employees has made a new requisition. Please check and see for more information.";
        Utility.sendMail(receiver, subject, body);
    }

    private void HeadMailNotification(String receiver1)
    {
        string subject1 = "New Requisition";
        string body1 = "Dear Department Head,\nOne of your employees has made a new requisition. Please check and see for more information.";
        Utility.sendMail(receiver1, subject1, body1);
    }
}
