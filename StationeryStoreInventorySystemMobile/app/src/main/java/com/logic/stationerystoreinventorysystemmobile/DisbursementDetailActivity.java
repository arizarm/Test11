package com.logic.stationerystoreinventorysystemmobile;

import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;

import java.util.HashMap;

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
}
