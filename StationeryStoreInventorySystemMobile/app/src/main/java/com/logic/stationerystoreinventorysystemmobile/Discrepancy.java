package com.logic.stationerystoreinventorysystemmobile;

import org.json.JSONArray;

import java.util.ArrayList;
import java.util.HashMap;

/**
 * Created by edwon on 27/1/2018.
 */

public class Discrepancy extends HashMap<String, String> implements Comparable<Discrepancy>{

    static String ip = "172.17.249.125";
//    static String ip = "172.23.202.59";
    static String host = "http://"+ ip + "/StationeryStoreInventorySystem/DiscrepancyService.svc/";

    //for submission to WCF
    public Discrepancy(String itemCode, Integer requestedBy, Integer adjustmentQty, String remarks, String status){
        put("ItemCode", itemCode);
        put("RequestedBy",requestedBy.toString());
        put("AdjustmentQty",adjustmentQty.toString());
        put("Remarks",remarks);
        put("Status",status);
    }

    //for display in summary page
    public Discrepancy(String itemCode, String description, Integer balanceQty, Integer adjustmentQty){
        put("itemCode", itemCode);
        put("description", description);
        put("balanceQty", balanceQty.toString());
        put("adjustmentQty", getAdjustmentString(adjustmentQty));
    }

    public static void submitDiscrepancies(ArrayList<Discrepancy> dList){
        JSONArray a = new JSONArray(dList);
        String url = host + "SubmitDiscrepancies";
        JSONParser.postStream(url, a.toString());
    }

    private String getAdjustmentString(Integer adj){
        String adjStr = "";
        if (adj > 0)
        {
            adjStr = "+" + adj.toString();
        }
        else
        {
            adjStr = adj.toString();
        }
        return adjStr;
    }

    public int compareTo(Discrepancy d){
        return this.get("itemCode").compareTo(d.get("itemCode"));
    }
}
