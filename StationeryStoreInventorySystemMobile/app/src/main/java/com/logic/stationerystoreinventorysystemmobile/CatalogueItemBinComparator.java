package com.logic.stationerystoreinventorysystemmobile;

import android.content.Intent;

import java.util.Comparator;

/**
 * Created by edwon on 29/1/2018.
 */

public class CatalogueItemBinComparator implements Comparator<CatalogueItem>{

    @Override
    public int compare(CatalogueItem ci1, CatalogueItem ci2){
        String itemBin1 = ci1.get("bin");
        String itemBin2 = ci2.get("bin");
        int result = itemBin1.substring(0, 1).compareTo(itemBin2.substring(0, 1));
        if(result != 0){
            return result;
        }
        else{
            Integer index1 = Integer.parseInt(itemBin1.substring(1));
            Integer index2 = Integer.parseInt(itemBin2.substring(1));
            return index1.compareTo(index2);
        }
    }
}
