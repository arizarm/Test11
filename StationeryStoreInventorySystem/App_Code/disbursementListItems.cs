using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DisbursementListItems
/// </summary>
public class DisbursementListItems
{

    private string disbId;
    private string collectionDate;
    private string collectionTime;
    private string depName;
    private string collectionPoint;

    public DisbursementListItems(string disbId, string collectionDate, string collectionTime, string depName, string collectionPoint)
    {
        this.disbId = disbId;
        this.collectionDate = collectionDate;
        this.collectionTime = collectionTime;
        this.depName = depName;
        this.collectionPoint = collectionPoint;
    }

    public string DisbId
    {
        get
        {
            return disbId;
        }

        set
        {
            disbId = value;
        }
    }

    public string CollectionDate
    {
        get
        {
            return collectionDate;
        }

        set
        {
            collectionDate = value;
        }
    }

    public string CollectionTime
    {
        get
        {
            return collectionTime;
        }

        set
        {
            collectionTime = value;
        }
    }

    public string DepName
    {
        get
        {
            return depName;
        }

        set
        {
            depName = value;
        }
    }

    public string CollectionPoint
    {
        get
        {
            return collectionPoint;
        }

        set
        {
            collectionPoint = value;
        }
    }
}