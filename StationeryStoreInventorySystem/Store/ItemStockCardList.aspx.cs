using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ItemStockCardList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void SearchBtn_Click(object sender, EventArgs e)
    {
        List<Item> iList = EFBroker_Item.GetActiveItemList();
        List<Item> searchResults = new List<Item>();
        string searchString = SearchBox.Text.ToLower();
        foreach(Item i in iList)
        {
            if(i.ItemCode.ToLower().Contains(searchString) || i.Description.ToLower().Contains(searchString))
            {
                searchResults.Add(i);
            }
        }
        BindGrid(iList);
    }

    protected void Display_Click(object sender, EventArgs e)
    {
        List<Item> iList = EFBroker_Item.GetActiveItemList();
        BindGrid(iList);
    }

    private void BindGrid(List<Item> iList)
    {
        GridView1.DataSource = iList;
        GridView1.DataBind();

        foreach(GridViewRow row in GridView1.Rows)
        {
            HyperLink itemLink = row.FindControl("lnkStockCard") as HyperLink;
            Label lblItemCode = row.FindControl("lblItemCode") as Label;
            itemLink.NavigateUrl = "~/Store/ItemStockCard.aspx?itemCode=" + lblItemCode.Text;
        }
    }
}