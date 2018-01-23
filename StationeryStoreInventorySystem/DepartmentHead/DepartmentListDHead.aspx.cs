using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DepartmentListDHead : System.Web.UI.Page
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

                Department dept = DeptBusinessLogic.GetDepartByDepCode(dcode);
                Employee emp = DeptBusinessLogic.GetDHeadByDeptCode(dcode);
                Employee empDRep = DeptBusinessLogic.GetEmployeeListForDRepSelected(dcode);

                string dname = dept.DeptName;
                string contactname = dept.DeptContactName;
                string telephone = dept.DeptTelephone;
                string fax = dept.DeptFax;
                string dheadname = emp.EmpName;



                //DateTime? endTime = emp.EndDate;
                lblDeptName.Text = dname;
                lblContactName.Text = contactname;
                lblPhone.Text = telephone;
                lblFax.Text = fax;
                lblHeadname.Text = dheadname;


                //Date

                cmpToday.ValueToCompare = DateTime.Now.ToShortDateString();
               //CompareValidator1.ValueToCompare = DateTime.Now.ToShortDateString();
                
                RequiredFieldValidator1.Enabled = true;
                RequiredFieldValidator2.Enabled = true;
                cmpToday.Enabled = true;
                cmpStartAndEndDates.Enabled = true;



                int empRid = empDRep.EmpID;//ForDeptRep Id

                //UpdateActingDHead  
                if (DeptBusinessLogic.GetEmployeeListForActingDHeadSelectedCount(dcode) <= 0)
                {

                    DropDownListActingDHead.DataSource = DeptBusinessLogic.GetEmployeeListForActingDHead(dcode, empRid);
                    DropDownListActingDHead.DataTextField = "EmpName";
                    DropDownListActingDHead.DataValueField = "EmpID";
                    DropDownListActingDHead.DataBind();
                    DropDownListActingDHead.Items.Insert(0, new ListItem("--Revoke authority--", "0"));
                    DropDownListActingDHead.SelectedIndex = 0;
                    if (DropDownListActingDHead.SelectedValue == "0")
                    {
                        txtSDate.Enabled = false;
                        txtEDate.Enabled = false;
                    }


                    int empid = 0;
                    string empDRepname = empDRep.EmpName;
                    DropDownListDRep.DataSource = DeptBusinessLogic.GetEmployeeListForDRep(dcode, empid);
                    DropDownListDRep.DataTextField = "EmpName";
                    DropDownListDRep.DataValueField = "EmpID";
                    DropDownListDRep.DataBind();
                    DropDownListDRep.Items.FindByText(empDRepname).Selected = true;
                }
                else
                {

                    Employee empActingDHead = DeptBusinessLogic.GetEmployeeListForActingDHeadSelected(dcode);

                    string empActingDHeadname = empActingDHead.EmpName;
                    string startdate = empActingDHead.StartDate.GetValueOrDefault().Date.ToShortDateString();
                    string enddate = empActingDHead.EndDate.GetValueOrDefault().ToShortDateString();

                    txtSDate.Text = startdate;
                    txtEDate.Text = enddate;
                    //if (empActingDHead.EndDate != null && txtSDate.Text.ToString()==empActingDHead.StartDate.GetValueOrDefault().ToShortDateString())
                    //{
                    //    cmpToday.Enabled = false;
                    //}else { cmpToday.Enabled = true; }

                    DropDownListActingDHead.DataSource = DeptBusinessLogic.GetEmployeeListForActingDHead(dcode, empRid);
                    DropDownListActingDHead.DataTextField = "EmpName";
                    DropDownListActingDHead.DataValueField = "EmpID";
                    DropDownListActingDHead.DataBind();
                    DropDownListActingDHead.Items.FindByText(empActingDHeadname).Selected = true;
                    DropDownListActingDHead.Items.Insert(0, new ListItem("--Revoke authority--", "0"));

                    //UpdateDeptRp
                    int empid = empActingDHead.EmpID;
                    string empDRepname = empDRep.EmpName;
                    DropDownListDRep.DataSource = DeptBusinessLogic.GetEmployeeListForDRep(dcode, empid);
                    DropDownListDRep.DataTextField = "EmpName";
                    DropDownListDRep.DataValueField = "EmpID";
                    DropDownListDRep.DataBind();
                    DropDownListDRep.Items.FindByText(empDRepname).Selected = true;
                }

                //UpdateCollectionPoint
                string empCollectionname = DeptBusinessLogic.GetDepartmentForCollectionPointSelected(dcode);
                DropDownListCollectionPoint.DataSource = DeptBusinessLogic.GetCollectionPointList();
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
            if (dcode != null)
            {
                int c = Convert.ToInt16(DropDownListCollectionPoint.SelectedValue);
                DeptBusinessLogic.UpdateCollectionPoint(dcode, c);

                int empid = Convert.ToInt16(DropDownListDRep.SelectedValue);
                DeptBusinessLogic.UpdateDeptRep(dcode, empid);

                if (Convert.ToInt32(DropDownListActingDHead.SelectedValue) == 0)
                {
                    if (DeptBusinessLogic.GetEmployeeListForActingDHeadSelectedCount(dcode) > 0)
                    {
                        DeptBusinessLogic.UpdateRevoke();

                    }

                }
                else
                {
                    int Aempid = Convert.ToInt16(DropDownListActingDHead.SelectedValue);
                    RequiredFieldValidator1.Enabled = true;
                    RequiredFieldValidator2.Enabled = true;
                    cmpToday.Enabled = true;
                    cmpStartAndEndDates.Enabled = true;

                    string sdate = txtSDate.Text;
                    string edate = txtEDate.Text;
                    DeptBusinessLogic.UpdateActingDHead(dcode, Aempid, sdate, edate);


                }
                Response.Redirect("~/Department/DepartmentDetailInfo.aspx");

            }
            else
            {
                lblMessage.Text = "Update Failed!";
            }
        }
        else
        {
            Utility.logout();
        }
    }



    protected void DropDownListActingDHead_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Session["emp"] != null)
        {
            Employee empSession = (Employee)Session["emp"];

            string dcode = empSession.DeptCode;
            string empRole = empSession.Role;

            Employee empDRep = DeptBusinessLogic.GetEmployeeListForDRepSelected(dcode);

            string empDRepname = empDRep.EmpName;

            int a = Convert.ToInt16(DropDownListActingDHead.SelectedValue);

            DropDownListDRep.DataSource = DeptBusinessLogic.GetEmployeeListForDRep(dcode, a);
            DropDownListDRep.DataBind();
            DropDownListDRep.Items.FindByText(empDRepname).Selected = true;
            if (a == 0)
            {
                RequiredFieldValidator1.Enabled = false;
                RequiredFieldValidator2.Enabled = false;
                cmpToday.Enabled = false;
                cmpStartAndEndDates.Enabled = false;
                txtSDate.Text = "";
                txtSDate.Enabled = false;
                txtEDate.Text = "";
                txtEDate.Enabled = false;
            }
            else
            {
                RequiredFieldValidator1.Enabled = true;
                RequiredFieldValidator2.Enabled = true;
                cmpToday.Enabled = true;
                cmpStartAndEndDates.Enabled = true;
                txtSDate.Enabled = true;
                txtEDate.Enabled = true;
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