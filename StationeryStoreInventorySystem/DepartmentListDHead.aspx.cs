using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DepartmentListDHead : System.Web.UI.Page
{
    static string dcode = "BDTD";
    Employee empActingDHead = DeptBusinessLogic.getEmployeeListForActingDHeadSelected(dcode);
    Employee empDRep = DeptBusinessLogic.getEmployeeListForDRepSelected(dcode);
    protected void Page_Load(object sender, EventArgs e)
    {

        Session["deptcode"] = dcode;
        if (!IsPostBack)
        {
            Department dept = DeptBusinessLogic.getDepartByDepCode(dcode);
            Employee emp = DeptBusinessLogic.getEmployeeByDeptCode(dcode);


            string dname = dept.DeptName;
            string contactname = dept.DeptContactName;
            string telephone = dept.DeptTelephone;
            string fax = dept.DeptFax;
            string dheadname = emp.EmpName;
            string startdate = empActingDHead.StartDate.GetValueOrDefault().Date.ToShortDateString();
            string enddate = empActingDHead.EndDate.GetValueOrDefault().ToShortDateString();


            //DateTime? endTime = emp.EndDate;
            lblDeptName.Text = dname;
            lblContactName.Text = contactname;
            lblPhone.Text = telephone;
            lblFax.Text = fax;
            lblHeadname.Text = dheadname;
            txtSDate.Text = startdate;
            txtEDate.Text = enddate;

            //Date
            CompareEndTodayValidator.ValueToCompare = DateTime.Now.ToShortDateString();

            int empRid = empDRep.EmpID;//ForDeptRep Id

            //UpdateActingDHead   
            string empActingDHeadname = empActingDHead.EmpName;
            int empid = empActingDHead.EmpID;
            DropDownListActingDHead.DataSource = DeptBusinessLogic.getEmployeeListForActingDHead(dcode, empRid);
            DropDownListActingDHead.DataTextField = "EmpName";
            DropDownListActingDHead.DataValueField = "EmpID";
            DropDownListActingDHead.DataBind();
            DropDownListActingDHead.Items.FindByText(empActingDHeadname).Selected = true;

            //UpdateDeptRp
            string empDRepname = empDRep.EmpName;
            DropDownListDRep.DataSource = DeptBusinessLogic.getEmployeeListForDRep(dcode, empid);
            DropDownListDRep.DataTextField = "EmpName";
            DropDownListDRep.DataValueField = "EmpID";
            DropDownListDRep.DataBind();
            DropDownListDRep.Items.FindByText(empDRepname).Selected = true;

            //UpdateCollectionPoint
            string empCollectionname = DeptBusinessLogic.getDepartmentForCollectionPointSelected(dcode);
            DropDownListCollectionPoint.DataSource = DeptBusinessLogic.getCollectionPointList();
            DropDownListCollectionPoint.DataTextField = "CollectionPoint1";
            DropDownListCollectionPoint.DataValueField = "CollectionLocationID";
            DropDownListCollectionPoint.DataBind();
            DropDownListCollectionPoint.Items.FindByText(empCollectionname).Selected = true;
        }

    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        Session["deptcode"] = dcode;
        if (dcode != null)
        {
            int c = Convert.ToInt16(DropDownListCollectionPoint.SelectedValue);
            DeptBusinessLogic.UpdateCollectionPoint(dcode, c);

            int empid = Convert.ToInt16(DropDownListDRep.SelectedValue);
            DeptBusinessLogic.UpdateDeptRep(dcode, empid);

            int Aempid = Convert.ToInt16(DropDownListActingDHead.SelectedValue);
            string sdate = txtSDate.Text;
            string edate = txtEDate.Text;
            DeptBusinessLogic.UpdateActingDHead(dcode, Aempid, sdate, edate);
            lblMessage.Text = "Update Successfully!";
        }
        else
        {
            lblMessage.Text = "Update Failed!";
        }
    }



    protected void DropDownListActingDHead_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["deptcode"] = dcode;
        string empDRepname = empDRep.EmpName;

        int a = Convert.ToInt16(DropDownListActingDHead.SelectedValue);
        //lblFax.Text = a.ToString();
        DropDownListDRep.DataSource = DeptBusinessLogic.getEmployeeListForDRep(dcode, a);
        DropDownListDRep.DataBind();
        DropDownListDRep.Items.FindByText(empDRepname).Selected = true;
    }



    protected void DropDownListDRep_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Session["deptcode"] = dcode;
        //string empActingDHeadname = empActingDHead.EmpName;
        //int a = Convert.ToInt16(DropDownListDRep.SelectedValue);
        //lblFax.Text = a.ToString();
        //DropDownListActingDHead.DataSource = DeptBusinessLogic.getEmployeeListForActingDHead(dcode, a);
        //DropDownListActingDHead.DataBind();
        // DropDownListActingDHead.Items.FindByText(empActingDHeadname).Selected = true;

    }
}