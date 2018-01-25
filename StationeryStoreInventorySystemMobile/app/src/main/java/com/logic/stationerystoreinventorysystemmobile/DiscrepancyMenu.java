package com.logic.stationerystoreinventorysystemmobile;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.ListView;

public class DiscrepancyMenu extends Activity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
    }

    protected void adhocClick(View v){
        Intent i = new Intent(this, DiscrepancyAdhoc.class);
        startActivity(i);
    }

    protected void monthlyClick(View v){

    }
}
