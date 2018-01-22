using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DepartmentListActingDHead : System.Web.UI.Page
{
    static string dcode = "BDTD";
    Employee empActingDHead = DeptBusinessLogic.GetEmployeeListForActingDHeadSelected(dcode);
    Employee empDRep = DeptBusinessLogic.GetEmployeeListForDRepSelected(dcode);
    protected void Page_Load(object sender, EventArgs e)
    {

        Session["deptcode"] = dcode;
        if (!IsPostBack)
        {
            Department dept = DeptBusinessLogic.GetDepartByDepCode(dcode);
            Employee emp = DeptBusinessLogic.GetDHeadByDeptCode(dcode);

            string aheadname = empActingDHead.EmpName;
            string dname = dept.DeptName;
            string contactname = dept.DeptContactName;
            string telephone = dept.DeptTelephone;
            string fax = dept.DeptFax;
            string dheadname = emp.EmpName;
            string startdate = empActingDHead.StartDate.GetValueOrDefault().ToShortDateString();
            string enddate = empActingDHead.EndDate.GetValueOrDefault().ToShortDateString();


            //DateTime? endTime = emp.EndDate;
            lblDeptName.Text = dname;
            lblContactName.Text = contactname;
            lblPhone.Text = telephone;
            lblFax.Text = fax;
            lblHeadname.Text = dheadname;
            lblActingDHead.Text = aheadname;
            lblStartD.Text = startdate;
            lblEndD.Text = enddate;
            int empRid = empDRep.EmpID;//ForDeptRep Id

            //UpdateActingDHead   
            string empActingDHeadname = empActingDHead.EmpName;
            int empid = empActingDHead.EmpID;


            //UpdateDeptRp
            string empDRepname = empDRep.EmpName;
            DropDownListDRep.DataSource = DeptBusinessLogic.GetEmployeeListForDRep(dcode, empid);
            DropDownListDRep.DataTextField = "EmpName";
            DropDownListDRep.DataValueField = "EmpID";
            DropDownListDRep.DataBind();
            DropDownListDRep.Items.FindByText(empDRepname).Selected = true;

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

        int empid = Convert.ToInt16(DropDownListDRep.SelectedValue);
        DeptBusinessLogic.UpdateDeptRep(dcode, empid);

        Response.Redirect("DepartmentDetailInfo.aspx");


    }




}