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
    static String ip = "172.17.249.125";
//    static String ip = "172.23.202.59";
//    static String ip = "192.168.1.224";

    static String host = "http://"+ ip + "/StationeryStoreInventorySystem/ItemService.svc/";

    public CatalogueItem(String itemCode, String description, String unitOfMeasure, Integer balanceQty, String adjustments, String bin){
        put("itemCode", itemCode);
        put("description", description);
        put("unitOfMeasure", unitOfMeasure);
        put("balanceQty", balanceQty.toString());
        put("adjustments", adjustments);
        put("bin", bin);
    }

    public void monthlyActualInput(String actualQty){
        put("correctQty", "N");
        put("actualQty", actualQty.toString());
    }

    public void monthlyCorrectInput(){
        put("correctQty", "Y");
    }

    public static ArrayList<CatalogueItem> getAllItems(){
        ArrayList<CatalogueItem> ciList = new ArrayList<CatalogueItem>();
        try{
            JSONArray a = JSONParser.getJSONArrayFromUrl(host+"CatalogueItems");
            for(int i =0;i<a.length();i++)
            {
                JSONObject b= a.getJSONObject(i);
                Integer adjustments = b.getInt("Adjustments");
                String adjStr = getAdjustmentString(adjustments);
                CatalogueItem ci = new CatalogueItem(b.getString("ItemCode"), b.getString("Description"), b.getString("UnitOfMeasure"), b.getInt("BalanceQty"), adjStr, b.getString("Bin"));
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
                Integer adjustments = b.getInt("Adjustments");
                String adjStr = getAdjustmentString(adjustments);
                CatalogueItem ci = new CatalogueItem(b.getString("ItemCode"), b.getString("Description"), b.getString("UnitOfMeasure"), b.getInt("BalanceQty"), adjStr, b.getString("Bin"));
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
            Integer adjustments = b.getInt("Adjustments");
            String adjStr = getAdjustmentString(adjustments);
            ci = new CatalogueItem(b.getString("ItemCode"), b.getString("Description"), b.getString("UnitOfMeasure"), b.getInt("BalanceQty"), adjStr, b.getString("Bin"));
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
