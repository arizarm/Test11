using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Store_RequisitionDetail : System.Web.UI.Page
{
    StationeryEntities context = new StationeryEntities();

    int id = 0;
    string des;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["RequisitionNo"] != null)
        {
            id = Convert.ToInt32(Session["RequisitionNo"]);

            ReqisitionListItem r = RequisitionControl.getRequisitionForApprove(id);

            Label1.Text = r.EmployeeName;
            Label2.Text = r.Date;
            Label3.Text = r.Status;

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
        GridView1.DataSource = RequisitionControl.getList(id);
        GridView1.DataBind();
    }

}