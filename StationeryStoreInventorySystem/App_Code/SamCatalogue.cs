using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class SamCatalogue
{
    private String itemNumber;
    private String category;
    private String description;
    private String reorderLevel;
    private String reorderQty;
    private String unitOfMeasure;

    public SamCatalogue()
    {
       
    }

    public SamCatalogue( String itemNumber, String category,String description, String reorderLevel, String reorderQty, String unitOfMeasure)
    {
        this.itemNumber = itemNumber;
        this.category = category;
        this.description = description;
        this.reorderLevel = reorderLevel;
        this.reorderQty = reorderQty;
        this.unitOfMeasure = unitOfMeasure;
    }

    public String ItemNumber
    {
        get { return itemNumber; }
    }

    public String Category
    {
        get { return category; }
    }

    public String Description
    {
        get { return description; }
    }

    public String ReorderLevel
    {
        get { return reorderLevel; }
    }

    public String ReorderQty
    {
        get { return reorderQty; }
    }

    public String UnitOfMeasure
    {
        get { return unitOfMeasure; }
    }
}