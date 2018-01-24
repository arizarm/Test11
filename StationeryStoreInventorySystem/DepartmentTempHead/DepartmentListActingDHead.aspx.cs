using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DepartmentListActingDHead : System.Web.UI.Page
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

                Employee empActingDHead = EFBroker_DeptEmployee.GetEmployeeListForActingDHeadSelected(dcode);
                Employee empDRep = EFBroker_DeptEmployee.GetEmployeeListForDRepSelected(dcode);
                Department dept = EFBroker_DeptEmployee.GetDepartByDepCode(dcode);
                Employee emp = EFBroker_DeptEmployee.GetDHeadByDeptCode(dcode);

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
                DropDownListDRep.DataSource = EFBroker_DeptEmployee.GetEmployeeListForDRep(dcode, empid);
                DropDownListDRep.DataTextField = "EmpName";
                DropDownListDRep.DataValueField = "EmpID";
                DropDownListDRep.DataBind();
                DropDownListDRep.Items.FindByText(empDRepname).Selected = true;

                //UpdateCollectionPoint
                string empCollectionname = EFBroker_DeptEmployee.GetDepartmentForCollectionPointSelected(dcode);
                DropDownListCollectionPoint.DataSource = EFBroker_DeptEmployee.GetCollectionPointList();
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

            Employee empDRep = EFBroker_DeptEmployee.GetEmployeeListForDRepSelected(dcode);
            int cid = EFBroker_DeptEmployee.GetCollectionidbyDeptCode(dcode);
            int c = Convert.ToInt16(DropDownListCollectionPoint.SelectedValue);
            EFBroker_DeptEmployee.UpdateCollectionPoint(dcode, c);

            int empRepid = empDRep.EmpID;
            int empid = Convert.ToInt16(DropDownListDRep.SelectedValue);
            EFBroker_DeptEmployee.UpdateDeptRep(dcode, empid);

            if (c==cid && empid==empRepid)
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