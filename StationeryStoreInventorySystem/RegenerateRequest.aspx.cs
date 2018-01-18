using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RegenerateRequest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        GridView1.Visible = true;
        //Item i1 = new Item("C010","Clips Double 2",5);
        //Item i2= new Item("S002", "Short Hand Book", 2);
        //Item i3 = new Item("P049", "Pad Positit 2 x 4", 3);

        //List<Item> itemList = new List<Item>();
        //itemList.Add(i1);
        //itemList.Add(i2);
        //itemList.Add(i3);

        //GridView1.DataSource = itemList;
        GridView1.DataBind();
    }
}