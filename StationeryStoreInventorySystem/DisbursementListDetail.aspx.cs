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

        if(!IsPostBack)
        {
            gvDisbDetail.DataSource = DisbursementCotrol.gvDisbursementDetailPopulate();
            gvDisbDetail.DataBind();
        }
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


        List<RegenerateRequestItems> shortfallItem = new List<RegenerateRequestItems>();
        RegenerateRequestItems regReq;

        foreach (GridViewRow r in gvDisbDetail.Rows)
        {
            int actualQty = Convert.ToInt32((r.FindControl("txtactualQty") as TextBox).Text);
            int reqQty = Convert.ToInt32((r.FindControl("lblreqQty") as Label).Text);
                        
            if (actualQty < reqQty)
            {
                string iCode = (r.FindControl("hdnflditemCode") as HiddenField).Value;
                string iDesc = (r.FindControl("lblitemDesc") as Label).Text;
                int shortfallQty = reqQty - actualQty;  
                regReq = new RegenerateRequestItems(iCode, iDesc, shortfallQty);
                shortfallItem.Add(regReq);
            }
        }
        if (shortfallItem.Count != 0)
        {
            Session["RegenerateDate"] = DisbursementCotrol.getRegenrateDate();
            Session["RegenerateDep"] = lblDepartment.Text;
            Session["RegrenerateItems"] = shortfallItem;
            Response.Redirect("~/RegenerateRequest.aspx");
        }
        else
        {
            Response.Redirect("~/DisbursementList.aspx");
        }
    }
}