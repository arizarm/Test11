using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RegenerateRequest : System.Web.UI.Page
{
    static string status = "Priority";

    DisbursementCotrol disbCon = new DisbursementCotrol();

    protected void Page_Load(object sender, EventArgs e)
    {
        DateTime date = (DateTime)Session["RegenerateDate"];
        string depName = (string)Session["RegenerateDep"]; ;
        List<RequestedItem> shortfallItem = (List<RequestedItem>)Session["RegrenerateItems"];

        if (!IsPostBack)
        {
            gvRegenerate.DataSource = shortfallItem;
            gvRegenerate.DataBind();
        }

        lblReqDate.Text = date.ToLongDateString();
        lblDepartment.Text = depName;
        string requestedBy = EFBroker_DeptEmployee.GetDeptRepByDeptCode(depName);
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
        List<RequestedItem> shortfallItem = (List<RequestedItem>)Session["RegrenerateItems"];

        List<RequestedItem> regenerateItem = new List<RequestedItem>();

        foreach (GridViewRow r in gvRegenerate.Rows)
        {
            if (((CheckBox)r.FindControl("CheckBox")).Checked)
            {
                int i = r.RowIndex;
                regenerateItem.Add(shortfallItem[i]);
            }
        }

        redirectCheck();
    }

    protected void redirectCheck()
    {
        if (((Dictionary<Item, int>)Session["discrepancyList"]).Count != 0)
        {
            Session["ItemToUpdate"] = true;
            Response.Redirect("~/GenerateDiscrepancyAdhocV2.aspx");
        }
        else
        {
            Response.Redirect("~/Store/DisbursementList.aspx");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        redirectCheck();
    }
}