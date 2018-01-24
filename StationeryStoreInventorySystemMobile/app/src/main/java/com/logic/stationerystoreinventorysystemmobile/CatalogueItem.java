package com.logic.stationerystoreinventorysystemmobile;

import java.util.HashMap;

/**
 * Created by edwon on 24/1/2018.
 */

public class CatalogueItem extends HashMap<String, String> {

    public CatalogueItem(String itemCode, Integer categoryID, String description, Integer reorderLevel, Integer reorderQty, String unitOfMeasure, String bin, String activeStatus, Integer balanceQty){
        put("itemCode", itemCode);
        put("categoryID", categoryID.toString());
        put("description", description);
        put("reorderLevel", reorderLevel.toString());
        put("reorderQty", reorderQty.toString());
        put("unitOfMeasure", unitOfMeasure);
        put("bin", bin);
        put("activeStatus", activeStatus);
        put("balanceQty", balanceQty.toString());
    }
}
