package com.logic.stationerystoreinventorysystemmobile;

import android.app.ProgressDialog;
import android.content.Intent;
import android.os.AsyncTask;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.ListView;
import android.widget.SimpleAdapter;
import android.widget.TextView;

import java.util.ArrayList;
import java.util.List;

public class RegenerateRequisitionActivity extends AppCompatActivity {

    private static ArrayList<RegenerateRequisition> regenReqList = new ArrayList<RegenerateRequisition>();

    ArrayList<RegenerateRequisition> regenerateList = new ArrayList<RegenerateRequisition>();

    private static RegenerateRequisition regenReq;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_regenerate_requisition);

        Bundle b = getIntent().getExtras();

        TextView txtReqDate = (TextView) findViewById(R.id.regenReqDate);
        String reqDate = regenReq.get("ReqDate").toString();
        txtReqDate.setText(reqDate);

        TextView txtDepName = (TextView) findViewById(R.id.depName);
        String depName = regenReq.get("DepName").toString();
        txtDepName.setText(depName);

        TextView txtReqBy = (TextView) findViewById(R.id.reqBy);
        String reqBy = regenReq.get("ReqBy").toString();
        txtReqBy.setText(reqBy);

        final ListView lv = (ListView) findViewById(R.id.lstRegenReq);
        lv.setAdapter(new SimpleAdapter
                (this, regenReqList, R.layout.regenerate_requisition_row,
                        new String[]{"Code", "Description", "ShortfallQty"},
                        new int[]{R.id.itemCodeHidden, R.id.itemDesc, R.id.shortfallQty}));

        Button button = findViewById(R.id.btnGenerate);
        button.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {

//                for (int i = 0; i < lv.getCount(); i++) {
//                    v = lv.getChildAt(i);
//                    CheckBox checkBox = v.findViewById(R.id.checkBox);
//                    if (checkBox.isChecked()) {
//                        RegenerateRequisition reqItem = (RegenerateRequisition) lv.getAdapter().getItem(i);
//                        regenerateList.add(reqItem);
//                    }
//                }

//                String message;
//                if (regenerateList.size() != 0) {
                    new AsyncTask<Void, Void, Void>() {
                        ProgressDialog progress;

                        @Override
                        protected void onPreExecute() {
                            progress = ProgressDialog.show(RegenerateRequisitionActivity.this, "Loading", "Generating Requisition", true);
                        }

                        @Override
                        protected Void doInBackground(Void... params) {
                            RegenerateRequisition.RegenerateRequisition(regenReqList);
                            return null;
                        }

                        @Override
                        protected void onPostExecute(Void params) {

                            Util.greenToast("Requisition Generation Successful", RegenerateRequisitionActivity.this);
                            Intent intent = new Intent(getApplicationContext(), DisbursementActivity.class);
                            startActivity(intent);
                            progress.dismiss();
                        }
                    }.execute();
//                } else {
//                    message = "Please select item to request!";
//                    Util.redsToast(message, RegenerateRequisitionActivity.this);
//                }
            }
        });
    }

    public static void setRegenReqList(ArrayList<RegenerateRequisition> r) {
        regenReqList = r;
    }

    public void btnCancelClick(View v) {
        Intent intent = new Intent(getApplicationContext(), DisbursementActivity.class);
        startActivity(intent);
    }

    public static void setRegenerateRequisition(RegenerateRequisition r) {
        regenReq = r;
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
