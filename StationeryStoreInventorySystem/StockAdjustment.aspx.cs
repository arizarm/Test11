using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StockAdjustment : System.Web.UI.Page
{
    string a = "P006";
    string b = "-6";
    string c = "Broken items";


    protected void Page_Load(object sender, EventArgs e)
    {
        Label2.Text = DateTime.Today.ToString();

        Samitem i1 = new Samitem("P006","-2","Broken items");
        Samitem i2 = new Samitem("F120","8", "Free gift");

        List<Samitem> items = new List<Samitem>();
        items.Add(i1);
        items.Add(i2);

        GridView1.DataSource = items;
        GridView1.DataBind();
    }
}

