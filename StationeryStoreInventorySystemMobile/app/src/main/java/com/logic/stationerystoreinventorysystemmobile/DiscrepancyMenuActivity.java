package com.logic.stationerystoreinventorysystemmobile;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;

public class DiscrepancyMenuActivity extends Activity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_discrepancy_menu);

    }

    protected void adhocClick(View v){
        DiscrepancyHolder.clearDiscrepancies();
        DiscrepancyHolder.setAdhocMode();
        Intent i = new Intent(this, DiscrepancyAdhocActivity.class);
        startActivity(i);
    }

    protected void monthlyClick(View v){
        DiscrepancyHolder.clearDiscrepancies();
        DiscrepancyHolder.setMonthlyMode();
        Intent i = new Intent(this, DiscrepancyMonthlyActivity.class);
        startActivity(i);
    }
}
