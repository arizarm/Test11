using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DepartmentList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            StationeryEntities smodel = new StationeryEntities();
            GridViewDept.DataSource = EFBroker_DeptEmployee.GetDepartList();
            GridViewDept.DataBind();

        }
    }
}