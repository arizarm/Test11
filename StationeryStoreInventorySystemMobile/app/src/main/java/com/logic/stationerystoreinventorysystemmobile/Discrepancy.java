package com.logic.stationerystoreinventorysystemmobile;

import java.util.HashMap;

/**
 * Created by edwon on 27/1/2018.
 */

public class Discrepancy extends HashMap<String, String> {

//    static String ip = "172.17.249.125";
    static String ip = "172.23.200.138";
    static String host = "http://"+ ip + "/StationeryStoreInventorySystem/ItemService.svc/";

//    //for submission
//    public Discrepancy(String itemCode, Integer requestedBy, Integer adjustmentQty, String remarks, String status){
//        put("itemCode", itemCode);
//        put("requestedBy",requestedBy.toString());
//        put("adjustmentQty",adjustmentQty.toString());
//        put("remarks",remarks);
//        put("status",status);
//    }

    //for display in summary page
    public Discrepancy(String itemCode, String itemName, Integer balanceQty, Integer adjustmentQty){
        put("itemCode", itemCode);
        put("itemName", itemName);
        put("balanceQty", balanceQty.toString());
        put("adjustmentQty", getAdjustmentString(adjustmentQty));
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
}
