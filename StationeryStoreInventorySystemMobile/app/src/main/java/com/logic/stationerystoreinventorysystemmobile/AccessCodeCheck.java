package com.logic.stationerystoreinventorysystemmobile;


import android.os.Parcelable;
import android.util.Log;

import org.json.JSONArray;
import org.json.JSONObject;

import java.io.Serializable;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

/**
 * Created by Mo Mo on 28/1/2018.
 */

public class AccessCodeCheck extends HashMap<String,String> implements Serializable {


    static String result;

    final static String host="http://192.168.0.100/StationeryStoreInventorySystem/DisbursementService.svc";
    //final static String host="http://172.17.249.194/StationeryStoreInventorySystem/DisbursementService.svc";

    public AccessCodeCheck(){}

    public AccessCodeCheck(String disbId, String accessCode) {
        put("disbId", disbId);
        put("accessCode", accessCode);
    }


    public static boolean checkAccessCode(AccessCodeCheck a) {
        JSONObject jCheckAccess = new JSONObject();
        try {
            jCheckAccess.put("disbId", a.get("disbId"));
            jCheckAccess.put("accessCode", a.get("accessCode"));

            result = JSONParser.postStream(host+"/AccessCodeValidate", jCheckAccess.toString());
        } catch (Exception e) {
        }

        //String result = JSONParser.postStream(host+"/AccessCodeValidate", jCheckAccess.toString());//AccessCodeValidateResult
        if(result.contains("True"))
        {
            return  true;
        }
        else
        {
            return  false;
        }
    }
}
