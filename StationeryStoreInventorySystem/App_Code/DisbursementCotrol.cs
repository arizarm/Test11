using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DisbursementCotrol
/// </summary>
public class DisbursementCotrol
{
    static StationeryEntities context = new StationeryEntities();
    static List<Disbursement> testDisb;
    static List<disbursementListItems> disbursementListItems;
    static disbursementListItems item;


    public static List<disbursementListItems> gvDisbursementPopulate()
    {
        testDisb = new List<Disbursement>();
        disbursementListItems = new List<disbursementListItems>();

        testDisb = context.Disbursements.ToList();

        string disbId;
        string depName;
        string collectionPoint;
        string collectionDate;
        string collectionTime;
        string depCode;
        int colId;

        for (int i = 0; i < testDisb.Count; i++)
        {
            disbId = testDisb[i].DisbursementID.ToString();

            depCode = testDisb[i].DeptCode;
            depName = context.Departments.Where(x => x.DeptCode.Equals(depCode)).Select(x => x.DeptName).First().ToString();

            collectionDate = testDisb[i].CollectionDate.Value.ToLongDateString();
            collectionTime = testDisb[i].CollectionTime.ToString();

            colId = Convert.ToInt32(context.Departments.Where(x => x.DeptCode.Equals(depCode)).Select(x => x.CollectionLocationID).First().ToString());
            collectionPoint = context.CollectionPoints.Where(x => x.CollectionLocationID.Equals(colId)).Select(x => x.CollectionPoint1).First().ToString();

            item = new disbursementListItems(disbId, collectionDate, collectionTime, depName, collectionPoint);
            disbursementListItems.Add(item);
        }

        return disbursementListItems;
    }

    public static List<string> gvDisbursementDetailPopulate(string disbId)
    {
        List<string> date = new List<string>();

        foreach(disbursementListItems i in disbursementListItems)
        {
            if(i.DisbId.ToString() == disbId)
            {
                date.Add(i.CollectionDate.ToString());
            }            
        }
        return date;
    }
}