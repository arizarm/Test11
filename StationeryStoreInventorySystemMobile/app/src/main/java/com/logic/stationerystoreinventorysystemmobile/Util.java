package com.logic.stationerystoreinventorysystemmobile;

import android.content.Context;
import android.content.res.AssetManager;

import java.io.IOException;
import java.io.InputStream;
import java.util.Properties;

/**
 * Created by Yimon Soe on 24/1/2018.
 */
public class Util {
    public static String getProperty(String key,Context context) throws IOException {
        Properties properties = new Properties();;
        AssetManager assetManager = context.getAssets();
        InputStream inputStream = assetManager.open("config.properties");
        properties.load(inputStream);
        return properties.getProperty(key);

    }

    public static boolean isInt(String toBeChecked){
        try{
            Integer i = Integer.parseInt(toBeChecked);
            return true;
        }
        catch (Exception e){
            return false;
        }
    }
}