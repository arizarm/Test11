package com.logic.stationerystoreinventorysystemmobile;

import android.util.Log;

import org.json.JSONArray;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

/**
 * Created by Mo Mo on 23/1/2018.
 */

public class DisbursementListItems extends HashMap<String,String>
{
        final static String host="http://192.168.0.100/StationaryStoreInventorySystem/DisbursementService.svc";

//    public DisbursementListItems(String description, String reqQty, String uom, String status){
//        put("Description",description);
//        put("ReqQty",reqQty);
//        put("Status",status);
//        put("Uom",uom);
//    }
//
    public DisbursementListItems(){}

    public DisbursementListItems(String CollectionDate,String CollectionPoint, String CollectionTime,
                                 String DepName, String DisbId){
        put("CollectionDate",CollectionDate);
        put("CollectionPoint",CollectionPoint);
        put("CollectionTime",CollectionTime);
        put("DepName",DepName);
        put("DisbId",DisbId);
    }

    public static List<DisbursementListItems> getJSONObj(String url)
    {
        JSONArray a = JSONParser.getJSONArrayFromUrl(url);
        List<DisbursementListItems> list = new ArrayList<>();
        try{
            for(int i=0;i<a.length();i++){
                JSONObject b = a.getJSONObject(i);
                list.add(new DisbursementListItems(b.getString("CollectionDate"),b.getString("CollectionPoint"),
                        b.getString("CollectionTime"),b.getString("DepName"),b.getString("DisbId")));
            }
        }catch(Exception e){
            Log.e("DisbursementListItems()", e.toString() );
        }
        return list;
    }

    public static List<DisbursementListItems> getDisbursementListItems(){

        String url = host+"/Disbursement";
        return getJSONObj(url);
    }

    public static List<DisbursementListItems> getDisbursementListItemsbyId(String id){
        String url = host + "/Disbursement/" + id;
        return  getJSONObj(url);
    }
}