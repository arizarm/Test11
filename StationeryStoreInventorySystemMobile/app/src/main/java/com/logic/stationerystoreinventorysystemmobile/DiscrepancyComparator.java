package com.logic.stationerystoreinventorysystemmobile;

import com.logic.stationerystoreinventorysystemmobile.Discrepancy;

import java.util.Comparator;

/**
 * Created by edwon on 27/1/2018.
 */

public class DiscrepancyComparator implements Comparator<Discrepancy> {
    @Override
    public int compare(Discrepancy d1, Discrepancy d2) {
        return d1.get("itemCode").compareTo(d2.get("itemCode"));
    }
}
