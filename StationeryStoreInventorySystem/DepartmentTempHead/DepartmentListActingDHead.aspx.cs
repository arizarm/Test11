using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DepartmentListActingDHead : System.Web.UI.Page
{

    DeptController deptController = new DeptController();

    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            if (Session["emp"] != null)
            {
                Employee empSession = (Employee)Session["emp"];
                string dcode = empSession.DeptCode;
                string empRole = empSession.Role;

                Employee empActingDHead = deptController.GetEmployeeListForActingDHeadSelected(dcode);
                Employee empDRep = deptController.GetEmployeeListForDRepSelected(dcode);
                Department dept = deptController.GetDepartByDepCode(dcode);
                Employee emp = deptController.GetDHeadByDeptCode(dcode);

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
                DropDownListDRep.DataSource = deptController.GetEmployeeListForDRep(dcode, empid);
                DropDownListDRep.DataTextField = "EmpName";
                DropDownListDRep.DataValueField = "EmpID";
                DropDownListDRep.DataBind();
                DropDownListDRep.Items.FindByText(empDRepname).Selected = true;

                //UpdateCollectionPoint
                string empCollectionname = deptController.GetDepartmentForCollectionPointSelected(dcode);
                DropDownListCollectionPoint.DataSource = deptController.GetCollectionPointList();
                DropDownListCollectionPoint.DataTextField = "CollectionPoint1";
                DropDownListCollectionPoint.DataValueField = "CollectionLocationID";
                DropDownListCollectionPoint.DataBind();
                DropDownListCollectionPoint.Items.FindByText(empCollectionname).Selected = true;
            }

            else
            {
                Utility.logout();
            }
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {

        if (Session["emp"] != null)
        {

            Employee empSession = (Employee)Session["emp"];
            string dcode = empSession.DeptCode;
            string empRole = empSession.Role;

            Employee empDRep = deptController.GetEmployeeListForDRepSelected(dcode);
            int cid = deptController.GetCollectionidbyDeptCode(dcode);
            int c = Convert.ToInt16(DropDownListCollectionPoint.SelectedValue);
            deptController.UpdateCollectionPoint(dcode, c);

            int empRepid = empDRep.EmpID;
            int empid = Convert.ToInt16(DropDownListDRep.SelectedValue);
            deptController.UpdateDeptRep(dcode, empid);

            if (c == cid && empid == empRepid)
            {
                Response.Redirect("~/Department/DepartmentDetailInfo.aspx");
            }
            else
            {

                Response.Redirect("~/Department/DepartmentDetailInfo.aspx?SuccessMsg=" + "Successfully Updated!!");

            }

        }//ispostback
        else
        {
            Utility.logout();
        }

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {

        Response.Redirect("~/Department/DepartmentDetailInfo.aspx");
    }




}