using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DisbursementListItems
/// </summary>
public class DisbursementListItems
{

    private int disbId;
    private string collectionDate;
    private string collectionTime;
    private string depCode;
    private string depName;
    private string collectionPoint;

    public DisbursementListItems(int disbId, string collectionDate, string collectionTime, string depCode, string depName, string collectionPoint)
    {
        this.disbId = disbId;
        this.collectionDate = collectionDate;
        this.collectionTime = collectionTime;
        this.depCode = depCode;
        this.depName = depName;
        this.collectionPoint = collectionPoint;
    }

    public int DisbId
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

    public string DepCode
    {
        get
        {
            return depCode;
        }

        set
        {
            depCode = value;
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