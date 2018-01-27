package com.logic.stationerystoreinventorysystemmobile;

import android.util.Log;

import org.json.JSONArray;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.HashMap;

/**
 * Created by edwon on 24/1/2018.
 */

public class CatalogueItem extends HashMap<String, String> {
//    static String ip = "172.17.249.125";
    static String ip = "172.23.200.138";
    static String host = "http://"+ ip + "/StationeryStoreInventorySystem/ItemService.svc/";

//    public CatalogueItem(String itemCode, Integer categoryID, String description, Integer reorderLevel, Integer reorderQty, String unitOfMeasure, String bin, String activeStatus, Integer balanceQty){
//        put("itemCode", itemCode);
//        put("description", description);
//        put("unitOfMeasure", unitOfMeasure);
//        put("balanceQty", balanceQty.toString());
//        put("categoryID", categoryID.toString());
//        put("reorderLevel", reorderLevel.toString());
//        put("reorderQty", reorderQty.toString());
//        put("bin", bin);
//        put("activeStatus", activeStatus);
//    }

    public CatalogueItem(String itemCode, String description, String unitOfMeasure, Integer balanceQty, String adjustments){
        put("itemCode", itemCode);
        put("description", description);
        put("unitOfMeasure", unitOfMeasure);
        put("balanceQty", balanceQty.toString());
        put("adjustments", adjustments);
//        put("balanceQtyWithAdj", balanceQty.toString() + " (" + adjustments + ")");
    }

    public static ArrayList<CatalogueItem> getAllItems(){
        ArrayList<CatalogueItem> ciList = new ArrayList<CatalogueItem>();
        try{
            JSONArray a = JSONParser.getJSONArrayFromUrl(host+"CatalogueItems");
            for(int i =0;i<a.length();i++)
            {
                JSONObject b= a.getJSONObject(i);
                Integer adjustments = b.getInt("adjustments");
                String adjStr = getAdjustmentString(adjustments);
                CatalogueItem ci = new CatalogueItem(b.getString("itemCode"), b.getString("description"), b.getString("unitOfMeasure"), b.getInt("balanceQty"), adjStr);
                ciList.add(ci);
            }
        }
        catch(Exception e){
            e.printStackTrace();
        }
        return ciList;
    }
    public static ArrayList<CatalogueItem> searchItems(String searchString){
        ArrayList<CatalogueItem> ciList = new ArrayList<CatalogueItem>();
        try{
            JSONArray a = JSONParser.getJSONArrayFromUrl(host+"CatalogueItems/" + searchString);
            for(int i =0;i<a.length();i++)
            {
                JSONObject b= a.getJSONObject(i);
                Integer adjustments = b.getInt("adjustments");
                String adjStr = getAdjustmentString(adjustments);
                CatalogueItem ci = new CatalogueItem(b.getString("itemCode"), b.getString("description"), b.getString("unitOfMeasure"), b.getInt("balanceQty"), adjStr);
                ciList.add(ci);
            }
        }
        catch(Exception e){
            e.printStackTrace();
        }
        return ciList;
    }

    public static CatalogueItem getItem(String itemCode) {
        CatalogueItem ci = null;
        try {
            JSONObject b = JSONParser.getJSONFromUrl(host+"GetItem/"+itemCode);
            Integer adjustments = b.getInt("adjustments");
            String adjStr = getAdjustmentString(adjustments);
            ci = new CatalogueItem(b.getString("itemCode"), b.getString("description"), b.getString("unitOfMeasure"), b.getInt("balanceQty"), adjStr);
        }
        catch(Exception e){
            e.printStackTrace();
        }
        return ci;
    }

    private static String getAdjustmentString(Integer adjustments){
        String adjStr = "";
        if (adjustments > 0)
        {
            adjStr = "+" + adjustments.toString();
        }
        else
        {
            adjStr = adjustments.toString();
        }
        return adjStr;
    }
}
