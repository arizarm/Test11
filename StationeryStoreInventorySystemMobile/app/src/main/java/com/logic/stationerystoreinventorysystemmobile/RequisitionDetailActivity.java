package com.logic.stationerystoreinventorysystemmobile;

import android.app.ListActivity;
import android.content.Intent;
import android.os.AsyncTask;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.ListView;
import android.widget.SimpleAdapter;

import java.util.List;

public class RequisitionDetailActivity extends AppCompatActivity{

    SimpleAdapter sa;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_requisition_detail);

        Intent i = getIntent();
        String rid = i.getStringExtra("RequisitionNo");
        final ListView lv =(ListView) findViewById(R.id.detailListView);
        new AsyncTask<String,Void,List<Requisition_ItemList>>(){
            @Override
            protected List<Requisition_ItemList> doInBackground(String...params){
                return Requisition_ItemList.getList(params[0]);
            }

            protected  void onPostExecute(List<Requisition_ItemList> result){
                //setListAdapter(new SimpleAdapter(RequisitionDetailActivity.this,result,android.R.layout.simple_list_item_2,new String[]{"Description","ReqQty"}, new int[]{android.R.id.text1,android.R.id.text2}));

                lv.setAdapter(sa=new SimpleAdapter(RequisitionDetailActivity.this,result,R.layout.rowfordetails,new String[]{"Description","ReqQty","Uom"},new int[]{R.id.textView3,R.id.textView4,R.id.textView5}));
            }
        }.execute(rid);
    }
}
