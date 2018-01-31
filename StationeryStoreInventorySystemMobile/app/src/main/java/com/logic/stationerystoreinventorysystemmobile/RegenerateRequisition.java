package com.logic.stationerystoreinventorysystemmobile;

import android.util.Log;

import org.json.JSONArray;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.HashMap;

/**
 * Created by Mo Mo on 30/1/2018.
 */

public class RegenerateRequisition extends HashMap<String, String> {


    //final static String host = "http://192.168.0.100/StationeryStoreInventorySystem/DisbursementService.svc";
    final static String host="http://172.17.249.194/StationeryStoreInventorySystem/DisbursementService.svc";

    public RegenerateRequisition() {
    }

    public RegenerateRequisition(String ReqDate, String ReqBy, String DepName) {
        put("ReqDate", ReqDate);
        put("ReqBy", ReqBy);
        put("DepName", DepName);
    }

    public RegenerateRequisition(String Code, String Description, String ShortfallQty, String DisbId){
        put("Code",Code);
        put("Description",Description);
        put("ShortfallQty",ShortfallQty);
        put("DisbId", DisbId);
    }

    public static RegenerateRequisition GetRegenrateInfo(String id)
    {
        RegenerateRequisition list = new RegenerateRequisition();
        try
        {
            JSONObject b = JSONParser.getJSONFromUrl(host + "/GetRegenerateDate/" + id);
            list = new RegenerateRequisition(b.getString("ReqDate"),
                    b.getString("ReqBy"), b.getString("DepName"));
        } catch (Exception e) {
            Log.e("GetRegenrateInfo", e.toString());
        }
        return list;
    }

    public static void RegenerateRequisition(ArrayList<RegenerateRequisition> regenList){
        JSONArray a = new JSONArray(regenList);
        String url = host + "/RegenerateRequest";
        JSONParser.postStream(url, a.toString());
    }
}
