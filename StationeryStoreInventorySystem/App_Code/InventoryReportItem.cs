using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for InventoryReportItem
/// </summary>
public class InventoryReportItem
{
    private string itemCode;
    private string description;
    private string bin;
    private string unitOfMeasurement;
    private int currentQty;
    private int reorderLevel;

    public InventoryReportItem()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public InventoryReportItem(Item item)
    {
        this.itemCode = item.ItemCode ?? "None";
        this.description = item.Description ?? "None";
        this.bin = item.Bin ?? "None";
        this.unitOfMeasurement = item.UnitOfMeasure ?? "None";
        this.currentQty = item.BalanceQty ?? 0;
        this.reorderLevel = item.ReorderLevel;
    }
    public static List<InventoryReportItem> getInventoryReportItems()
    {
        ItemBusinessLogic itemBusinessLogic = new ItemBusinessLogic();
        List<InventoryReportItem> reportItemList = new List<InventoryReportItem>();
        List<Item> iList = itemBusinessLogic.getItemList();
        foreach(Item i in iList)
        {
            InventoryReportItem rItem = new InventoryReportItem(i);
            reportItemList.Add(rItem);
        }
        return reportItemList;
    }
}