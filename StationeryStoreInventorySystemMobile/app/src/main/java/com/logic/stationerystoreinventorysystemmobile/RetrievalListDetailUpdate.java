package com.logic.stationerystoreinventorysystemmobile;

import android.util.Log;

import org.json.JSONArray;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

//AUTHOR : CHOU MING SHENG
public class RetrievalListDetailUpdate extends java.util.HashMap<String,String> {

     final static String host = Util.host +"/RetrievalService.svc";

    public RetrievalListDetailUpdate(String retrievalID,String itemCode,String itemQty ) {
        put("RetrievalID", retrievalID);
        put("ItemCode", itemCode);
        put("ItemQty", itemQty);
    }

    public static void updateRetrieval(String retrievalID,String itemCode,String itemQty) {
        JSONObject jretrieval = new JSONObject();
        try {
            jretrieval.put("RetrievalID", retrievalID);
            jretrieval.put("ItemCode", itemCode);
            jretrieval.put("ItemQty", itemQty);
        } catch (Exception e) {
        }
        String result = JSONParser.postStream(host+"/RetrievalListDetailUpdate", jretrieval.toString());
    }

    //try jsonarray
//    public static List<RetrievalListDetailUpdate>updateRetrieval() {
//        List<RetrievalListDetailUpdate> list = new ArrayList<RetrievalListDetailUpdate>();
//        JSONArray a = JSONParser.getJSONArrayFromUrl(host + "/RetrievalListDetailUpdate");
//        try {
//            for (int i =0; i<a.length(); i++) {
//                JSONObject b = a.getJSONObject(i);
//                list.add(new RetrievalListDetailUpdate(
//                        b.getString("RetrievalID"),
//                        b.getString("ItemCode"),
//                        b.getString("ItemQty")));
//            }
//        } catch (Exception e) {
//        }
//        return(list);
//    }
    //
}