package com.logic.stationerystoreinventorysystemmobile;

import android.app.Activity;
import android.app.ProgressDialog;
import android.content.Context;
import android.content.SharedPreferences;
import android.os.AsyncTask;
import android.os.Bundle;
import android.preference.PreferenceManager;
import android.view.Gravity;
import android.view.View;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.SimpleAdapter;
import android.widget.TextView;
import android.widget.Toast;

import org.w3c.dom.Text;

import java.util.ArrayList;
import java.util.Collections;
import java.util.HashMap;
import java.util.Map;

public class DiscrepancySummaryActivity extends Activity {

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
                SimpleAdapter adapter = new SimpleAdapter(getApplicationContext(), dList, R.layout.discrepancy_summary_row, new String[]{"itemCode", "description", "balanceQty", "adjustmentQty"}, new int[]{R.id.tvItemCode,R.id.tvItemName, R.id.tvBalance, R.id.tvAdj});
                list.setAdapter(adapter);
                progress.dismiss();
            }
        }.execute();
    }

    protected void submitClick(View v){
//        SharedPreferences pref = PreferenceManager.getDefaultSharedPreferences(getApplicationContext());
//        String eid = pref.getString("eid", null);
        String eid = "1001";
        boolean complete = true;
        String status = DiscrepancyHolder.isMonthly() ? "Monthly":"Pending";
        TextView tvError = findViewById(R.id.tvError);

        if(Util.isInt(eid)){
            View v2;

            ListView listDisc = findViewById(R.id.listDiscrepancies);
            TextView tvItemCode;
            TextView tvAdjustmentQty;
            EditText etRemarks;
            int requestedBy = Integer.parseInt(eid);
            final ArrayList<Discrepancy> toBeSubmitted = new ArrayList<Discrepancy>();

            for (int i = 0; i < listDisc.getCount(); i++) {
                v = listDisc.getChildAt(i);
                tvItemCode = v.findViewById(R.id.tvItemCode);
                tvAdjustmentQty = v.findViewById(R.id.tvAdj);
                etRemarks = v.findViewById(R.id.etRemarks);


                String itemCode = tvItemCode.getText().toString();
                int adjustmentQty = revertAdjustmentQtyStr(tvAdjustmentQty.getText().toString());
                String remarks = etRemarks.getText().toString();
                if(remarks.isEmpty()){
                    tvError.setText("Please input remarks for all items");
                    complete = false;
                }
                else{
                    Discrepancy d = new Discrepancy(itemCode, requestedBy, adjustmentQty, remarks, status);
                    toBeSubmitted.add(d);
                }
            }
            if(complete){
                try {
                    new AsyncTask<Void, Void, Void>() {
                        ArrayList<Discrepancy> dList = toBeSubmitted;
                        ProgressDialog progress;
                        @Override
                        protected void onPreExecute() {
                            progress = ProgressDialog.show(DiscrepancySummaryActivity.this, "Search", "Searching through items", true);
                        }
                        @Override
                        public Void doInBackground(Void... voids) {
                            Discrepancy.submitDiscrepancies(dList);
                            return null;
                        }

                        @Override
                        public void onPostExecute(Void voids) {
                            progress.dismiss();
                            Toast t = Toast.makeText(getApplicationContext(), "Discrepancies reported", Toast.LENGTH_LONG);
                            Context c = getApplicationContext();
                            int offset = Math.round(150 * c.getResources().getDisplayMetrics().density);
                            t.setGravity(Gravity.CENTER|Gravity.CENTER_HORIZONTAL, 0, offset);
                            t.show();
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
}