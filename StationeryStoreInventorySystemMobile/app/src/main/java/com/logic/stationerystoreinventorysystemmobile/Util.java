package com.logic.stationerystoreinventorysystemmobile;

import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.content.res.AssetManager;
import android.graphics.Color;
import android.preference.PreferenceManager;
import android.view.Gravity;
import android.widget.TextView;
import android.widget.Toast;

import java.io.IOException;
import java.io.InputStream;
import java.util.Properties;

/**
 * Created by Yimon Soe on 24/1/2018.
 */
public class Util {

    final static String host = "http://172.17.250.219/StationeryStoreInventorySystem/";

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

    public static void LogOut(Context context)
    {
        SharedPreferences pref = PreferenceManager.getDefaultSharedPreferences(context);
        SharedPreferences.Editor editor = pref.edit();
        editor.clear();
        editor.commit();
        Intent i = new Intent(context, LoginActivity.class);
        context.startActivity(i);
        Toast.makeText(context,
                "Logged out",Toast.LENGTH_SHORT).show();
    }

    //custom toast message box
    public static void customToast(String message, Context context) {

        Toast toast = Toast.makeText(context, message,
                Toast.LENGTH_LONG);
        TextView toastMessage = (TextView) toast.getView().findViewById(android.R.id.message);
        toastMessage.setTextColor(Color.RED);
        toast.show();
    }

    //custom toast error message box
    public static void redsToast(String message, Context context) {

        Toast toast = Toast.makeText(context, message,
                Toast.LENGTH_LONG);
        TextView toastMessage = (TextView) toast.getView().findViewById(android.R.id.message);
        toastMessage.setTextColor(Color.RED);
        int offset = Math.round(150 * context.getResources().getDisplayMetrics().density);
        toast.setGravity(Gravity.CENTER|Gravity.CENTER_HORIZONTAL,0,offset);
        toast.show();
    }

    //custom toast message box
    public static void greenToast(String message, Context context) {

        Toast toast = Toast.makeText(context, message,
                Toast.LENGTH_LONG);
        TextView toastMessage = (TextView) toast.getView().findViewById(android.R.id.message);
        toastMessage.setTextColor(Color.GREEN);
        int offset = Math.round(150 * context.getResources().getDisplayMetrics().density);
        toast.setGravity(Gravity.CENTER|Gravity.CENTER_HORIZONTAL,0,offset);
        toast.show();
    }
}