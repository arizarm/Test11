package com.logic.stationerystoreinventorysystemmobile;

import android.app.ListActivity;
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

public class RequisitionDetailActivity extends AppCompatActivity implements View.OnClickListener{

    SimpleAdapter sa;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_requisition_detail);
        getWindow().setSoftInputMode(WindowManager.LayoutParams.SOFT_INPUT_ADJUST_PAN);

        Intent i = getIntent();
        String rid = i.getStringExtra("RequisitionNo");
        TextView reqBy = (TextView) findViewById(R.id.reqByData);
        TextView reqDate = (TextView)findViewById(R.id.reqDateData);
        Button btnApprove=(Button) findViewById(R.id.btnApprove);
        btnApprove.setOnClickListener(this);
        //reqBy.setText();
        final ListView lv =(ListView) findViewById(R.id.detailListView);
        lv.setClickable(false);
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

    @Override
    public void onClick(View v){
        EditText remarks = (EditText) findViewById(R.id.remarksText);
        Button save = (Button) findViewById(R.id.btnSave);
        remarks.setVisibility(View.VISIBLE);
        save.setVisibility(View.VISIBLE);
    }
}
