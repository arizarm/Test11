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

    final static String host="http://172.17.156.145/StationeryStoreInventorySystem/DisbursementService.svc";

    public AccessCodeCheck(){}

    public AccessCodeCheck(String disbId, String accessCode) {
        put("disbId", disbId);
        put("accessCode", accessCode);
    }

    public static String checkAccessCode(AccessCodeCheck a) {
        JSONObject jCheckAccess = new JSONObject();
        try {
            jCheckAccess.put("disbId", a.get("disbId"));
            jCheckAccess.put("accessCode", a.get("accessCode"));
        } catch (Exception e) {
        }
        String result = JSONParser.postStream(host+"/AccessCodeValidate", jCheckAccess.toString());
        return  result;
    }

}
