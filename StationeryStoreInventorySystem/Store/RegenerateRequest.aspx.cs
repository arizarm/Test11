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
        string requestedBy = (string)Session["RequestedByName"];
        List<RequestedItem> shortfallItem = (List<RequestedItem>)Session["RegrenerateItems"];

        if (!IsPostBack)
        {
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

        DateTime date = (DateTime)Session["RegenerateDate"];
        string depName = (string)Session["RegenerateDep"]; ;
        string requestedBy = (string)Session["RequestedBy"];
        int empID = EFBroker_DeptEmployee.GetDeptRepEmpIDByDeptCode(depName);
        string depCode = EFBroker_DeptEmployee.GetDepartByEmpID(empID).DeptCode;

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

        RequisitionControl.addNewRequisitionItem(regenerateItem, date, status,empID, depCode);

        redirectCheck();
    }

    protected void redirectCheck()
    {
        if (((Dictionary<Item, int>)Session["discrepancyList"]).Count != 0)
        {
            Session["ItemToUpdate"] = true;          
            Response.Redirect(LoginController.GenerateDiscrepancyAdhocV2URI);
        }
        else
        {
            Response.Redirect(LoginController.DisbursementListURI);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        redirectCheck();
    }
}