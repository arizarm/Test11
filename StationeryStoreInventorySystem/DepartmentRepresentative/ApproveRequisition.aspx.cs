using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ApproveRequisition : System.Web.UI.Page
{
    StationeryEntities context = new StationeryEntities();

    int id = 0;
    string des;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["id"] != null)
        {
            id = Convert.ToInt32(Request.QueryString["id"]);

            ReqisitionListItem r = RequisitionControl.getRequisitionForApprove(id);

            lblRequestBy.Text = r.EmployeeName;
            lblDate.Text = r.Date;
            lblStatus.Text = r.Status;
            if (lblStatus.Text.Equals("Approved") || lblStatus.Text.Equals("approved") || lblStatus.Text.Equals("InProgress"))
            {
                lblStatus.ForeColor = System.Drawing.Color.Green;
            }
            else if (lblStatus.Text.Equals("Pending"))
            {
                lblStatus.ForeColor = System.Drawing.Color.Blue;
            }
            else if (lblStatus.Text.Equals("Priority"))
            {
                lblStatus.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                lblStatus.ForeColor = System.Drawing.Color.Black;
            }
        }
        else
        {
            Utility.logout();
        }
        if (!IsPostBack)
        {
            showAllItems();
        }
    }

    protected void showAllItems()
    {
        gvItemList.DataSource = RequisitionControl.getList(id);
        gvItemList.DataBind();
    }
    protected void BtnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/DepartmentRepresentative/CollectionListDepRep.aspx");
    }
}