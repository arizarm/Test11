using System;
using System.Collections.Generic;
using System.Linq;
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

                RequiredFieldValidator1.Enabled = true;
                RequiredFieldValidator2.Enabled = true;
                cmpToday.Enabled = true;
                cmpStartAndEndDates.Enabled = true;



                int empRid = empDRep.EmpID;//ForDeptRep Id

                //UpdateActingDHead  
                if (deptController.GetEmployeeListForActingDHeadSelectedCount(dcode) <= 0)
                {

                    DropDownListActingDHead.DataSource = deptController.GetEmployeeListForActingDHead(dcode, empRid);
                    DropDownListActingDHead.DataTextField = "EmpName";
                    DropDownListActingDHead.DataValueField = "EmpID";
                    DropDownListActingDHead.DataBind();
                    DropDownListActingDHead.Items.Insert(0, new ListItem("--Revoke authority--", "0"));
                    DropDownListActingDHead.SelectedIndex = 0;
                    //if (DropDownListActingDHead.SelectedValue == "0")
                    //{
                    txtSDate.Enabled = false;
                    txtEDate.Enabled = false;
                    txtSDate.Visible = true;
                    txtEDate.Visible = true;
                    btnEditDate.Visible = false;
                    RequiredFieldValidator1.Enabled = false;
                    RequiredFieldValidator2.Enabled = false;

                    // }


                    int empid = 0;
                    string empDRepname = empDRep.EmpName;
                    DropDownListDRep.DataSource = deptController.GetEmployeeListForDRep(dcode, empid);
                    DropDownListDRep.DataTextField = "EmpName";
                    DropDownListDRep.DataValueField = "EmpID";
                    DropDownListDRep.DataBind();
                    DropDownListDRep.Items.FindByText(empDRepname).Selected = true;
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

                    DropDownListActingDHead.DataSource = deptController.GetEmployeeListForActingDHead(dcode, empRid);
                    DropDownListActingDHead.DataTextField = "EmpName";
                    DropDownListActingDHead.DataValueField = "EmpID";
                    DropDownListActingDHead.DataBind();
                    DropDownListActingDHead.Items.FindByText(empActingDHeadname).Selected = true;
                    DropDownListActingDHead.Items.Insert(0, new ListItem("--Revoke authority--", "0"));

                    //UpdateDeptRp
                    int empid = empActingDHead.EmpID;
                    string empDRepname = empDRep.EmpName;
                    DropDownListDRep.DataSource = deptController.GetEmployeeListForDRep(dcode, empid);
                    DropDownListDRep.DataTextField = "EmpName";
                    DropDownListDRep.DataValueField = "EmpID";
                    DropDownListDRep.DataBind();
                    DropDownListDRep.Items.FindByText(empDRepname).Selected = true;
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

            if (dcode != null)
            {
                int cid = deptController.GetCollectionidbyDeptCode(dcode);
                int c = Convert.ToInt16(DropDownListCollectionPoint.SelectedValue);
                deptController.UpdateCollectionPoint(dcode, c);

                Employee empDRep = deptController.GetEmployeeListForDRepSelected(dcode);
                int empRepid = empDRep.EmpID;
                string empRepEmail = empDRep.Email;
                int empid = Convert.ToInt16(DropDownListDRep.SelectedValue);
                Employee newDeptRep = deptController.GetEmployeeEmailByEid(empid);
                String newempEmail = newDeptRep.Email;
                deptController.UpdateDeptRep(dcode, empid);


                if (Convert.ToInt32(DropDownListActingDHead.SelectedValue) == 0)
                {

                    if (deptController.GetEmployeeListForActingDHeadSelectedCount(dcode) > 0)
                    {
                        int Aempid = Convert.ToInt16(DropDownListActingDHead.SelectedValue);
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
                            if (Aempid != 0 || sdate != "" || edate != "")
                            {
                                Utility.sendMail(oldDeptTempEmail, "Change Acting Department Head", "Your authority have been revoked");
                            }
                            deptController.UpdateRevoke();

                            Response.Redirect(LoginController.DepartmentDetailInfoURI + "?SuccessMsg=" + "Successfully Updated!!");

                        }

                    }
                    else
                    {
                        int Aempid = Convert.ToInt16(DropDownListActingDHead.SelectedValue);
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
                            //if (Aempid != 0 || sdate != "" || edate != "")
                            //{
                            //    Utility.sendMail(newDeptTempEmail, "Change Acting Department Head", "Your authority have been revoked");
                            //}
                            Response.Redirect(LoginController.DepartmentDetailInfoURI + "?SuccessMsg=" + "Successfully Updated!!");

                        }
                    }

                }
                else
                {
                    if (deptController.GetEmployeeListForActingDHeadSelectedCount(dcode) > 0)
                    {
                        int Aempid = Convert.ToInt16(DropDownListActingDHead.SelectedValue);
                        RequiredFieldValidator1.Enabled = true;
                        RequiredFieldValidator2.Enabled = true;
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
                            if (Aempid != aid || sdate != ssdate || edate != eedate)
                            {
                                Utility.sendMail(newDeptTempEmail, "Change Acting Department Head", "Your Role have authorized to Acting Head");
                                Utility.sendMail(oldDeptTempEmail, "Change Acting Department Head", "Your authority have been revoked");

                            }
                            deptController.UpdateActingDHead(dcode, Aempid, sdate, edate);
                            Response.Redirect(LoginController.DepartmentDetailInfoURI + "?SuccessMsg=" + "Successfully Updated!!");

                        }
                    }
                    else
                    {
                        int Aempid = Convert.ToInt16(DropDownListActingDHead.SelectedValue);
                        Employee newDeptTemp = deptController.GetEmployeeEmailByEid(Aempid);
                        String newDeptTempEmail = newDeptTemp.Email;
                        RequiredFieldValidator1.Enabled = true;
                        RequiredFieldValidator2.Enabled = true;
                        cmpToday.Enabled = true;
                        cmpStartAndEndDates.Enabled = true;

                        string sdate = txtSDate.Text;
                        string edate = txtEDate.Text;

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
                        Utility.sendMail(newDeptTempEmail, "Change Acting Department Head", "Your Role have authorized to Acting Head");

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



    protected void DropDownListActingDHead_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Session["emp"] != null)
        {
            Employee empSession = (Employee)Session["emp"];

            string dcode = empSession.DeptCode;
            string empRole = empSession.Role;

            Employee empDRep = deptController.GetEmployeeListForDRepSelected(dcode);

            string empDRepname = empDRep.EmpName;

            int a = Convert.ToInt16(DropDownListActingDHead.SelectedValue);

            DropDownListDRep.DataSource = deptController.GetEmployeeListForDRep(dcode, a);
            DropDownListDRep.DataBind();
            DropDownListDRep.Items.FindByText(empDRepname).Selected = true;
            if (deptController.GetEmployeeListForActingDHeadSelectedCount(dcode) > 0)
            {
                if (a == 0)
                {
                    RequiredFieldValidator1.Enabled = false;
                    RequiredFieldValidator2.Enabled = false;
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
                    RequiredFieldValidator1.Enabled = true;
                    RequiredFieldValidator2.Enabled = true;
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
                    RequiredFieldValidator1.Enabled = false;
                    RequiredFieldValidator2.Enabled = false;
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
                    RequiredFieldValidator1.Enabled = true;
                    RequiredFieldValidator2.Enabled = true;
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








    protected void btnEditDate_Click(object sender, EventArgs e)
    {
        txtSDate.Visible = true;
        txtEDate.Visible = true;
        lblStartDate.Visible = false;
        lblEndDate.Visible = false;
        btnEditDate.Visible = false;
    }
}