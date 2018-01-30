package com.logic.stationerystoreinventorysystemmobile;

import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;

import java.io.IOException;

public class MainActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        try {
            String text = Util.getProperty("name",getApplicationContext());
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    protected void generateDiscrepancyClick(View v){
        Intent i = new Intent(this, DiscrepancyMenuActivity.class);
        startActivity(i);
    }
}
