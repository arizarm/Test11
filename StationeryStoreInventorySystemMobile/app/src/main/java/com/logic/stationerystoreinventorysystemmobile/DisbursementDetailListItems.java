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

public class DisbursementDetailListItems extends HashMap<String,String>
{
    final static String host="http://192.168.0.100/StationaryStoreInventorySystem/DisbursementService.svc";

    public DisbursementDetailListItems(){}

    public DisbursementDetailListItems(String ActualQty,String ItemCode, String ItemDesc,
                                 String Remarks, String ReqQty){
        put("ActualQty",ActualQty);
        put("ItemCode",ItemCode);
        put("ItemDesc",ItemDesc);
        put("Remarks",Remarks);
        put("ReqQty",ReqQty);
    }

    public static List<DisbursementDetailListItems> getDisbursementDetailListItems(){
        List<DisbursementDetailListItems> list = new ArrayList<>();
        JSONArray a = JSONParser.getJSONArrayFromUrl(host+"/DisbursementDetail");
        try{
            for(int i=0;i<a.length();i++){
                JSONObject b = a.getJSONObject(i);
                list.add(new DisbursementDetailListItems(b.getString("ActualQty"),Integer.toString(b.getInt ("ItemCode")),
                        b.getString("ItemDesc"),b.getString("Remarks"),Integer.toString(b.getInt ("ReqQty"))));
            }
        }catch(Exception e){
            Log.e("getDisbDetailList", e.toString() );
        }
        return list;
    }
}