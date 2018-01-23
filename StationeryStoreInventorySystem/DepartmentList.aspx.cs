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

<<<<<<< HEAD

            GridViewDept.DataSource = DeptBusinessLogic.GetDepartDetailInfoList();
=======
            StationeryEntities smodel = new StationeryEntities();
            GridViewDept.DataSource = EFBroker_DeptEmployee.GetDepartList();
>>>>>>> 78d17ece77be94045f99549cd09a91f32f56e79d
            GridViewDept.DataBind();

        }
    }
}