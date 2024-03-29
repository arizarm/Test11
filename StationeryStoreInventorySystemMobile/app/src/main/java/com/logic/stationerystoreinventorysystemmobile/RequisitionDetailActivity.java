package com.logic.stationerystoreinventorysystemmobile;

/*import android.app.FragmentManager;*/
import android.app.Dialog;
import android.app.ProgressDialog;
import android.content.SharedPreferences;
import android.preference.PreferenceManager;
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
import android.view.MenuItem;
import android.view.View;
import android.view.WindowManager;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.SimpleAdapter;
import android.widget.TextView;
import android.widget.Toast;

import java.util.List;

//AUTHOR : APRIL SHAR
public class RequisitionDetailActivity extends FragmentActivity implements View.OnClickListener{

    String remarkTxt;
    Button btnApprove;
    Button btnReject;
    SimpleAdapter sa;
    String rid;
    SharedPreferences pref;
    String deptCode;
    String eid;
    String role;

    @Override
    public void onSaveInstanceState(Bundle outState) {
        super.onSaveInstanceState(outState);
        outState.putString("deptCode", deptCode);
    }

    void restoreInstance(Bundle state) {
        if (state != null) {
            deptCode = state.getString("deptCode");
        }
    }

    /*@Override
    public boolean onOptionsItemSelected(MenuItem item) {
        switch (item.getItemId()) {
            case R.id.item1:
                Intent i1 = new Intent(this, LoginActivity.class);
                startActivity(i1);
                return true;
            case R.id.item2:
                Intent i2 = new Intent(this, RequisitionListActivity.class);
                startActivity(i2);
                return true;
            case R.id.item3:
                SharedPreferences.Editor editor = pref.edit();
                editor.clear();
                editor.commit();
                Intent i3 = new Intent(this, LoginActivity.class);
                startActivity(i3);
                Util.greenToast("Logged out",getApplicationContext());
//                Toast.makeText(getApplicationContext(),
//                        "Logged out",Toast.LENGTH_SHORT).show();
                return true;
            default:
                return super.onOptionsItemSelected(item);
        }
    }*/

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_requisition_detail);
        getWindow().setSoftInputMode(WindowManager.LayoutParams.SOFT_INPUT_ADJUST_PAN);

        pref = PreferenceManager.getDefaultSharedPreferences(getApplicationContext());
        deptCode=pref.getString("deptCode","ENGL");
        eid=pref.getString("eid","1027");
        role=pref.getString("role","");

        btnApprove = (Button) findViewById(R.id.btnApprove);
        btnReject = (Button) findViewById(R.id.btnReject);
        Intent i = getIntent();
        rid = i.getStringExtra("RequisitionNo");
        TextView reqBy = (TextView) findViewById(R.id.reqByData);
        TextView reqDate = (TextView) findViewById(R.id.reqDateData);

        setTitle("Requisition No: "+rid);
        String requestor = i.getStringExtra("Requestor");
        String date = i.getStringExtra("Date");

        reqBy.setText(requestor);
        reqDate.setText(date);

        if(role.equals("DepartmentHead")) {
            new AsyncTask<String, Void, Boolean>() {
                @Override
                protected Boolean doInBackground(String... params) {
                    return Employee.CheckHasTempHead(params[0]);
                }

                @Override
                protected void onPostExecute(Boolean result) {
                    if (result) {
                        btnApprove.setVisibility(View.INVISIBLE);
                        btnReject.setVisibility(View.INVISIBLE);
                    } else {
                        btnApprove.setVisibility(View.VISIBLE);
                        btnReject.setVisibility(View.VISIBLE);
                    }

                }
            }.execute(deptCode);
        }

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
                        if(remarkTxt.isEmpty()|| remarkTxt==" "){remarkTxt=null;}
                        final Requisition r = new Requisition(rid, eid, remarkTxt);

                        new AsyncTask<Requisition,Void,Void>(){
                            @Override
                            protected Void doInBackground(Requisition...params) {
                                Requisition.rejectRequisition(params[0]);
                                return null;
                            }

                            @Override
                            protected void onPostExecute(Void result){
                                Intent i = new Intent(RequisitionDetailActivity.this,RequisitionListActivity.class);
                                Util.redsToast("Requisition Rejected",getApplicationContext());
                                //Toast.makeText(getApplicationContext(), "Requisition Rejected", Toast.LENGTH_LONG).show();
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
                final Requisition r = new Requisition(rid,eid , remarkTxt);

                new AsyncTask<Requisition,Void,Void>(){
                    @Override
                    protected Void doInBackground(Requisition...params) {
                        Requisition.approveRequisition(params[0]);
                        return null;
                    }

                    ProgressDialog progress;
                    @Override
                    protected void onPreExecute() {
                        progress = ProgressDialog.show(RequisitionDetailActivity.this, "Requisition Approved", "Sending mail to store clerks", true);
                    }

                    @Override
                    protected void onPostExecute(Void result){
                        Intent i = new Intent(RequisitionDetailActivity.this,RequisitionListActivity.class);
                        //Util.customToast();
                        Util.greenToast("Requisition Approved",getApplicationContext());
                        //Toast.makeText(getApplicationContext(), "Requisition Approved", Toast.LENGTH_LONG).show();
                        startActivity(i);
                    }
                }.execute(r);
                d.dismiss();
            }
        });
        d.show();
    }
}
