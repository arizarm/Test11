using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DepartmentListDRep : System.Web.UI.Page
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

                if (deptController.GetEmployeeListForActingDHeadSelectedCount(dcode) <= 0)
                {
                    Department dept = deptController.GetDepartByDepCode(dcode);
                    Employee emp = deptController.GetDHeadByDeptCode(dcode);
                    Employee empDRep = deptController.GetEmployeeListForDRepSelected(dcode);
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
                    Department dept = deptController.GetDepartByDepCode(dcode);
                    Employee emp = deptController.GetDHeadByDeptCode(dcode);
                    Employee empActingDHead = deptController.GetEmployeeListForActingDHeadSelected(dcode);
                    Employee empDRep = deptController.GetEmployeeListForDRepSelected(dcode);
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
                string empCollectionname = deptController.GetDepartmentForCollectionPointSelected(dcode);
                DropDownListCollectionPoint.DataSource = deptController.GetCollectionPointList();
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

            int cid = deptController.GetCollectionidbyDeptCode(dcode);
            int c = Convert.ToInt16(DropDownListCollectionPoint.SelectedValue);
            //lblFax.Text = cid.ToString();
            //lblPhone.Text = c.ToString();
            if (c != cid)
            {
                deptController.UpdateCollectionPoint(dcode, c);
                Response.Redirect("~/Department/DepartmentDetailInfo.aspx?SuccessMsg=" + "Successfully Updated!!");
            }
            else
            {
                Response.Redirect("~/Department/DepartmentDetailInfo.aspx");
            }
        }
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