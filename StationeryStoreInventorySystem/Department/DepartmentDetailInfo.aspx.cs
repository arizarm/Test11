using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Department_DepartmentDetailInfo : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        DeptController deptController = new DeptController();
        if (Session["emp"] != null)
        {
            Employee empSession = (Employee)Session["emp"];
            string dcode = empSession.DeptCode;
            string empRole = empSession.Role;
            string tempHead = empSession.IsTempHead;

            if (deptController.GetEmployeeListForActingDHeadSelectedCount(dcode) > 0)
            {
                Employee empActingDHead = deptController.GetEmployeeListForActingDHeadSelected(dcode);
                DateTime? endDate = empActingDHead.EndDate;
                DateTime today = DateTime.Now.Date;

                if (today > endDate)
                {
                    deptController.UpdateRevoke();
                    lblActingDHead.Text = null;


                }
            }
            if (!IsPostBack)
            {
                string s = Request.QueryString["SuccessMsg"];
                lblMessage.Text = s;


                if (empRole == "Employee" && tempHead == "N")
                {
                    btnUpdate.Visible = false;

                }

                if (deptController.GetEmployeeListForActingDHeadSelectedCount(dcode) <= 0)
                {
                    Employee empDRep = deptController.GetEmployeeListForDRepSelected(dcode);
                    Department dept = deptController.GetDepartByDepCode(dcode);
                    Employee emp = deptController.GetDHeadByDeptCode(dcode);

                    string aheadname = "No Acting Head";
                    string detpRname = empDRep.EmpName;
                    string dname = dept.DeptName;
                    string contactname = dept.DeptContactName;
                    string telephone = dept.DeptTelephone;
                    string fax = dept.DeptFax;
                    string dheadname = emp.EmpName;
                    string empCollectionname = deptController.GetDepartmentForCollectionPointSelected(dcode);


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
                    Employee empActingDHead = deptController.GetEmployeeListForActingDHeadSelected(dcode);
                    Employee empDRep = deptController.GetEmployeeListForDRepSelected(dcode);
                    Department dept = deptController.GetDepartByDepCode(dcode);
                    Employee emp = deptController.GetDHeadByDeptCode(dcode);

                    string aheadname = empActingDHead.EmpName;
                    string detpRname = empDRep.EmpName;
                    string dname = dept.DeptName;
                    string contactname = dept.DeptContactName;
                    string telephone = dept.DeptTelephone;
                    string fax = dept.DeptFax;
                    string dheadname = emp.EmpName;
                    string startdate = empActingDHead.StartDate.GetValueOrDefault().Date.ToShortDateString();
                    string enddate = empActingDHead.EndDate.GetValueOrDefault().ToShortDateString();


                    string empCollectionname = deptController.GetDepartmentForCollectionPointSelected(dcode);
                    //DateTime? endDate = empActingDHead.EndDate;
                    //DateTime today = DateTime.Now;

                    //if (today > endDate)
                    //{
                    //    deptController.UpdateRevoke();
                    //    lblActingDHead.Text = null;


                    //}

                    lblDeptName.Text = dname;
                    lblContactName.Text = contactname;
                    lblPhone.Text = telephone;
                    lblFax.Text = fax;
                    lblHeadname.Text = dheadname;
                    lblActingDHead.Text = aheadname;
                    lblDeptRep.Text = detpRname;
                    lblCollectPoint.Text = empCollectionname;
                    lblSDate.Text = startdate;
                    lblEDate.Text = enddate;

                }

            }//ispostback


        }
        else
        {
            Utility.logout();
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (Session["emp"] != null)
        {
            Employee empSession = (Employee)Session["emp"];
            string dcode = empSession.DeptCode;
            string empRole = empSession.Role;


            if (empRole == "DepartmentHead")
            {
                Response.Redirect(LoginController.DepartmentListDHeadURI);
            }
            else if (empRole == "Representative")
            {
                Response.Redirect(LoginController.DepartmentListDRepURI);
            }
            else if (empRole == "DepartmentTempHead")
            {
                Response.Redirect(LoginController.DepartmentListActingDHeadURI);
            }
        }
        else
        {
            Utility.logout();
        }
    }
}

