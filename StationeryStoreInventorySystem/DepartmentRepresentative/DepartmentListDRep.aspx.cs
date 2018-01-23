using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DepartmentListDRep : System.Web.UI.Page
{
    static string dcode = "BDTD";

    protected void Page_Load(object sender, EventArgs e)
    {
        Session["deptcode"] = dcode;
        if (!IsPostBack)
        {
            if (DeptBusinessLogic.GetEmployeeListForActingDHeadSelectedCount(dcode) <= 0)
            {
                Department dept = DeptBusinessLogic.GetDepartByDepCode(dcode);
                Employee emp = DeptBusinessLogic.GetDHeadByDeptCode(dcode);
                Employee empDRep = DeptBusinessLogic.GetEmployeeListForDRepSelected(dcode);
                string aheadname = "No Acting Head";
                string detpRname = empDRep.EmpName;
                string dname = dept.DeptName;
                string contactname = dept.DeptContactName;
                string telephone = dept.DeptTelephone;
                string fax = dept.DeptFax;
                string dheadname = emp.EmpName;



                lblDeptName.Text = dname;
                lblContactName.Text = contactname;
                lblPhone.Text = telephone;
                lblFax.Text = fax;
                lblHeadname.Text = dheadname;
                lblActingDHead.Text = aheadname;
                lblActingDHead.ForeColor = System.Drawing.Color.Red;
                lblDeptRep.Text = detpRname;
            }
            else
            {
                Department dept = DeptBusinessLogic.GetDepartByDepCode(dcode);
                Employee emp = DeptBusinessLogic.GetDHeadByDeptCode(dcode);
                Employee empActingDHead = DeptBusinessLogic.GetEmployeeListForActingDHeadSelected(dcode);
                Employee empDRep = DeptBusinessLogic.GetEmployeeListForDRepSelected(dcode);
                string aheadname = empActingDHead.EmpName;
                string detpRname = empDRep.EmpName;
                string dname = dept.DeptName;
                string contactname = dept.DeptContactName;
                string telephone = dept.DeptTelephone;
                string fax = dept.DeptFax;
                string dheadname = emp.EmpName;



                lblDeptName.Text = dname;
                lblContactName.Text = contactname;
                lblPhone.Text = telephone;
                lblFax.Text = fax;
                lblHeadname.Text = dheadname;
                lblActingDHead.Text = aheadname;
                lblDeptRep.Text = detpRname;
            }
            //UpdateCollectionPoint
            string empCollectionname = DeptBusinessLogic.GetDepartmentForCollectionPointSelected(dcode);
            DropDownListCollectionPoint.DataSource = DeptBusinessLogic.GetCollectionPointList();
            DropDownListCollectionPoint.DataTextField = "CollectionPoint1";
            DropDownListCollectionPoint.DataValueField = "CollectionLocationID";
            DropDownListCollectionPoint.DataBind();
            DropDownListCollectionPoint.Items.FindByText(empCollectionname).Selected = true;
        }

    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {

        Session["deptcode"] = dcode;
        int c = Convert.ToInt16(DropDownListCollectionPoint.SelectedValue);
        DeptBusinessLogic.UpdateCollectionPoint(dcode, c);

        Response.Redirect("~/Department/DepartmentDetailInfo.aspx");

    }
}