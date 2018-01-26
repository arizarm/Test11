package com.logic.stationerystoreinventorysystemmobile;

import java.util.HashMap;

/**
 * Created by edwon on 24/1/2018.
 */

public class DiscrepancyHolder {
    public static HashMap<CatalogueItem, Integer> getItemList() {
        return discrepancies;
    }

    public static void clearDiscrepancies(){discrepancies = new HashMap<>();}

    static HashMap<CatalogueItem, Integer> discrepancies = new HashMap<CatalogueItem, Integer>();
}
