package com.logic.stationerystoreinventorysystemmobile;

import android.app.Activity;
import android.app.ProgressDialog;
import android.content.Intent;
import android.os.AsyncTask;
import android.os.Bundle;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ListView;
import android.widget.SimpleAdapter;
import android.widget.TextView;

import java.util.ArrayList;

public class DiscrepancyMonthlyActivity extends Activity  implements AdapterView.OnItemClickListener  {
    ListView list;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_discrepancy_monthly);

        list = findViewById(R.id.listItems);
        list.setOnItemClickListener(this);
        ArrayList<CatalogueItem> ciList = DiscrepancyHolder.getMonthlyItems();

        if(ciList != null){
            if(ciList.size() == 0){
                initialiseItemList();
                ciList = DiscrepancyHolder.getMonthlyItems();
            }
        }
        else{
            initialiseItemList();
            ciList = DiscrepancyHolder.getMonthlyItems();
        }

        SimpleAdapter adapter = new SimpleAdapter(getApplicationContext(), ciList, R.layout.monthly_discrepancy_row, new String[]{"itemCode", "description", "correctQty", "actualQty"}, new int[]{R.id.tvItemCode,R.id.tvItemName, R.id.tvCorrect, R.id.tvActual});
        list.setAdapter(adapter);
    }

    protected void finaliseClick(View v){
        ArrayList<CatalogueItem> ciList = DiscrepancyHolder.getMonthlyItems();

        if(DiscrepancyHolder.monthlyComplete()){
            for(CatalogueItem ci : ciList){
                if(ci.get("correctQty").equals("N") && ci.get("actualQty") != null){
                    int adjustment = Integer.parseInt(ci.get("actualQty")) - Integer.parseInt(ci.get("balanceQty"));
                    DiscrepancyHolder.addDiscrepancy(ci.get("itemCode"), adjustment);
                }
            }
            Intent i = new Intent(this, DiscrepancySummaryActivity.class);
            startActivity(i);
        }
        else{
            TextView tvError = findViewById(R.id.tvError);
            String missedItems = "Please check the following items: " + DiscrepancyHolder.getMissedItems();
            tvError.setText(missedItems);
        }
    }

    @Override
    public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
        CatalogueItem ci = (CatalogueItem) parent.getAdapter().getItem(position);
        Intent i = new Intent(this, DiscrepancyMonthlyItemDetailsActivity.class);
        i.putExtra("itemCode", ci.get("itemCode"));
        startActivity(i);
    }

    private void initialiseItemList(){
        new AsyncTask<Void, Void, Void>(){
            ProgressDialog progress;
            @Override
            protected void onPreExecute() {
                progress = ProgressDialog.show(DiscrepancyMonthlyActivity.this, "Loading", "Loading Items", true);
            }
            @Override
            protected Void doInBackground(Void... input){
                DiscrepancyHolder.initialiseMonthlyItems();
                return null;
            }

            @Override
            protected void onPostExecute(Void voids){
                progress.dismiss();
            }
        }.execute();
    }
}
