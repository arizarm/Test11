package com.logic.stationerystoreinventorysystemmobile;

import org.json.JSONObject;

import java.util.HashMap;

/**
 * Created by April Wang on 23/1/2018.
 */

public class Requisition extends HashMap<String,String>{
    final static String host ="http://172.17.254.6/StationeryStoreInventorySystem/RequisitionListService.svc";

    public Requisition(String requisitionNo, String approvedBy, String status, String remarks){
        put("RequisitionNo", requisitionNo);
        put("ApprovedBy", approvedBy);
        put("Status",status);
        put("Remarks",remarks);
    }
}
