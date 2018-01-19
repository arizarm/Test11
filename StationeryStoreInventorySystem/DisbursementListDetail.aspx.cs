using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DisbursementListDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string disbId = Session["SelectedDisb"].ToString();

        DisbursementListItems disb = DisbursementCotrol.DisbursementListItemsObj(disbId);

        lblDate.Text = disb.CollectionDate.ToString();
        lblTime.Text = disb.CollectionTime.ToString();
        lblDepartment.Text = disb.DepName.ToString();
        lblColPoint.Text = disb.CollectionPoint.ToString();

        gvDisbDetail.DataSource = DisbursementCotrol.gvDisbursementDetailPopulate();
        gvDisbDetail.DataBind();
    }


    protected void btnAck_Click(object sender, EventArgs e)
    {
        string message;

        if (DisbursementCotrol.checkAccessCode(txtAccessCode.Text))
        {
            message = "Disbursement Acknowledgement Successful!";
        }
        else
        {
            message = "Incorrect Access Code!";
        }
        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + "');", true);

        //TO FOLLOW UP
        //List<string> itemCode = new List<string>();

        //foreach (GridViewRow r in gvDisbDetail.Rows)
        //{
        //    int actuaQty = Convert.ToInt32((r.FindControl("actualQty") as Label).Text);
        //    int reqQty = Convert.ToInt32((r.FindControl("reqQty") as Label).Text);

        //    if (reqQty < actuaQty)
        //    {
        //        itemCode.Add((r.FindControl("hdnflditemCode") as Label).Text);
        //    }
        //}
        //if (itemCode.Count != 0)
        //{
        //    Session["RegrenerateItems"] = itemCode;
        //    Response.Redirect("~/RegenerateRequest.aspx");
        //}  
        //else
        //{
        //    Response.Redirect("~/DisbursementList.aspx");
        //}      
    }
}