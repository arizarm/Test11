package com.logic.stationerystoreinventorysystemmobile;

import android.util.Log;

import org.json.JSONArray;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.List;

public class Retrieval extends java.util.HashMap<String,String> {

    final static String host = Util.host +"RetrievalService.svc";

    public Retrieval(String retrievedDate, String retrievalID, String retrievedBy, String retrievalStatus) {
        put("RetrievedDate", retrievedDate);
        put("RetrievalID", retrievalID);
        put("RetrievedBy", retrievedBy);
        put("RetrievalStatus", retrievalStatus);
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
                        b.getString("RetrievedBy"),
                        b.getString("RetrievalStatus")));
            }
        } catch (Exception e) {
            Log.e("Retrieval.list()", "JSONArray error");
        }
        return(list);
    }
}