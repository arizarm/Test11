package com.logic.stationerystoreinventorysystemmobile;

import android.util.Log;

import org.json.JSONArray;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.List;

//AUTHOR : KHIN MYO MYO SHWE
public class Department extends  java.util.HashMap<String,String> {
    final static String hostURL = Util.host +"DeptService.svc/";

    public Department(){

    }
    public Department(  String departmentName, String contactName, String phone, String fax,String dHead, String deptRep,String collectId) {
        //put("dCode", dCode);

        put("departmentName", departmentName);
        put("contactName", contactName);
        put("phone", phone);
        put("fax", fax);
        put("dHead",dHead);
        put("deptRep",deptRep);
        put("collectId", collectId);
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
        JSONObject e=JSONParser.getJSONFromUrl(hostURL+"Employee/DeptHead/"+dcode);

        JSONObject re=JSONParser.getJSONFromUrl(hostURL+"Employee/DeptRep/"+dcode);
        try{
            return new Department(
                    b.getString("Dname"),b.getString("Contactname"),
                    b.getString("Telephone"),b.getString("Fax"),e.getString("Ename"),re.getString("Ename"),b.getString("Collectid"));
        }
        catch (Exception ex){
            Log.e("Department.list()","JSONArray error");
        }
        return(null);
    }

    public static String getCollectionID(String dcode) {
        JSONObject b = JSONParser.getJSONFromUrl(hostURL + "Dept/"+dcode);

        try {
            return b.getString("Collectid");
        } catch (Exception ex) {
            Log.e("Department.list()", "JSONArray error");
        }
        return (null);
    }

    public static void updateCollectionPoint(Department dept){
        JSONObject jcpoint=new JSONObject();
        try
        {
            jcpoint.put("DeptCode",dept.get("dCode"));
            jcpoint.put("Collectid",dept.get("collectId"));
        }catch (Exception e)
        {

        }
        String result=JSONParser.postStream(hostURL+"UpdateCollect",jcpoint.toString());
    }




}
