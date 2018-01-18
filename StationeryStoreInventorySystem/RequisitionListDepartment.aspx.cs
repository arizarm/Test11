using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ReqisitionListEmployee : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["empRole"] = "Employee";
        //Session["empRole"] = "Employee";

        if (Session["empRole"].ToString() == "Head")
        {
            GridView1.Visible = false;
            GridView2.Visible = true;
        }
        else if (Session["empRole"].ToString() == "Employee")
        {
            GridView1.Visible = true;
            GridView2.Visible = false;
        }

        if (!IsPostBack)
        {
            GridView1.DataSource = ReqBS.getRequisitionList();
            GridView1.DataBind();
            GridView2.DataSource = ReqBS.getRequisitionList();
            GridView2.DataBind();
        }
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        string selectedStatus = DropDownList1.SelectedValue;

        GridView1.DataSource = ReqBS.getRequisitionListByStatus(selectedStatus);
        GridView1.DataBind();

        GridView2.DataSource = ReqBS.getRequisitionListByStatus(selectedStatus);
        GridView2.DataBind();
    }

}