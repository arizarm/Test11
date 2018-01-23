package com.logic.stationerystoreinventorysystemmobile;

import org.json.JSONArray;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

/**
 * Created by April Wang on 20/1/2018.
 */

public class Item extends HashMap<String, String> {
    final static String host="";

    public Item(String itemCode, String description, String reqQty, String uom){
        put("Code",itemCode);
        put("Des", description);
        put("ReqQty",reqQty);
        put("Uom",uom);
    }

    public Item(String itemCode,String description){
        put("Code",itemCode);
        put("Des", description);
    }

    public Item(){}

    public static List<Item> ListItem(){
        List<Item> list = new ArrayList<Item>();
        try{
            JSONArray a = JSONParser.getJSONArrayFromUrl(host+"/Items");
            for(int i =0;i<a.length();i++)
            {
                JSONObject it= a.getJSONObject(i);
                list.add(new Item(it.getString("Code"),it.getString("Des")));
            }
        }catch(Exception e){}
        return list;
    }
}
