using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PurchaseOrderForm : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        GridView1.DataSource = getItems();
        GridView1.DataBind();
    }

    private List<StationeryItem> getItems()
    {
        List<StationeryItem> itemList = new List<StationeryItem>();
        StationeryItem it1 = new StationeryItem("1", "Pencil", 25,10, 1.2, 42.0);
        itemList.Add(it1);
        StationeryItem it2 = new StationeryItem("2", "Pen", 15,5, 2.2, 44.0);
        itemList.Add(it2);
        StationeryItem it3 = new StationeryItem("3", "Highlighter", 5,4, 3.2, 28.8);
        itemList.Add(it3);
        StationeryItem it4 = new StationeryItem("4", "Eraser", 5,30, 0.70,24.5);
        itemList.Add(it4);
        StationeryItem it5 = new StationeryItem("5", "Shorthand Book",2, 5, 6.2, 43.4);
        itemList.Add(it5);
        StationeryItem it6 = new StationeryItem("6", "Marker", 10,10, 4.2, 84.0);
        itemList.Add(it6);
        StationeryItem it7 = new StationeryItem("7", "Exercise Book", 2,4, 7.2, 43.2);
        itemList.Add(it7);
        StationeryItem it8 = new StationeryItem("7", "Pen Ball point blue", 25,50, 1.2, 90.0);
        itemList.Add(it8);
        return itemList;

    }
}