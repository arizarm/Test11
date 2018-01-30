package com.logic.stationerystoreinventorysystemmobile;

import android.content.SharedPreferences;
import android.preference.PreferenceActivity;
import android.preference.PreferenceManager;
import android.util.Log;

import org.json.JSONArray;
import org.json.JSONObject;


/**
 * Created by Yimon Soe on 25/1/2018.
 */

public class Employee extends java.util.HashMap<String,String> {

<<<<<<< HEAD
    final static String hostURL = "http://172.17.252.209/StationeryStoreInventorySystem/DeptService.svc/";

    public Employee(){

    }
    public Employee(String dCode,String eId,String eName,String role) {
        put("deptCode", dCode);
        put("eid", eId);
        put("ename", eName);
        put("role",role);
    }


=======
    final static String hostURL = "http://172.17.255.216/StationeryStoreInventorySystem/DeptService.svc/";
>>>>>>> a6d8fd248be86a5a7788ed7d743f938c500e990b
    public Employee(int eid, String deptCode, String ename, String role, String password, String email, String isTemphead, String startDate, String endDate) {
        put("eid", Integer.toString(eid));
        put("deptCode", deptCode);
        put("ename", ename);
        put("role", role);
        put("password", password);
        put("email", email);
        put("isTemphead", isTemphead);
        put("startDate", startDate);
        put("endDate", endDate);
    }
    public  Employee(String ename,String startDate,String endDate){
        put("ename", ename);
        put("startDate", startDate);
        put("endDate", endDate);
    }

    public Employee(String email, String password){
        put("email", email);
        put("password", password);
    }

    public  static  Employee VerifyEmployee(String email,String password)
    {
        JSONObject b = JSONParser.getJSONFromUrl(hostURL+"GetEmployeeByEmail/"+email+"/"+password);
        Employee emp = null;
        boolean loginSuccess = false ;

        if(b != null)
        {
            try{
                emp =
                        new Employee(b.getInt("Eid"),
                        b.getString("DeptCode"),
                        b.getString("Ename"),
                        b.getString("Role"),
                        b.getString("Password"),
                        b.getString("Email"),
                        b.getString("IsTemphead"),
                        b.getString("StartDate"),
                        b.getString("EndDate"));
            }
            catch (Exception ex){
                Log.e("Department.list()","JSONArray error");
            }
        }
        else
        {
            loginSuccess = false;
        }

        return  emp;
    }

    public static boolean CheckIsTempHead(String id)
    {
/*        JSONArray b = JSONParser.getJSONArrayFromUrl(hostURL+"Employee/"+id);
        for(int i=0;i<b.length();i++){
            b.getBoolean(0);
        }*/

        JSONObject b = JSONParser.getJSONFromUrl(hostURL+"Employee/"+id);
        try{
            return b.getBoolean("IsTempHead");
        }catch (Exception ex){
            Log.e("Error", ex.toString());
        }
        return false;
    }


//    WCFEmployee e = new WCFEmployee();
//    e.eid = eid;
//    e.deptCode = deptCode;
//    e.ename = ename;
//    e.role = role;
//    e.password = password;
//    e.email = email;
//    e.isTemphead = isTemphead;
//    e.startDate = startDate;
//    e.endDate = endDate;
//        return e;

/*    JSONObject b=JSONParser.getJSONFromUrl(hostURL+"Dept/"+dcode);
        try{
        return new Department(b.getString("DeptCode"),b.getString
                ("Collectid"),b.getString("Dname"),b.getString("Contactname"),
                b.getString("Telephone"),b.getString("Fax"));
    }
        catch (Exception e){
        Log.e("Department.list()","JSONArray error");
    }
        return(null);*/

//int eid, string deptCode, string ename, string role, string password, string email, string isTemphead, string startDate, string endDate
//      return WCFEmployee.Make(emp.EmpID, emp.DeptCode, emp.EmpName, emp.Role, emp.Password
//        , emp.Email, emp.IsTempHead, emp.StartDate.GetValueOrDefault().ToShortDateString()
//        , emp.EndDate.GetValueOrDefault().ToShortDateString());
<<<<<<< HEAD

    public static LinkedHashMap<String, String> listEmployee(String dcode) {
        LinkedHashMap<String, String> elist = new LinkedHashMap<String, String>();
        JSONArray a = JSONParser.getJSONArrayFromUrl(hostURL + "Employee/ForDeptRep/" + dcode + "/0");
        try {
            for (int i = 0; i < a.length(); i++) {
                JSONObject b = a.getJSONObject(i);
                elist.put(Integer.toString(b.getInt("Eid")), b.getString("Ename"));

            }
        } catch (Exception e) {
            Log.e("Employee.list()", "JSONArray error");
        }
        return (elist);
    }

    public static String getDeptRepID(String dcode) {
        JSONObject b = JSONParser.getJSONFromUrl(hostURL + "Employee/DeptRep/" + dcode);

        try {
            return b.getString("Ename");
        } catch (Exception ex) {
            Log.e("Department.list()", "JSONArray error");
        }
        return (null);
    }

    public static void updateDeptRep(Employee emp) {
        JSONObject jdeptRep = new JSONObject();
        try {
            jdeptRep.put("DeptCode", emp.get("deptcode"));
            jdeptRep.put("Eid", emp.get("eid"));
            jdeptRep.put("Role", emp.get("role"));
        } catch (Exception e) {

        }
        String result = JSONParser.postStream(hostURL + "UpdateDeptRep", jdeptRep.toString());
    }

    public static LinkedHashMap<String, String> listActingHead(String dcode) {
        LinkedHashMap<String, String> elist = new LinkedHashMap<String, String>();
        JSONArray a = JSONParser.getJSONArrayFromUrl(hostURL + "Employee/ForActingDHead/" + dcode + "/0");
        try {
            elist.put("0","--Revoked Authority--");
            for (int i = 0; i < a.length(); i++) {
                JSONObject b = a.getJSONObject(i);

                elist.put(Integer.toString(b.getInt("Eid")), b.getString("Ename"));

            }
        } catch (Exception e) {
            Log.e("Employee.list()", "JSONArray error");
        }
        return (elist);
    }

    public static Employee getActingDeptHeadID(String dcode) {
        JSONObject b = JSONParser.getJSONFromUrl(hostURL + "Employee/ActingDHead/" + dcode);

        try {
            return
                    new Employee(b.getString("Ename"), b.getString("StartDate"), b.getString("EndDate"));
        } catch (Exception ex) {
            Log.e("Employee.list()", "JSONArray error");
        }
        return (null);
    }

    public static void updateActingDHead(Employee emp) {
        JSONObject jdeptAHead= new JSONObject();
        try {
            jdeptAHead.put("DeptCode", emp.get("deptcode"));
            jdeptAHead.put("Eid", emp.get("eid"));
            jdeptAHead.put("Role", emp.get("role"));
            jdeptAHead.put("IsTemphead",emp.get("isTemphead"));
            jdeptAHead.put("StartDate",emp.get("startDate"));
            jdeptAHead.put("EndDate",emp.get("endDate"));
        } catch (Exception e) {

        }
        String result = JSONParser.postStream(hostURL + "UpdateActingDHead", jdeptAHead.toString());
    }


=======
>>>>>>> a6d8fd248be86a5a7788ed7d743f938c500e990b
}
