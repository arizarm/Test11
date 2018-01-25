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
    static String host = "http://172.17.249.125/StationeryStoreInventorySystem/ItemService.svc/";

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

    public CatalogueItem(String itemCode, String description, String unitOfMeasure, Integer balanceQty){
        put("itemCode", itemCode);
        put("description", description);
        put("unitOfMeasure", unitOfMeasure);
        put("balanceQty", balanceQty.toString());
    }

    public static ArrayList<CatalogueItem> getAllBooks(){
        ArrayList<CatalogueItem> ciList = new ArrayList<CatalogueItem>();
        try{
            JSONArray a = JSONParser.getJSONArrayFromUrl(host+"CatalogueItems");
            for(int i =0;i<a.length();i++)
            {
                JSONObject b= a.getJSONObject(i);
                CatalogueItem ci = new CatalogueItem(b.getString("itemCode"), b.getString("description"), b.getString("unitOfMeasure"), b.getInt("balanceQty"));
                ciList.add(ci);
            }
        }
        catch(Exception e){
            e.printStackTrace();
        }
        return ciList;
    }
}
