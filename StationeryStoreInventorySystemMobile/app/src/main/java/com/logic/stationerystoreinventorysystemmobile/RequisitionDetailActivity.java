package com.logic.stationerystoreinventorysystemmobile;

/*import android.app.FragmentManager;*/
import android.app.Dialog;
import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentManager;
import android.support.v4.app.FragmentTransaction;

import android.app.ListActivity;
import android.content.Intent;
import android.os.AsyncTask;
import android.support.v4.app.FragmentActivity;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.view.WindowManager;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.SimpleAdapter;
import android.widget.TextView;
import android.widget.Toast;

import java.util.List;

public class RequisitionDetailActivity extends FragmentActivity implements View.OnClickListener{

    String remarkTxt;
    Button btnApprove;
    Button btnReject;
    SimpleAdapter sa;
    String rid;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_requisition_detail);
        getWindow().setSoftInputMode(WindowManager.LayoutParams.SOFT_INPUT_ADJUST_PAN);

        btnApprove = (Button) findViewById(R.id.btnApprove);
        btnReject = (Button) findViewById(R.id.btnReject);
        Intent i = getIntent();
        rid = i.getStringExtra("RequisitionNo");
        TextView reqBy = (TextView) findViewById(R.id.reqByData);
        TextView reqDate = (TextView) findViewById(R.id.reqDateData);

        String requestor = i.getStringExtra("Requestor");
        String date = i.getStringExtra("Date");

        reqBy.setText(requestor);
        reqDate.setText(date);

        btnApprove.setOnClickListener(this);
        final ListView lv = (ListView) findViewById(R.id.detailListView);
        lv.setClickable(false);

        new AsyncTask<String, Void, List<Requisition_ItemList>>() {
            @Override
            protected List<Requisition_ItemList> doInBackground(String... params) {
                return Requisition_ItemList.getList(params[0]);
            }

            protected void onPostExecute(List<Requisition_ItemList> result) {
                //setListAdapter(new SimpleAdapter(RequisitionDetailActivity.this,result,android.R.layout.simple_list_item_2,new String[]{"Description","ReqQty"}, new int[]{android.R.id.text1,android.R.id.text2}));

                lv.setAdapter(sa = new SimpleAdapter(RequisitionDetailActivity.this, result, R.layout.rowfordetails, new String[]{"Description", "ReqQty", "Uom"}, new int[]{R.id.textView3, R.id.textView4, R.id.textView5}));
            }
        }.execute(rid);

        btnReject.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                final Dialog d = new Dialog(RequisitionDetailActivity.this);
                d.setTitle("Remarks");
                d.setContentView(R.layout.remark_dialog);
                d.setCancelable(true);
                final EditText t = (EditText) d.findViewById(R.id.edit1);
                Button b = (Button) d.findViewById(R.id.button);
                b.setOnClickListener(new View.OnClickListener() {
                    @Override
                    public void onClick(View v) {
                        remarkTxt=t.getText().toString();
                        if(remarkTxt==""|| remarkTxt==" "){remarkTxt=null;}
                        final Requisition r = new Requisition(rid, "1006", remarkTxt);

                        new AsyncTask<Requisition,Void,Void>(){
                            @Override
                            protected Void doInBackground(Requisition...params) {
                                Requisition.rejectRequisition(params[0]);
                                return null;
                            }
                            @Override
                            protected void onPostExecute(Void result){
                                Intent i = new Intent(RequisitionDetailActivity.this,RequisitionListActivity.class);
                                Toast.makeText(getApplicationContext(), "Requisition Rejected", Toast.LENGTH_LONG).show();
                                startActivity(i);
                            }
                        }.execute(r);
                        d.dismiss();
                    }
                });
                d.show();
            }
        });
    }

    @Override
    public void onClick(View v) {
        final Dialog d = new Dialog(this);
        d.setTitle("Remarks");
        d.setContentView(R.layout.remark_dialog);
        d.setCancelable(true);
        final EditText t = (EditText) d.findViewById(R.id.edit1);
        Button b = (Button) d.findViewById(R.id.button);
        b.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                remarkTxt=t.getText().toString();
                final Requisition r = new Requisition(rid, "1006", remarkTxt);

                new AsyncTask<Requisition,Void,Void>(){
                    @Override
                    protected Void doInBackground(Requisition...params) {
                        Requisition.approveRequisition(params[0]);
                        return null;
                    }
                    @Override
                    protected void onPostExecute(Void result){
                        Intent i = new Intent(RequisitionDetailActivity.this,RequisitionListActivity.class);
                        Toast.makeText(getApplicationContext(), "Requisition Approved", Toast.LENGTH_LONG).show();
                        startActivity(i);
                    }
                }.execute(r);
                d.dismiss();
            }
        });
        d.show();
    }
}
