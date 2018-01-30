package com.logic.stationerystoreinventorysystemmobile;

import org.json.JSONArray;

import java.util.ArrayList;
import java.util.HashMap;

/**
 * Created by edwon on 27/1/2018.
 */

public class Discrepancy extends HashMap<String, String> implements Comparable<Discrepancy>{

//    static String ip = "172.17.249.125";
    static String ip = "172.23.202.59";
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

<<<<<<< HEAD
    public void saveRemarks(String remarks){
        put("remarks", remarks);
    }

    public static boolean submitDiscrepancies(ArrayList<Discrepancy> dList){
=======
    public static void submitDiscrepancies(ArrayList<Discrepancy> dList){
>>>>>>> ff49bafc83d38a2417f4e01ae5c35ec979ed0e5e
        JSONArray a = new JSONArray(dList);
        String url = host + "SubmitDiscrepancies";
        String result = JSONParser.postStream(url, a.toString());

        //Check whether saving to database was successful, based on bool return from WCF
        if(result.toLowerCase().contains("true")){
            return true;
        }
        else{
            return false;
        }
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
