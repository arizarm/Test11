package com.logic.stationerystoreinventorysystemmobile;

import android.util.Log;

import org.json.JSONArray;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.List;

/**
 * Created by Branda Ling on 22/1/2018.
 */

public class Department extends  java.util.HashMap<String,String> {
    final static String hostURL = "http://172.17.252.209/StationeryStoreInventorySystem/DeptService.svc/";
    public Department(String dCode,String collectId, String departmentName, String contactName, String phone,String fax) {
        put("dCode", dCode);
        put("collectId", collectId);
        put("departmentName", departmentName);
        put("contactName", contactName);
        put("phone", phone);
        put("fax", fax);
    }
    public Department(String dCode, String departmentName){
        put("dCode", dCode);
        put("departmentName", departmentName);
    }

    public  static List<Department> listDepartment(){
        List<Department> dlist=new ArrayList<Department>();
        JSONArray a=JSONParser.getJSONArrayFromUrl(hostURL+"Dept");
        try{
            for(int i=0;i<a.length();i++){
                JSONObject b=a.getJSONObject(i);
                dlist.add(new Department(b.getString("DeptCode"),b.getString("Dname")));
            }
        }catch (Exception e){
            Log.e("Department.list()","JSONArray error");
        }
        return(dlist);
    }

    public static Department getDept(String dcode){
        JSONObject b=JSONParser.getJSONFromUrl(hostURL+"Dept/"+dcode);
        try{
            return new Department(b.getString("DeptCode"),b.getString
                    ("Collectid"),b.getString("Dname"),b.getString("Contactname"),
                    b.getString("Telephone"),b.getString("Fax"));
        }
        catch (Exception e){
            Log.e("Department.list()","JSONArray error");
        }
        return(null);
    }




}
