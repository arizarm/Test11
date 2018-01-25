package com.logic.stationerystoreinventorysystemmobile;

import android.app.Activity;
import android.os.Bundle;
import android.view.View;
import android.widget.ListView;

public class DiscrepancyAdhoc extends Activity {
    ListView list;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_discrepancy_adhoc);
        setContentView(R.layout.activity_discrepancy_menu);
        list = findViewById(R.id.listItemsAdhoc);
    }

    protected void searchClick(View v){

    }

    protected void finaliseClick(View v){

    }
}
