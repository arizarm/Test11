package com.logic.stationerystoreinventorysystemmobile;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;

import java.util.ArrayList;

//AUTHOR : EDWIN TAN
public class DiscrepancyMenuActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_discrepancy_menu);

    }

    protected void adhocClick(View v){
        DiscrepancyHolder.clearDiscrepancies();
        DiscrepancyHolder.clearMonthlyItems();
        DiscrepancyHolder.setAdhocMode();
        Intent i = new Intent(this, DiscrepancyAdhocActivity.class);
        startActivity(i);
    }

    protected void monthlyClick(View v){
        DiscrepancyHolder.clearDiscrepancies();
        DiscrepancyHolder.clearMonthlyItems();
        DiscrepancyHolder.setMonthlyMode();
        Intent i = new Intent(this, DiscrepancyMonthlyActivity.class);
        startActivity(i);
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        getMenuInflater().inflate(R.menu.storemenu, menu);
        return true;
    }

    public boolean onOptionsItemSelected(MenuItem item) {
        switch (item.getItemId()) {
            case R.id.item1:
                Intent i1 = new Intent(this, RetrievalListActivity.class);
                startActivity(i1);
                return true;
            case R.id.item2:
                Intent i2 = new Intent(this, DisbursementActivity.class);
                startActivity(i2);
                return true;
            case R.id.item3:
                Intent i3 = new Intent(this, DiscrepancyMenuActivity.class);
                startActivity(i3);
                return true;
            case R.id.item4:
                Intent i4 = new Intent(this, DeptActivity.class);
                startActivity(i4);
                return true;
            case R.id.item5:
                Util.LogOut(this);
                return true;
            default:
                return super.onOptionsItemSelected(item);
        }
    }
}
