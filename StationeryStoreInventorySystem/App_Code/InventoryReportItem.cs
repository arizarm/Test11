using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for InventoryReportItem
/// </summary>
public class InventoryReportItem
{
    public string itemCode { get; set; }
    public string description { get; set; }
    public string bin { get; set; }
    public string unitOfMeasurement { get; set; }
    public int currentQty { get; set; }
    public int reorderLevel { get; set; }

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
}