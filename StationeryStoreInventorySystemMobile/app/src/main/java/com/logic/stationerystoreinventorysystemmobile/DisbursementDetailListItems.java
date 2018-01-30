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

    //final static String host="http://192.168.0.100/StationeryStoreInventorySystem/DisbursementService.svc";
    final static String host="http://172.17.249.194/StationeryStoreInventorySystem/DisbursementService.svc";

    public DisbursementDetailListItems(){}

    public DisbursementDetailListItems(String ActualQty,String ItemCode, String ItemDesc,
                                 String Remarks, String ReqQty){
        put("ActualQty",ActualQty);
        put("ItemCode",ItemCode);
        put("ItemDesc",ItemDesc);
        put("Remarks",Remarks);
        put("ReqQty",ReqQty);
    }

    public DisbursementDetailListItems(String DisbId, String ActualQty, String Remark){
        put("DisbId",DisbId);
        put("ActualQty",ActualQty);
        put("Remark",Remark);
    }

    public void saveActualQty(String actualQty){
        put("ActualQty", actualQty);
    }

    public void saveRemarks(String remarks){
        put("Remark", remarks);
    }

    public static void UpdateDisbursement(ArrayList<DisbursementDetailListItems> dList){
        JSONArray a = new JSONArray(dList);
        String url = host + "/UpdateDisbursement";
        JSONParser.postStream(url, a.toString());
    }

    public static List<DisbursementDetailListItems> getDisbursementDetailListItems(String id){
        List<DisbursementDetailListItems> list = new ArrayList<>();
        JSONArray a = JSONParser.getJSONArrayFromUrl(host+"/Disbursement/"+id);
        try{
            for(int i=0;i<a.length();i++){
                JSONObject b = a.getJSONObject(i);
                list.add(new DisbursementDetailListItems(b.getString("ActualQty"),b.getString("ItemCode"),
                        b.getString("ItemDesc"),b.getString("Remarks"),Integer.toString(b.getInt ("ReqQty"))));
            }
        }catch(Exception e){
            Log.e("getDisbDetailList", e.toString() );
        }
        return list;
    }
}