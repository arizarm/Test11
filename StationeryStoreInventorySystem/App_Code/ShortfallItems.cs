using System;


//AUTHOR : KIRUTHIKA VENKATESH
public class ShortfallItems
{
    private DateTime edate;
    public string Description { get; set; }
    public DateTime? ExpectedDeliveryDate { get; set; }
    public string ItemCode { get; set; }
    public int? Quantityinhand { get; set; }
    public int? ReorderLevel { get; set; }
    public int? ReorderQuantity { get; set; }
    
    public decimal? Balance { get; set; }
    public string UnitOfMeasure { get; set; }
    public int? PurchaseOrderNo { get; set; }
    public DateTime? ExpectedDate { get; set; }
    public DateTime? OrderDate { get; set; }

    public string NullablePurchaseOrderNo
    {
        get
        {
            if (PurchaseOrderNo.HasValue)
            {
                return Convert.ToString(PurchaseOrderNo.Value);
            }
            else
            {
                return "";
            }


        }

        set {; }
    }
    public string FormattedExpectedDate {
        get
        {
            if(ExpectedDate.HasValue)
            {
                return ExpectedDate.Value.ToShortDateString();
            }
            else
            {
                return "";
            }
            

        }

        set {; }
    }
 
    
}