using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class InventoryStatusReport : System.Web.UI.Page
{
    ItemBusinessLogic ilogic;

    protected void Page_Load(object sender, EventArgs e)
    {
        //InventoryCrystalReport cr = new InventoryCrystalReport();
        //cr.SetDataSource(InventoryReportItem.getInventoryReportItems());
        //CrystalReportViewer1.ReportSource = cr;
        showAll();
    }
    protected void Display_Click(object sender, EventArgs e)
    {
        showAll();
    }
    protected void SearchBtn_Click(object sender, EventArgs e)
    {
        List<InventoryReportItem> searchResults = ItemBusinessLogic.GetSelectedInventoryReportItemList(SearchBox.Text.ToLower());
        BindGrid(searchResults);
    }
    private void BindGrid(List<InventoryReportItem> iList)
    {
        GridView1.DataSource = iList;
        GridView1.DataBind();

        foreach (GridViewRow row in GridView1.Rows)
        {
            HyperLink itemLink = row.FindControl("lnkStockCard") as HyperLink;
            Label lblItemCode = row.FindControl("lblItemCode") as Label;
            itemLink.NavigateUrl = LoginController.ItemStockCardURI + "?itemCode=" + lblItemCode.Text;
        }
    }
    private void showAll()
    {
        List<InventoryReportItem> iList = ItemBusinessLogic.GetInventoryReportItemList();
        BindGrid(iList);
    }
}