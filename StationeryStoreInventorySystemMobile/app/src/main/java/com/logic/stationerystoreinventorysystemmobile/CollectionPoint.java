package com.logic.stationerystoreinventorysystemmobile;

import android.util.Log;

import org.json.JSONArray;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.List;

//AUTHOR : KHIN MYO MYO SHWE
public class CollectionPoint extends  java.util.HashMap<String,String> {
    final static String hostURL = Util.host +"DeptService.svc/";

    public CollectionPoint(){};
    public CollectionPoint(String collectId, String collectpointName, String defaulttime) {
        put("collectId", collectId);
        put("collectpointName", collectpointName);
        put("defaulttime", defaulttime);
    }

    public CollectionPoint(String collectId, String collectpointName) {
        put("collectId", collectId);
        put("collectpointName", collectpointName);
    }

    public CollectionPoint(String collectpointName) {

        put("collectId", collectpointName);
    }

    public static List<String> listCollectionPoint() {
        List<String> dlist = new ArrayList<String>();
        JSONArray a = JSONParser.getJSONArrayFromUrl(hostURL + "Collectpoint");
        try {
            for (int i = 0; i < a.length(); i++) {
                JSONObject b = a.getJSONObject(i);
                dlist.add(b.getString("Collectionpoint"));
            }
        } catch (Exception e) {
            Log.e("CollectionPoint.list()", "JSONArray error");
        }
        return (dlist);
    }






}