package com.logic.stationerystoreinventorysystemmobile;

import org.apache.http.params.CoreConnectionPNames;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;

/**
 * Created by edwon on 24/1/2018.
 */

public class DiscrepancyHolder {
    static HashMap<String, Integer> discrepancies = new HashMap<String, Integer>();
    static ArrayList<CatalogueItem> monthlyItems;
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

    //For monthly inventory check mode
    public static void initialiseMonthlyItems(){
        monthlyItems = CatalogueItem.getAllItems();
    }

    public static ArrayList<CatalogueItem> getMonthlyItems() {
        return monthlyItems;
    }

    public static void clearMonthlyItems(){
        monthlyItems = new ArrayList<CatalogueItem>();
    }

    public static boolean monthlyComplete(){
        for(CatalogueItem ci : monthlyItems){
            String correctQty = ci.get("correctQty");
            if(correctQty == null){
                return false;
            }
            else{
                if(!(correctQty.equals("N") || correctQty.equals("Y"))){
                    return false;
                }
            }
        }
        return true;
    }

    public static String getMissedItems(){
        StringBuilder sb = new StringBuilder();
        boolean firstItem = true;
        for(CatalogueItem ci : monthlyItems){
            String correctQty = ci.get("correctQty");
            if(correctQty != null){
                if(!(correctQty.equals("N") || correctQty.equals("Y"))){
                    if(!firstItem){
                        sb.append(", ");
                    }
                    else{
                        firstItem = false;
                    }
                    sb.append(ci.get("itemCode"));
                }
            }
            else{
                if(!firstItem){
                    sb.append(", ");
                }
                else{
                    firstItem = false;
                }
                sb.append(ci.get("itemCode"));
            }
        }
        return sb.toString();
    }
}
