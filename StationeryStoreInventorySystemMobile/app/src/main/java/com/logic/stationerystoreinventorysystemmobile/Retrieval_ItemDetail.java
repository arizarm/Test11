package com.logic.stationerystoreinventorysystemmobile;

import android.util.Log;

import org.json.JSONArray;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.List;

/**
 * Created by samch on 26/1/2018.
 */

public class Retrieval_ItemDetail extends java.util.HashMap<String,String> {

    // final static String host = "http://172.17.255.213/StationeryStoreInventorySystem/RetrievalService.svc";//iss
    //final static String host = "http://192.168.1.8/StationeryStoreInventorySystem/RetrievalService.svc";//home
    final static String host = "http://172.23.230.9/StationeryStoreInventorySystem/RetrievalService.svc";//pc

    public Retrieval_ItemDetail(String bin, String description, String totalRequestedQty) {//, String actualQty
        put("Bin", bin);
        put("Description", description);
        put("TotalRequestedQty", totalRequestedQty);
    }

    public static List<Retrieval_ItemDetail>getRetrieval_ItemDetail(String retrievalID) {
        List<Retrieval_ItemDetail> list = new ArrayList<Retrieval_ItemDetail>();
        JSONArray a = JSONParser.getJSONArrayFromUrl(host+"/Retrieval/"+retrievalID);
        try {
            for (int i =0; i<a.length(); i++) {
                JSONObject b = a.getJSONObject(i);
                list.add(new Retrieval_ItemDetail(
                        b.getString("Bin"),
                        b.getString("Description"),
                        Integer.toString(b.getInt("TotalRequestedQty"))));
            }
        } catch (Exception e) {
            Log.e("Retrieval.list()", "JSONArray error");
        }
        return(list);
    }
}