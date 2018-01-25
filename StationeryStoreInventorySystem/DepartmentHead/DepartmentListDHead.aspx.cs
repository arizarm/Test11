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

                Department dept = EFBroker_DeptEmployee.GetDepartByDepCode(dcode);
                Employee emp = EFBroker_DeptEmployee.GetDHeadByDeptCode(dcode);
                Employee empDRep = EFBroker_DeptEmployee.GetEmployeeListForDRepSelected(dcode);

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
                if (EFBroker_DeptEmployee.GetEmployeeListForActingDHeadSelectedCount(dcode) <= 0)
                {

                    DropDownListActingDHead.DataSource = EFBroker_DeptEmployee.GetEmployeeListForActingDHead(dcode, empRid);
                    DropDownListActingDHead.DataTextField = "EmpName";
                    DropDownListActingDHead.DataValueField = "EmpID";
                    DropDownListActingDHead.DataBind();
                    DropDownListActingDHead.Items.Insert(0, new ListItem("--Revoke authority--", "0"));
                    DropDownListActingDHead.SelectedIndex = 0;
                    if (DropDownListActingDHead.SelectedValue == "0")
                    {
                        txtSDate.Enabled = false;
                        txtEDate.Enabled = false;
                        RequiredFieldValidator1.Enabled = false;
                        RequiredFieldValidator2.Enabled = false;
                    }


                    int empid = 0;
                    string empDRepname = empDRep.EmpName;
                    DropDownListDRep.DataSource = EFBroker_DeptEmployee.GetEmployeeListForDRep(dcode, empid);
                    DropDownListDRep.DataTextField = "EmpName";
                    DropDownListDRep.DataValueField = "EmpID";
                    DropDownListDRep.DataBind();
                    DropDownListDRep.Items.FindByText(empDRepname).Selected = true;
                }
                else
                {

                    Employee empActingDHead = EFBroker_DeptEmployee.GetEmployeeListForActingDHeadSelected(dcode);

                    string empActingDHeadname = empActingDHead.EmpName;
                    string startdate = empActingDHead.StartDate.GetValueOrDefault().Date.ToShortDateString();
                    string enddate = empActingDHead.EndDate.GetValueOrDefault().ToShortDateString();

                    txtSDate.Text = startdate;
                    txtEDate.Text = enddate;
                    //if (empActingDHead.EndDate != null && txtSDate.Text.ToString()==empActingDHead.StartDate.GetValueOrDefault().ToShortDateString())
                    //{
                    //    cmpToday.Enabled = false;
                    //}else { cmpToday.Enabled = true; }
                    DateTime? endDate = empActingDHead.EndDate;
                    DateTime today = DateTime.Now;
                    if (today > endDate)
                    {
                        EFBroker_DeptEmployee.UpdateRevoke();

                    }

                    DropDownListActingDHead.DataSource = EFBroker_DeptEmployee.GetEmployeeListForActingDHead(dcode, empRid);
                    DropDownListActingDHead.DataTextField = "EmpName";
                    DropDownListActingDHead.DataValueField = "EmpID";
                    DropDownListActingDHead.DataBind();
                    DropDownListActingDHead.Items.FindByText(empActingDHeadname).Selected = true;
                    DropDownListActingDHead.Items.Insert(0, new ListItem("--Revoke authority--", "0"));

                    //UpdateDeptRp
                    int empid = empActingDHead.EmpID;
                    string empDRepname = empDRep.EmpName;
                    DropDownListDRep.DataSource = EFBroker_DeptEmployee.GetEmployeeListForDRep(dcode, empid);
                    DropDownListDRep.DataTextField = "EmpName";
                    DropDownListDRep.DataValueField = "EmpID";
                    DropDownListDRep.DataBind();
                    DropDownListDRep.Items.FindByText(empDRepname).Selected = true;
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

            if (dcode != null)
            {
                int cid = EFBroker_DeptEmployee.GetCollectionidbyDeptCode(dcode);
                int c = Convert.ToInt16(DropDownListCollectionPoint.SelectedValue);
                EFBroker_DeptEmployee.UpdateCollectionPoint(dcode, c);

                Employee empDRep = EFBroker_DeptEmployee.GetEmployeeListForDRepSelected(dcode);
                int empRepid = empDRep.EmpID;
                int empid = Convert.ToInt16(DropDownListDRep.SelectedValue);
                EFBroker_DeptEmployee.UpdateDeptRep(dcode, empid);


                if (Convert.ToInt32(DropDownListActingDHead.SelectedValue) == 0)
                {

                    if (EFBroker_DeptEmployee.GetEmployeeListForActingDHeadSelectedCount(dcode) > 0)
                    {
                        EFBroker_DeptEmployee.UpdateRevoke();

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
                    EFBroker_DeptEmployee.UpdateActingDHead(dcode, Aempid, sdate, edate);


                }
                if (EFBroker_DeptEmployee.GetEmployeeListForActingDHeadSelectedCount(dcode) > 0)
                {
                    Employee empActingDHead = EFBroker_DeptEmployee.GetEmployeeListForActingDHeadSelected(dcode);
                    int aid = empActingDHead.EmpID;
                    int Aempid = Convert.ToInt16(DropDownListActingDHead.SelectedValue);
                    string sdate = txtSDate.Text;
                    string edate = txtEDate.Text;
                    string ssdate = empActingDHead.StartDate.GetValueOrDefault().ToShortDateString();
                    string eedate = empActingDHead.EndDate.GetValueOrDefault().ToShortDateString();
                    //lblFax.Text = ssdate;
                    //lblPhone.Text = sdate;
                    if (c == cid && empid == empRepid && Aempid == aid && sdate == ssdate && edate == eedate)
                    {
                        Response.Redirect("~/Department/DepartmentDetailInfo.aspx");
                    }
                    else
                    {

                        Response.Redirect("~/Department/DepartmentDetailInfo.aspx?SuccessMsg=" + "Successfully Updated!!");

                    }
                }
                else
                {
                    int Aempid = Convert.ToInt16(DropDownListActingDHead.SelectedValue);
                    string sdate = txtSDate.Text;
                    string edate = txtEDate.Text;
                    //lblFax.Text = Aempid.ToString();

                    if (c == cid && empid == empRepid && Aempid == 0 && sdate == "" && edate == "")
                    {
                        Response.Redirect("~/Department/DepartmentDetailInfo.aspx");
                    }
                    else
                    {

                        Response.Redirect("~/Department/DepartmentDetailInfo.aspx?SuccessMsg=" + "Successfully Updated!!");

                    }
                }
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

            Employee empDRep = EFBroker_DeptEmployee.GetEmployeeListForDRepSelected(dcode);

            string empDRepname = empDRep.EmpName;

            int a = Convert.ToInt16(DropDownListActingDHead.SelectedValue);

            DropDownListDRep.DataSource = EFBroker_DeptEmployee.GetEmployeeListForDRep(dcode, a);
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