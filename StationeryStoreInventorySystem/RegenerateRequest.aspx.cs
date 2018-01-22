using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RegenerateRequest : System.Web.UI.Page
{
    static DateTime date;
    static string depName;
    static string requestedBy;
    static string status = "Priority";

    List<RequestedItem> shortfallItem = new List<RequestedItem>();
    List<RequestedItem> regenerateItem = new List<RequestedItem>();    

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            date = (DateTime)Session["RegenerateDate"];
            depName = (string)Session["RegenerateDep"];
            shortfallItem = (List<RequestedItem>)Session["RegrenerateItems"];
            requestedBy = DisbursementCotrol.getDepRep(depName);
            gvRegenerate.DataSource = shortfallItem;
            gvRegenerate.DataBind();
        }

        lblReqDate.Text = date.ToLongDateString();
        lblDepartment.Text = depName;
        lblReqBy.Text = requestedBy;

    }

    protected void CheckAll_CheckedChanged(object sender, EventArgs e)
    {
        if (((CheckBox)gvRegenerate.HeaderRow.FindControl("CheckAll")).Checked)
        {
            foreach (GridViewRow row in gvRegenerate.Rows)
            {
                ((CheckBox)row.FindControl("CheckBox")).Checked = true;
            }
        }

        if (!((CheckBox)gvRegenerate.HeaderRow.FindControl("CheckAll")).Checked)
        {
            foreach (GridViewRow row in gvRegenerate.Rows)
            {
                ((CheckBox)row.FindControl("CheckBox")).Checked = false;
            }
        }
    }

    protected void btnReGenReq_Click(object sender, EventArgs e)
    {
        foreach(GridViewRow r in gvRegenerate.Rows)
        {
            if( ( (CheckBox) r.FindControl("CheckBox")).Checked)
            {
                int i = r.RowIndex;
                regenerateItem.Add(shortfallItem[i]);
            }
        }
        DisbursementCotrol.addNewRequisitionItem(regenerateItem, date, status, DisbursementCotrol.getEmpIdbyEmpName(requestedBy));

        ModalPopupExtender.Show();
    }

    protected void btnOkay_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/DisbursementList.aspx");
    }
}