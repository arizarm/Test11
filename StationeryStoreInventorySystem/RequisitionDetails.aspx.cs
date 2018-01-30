using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RequisitionDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Product p1 = new Product("P001", "2B Pencil", "Each", 40);
        Product p2 = new Product("Stapler Refills", 50);
        Product p3 = new Product("Ball-point Pen (Blue)", 40);
        //Product p4 = new Product("P004", "Fountain Pen (Black)", "Each", 20);
        Product p5 = new Product("Postit", 30);
        List<Product> pList = new List<Product>();
        //pList.Add(p1);
        pList.Add(p2);
        pList.Add(p3);
        //pList.Add(p4);
        pList.Add(p5);
        GridView1.DataSource = pList;
        GridView1.DataBind();
    }
}