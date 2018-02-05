using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//AUTHOR : KHIN MYO MYO SHWE
public partial class DepartmentList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DeptController deptController = new DeptController();
        if (!IsPostBack)
        {



            GridViewDept.DataSource =EFBroker_DeptEmployee.GetDepartDetailInfoList();

            StationeryEntities smodel = new StationeryEntities();
            GridViewDept.DataSource = deptController.GetDepartDetailInfoList();

            GridViewDept.DataBind();

        }
    }
}