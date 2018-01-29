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
            string empRepEmail = empDRep.Email;
            Employee newDeptRep = deptController.GetEmployeeEmailByEid(empid);
            String newempEmail = newDeptRep.Email;
            deptController.UpdateDeptRep(dcode, empid);

            if (c == cid && empid == empRepid)
            {
                Response.Redirect(LoginController.DepartmentDetailInfoURI);
            }
            else
            {
                if (c != cid)
                {
                    List<String> clerkEmails = EmployeeController.getAllClerkMails();

                    if (clerkEmails != null)
                    {
                        for (int i = 0; i < clerkEmails.Count; i++)
                        {
                            Utility.sendMail(clerkEmails[i].ToString(), "Change Collection Point", "New Collection Point is updated!");
                        }
                    }
                }
                if (empid != empRepid)
                {
                    Utility.sendMail(newempEmail, "Change Department Rep", "Your Role have changed to Department Rep");
                    Utility.sendMail(empRepEmail, "Change Department Rep", "Your Role have changed to Employee");
                }
                Response.Redirect(LoginController.DepartmentDetailInfoURI +"? SuccessMsg=" + "Successfully Updated!!");

            }

        }//ispostback
        else
        {
            Utility.logout();
        }

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {

        Response.Redirect(LoginController.DepartmentDetailInfoURI);
    }




}