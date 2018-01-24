using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DepartmentListDRep : System.Web.UI.Page
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

                if (EFBroker_DeptEmployee.GetEmployeeListForActingDHeadSelectedCount(dcode) <= 0)
                {
                    Department dept = EFBroker_DeptEmployee.GetDepartByDepCode(dcode);
                    Employee emp = EFBroker_DeptEmployee.GetDHeadByDeptCode(dcode);
                    Employee empDRep = EFBroker_DeptEmployee.GetEmployeeListForDRepSelected(dcode);
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
                    Department dept = EFBroker_DeptEmployee.GetDepartByDepCode(dcode);
                    Employee emp = EFBroker_DeptEmployee.GetDHeadByDeptCode(dcode);
                    Employee empActingDHead = EFBroker_DeptEmployee.GetEmployeeListForActingDHeadSelected(dcode);
                    Employee empDRep = EFBroker_DeptEmployee.GetEmployeeListForDRepSelected(dcode);
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
                string empCollectionname = EFBroker_DeptEmployee.GetDepartmentForCollectionPointSelected(dcode);
                DropDownListCollectionPoint.DataSource = EFBroker_DeptEmployee.GetCollectionPointList();
                DropDownListCollectionPoint.DataTextField = "CollectionPoint1";
                DropDownListCollectionPoint.DataValueField = "CollectionLocationID";
                DropDownListCollectionPoint.DataBind();
                DropDownListCollectionPoint.Items.FindByText(empCollectionname).Selected = true;

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
            int c = Convert.ToInt16(DropDownListCollectionPoint.SelectedValue);
            EFBroker_DeptEmployee.UpdateCollectionPoint(dcode, c);

            Response.Redirect("~/Department/DepartmentDetailInfo.aspx");
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