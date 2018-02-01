package com.logic.stationerystoreinventorysystemmobile;

import org.json.JSONArray;

import java.util.ArrayList;
import java.util.HashMap;

public class Discrepancy extends HashMap<String, String> implements Comparable<Discrepancy>{

    static String host = R.string.host_add + "DiscrepancyService.svc/";

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

    public void saveRemarks(String remarks){
        put("remarks", remarks);
    }

    public static boolean submitDiscrepancies(ArrayList<Discrepancy> dList, boolean itemToUpdate){
        JSONArray a = new JSONArray(dList);
        String destination = "";
        if(itemToUpdate){
            destination = "SubmitDiscrepanciesWithItemUpdate";
        }
        else{
            destination = "SubmitDiscrepancies";
        }
        String url = host + destination;
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
