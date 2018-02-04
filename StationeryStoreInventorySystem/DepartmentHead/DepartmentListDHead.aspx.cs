using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DepartmentListDHead : System.Web.UI.Page
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

                Department dept = deptController.GetDepartByDepCode(dcode);
                Employee emp = deptController.GetDHeadByDeptCode(dcode);
                Employee empDRep = deptController.GetEmployeeListForDRepSelected(dcode);

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

                reqForSDate.Enabled = true;
                reqForEDate.Enabled = true;
                cmpToday.Enabled = true;
                cmpStartAndEndDates.Enabled = true;



                int empRid = empDRep.EmpID;//ForDeptRep Id

                //UpdateActingDHead  
                if (deptController.GetEmployeeListForActingDHeadSelectedCount(dcode) <= 0)
                {

                    ddlActingDHead.DataSource = deptController.GetEmployeeListForActingDHead(dcode, empRid);
                    ddlActingDHead.DataTextField = "EmpName";
                    ddlActingDHead.DataValueField = "EmpID";
                    ddlActingDHead.DataBind();
                    ddlActingDHead.Items.Insert(0, new ListItem("--Revoke authority--", "0"));
                    ddlActingDHead.SelectedIndex = 0;
                    //if (DropDownListActingDHead.SelectedValue == "0")
                    //{
                    txtSDate.Enabled = false;
                    txtEDate.Enabled = false;
                    txtSDate.Visible = true;
                    txtEDate.Visible = true;
                    btnEditDate.Visible = false;
                    reqForSDate.Enabled = false;
                    reqForEDate.Enabled = false;

                    // }


                    int empid = 0;
                    string empDRepname = empDRep.EmpName;
                    ddlDRep.DataSource = deptController.GetEmployeeListForDRep(dcode, empid);
                    ddlDRep.DataTextField = "EmpName";
                    ddlDRep.DataValueField = "EmpID";
                    ddlDRep.DataBind();
                    ddlDRep.Items.FindByText(empDRepname).Selected = true;
                }
                else
                {

                    Employee empActingDHead = deptController.GetEmployeeListForActingDHeadSelected(dcode);

                    string empActingDHeadname = empActingDHead.EmpName;
                    string startdate = empActingDHead.StartDate.GetValueOrDefault().Date.ToShortDateString();
                    string enddate = empActingDHead.EndDate.GetValueOrDefault().ToShortDateString();

                    lblStartDate.Text = startdate;
                    lblEndDate.Text = enddate;
                    txtSDate.Text = startdate;
                    txtEDate.Text = enddate;
                    //if (empActingDHead.EndDate != null && txtSDate.Text.ToString()==empActingDHead.StartDate.GetValueOrDefault().ToShortDateString())
                    //{
                    //    cmpToday.Enabled = false;
                    //}else { cmpToday.Enabled = true; }
                    DateTime? endDate = empActingDHead.EndDate;
                    DateTime? startDate = empActingDHead.StartDate;
                    DateTime today = DateTime.Now;

                    //if (today <= endDate && today>=startDate && txtEDate.Text==enddate && txtSDate.Text==startdate)
                    //{
                    //    cmpToday.Enabled = false;
                    //    cmpStartAndEndDates.Enabled =false;
                    //    RequiredFieldValidator1.Enabled = false;
                    //    RequiredFieldValidator2.Enabled = false;

                    //}

                    ddlActingDHead.DataSource = deptController.GetEmployeeListForActingDHead(dcode, empRid);
                    ddlActingDHead.DataTextField = "EmpName";
                    ddlActingDHead.DataValueField = "EmpID";
                    ddlActingDHead.DataBind();
                    ddlActingDHead.Items.FindByText(empActingDHeadname).Selected = true;
                    ddlActingDHead.Items.Insert(0, new ListItem("--Revoke authority--", "0"));

                    //UpdateDeptRp
                    int empid = empActingDHead.EmpID;
                    string empDRepname = empDRep.EmpName;
                    ddlDRep.DataSource = deptController.GetEmployeeListForDRep(dcode, empid);
                    ddlDRep.DataTextField = "EmpName";
                    ddlDRep.DataValueField = "EmpID";
                    ddlDRep.DataBind();
                    ddlDRep.Items.FindByText(empDRepname).Selected = true;
                }

                //UpdateCollectionPoint
                string empCollectionname = deptController.GetDepartmentForCollectionPointSelected(dcode);
                ddlCollectionPoint.DataSource = deptController.GetCollectionPointList();
                ddlCollectionPoint.DataTextField = "CollectionPoint1";
                ddlCollectionPoint.DataValueField = "CollectionLocationID";
                ddlCollectionPoint.DataBind();
                ddlCollectionPoint.Items.FindByText(empCollectionname).Selected = true;
            }//ispostback
            else
            {
                Utility.logout();
            }

        }
    }

    protected void BtnUpdate_Click(object sender, EventArgs e)
    {

        if (Session["emp"] != null)
        {
            Employee empSession = (Employee)Session["emp"];
            string dcode = empSession.DeptCode;
            string empRole = empSession.Role;

            if (dcode != null)
            {
                int cid = deptController.GetCollectionidbyDeptCode(dcode);
                int c = Convert.ToInt16(ddlCollectionPoint.SelectedValue);
               

                Employee empDRep = deptController.GetEmployeeListForDRepSelected(dcode);
                int empRepid = empDRep.EmpID;
                string empRepEmail = empDRep.Email;
                int empid = Convert.ToInt16(ddlDRep.SelectedValue);
                Employee newDeptRep = deptController.GetEmployeeEmailByEid(empid);
                String newempEmail = newDeptRep.Email;
               


                if (Convert.ToInt32(ddlActingDHead.SelectedValue) == 0)
                {

                    if (deptController.GetEmployeeListForActingDHeadSelectedCount(dcode) > 0)
                    {
                        int Aempid = Convert.ToInt16(ddlActingDHead.SelectedValue);
                        Employee oldDeptTemp = deptController.GetEmployeeListForActingDHeadSelected(dcode);
                        string oldDeptTempEmail = oldDeptTemp.Email;



                        string sdate = txtSDate.Text;
                        string edate = txtEDate.Text;
                        string lbsdate = lblStartDate.Text;
                        string lbedate = lblEndDate.Text;
                        //lblFax.Text = Aempid.ToString();

                        if (c == cid && empid == empRepid && Aempid == 0 && sdate == "" && edate == "" && lbsdate == "" && lbedate == "")
                        {

                            Response.Redirect(LoginController.DepartmentDetailInfoURI);
                        }
                        else
                        {
                            if (c != cid)
                            {
                                deptController.UpdateCollectionPoint(dcode, c);
                            }
                            if (empid != empRepid)
                            {
                                deptController.UpdateDeptRep(dcode, empid);
                            }
                            if (Aempid != 0 || sdate != "" || edate != "")
                            {
                                Thread emailThreadWithParam = new Thread(() => ADMailNotification(oldDeptTempEmail));
                                emailThreadWithParam.Start();
                            }
                            deptController.UpdateRevoke();

                            Response.Redirect(LoginController.DepartmentDetailInfoURI + "?SuccessMsg=" + "Successfully Updated!!");

                        }

                    }
                    else
                    {
                        int Aempid = Convert.ToInt16(ddlActingDHead.SelectedValue);
                        //Employee newDeptTemp = deptController.GetEmployeeEmailByEid(Aempid);
                        //String newDeptTempEmail = newDeptTemp.Email;
                        string sdate = txtSDate.Text;
                        string edate = txtEDate.Text;
                        string lbsdate = lblStartDate.Text;
                        string lbedate = lblEndDate.Text;
                        if (c == cid && empid == empRepid && Aempid == 0 && sdate == "" && edate == "" && lbsdate == "" && lbedate == "")
                        {

                            Response.Redirect(LoginController.DepartmentDetailInfoURI);
                        }
                        else
                        {
                            if (c != cid)
                            {
                                deptController.UpdateCollectionPoint(dcode, c);
                            }
                            if (empid != empRepid)
                            {
                                deptController.UpdateDeptRep(dcode, empid);
                            }
                           
                            Response.Redirect(LoginController.DepartmentDetailInfoURI + "?SuccessMsg=" + "Successfully Updated!!");

                        }
                    }

                }
                else
                {
                    if (deptController.GetEmployeeListForActingDHeadSelectedCount(dcode) > 0)
                    {
                        int Aempid = Convert.ToInt16(ddlActingDHead.SelectedValue);
                        reqForSDate.Enabled = true;
                        reqForEDate.Enabled = true;
                        cmpToday.Enabled = true;
                        cmpStartAndEndDates.Enabled = true;

                        string sdate = txtSDate.Text;
                        string edate = txtEDate.Text;




                        Employee empActingDHead = deptController.GetEmployeeListForActingDHeadSelected(dcode);
                        int aid = empActingDHead.EmpID;
                        string oldDeptTempEmail = empActingDHead.Email;
                        Employee newDeptTemp = deptController.GetEmployeeEmailByEid(Aempid);
                        String newDeptTempEmail = newDeptTemp.Email;
                        string ssdate = empActingDHead.StartDate.GetValueOrDefault().ToShortDateString();
                        string eedate = empActingDHead.EndDate.GetValueOrDefault().ToShortDateString();
                        string lbsdate = lblStartDate.Text;
                        string lbedate = lblEndDate.Text;
                        lblFax.Text = aid.ToString();
                        lblPhone.Text = Aempid.ToString();

                        if (c == cid && empid == empRepid && Aempid == aid && sdate == ssdate && edate == eedate && lbsdate == ssdate && lbedate == eedate)
                        {
                            Response.Redirect(LoginController.DepartmentDetailInfoURI);
                        }
                        else
                        {
                            if (c != cid)
                            {
                                deptController.UpdateCollectionPoint(dcode, c);
                            }
                            if (empid != empRepid)
                            {
                                deptController.UpdateDeptRep(dcode, empid);
                            }
                            if (Aempid != aid || sdate != ssdate || edate != eedate)
                            {
                                Thread emailThreadWithParamnew = new Thread(() => ADNewMailNotification(newDeptTempEmail));
                                emailThreadWithParamnew.Start();
                                Thread emailThreadWithParam = new Thread(() => ADMailNotification(oldDeptTempEmail));
                                emailThreadWithParam.Start();
                            }
                            deptController.UpdateActingDHead(dcode, Aempid, sdate, edate);
                            Response.Redirect(LoginController.DepartmentDetailInfoURI + "?SuccessMsg=" + "Successfully Updated!!");

                        }
                    }
                    else
                    {
                        int Aempid = Convert.ToInt16(ddlActingDHead.SelectedValue);
                        Employee newDeptTemp = deptController.GetEmployeeEmailByEid(Aempid);
                        String newDeptTempEmail = newDeptTemp.Email;
                        reqForSDate.Enabled = true;
                        reqForEDate.Enabled = true;
                        cmpToday.Enabled = true;
                        cmpStartAndEndDates.Enabled = true;

                        string sdate = txtSDate.Text;
                        string edate = txtEDate.Text;
                       

                            if (c != cid)
                            {
                                deptController.UpdateCollectionPoint(dcode, c);
                            }
                            if (empid != empRepid)
                            {
                                deptController.UpdateDeptRep(dcode, empid);
                            }
                        Thread emailThreadWithParamnew = new Thread(() => ADNewMailNotification(newDeptTempEmail));
                        emailThreadWithParamnew.Start();
                        deptController.UpdateActingDHead(dcode, Aempid, sdate, edate);
                            Response.Redirect(LoginController.DepartmentDetailInfoURI + "?SuccessMsg=" + "Successfully Updated!!");
                        
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

    private void ADMailNotification(String oldDeptTempEmail)
    {
        Utility.sendMail(oldDeptTempEmail, "Change Acting Department Head", "Your authority have been revoked");

    }

    private void ADNewMailNotification(String newDeptTempEmail)
    {
        Utility.sendMail(newDeptTempEmail, "Change Acting Department Head", "Your Role have authorized to Acting Head");

    }

    protected void DdlActingDHead_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Session["emp"] != null)
        {
            Employee empSession = (Employee)Session["emp"];

            string dcode = empSession.DeptCode;
            string empRole = empSession.Role;

            Employee empDRep = deptController.GetEmployeeListForDRepSelected(dcode);

            string empDRepname = empDRep.EmpName;

            int a = Convert.ToInt16(ddlActingDHead.SelectedValue);

            ddlDRep.DataSource = deptController.GetEmployeeListForDRep(dcode, a);
            ddlDRep.DataBind();
            ddlDRep.Items.FindByText(empDRepname).Selected = true;
            if (deptController.GetEmployeeListForActingDHeadSelectedCount(dcode) > 0)
            {
                if (a == 0)
                {
                    reqForSDate.Enabled = false;
                    reqForEDate.Enabled = false;
                    cmpToday.Enabled = false;
                    cmpStartAndEndDates.Enabled = false;
                    //txtSDate.Text = "";
                    txtSDate.Enabled = false;
                    //txtEDate.Text = "";
                    txtEDate.Enabled = false;
                    txtEDate.Visible = true;
                    txtSDate.Visible = true;
                    lblStartDate.Visible = false;
                    lblEndDate.Visible = false;
                    btnEditDate.Visible = false;
                }
                else
                {
                    reqForSDate.Enabled = true;
                    reqForEDate.Enabled = true;
                    cmpToday.Enabled = true;
                    cmpStartAndEndDates.Enabled = true;
                    txtSDate.Enabled = true;
                    txtEDate.Enabled = true;
                    txtEDate.Visible = false;
                    txtSDate.Visible = false;
                    lblStartDate.Visible = true;
                    lblEndDate.Visible = true;
                    btnEditDate.Visible = true;
                }
            }
            else
            {
                if (a == 0)
                {
                    reqForSDate.Enabled = false;
                    reqForEDate.Enabled = false;
                    cmpToday.Enabled = false;
                    cmpStartAndEndDates.Enabled = false;
                    txtSDate.Text = "";
                    txtSDate.Enabled = false;
                    txtEDate.Text = "";
                    txtEDate.Enabled = false;
                    //txtEDate.Visible = true;
                    //txtSDate.Visible = true;
                    //lblStartDate.Visible = false;
                    //lblEndDate.Visible = false;

                }
                else
                {
                    reqForSDate.Enabled = true;
                    reqForEDate.Enabled = true;
                    cmpToday.Enabled = true;
                    cmpStartAndEndDates.Enabled = true;
                    txtSDate.Enabled = true;
                    txtEDate.Enabled = true;
                    txtEDate.Visible = true;
                    txtSDate.Visible = true;
                    //lblStartDate.Visible = true;
                    //lblEndDate.Visible = true;

                }
            }
        }
        else
        {
            Utility.logout();
        }
    }








    protected void BtnEditDate_Click(object sender, EventArgs e)
    {
        txtSDate.Visible = true;
        txtEDate.Visible = true;
        lblStartDate.Visible = false;
        lblEndDate.Visible = false;
        btnEditDate.Visible = false;
    }



    private void ADMailNotification()
    {
       
    }
}