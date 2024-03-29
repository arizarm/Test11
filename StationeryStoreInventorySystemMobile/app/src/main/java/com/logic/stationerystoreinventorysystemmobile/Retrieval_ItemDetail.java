package com.logic.stationerystoreinventorysystemmobile;

import android.util.Log;

import org.json.JSONArray;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.List;

//AUTHOR : CHOU MING SHENG
public class Retrieval_ItemDetail extends java.util.HashMap<String,String> {

     final static String host = Util.host +"RetrievalService.svc";

    public Retrieval_ItemDetail(String itemCode,String bin, String description, String totalRequestedQty,String retrievedQty) {//, String actualQty
        put("ItemCode", itemCode);
        put("Bin", bin);
        put("Description", description);
        put("TotalRequestedQty", totalRequestedQty);
        put("RetrievedQty", retrievedQty);
    }

    public void saveRetrievedQty(String retrievedQty){
        put("RetrievedQty", retrievedQty);
    }

    public static List<Retrieval_ItemDetail>getRetrieval_ItemDetail(String retrievalID) {
        List<Retrieval_ItemDetail> list = new ArrayList<Retrieval_ItemDetail>();
        JSONArray a = JSONParser.getJSONArrayFromUrl(host+"/Retrieval/"+retrievalID);
        try {
            for (int i =0; i<a.length(); i++) {
                JSONObject b = a.getJSONObject(i);
                list.add(new Retrieval_ItemDetail(
                        b.getString("ItemCode"),
                        b.getString("Bin"),
                        b.getString("Description"),
                        Integer.toString(b.getInt("TotalRequestedQty")),
                        Integer.toString(b.getInt("RetrievedQty"))));
            }
        } catch (Exception e) {
            Log.e("Retrieval.list()", "JSONArray error");
        }
        return(list);
    }

    //
//    public static void updateRetrieval(Retrieval_ItemDetail r) {
//        JSONObject jcustomer = new JSONObject();
//        try {
//            jcustomer.put("Id", r.get("Id"));
//            jcustomer.put("Name", r.get("Name"));
//            jcustomer.put("Address", r.get("Address"));
//            jcustomer.put("Credit", Integer.parseInt(r.get("Credit")));
//        } catch (Exception e) {
//        }
//        String result = JSONParser.postStream(host+"/Update", jcustomer.toString());
//    }
    //
}