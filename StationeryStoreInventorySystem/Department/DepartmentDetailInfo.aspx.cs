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

        if (!IsPostBack)
        {
            if (Session["emp"] != null)
            {
                Employee empSession = (Employee)Session["emp"];
                string dcode = empSession.DeptCode;
                string empRole = empSession.Role;
                string tempHead = empSession.IsTempHead;

                if(empRole=="Employee" && tempHead == "N")
                {
                    btnUpdate.Visible = false;
                    
                }

                if (EFBroker_DeptEmployee.GetEmployeeListForActingDHeadSelectedCount(dcode) <= 0)
                {
                    Employee empDRep = EFBroker_DeptEmployee.GetEmployeeListForDRepSelected(dcode);
                    Department dept = EFBroker_DeptEmployee.GetDepartByDepCode(dcode);
                    Employee emp = EFBroker_DeptEmployee.GetDHeadByDeptCode(dcode);

                    string aheadname = "No Acting Head";
                    string detpRname = empDRep.EmpName;
                    string dname = dept.DeptName;
                    string contactname = dept.DeptContactName;
                    string telephone = dept.DeptTelephone;
                    string fax = dept.DeptFax;
                    string dheadname = emp.EmpName;
                    string empCollectionname = EFBroker_DeptEmployee.GetDepartmentForCollectionPointSelected(dcode);


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
                    Employee empActingDHead = EFBroker_DeptEmployee.GetEmployeeListForActingDHeadSelected(dcode);
                    Employee empDRep = EFBroker_DeptEmployee.GetEmployeeListForDRepSelected(dcode);
                    Department dept = EFBroker_DeptEmployee.GetDepartByDepCode(dcode);
                    Employee emp = EFBroker_DeptEmployee.GetDHeadByDeptCode(dcode);

                    string aheadname = empActingDHead.EmpName;
                    string detpRname = empDRep.EmpName;
                    string dname = dept.DeptName;
                    string contactname = dept.DeptContactName;
                    string telephone = dept.DeptTelephone;
                    string fax = dept.DeptFax;
                    string dheadname = emp.EmpName;
                    string startdate = empActingDHead.StartDate.GetValueOrDefault().Date.ToShortDateString();
                    string enddate = empActingDHead.EndDate.GetValueOrDefault().ToShortDateString();

                   
                    string empCollectionname = EFBroker_DeptEmployee.GetDepartmentForCollectionPointSelected(dcode);


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


            if (empRole == "DepartmentHead")
            {
                Response.Redirect("~/DepartmentHead/DepartmentListDHead.aspx");
            }
            else if (empRole == "Representative")
            {
                Response.Redirect("~/DepartmentRepresentative/DepartmentListDRep.aspx");
            }
            else if (empRole == "DepartmentTempHead")
            {
                Response.Redirect("~/DepartmentTempHead/DepartmentListActingDHead.aspx");
            }
        }
        else
        {
            Utility.logout();
        }
    }
}

