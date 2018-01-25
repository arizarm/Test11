package com.logic.stationerystoreinventorysystemmobile;

import android.util.Log;

import org.json.JSONArray;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.List;

/**
 * Created by samch on 24/1/2018.
 */

//
//class Retrieval {
//    public static List<Retrieval> list() {
//    }
//}


public class Retrieval extends java.util.HashMap<String,String> {

    //final static String host = "http://172.17.255.213/StationaryStoreTeam11/RetrievalService.svc";
    final static String host = "http://192.168.1.8/StationaryStoreTeam11/RetrievalService.svc";


    public Retrieval(String retrievedDate, String retrievalID, String retrievedBy) {
        put("RetrievedDate", retrievedDate);
        put("RetrievalID", retrievalID);
        put("RetrievedBy", retrievedBy);
    }

    public Retrieval(String bin, String description, String totalRequestedQty, String actualQty) {
        put("Bin", bin);
        put("Description", description);
        //put("ItemCode", itemCode);
        put("TotalRequestedQty", totalRequestedQty);
      //  put("ActualQty", actualQty);
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

    //The getCustomer() method retrieves the JSONArray of Customer
    // objects for the SimpleAdapter for the ListCustomer.

    public static Retrieval getRetrievalDetail(String retrievalID) {
        Retrieval r = null;
        try {
            JSONObject c = JSONParser.getJSONFromUrl(host+"/Retrieval/"+retrievalID);
            r = new Retrieval(c.getString("Bin"),
                    c.getString("Description"),
                    c.getString("ItemCode"),
                    c.getString("TotalRequestedQty"));
                //    c.getString("ActualQty"));
//                    Integer.toString(c.getInt("TotalRequestedQty"),
//                    Integer.toString(c.getInt("ActualQty"))));
        } catch (Exception e) {
        }
        return r;
    }
}