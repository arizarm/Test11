using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Department_DepartmentDetailInfo : System.Web.UI.Page
{
    static string dcode = "BDTD";

    protected void Page_Load(object sender, EventArgs e)
    {

        Session["deptcode"] = dcode;
        if (!IsPostBack)
        {
            if (DeptBusinessLogic.GetEmployeeListForActingDHeadSelectedCount(dcode) <= 0)
            {


                Employee empDRep = DeptBusinessLogic.GetEmployeeListForDRepSelected(dcode);
                Department dept = DeptBusinessLogic.GetDepartByDepCode(dcode);
                Employee emp = DeptBusinessLogic.GetDHeadByDeptCode(dcode);

                string aheadname = "No Acting Head";
                string detpRname = empDRep.EmpName;
                string dname = dept.DeptName;
                string contactname = dept.DeptContactName;
                string telephone = dept.DeptTelephone;
                string fax = dept.DeptFax;
                string dheadname = emp.EmpName;
                string empCollectionname = DeptBusinessLogic.GetDepartmentForCollectionPointSelected(dcode);

                
                lblDeptName.Text = dname;
                lblContactName.Text = contactname;
                lblPhone.Text = telephone;
                lblFax.Text = fax;
                lblHeadname.Text = dheadname;

                lblActingDHead.Text = aheadname;
                lblActingDHead.ForeColor = System.Drawing.Color.Red;
                lblDeptRep.Text = detpRname;
                lblCollectPoint.Text = empCollectionname;

            }
            else
            {
                Employee empActingDHead = DeptBusinessLogic.GetEmployeeListForActingDHeadSelected(dcode);
                Employee empDRep = DeptBusinessLogic.GetEmployeeListForDRepSelected(dcode);
                Department dept = DeptBusinessLogic.GetDepartByDepCode(dcode);
                Employee emp = DeptBusinessLogic.GetDHeadByDeptCode(dcode);

                string aheadname = empActingDHead.EmpName;
                string detpRname = empDRep.EmpName;
                string dname = dept.DeptName;
                string contactname = dept.DeptContactName;
                string telephone = dept.DeptTelephone;
                string fax = dept.DeptFax;
                string dheadname = emp.EmpName;
                string empCollectionname = DeptBusinessLogic.GetDepartmentForCollectionPointSelected(dcode);


                lblDeptName.Text = dname;
                lblContactName.Text = contactname;
                lblPhone.Text = telephone;
                lblFax.Text = fax;
                lblHeadname.Text = dheadname;
                lblActingDHead.Text = aheadname;
                lblDeptRep.Text = detpRname;
                lblCollectPoint.Text = empCollectionname;
            }

        }

    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {

        Session["deptcode"] = dcode;

        if (Session["userType"] == "All")
        {
            Response.Redirect("DepartmentListDHead.aspx");
        }
        else
        {
            Response.Redirect("DepartmentListActingDHead.aspx");
        }


    }




}

