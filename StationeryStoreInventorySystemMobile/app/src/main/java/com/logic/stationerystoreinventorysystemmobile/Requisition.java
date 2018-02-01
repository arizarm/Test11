package com.logic.stationerystoreinventorysystemmobile;

import org.json.JSONObject;

import java.util.HashMap;


public class Requisition extends HashMap<String,String>{
    final static String host =Util.host +"RequisitionListService.svc";
    String requisitionNo;
    String approvedBy;
    String remarks;

    public Requisition(String requisitionNo, String approvedBy, String status, String remarks, String requestedBy){
        put("RequisitionID", requisitionNo);
        put("ApprovedBy", approvedBy);
        put("RequestedBy",requestedBy);
        put("Status",status);
        put("Remarks",remarks);
    }

    public Requisition(String requisitionNo, String approvedBy, String remarks){
        put("RequisitionID", requisitionNo);
        put("ApprovedBy", approvedBy);
        put("Remarks",remarks);
    }

    public Requisition(){}

    public static void approveRequisition(Requisition r){
        JSONObject b = new JSONObject();
        try{
            b.put("RequisitionNo", r.get("RequisitionID"));
            b.put("Remarks",r.get("Remarks"));
            b.put("ApprovedBy", r.get("ApprovedBy"));
        }catch(Exception e){

        }
        String result=JSONParser.postStream(host+"/Approve",b.toString());
    }

    public static void rejectRequisition(Requisition r){
        JSONObject b = new JSONObject();
        try{
            b.put("RequisitionNo", r.get("RequisitionID"));
            b.put("Remarks",r.get("Remarks"));
            b.put("ApprovedBy", r.get("ApprovedBy"));
        }catch(Exception e){

        }
        String result=JSONParser.postStream(host+"/Reject",b.toString());
    }
}
