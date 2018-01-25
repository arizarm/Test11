package com.logic.stationerystoreinventorysystemmobile;

import android.util.Log;

import org.json.JSONArray;
import org.json.JSONObject;


/**
 * Created by Yimon Soe on 25/1/2018.
 */

public class Employee extends java.util.HashMap<String,String> {

    final static String hostURL = "http://172.17.252.209/StationeryStoreInventorySystem/DeptService.svc/";
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

    public Employee(String email, String password){
        put("email", email);
        put("password", password);
    }

    public  static  boolean VerifyEmployee(String email,String password)
    {
        JSONObject b = JSONParser.getJSONFromUrl(hostURL+"GetEmployeeByEmail/"+email);
        boolean loginSuccess = false ;

        if(b != null)
        {
            try{
                Employee e =
                        new Employee(b.getInt("eid"),
                        b.getString("deptCode"),
                        b.getString("ename"),
                        b.getString("role"),
                        b.getString("password"),
                        b.getString("email"),
                        b.getString("isTemphead"),
                        b.getString("startDate"),
                        b.getString("endDate"));
                loginSuccess = true;
            }
            catch (Exception e){
                Log.e("Department.list()","JSONArray error");
            }
        }
        else
        {
            loginSuccess = false;
        }

        return  loginSuccess;
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
}
