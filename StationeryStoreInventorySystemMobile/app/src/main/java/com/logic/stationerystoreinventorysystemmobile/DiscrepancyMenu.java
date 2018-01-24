package com.logic.stationerystoreinventorysystemmobile;

import android.app.Activity;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.preference.PreferenceManager;
import android.view.View;

public class DiscrepancyMenu extends Activity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_discrepancy_menu);
    }

    protected void adhocClick(View v){
        Intent i = new Intent(this, DiscrepancyAdhocItemList.class);
        startActivity(i);
    }

    protected void monthlyClick(View v){

    }
}
