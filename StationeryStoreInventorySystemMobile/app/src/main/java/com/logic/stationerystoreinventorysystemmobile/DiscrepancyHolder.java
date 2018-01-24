package com.logic.stationerystoreinventorysystemmobile;

import java.util.ArrayList;

/**
 * Created by edwon on 24/1/2018.
 */

public class DiscrepancyHolder {
    public static ArrayList<CatalogueItem> getItemList() {
        return itemList;
    }

    public static void setItemList(ArrayList<CatalogueItem> itemList) {
        DiscrepancyHolder.itemList = itemList;
    }

    static ArrayList<CatalogueItem> itemList = new ArrayList<CatalogueItem>();
}
