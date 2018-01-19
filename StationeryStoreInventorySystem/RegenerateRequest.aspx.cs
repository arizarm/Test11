using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RegenerateRequest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string date = (string) Session["RegenerateDate"];
        string depName = (string)Session["RegenerateDep"];
        lblReqDate.Text = date;
        lblDepartment.Text = depName;
        lblReqBy.Text = DisbursementCotrol.getDepRep(depName);
        string status = "Prioirty";
        List<RegenerateRequestItems> shortfallItem = (List < RegenerateRequestItems >) Session["RegrenerateItems"];
        if(!IsPostBack)
        {
            gvRegenerate.DataSource = shortfallItem;
            gvRegenerate.DataBind();
        }        
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
}