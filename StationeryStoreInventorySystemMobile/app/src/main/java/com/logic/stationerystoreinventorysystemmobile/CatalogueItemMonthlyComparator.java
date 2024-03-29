package com.logic.stationerystoreinventorysystemmobile;

import android.content.Intent;

import java.util.Comparator;

//AUTHOR : EDWIN TAN
public class CatalogueItemMonthlyComparator implements Comparator<CatalogueItem>{

    @Override
    public int compare(CatalogueItem ci1, CatalogueItem ci2){
        String itemBin1 = ci1.get("bin");
        String itemBin2 = ci2.get("bin");
        String correctQty1 = ci1.get("correctQty");
        String correctQty2 = ci2.get("correctQty");

        //Prioritise items that have no correctQty attribute (i.e. haven't been checked yet)
        if(correctQty1 == null && correctQty2 != null){
            return -1;
        }
        else if(correctQty1 != null && correctQty2 == null){
            return 1;
        }
        else{
            //The next sorting condition is the bin code
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
}
