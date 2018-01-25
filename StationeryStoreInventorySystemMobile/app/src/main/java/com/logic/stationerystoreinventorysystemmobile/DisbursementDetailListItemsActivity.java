package com.logic.stationerystoreinventorysystemmobile;

import android.content.Intent;
import android.os.AsyncTask;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.view.WindowManager;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.SimpleAdapter;
import android.widget.TextView;

import java.util.List;

public class DisbursementDetailListItemsActivity extends AppCompatActivity {

    SimpleAdapter sa;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        TextView disDate = (TextView) findViewById(R.id.disDate);
        TextView disbTime = (TextView) findViewById(R.id.disbTime);
        TextView depName = (TextView) findViewById(R.id.depName);
        TextView disColPoint = (TextView) findViewById(R.id.disColPoint);

        Intent i = getIntent();
        final String disbId = i.getStringExtra("DisbId");
        DisbursementListItems b = (DisbursementListItems) DisbursementListItems.getDisbursementListItemsbyId(disbId);

        disDate.setText(b.get("CollectionDate").toString());
        disbTime.setText(b.get("CollectionTime").toString());
        depName.setText(b.get("DepName").toString());
        disColPoint.setText(b.get("CollectionPoint").toString());

    }


}
