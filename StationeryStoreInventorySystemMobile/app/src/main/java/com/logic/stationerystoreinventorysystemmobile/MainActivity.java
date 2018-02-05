package com.logic.stationerystoreinventorysystemmobile;

import android.content.Intent;
import android.content.SharedPreferences;
import android.preference.PreferenceManager;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;

import java.io.IOException;

//AUTHOR : KHIN MO MO ZIN
public class MainActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        try {
            String text = Util.getProperty("name",getApplicationContext());

            SharedPreferences pref = PreferenceManager.getDefaultSharedPreferences(getApplicationContext());
            String role = pref.getString("role", null);

            if(role.equals("Store Clerk") || role.equals("Store Supervisor") || role.equals("Store Manager"))
            {
                setContentView(R.layout.home_store);
            }
            else
            {
                setContentView(R.layout.home_department);
            }
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    protected void generateDiscrepancyClick(View v){
        Intent i = new Intent(this, DiscrepancyMenuActivity.class);
        startActivity(i);
    }

    protected void btnDisbursementClick(View v){
        Intent i = new Intent(this, DisbursementActivity.class);
        startActivity(i);
    }

    protected void btnRetrievalListClick(View v){
        Intent i = new Intent(this, RetrievalListActivity.class);
        startActivity(i);
    }

    protected void btnDepDetailClick(View v){
        Intent i = new Intent(this, DeptActivity.class);
        startActivity(i);
    }

    protected void btnUpdateColPointClick(View v){
        Intent i = new Intent(this, UpdateCollectionPointActivity.class);
        startActivity(i);
    }

    protected void btnUpdateRepClick(View v){
        Intent i = new Intent(this, UpdateDeptRepActivity.class);
        startActivity(i);
    }

    protected void btnRequisitionListClick(View v){
        Intent i = new Intent(this, RequisitionListActivity.class);
        startActivity(i);
    }

    protected void btnLogOutClick(View v){
        Util.LogOut(this);
    }

}
