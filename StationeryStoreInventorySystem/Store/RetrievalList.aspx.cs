using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RetrievalList : System.Web.UI.Page
{
    RetrievalControl retCon = new RetrievalControl();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // transfer from RetrievedBy(employee ID) to employee name
            List<Retrieval> retList = retCon.DisplayRetrievalList();
            Dictionary<Retrieval, string> retDisplay = new Dictionary<Retrieval, string>();
            foreach (Retrieval r in retList)
            {
                
                retDisplay.Add(r, EFBroker_DeptEmployee.GetEmployeebyEmpID((int)r.RetrievedBy).EmpName);
            }

            gvReq.DataSource = retDisplay;
            gvReq.DataBind();

        }

    }

    protected void SearchBtn_Click(object sender, EventArgs e)
    {
        string searchWord = SearchBox.Text;

        List<Retrieval> retList = retCon.DisplaySearch(searchWord);
        Dictionary<Retrieval, string> retDisplay = new Dictionary<Retrieval, string>();
        foreach (Retrieval r in retList)
        {
            retDisplay.Add(r, EFBroker_DeptEmployee.GetEmployeebyEmpID((int)r.RetrievedBy).EmpName);
        }

        gvReq.DataSource = retDisplay;
        gvReq.DataBind();

    }

    protected void DisplayBtn_Click(object sender, EventArgs e)
    {
        List<Retrieval> retList = retCon.DisplayRetrievalList();
        Dictionary<Retrieval, string> retDisplay = new Dictionary<Retrieval, string>();
        foreach (Retrieval r in retList)
        {
            retDisplay.Add(r, EFBroker_DeptEmployee.GetEmployeebyEmpID((int)r.RetrievedBy).EmpName);
        }

        gvReq.DataSource = retDisplay;
        gvReq.DataBind();
    }


    protected void gvDetailBtn_Click(object sender, EventArgs e)
    {
        GridViewRow row = ((Button)sender).NamingContainer as GridViewRow;  //detail btn        
        Session["RetrievalID"] = Convert.ToInt32((row.FindControl("LabelRetrievalID") as Label).Text); //row.Cells[1]
        Response.Redirect("~/Store/RetrievalListDetail.aspx");
    }

    protected void gvReq_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label statusLabel = (Label)e.Row.FindControl("Label1");

            string status = statusLabel.Text;

            if (status == "InProgress")
            {
                statusLabel.ForeColor = System.Drawing.Color.Green;
            }
            else if (status == "Pending")
            {
                statusLabel.ForeColor = System.Drawing.Color.Blue;
            }
        }
    }
}