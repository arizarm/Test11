using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//AUTHOR : CHOU MING SHENG
public partial class Store_RequisitionDetail : System.Web.UI.Page
{
    int id = 0;
    string des;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["RequisitionNo"] != null)
        {
            id = Convert.ToInt32(Session["RequisitionNo"]);

            ReqisitionListItem r = RequisitionControl.getRequisitionForApprove(id);

            lblRequestedBy.Text = r.EmployeeName;
            lblDate.Text = r.Date;
            lblStatus.Text = r.Status;

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
        gvDetail.DataSource = RequisitionControl.getList(id);
        gvDetail.DataBind();
    }

}