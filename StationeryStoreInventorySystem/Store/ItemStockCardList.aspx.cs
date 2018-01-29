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
        showAll();
    }

    protected void SearchBtn_Click(object sender, EventArgs e)
    {
        List<Item> searchResults = EFBroker_Item.SearchItemsByItemCodeOrDesc(SearchBox.Text.ToLower());
        BindGrid(searchResults);
    }

    protected void Display_Click(object sender, EventArgs e)
    {
        showAll();
    }

    private void BindGrid(List<Item> iList)
    {
        GridView1.DataSource = iList;
        GridView1.DataBind();

        foreach(GridViewRow row in GridView1.Rows)
        {
            HyperLink itemLink = row.FindControl("lnkStockCard") as HyperLink;
            Label lblItemCode = row.FindControl("lblItemCode") as Label;
            itemLink.NavigateUrl = LoginController.ItemStockCardURI+"?itemCode=" + lblItemCode.Text;
        }
    }

    private void showAll()
    {
        List<Item> iList = EFBroker_Item.GetActiveItemList();
        BindGrid(iList);
    }
}