package com.logic.stationerystoreinventorysystemmobile;

import android.util.Log;

import org.json.JSONArray;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.List;

/**
 * Created by samch on 24/1/2018.
 */

public class Retrieval extends java.util.HashMap<String,String> {

   // final static String host = "http://172.17.255.213/StationeryStoreInventorySystem/RetrievalService.svc";//iss
   //final static String host = "http://192.168.1.8/StationeryStoreInventorySystem/RetrievalService.svc";//home
    final static String host = "http://172.23.230.9/StationeryStoreInventorySystem/RetrievalService.svc";//pc

    public Retrieval(String retrievedDate, String retrievalID, String retrievedBy) {
        put("RetrievedDate", retrievedDate);
        put("RetrievalID", retrievalID);
        put("RetrievedBy", retrievedBy);
    }

    public static List<Retrieval>list() {
        List<Retrieval> list = new ArrayList<Retrieval>();
        JSONArray a = JSONParser.getJSONArrayFromUrl(host + "/Retrieval");
        try {
            for (int i =0; i<a.length(); i++) {
                JSONObject b = a.getJSONObject(i);
                list.add(new Retrieval(
                        b.getString("RetrievedDate"),
                        b.getString("RetrievalID"),
                        b.getString("RetrievedBy")));
            }
        } catch (Exception e) {
            Log.e("Retrieval.list()", "JSONArray error");
        }
        return(list);
    }
}