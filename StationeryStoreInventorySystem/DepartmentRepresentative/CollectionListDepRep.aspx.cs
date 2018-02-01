using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ReqisitionListEmployee : System.Web.UI.Page
{
    string searchWord = "";
    Employee emp = new Employee();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["emp"] != null)
            {
                emp = (Employee)Session["emp"];
                //Approved requisition

                    Label5.Visible = false;
                    GridView1.DataSource = RequisitionControl.getCollectionList(emp.DeptCode);
                    GridView1.DataBind();
                    ViewState["DataSource"] = "displayAll";
                showEmptyLabel();
            }
            else
            {
                Utility.logout();
            }
        }
    }

    public void showEmptyLabel()
    {
        if (GridView1.Rows.Count <= 0)
        {
            Label2.Visible = true;
            Label2.Text = "No Requisition Found.";
        }
        else
        {
            Label2.Visible = false;
        }
    }

    protected void SearchBtn_Click(object sender, EventArgs e)
    {
        if (Session["emp"] != null)
        {
            searchWord = SearchBox.Text;
            if (SearchBox.Text == String.Empty)
            {
                ClientScript.RegisterStartupScript(Page.GetType(),
                "MessageBox",
                "<script language='javascript'>alert('" + "Please enter value to search!" + "');</script>");
            }
            else
            {
                GridView1.DataSource = RequisitionControl.DisplayCollectionListSearch(emp.DeptCode, searchWord.Trim());
                GridView1.DataBind();
                ViewState["DataSource"] = "displaySearch";
                showEmptyLabel();
            }
        }
        else

        {
            Utility.logout();
        }
    }
    protected void DisplayBtn_Click(object sender, EventArgs e)
    {
        if (Session["emp"] != null)
        {
            Employee emp = (Employee)Session["emp"];
            GridView1.DataSource = RequisitionControl.getCollectionList(emp.DeptCode);
            GridView1.DataBind();
            ViewState["DataSource"] = "displayAll";
            showEmptyLabel();
        }
        else
        {
            Utility.logout();
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Employee emp = (Employee)Session["emp"];
        GridView1.PageIndex = e.NewPageIndex;
        if(((string)ViewState["DataSource"]).Equals("displayAll"))
        {
            GridView1.DataSource = RequisitionControl.getCollectionList(emp.DeptCode);
        }
        else
        {
            GridView1.DataSource = RequisitionControl.DisplayCollectionListSearch(emp.DeptCode, searchWord.Trim());
        }
        GridView1.DataBind();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label statusLabel = (Label)e.Row.FindControl("Label4");

            string status = statusLabel.Text;
            if (status == "Approved" || status == "approved" || status == "InProgress")
            {
                statusLabel.ForeColor = System.Drawing.Color.Green;
            }
            else if (status == "Priority")
            {
                statusLabel.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                statusLabel.ForeColor = System.Drawing.Color.Black;
            }
        }
    }
}