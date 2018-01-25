package com.logic.stationerystoreinventorysystemmobile;

import android.util.Log;

import org.json.JSONArray;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

/**
 * Created by April Wang on 22/1/2018.
 */

public class Requisition_ItemList extends HashMap<String,String> {
    final static String host="http://172.17.255.216/StationeryStoreInventorySystem/RequisitionListService.svc";

    public Requisition_ItemList(String description, String reqQty, String uom, String status){
        put("Description",description);
        put("ReqQty",reqQty);
        put("Status",status);
        put("Uom",uom);
    }

    public Requisition_ItemList(){}

    public Requisition_ItemList(String description,String reqQty, String uom){
        put("Description",description);
        put("ReqQty",reqQty);
        put("Uom",uom);
    }

    public static List<Requisition_ItemList> getList(String id){
        List<Requisition_ItemList> list = new ArrayList<>();
        JSONArray a = JSONParser.getJSONArrayFromUrl(host+"/Requisition/"+id);
        //JSONObject b = JSONParser.getJSONFromUrl(host+"/Requisition/"+id);
        try{
            for(int i=0;i<a.length();i++){
                JSONObject b = a.getJSONObject(i);
                list.add(new Requisition_ItemList(b.getString("Description"),b.getString("ReqQty"),b.getString("Uom")));
            }
        }catch(Exception e){
            Log.e("Req.getList()", e.toString() );
        }
        return list;
    }
}
