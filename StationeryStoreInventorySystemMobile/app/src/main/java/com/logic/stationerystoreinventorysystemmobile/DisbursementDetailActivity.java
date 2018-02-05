package com.logic.stationerystoreinventorysystemmobile;

import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuItem;

import java.util.HashMap;

//AUTHOR : KHIN MO MO ZIN
public class DisbursementDetailActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_disbursement_detail);
        Bundle b = getIntent().getExtras();
        HashMap<String,String> d = (HashMap<String,String>)  b.get("disbursement");
        DisbursementDetailFragment  disDetailFrag = (DisbursementDetailFragment) getFragmentManager().findFragmentById(R.id.detail_frag);
        disDetailFrag.setDisbursementListItems(d);
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
