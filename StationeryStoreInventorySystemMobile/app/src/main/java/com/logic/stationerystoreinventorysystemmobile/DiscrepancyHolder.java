package com.logic.stationerystoreinventorysystemmobile;

import java.util.HashMap;
import java.util.Map;

/**
 * Created by edwon on 24/1/2018.
 */

public class DiscrepancyHolder {
    static HashMap<String, Integer> discrepancies = new HashMap<String, Integer>();
    static boolean monthly = false;

    public static HashMap<String, Integer> getDiscrepancyList()
    {
        return discrepancies;
    }

    public static boolean isMonthly(){
        return monthly;
    }

    public static void clearDiscrepancies(){
        discrepancies = new HashMap<String,Integer>();
    }

    public static void addDiscrepancy(String itemCode, Integer adj){
        discrepancies.put(itemCode, adj);
    }

    public static void setAdhocMode(){
        monthly = false;
    }

    public static void setMonthlyMode(){
        monthly = true;
    }
}
