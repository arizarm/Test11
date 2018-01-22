package com.logic.stationerystoreinventorysystemmobile;

import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;

public class RequisitionDetailActivity extends AppCompatActivity implements View.OnClickListener{

    final static int[] ids = {R.id.reqByData, R.id.reqDateData, R.id.editText3, R.id.editText4, R.id.editText5, R.id.editText6};
    final static String[] keys = {"BookID", "Title", "Author", "ISBN", "Price", "Stock"};

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_requisition_detail);
    }

    @Override
    public void onClick(View v){

    }
}
