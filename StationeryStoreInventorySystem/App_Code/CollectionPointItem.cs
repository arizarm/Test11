using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CollectionPointItem
/// </summary>
/// 
//AUTHOR : CHOU MING SHENG
public class CollectionPointItem
{
    private string collectionPoint;
    private string defaultCollectionTime;

    public CollectionPointItem(string collectionPoint, string defaultCollectionTime)
    {
        this.collectionPoint = collectionPoint;
        this.defaultCollectionTime = defaultCollectionTime;
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

    public string DefaultCollectionTime
    {
        get
        {
            return defaultCollectionTime;
        }

        set
        {
            defaultCollectionTime = value;
        }
    }
}