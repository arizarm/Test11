package com.logic.stationerystoreinventorysystemmobile;

import java.util.Comparator;

/**
 * Created by edwon on 29/1/2018.
 */

public class CatalogueItemBinComparator implements Comparator<CatalogueItem>{

    @Override
    public int compare(CatalogueItem ci1, CatalogueItem ci2){
        return ci1.get("bin").compareTo(ci2.get("bin"));
    }
}
