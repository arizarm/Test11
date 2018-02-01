package com.logic.stationerystoreinventorysystemmobile;

import android.app.Activity;
import android.app.ProgressDialog;
import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.AsyncTask;
import android.os.Bundle;
import android.preference.PreferenceManager;
import android.support.v7.app.AppCompatActivity;
import android.view.Gravity;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.TextView;
import android.widget.Toast;

import java.util.ArrayList;
import java.util.Collections;
import java.util.HashMap;
import java.util.Map;

public class DiscrepancySummaryActivity extends AppCompatActivity {

    ListView list;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_discrepancy_summary);

        list = findViewById(R.id.listDiscrepancies);

        new AsyncTask<Void, Void, ArrayList<Discrepancy>>(){
            ProgressDialog progress;
            @Override
            protected void onPreExecute() {
                progress = ProgressDialog.show(DiscrepancySummaryActivity.this, "Loading", "Getting discrepancies", true);
            }
            @Override
            protected ArrayList<Discrepancy> doInBackground(Void... voids){
                ArrayList<Discrepancy> dList = new ArrayList<Discrepancy>();
                HashMap<String, Integer> discrepancies = DiscrepancyHolder.getDiscrepancyList();
                for(Map.Entry<String, Integer> kvp: discrepancies.entrySet()){
                    CatalogueItem ci = CatalogueItem.getItem(kvp.getKey());
                    Discrepancy d = new Discrepancy(kvp.getKey(), ci.get("description"), Integer.parseInt(ci.get("balanceQty")), kvp.getValue());
                    dList.add(d);
                }
                Collections.sort(dList);
                return dList;
            }

            @Override
            protected void onPostExecute(ArrayList<Discrepancy> dList){
//                SimpleAdapter adapter = new SimpleAdapter(getApplicationContext(), dList, R.layout.discrepancy_summary_row, new String[]{"itemCode", "description", "balanceQty", "adjustmentQty"}, new int[]{R.id.tvItemCode,R.id.tvItemName, R.id.tvBalance, R.id.tvAdj});
                DiscrepancySummaryAdapter adapter = new DiscrepancySummaryAdapter(getApplicationContext(), R.layout.discrepancy_summary_row, dList);
                list.setAdapter(adapter);
                progress.dismiss();
            }
        }.execute();
    }

    protected void submitClick(View v){

        v.requestFocus();   //Get focus to ensure last EditText's OnFocusChange is triggered
        SharedPreferences pref = PreferenceManager.getDefaultSharedPreferences(getApplicationContext());
        String eid = pref.getString("eid", null);
        boolean complete = true;
        String status = DiscrepancyHolder.isMonthly() ? "Monthly":"Pending";
        final TextView tvError = findViewById(R.id.tvError);
        tvError.setText("");

        if(Util.isInt(eid)){
            View v2;

            ListView listDisc = findViewById(R.id.listDiscrepancies);
            TextView tvItemCode;
            TextView tvAdjustmentQty;
            EditText etRemarks;
            int requestedBy = Integer.parseInt(eid);
            final ArrayList<Discrepancy> toBeSubmitted = new ArrayList<Discrepancy>();

            for(int i = 0; i < listDisc.getAdapter().getCount(); i++){
                v2 = listDisc.getAdapter().getView(i, null, null);

                tvItemCode = v2.findViewById(R.id.tvItemCode);
                tvAdjustmentQty = v2.findViewById(R.id.tvAdj);
                etRemarks = v2.findViewById(R.id.etRemarks);

                String itemCode = tvItemCode.getText().toString();
                int adjustmentQty = revertAdjustmentQtyStr(tvAdjustmentQty.getText().toString());
                String remarks = etRemarks.getText().toString();

                if(remarks.isEmpty()){
                    tvError.setText("Please input remarks for all items");
                    complete = false;
                }
                else{
                    Discrepancy d = null;
//                    if(itemToUpdate){
//                        d = new Discrepancy(itemCode, requestedBy, adjustmentQty, remarks, status, itemToUpdate);
//                    }
//                    else{
                        d = new Discrepancy(itemCode, requestedBy, adjustmentQty, remarks, status);
//                    }
                    toBeSubmitted.add(d);
                }
            }
            if(complete){
                try {
                    new AsyncTask<Void, Void, Void>() {
                        ArrayList<Discrepancy> dList = toBeSubmitted;
                        ProgressDialog progress;
                        boolean submissionSuccessful = false;
                        @Override
                        protected void onPreExecute() {
                            progress = ProgressDialog.show(DiscrepancySummaryActivity.this, "Sending", "Reporting discrepancies", true);
                        }
                        @Override
                        public Void doInBackground(Void... voids) {
                            submissionSuccessful = Discrepancy.submitDiscrepancies(dList, DiscrepancyHolder.itemToUpdate());
                            return null;
                        }

                        @Override
                        public void onPostExecute(Void voids) {
                            progress.dismiss();
                            if(submissionSuccessful){
                                Toast t = Toast.makeText(getApplicationContext(), "Discrepancies reported", Toast.LENGTH_LONG);
                                Context c = getApplicationContext();
                                int offset = Math.round(150 * c.getResources().getDisplayMetrics().density);
                                t.setGravity(Gravity.CENTER|Gravity.CENTER_HORIZONTAL, 0, offset);
                                t.show();
                                DiscrepancyHolder.clearDiscrepancies();
                                DiscrepancyHolder.clearMonthlyItems();
                                DiscrepancyHolder.resetItemToUpdate();
                                DiscrepancyHolder.setAdhocMode();    //Reset to adhoc mode as default, monthly mode is strictly for monthly inventory check
                                Intent i = new Intent(getApplicationContext(), MainActivity.class);
                                startActivity(i);
                            }
                            else{
                                tvError.setText("Failed to send, please try again");
                            }
                        }
                    }.execute();
                }
                catch(Exception e){
                    e.printStackTrace();
                    Toast.makeText(getApplicationContext(), "Discrepancy reporting failed, please try again", Toast.LENGTH_LONG).show();
                }
            }
        }
        else{
            tvError.setText("User information error, please re-login");
        }
    }

    private int revertAdjustmentQtyStr(String adjustmentQtyStr){
        //Remove the + sign from adjustment qty if present, then convert to int
        int adjustment = 0;
        if(adjustmentQtyStr.substring(0,1).equals("+")){
            String temp = adjustmentQtyStr.substring(1);
            adjustment = Integer.parseInt(temp);
        }
        else{
            adjustment = Integer.parseInt(adjustmentQtyStr);
        }
        return adjustment;
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
