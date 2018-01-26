package com.logic.stationerystoreinventorysystemmobile;

import java.util.HashMap;

/**
 * Created by edwon on 24/1/2018.
 */

public class DiscrepancyHolder {
    static HashMap<String, Integer> discrepancies = new HashMap<String, Integer>();

    public static HashMap<String, Integer> getDiscrepancyList() {
        return discrepancies;
    }

    public static void clearDiscrepancies(){discrepancies = new HashMap<>();}

    public static void addDiscrepancy(String itemCode, Integer adj){
        discrepancies.put(itemCode, adj);
    }
}
