package com.logic.stationerystoreinventorysystemmobile;

import android.app.Activity;
import android.app.ProgressDialog;
import android.os.AsyncTask;
import android.os.Bundle;
import android.view.Gravity;
import android.view.View;
import android.widget.ListView;
import android.widget.SimpleAdapter;
import android.widget.Toast;

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
                String status = DiscrepancyHolder.isMonthly() ? "Monthly":"Pending";
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

    }
}
