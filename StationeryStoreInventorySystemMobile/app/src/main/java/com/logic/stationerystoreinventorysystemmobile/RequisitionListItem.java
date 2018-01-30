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

public class RequisitionListItem extends HashMap<String,String>{
    final static String host="http://172.17.254.6/StationeryStoreInventorySystem/RequisitionListService.svc";


    public RequisitionListItem(String date, String requisitionNo, String department, String status, String employeeName/*, String remarks*/){
        put("Date",date);
        put("RequisitionNo",requisitionNo);
        put("Department",department);
        put("Status",status);
        put("EmployeeName",employeeName);
        /*put("Remarks",remarks);*/
    }

    public RequisitionListItem(){}

    public RequisitionListItem(String Code, String Description, String ShortfallQty){
        put("Code",Code);
        put("Description",Description);
        put("ShortfallQty",ShortfallQty);
    }

    public RequisitionListItem(String date, String empName){
        put("Date",date);
        put("EmployeeName",empName);
    }
    public static List<RequisitionListItem> list(String deptCode){
        List<RequisitionListItem> list = new ArrayList<RequisitionListItem>();
        JSONArray a = JSONParser.getJSONArrayFromUrl(host+"/Requisitions/"+deptCode);
        try{
            for(int i=0;i<a.length();i++){
                JSONObject b = a.getJSONObject(i);
                list.add(new RequisitionListItem(b.getString("Date"),b.getString("RequisitionNo"),b.getString("Department"),b.getString("Status"),b.getString("EmployeeName")));
            }
        }catch(Exception ex){
            //Log.e("RequisitionList.list()","JSONArray error");
            Log.e("list: ", ex.toString() );
        }
        return (list);
    }
}
