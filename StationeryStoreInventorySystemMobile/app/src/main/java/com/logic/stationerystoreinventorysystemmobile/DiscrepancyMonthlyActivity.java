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
import java.util.Collections;

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
            }
            else{
                SimpleAdapter adapter = new SimpleAdapter(getApplicationContext(), ciList, R.layout.monthly_discrepancy_row, new String[]{"itemCode", "description", "bin", "correctQty", "actualQty"}, new int[]{R.id.tvItemCode,R.id.tvItemName, R.id.tvBin, R.id.tvCorrect, R.id.tvActual});
                list.setAdapter(adapter);
            }
        }
        else{
            initialiseItemList();
        }


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
//        startActivity(i);
        startActivityForResult(i, 0);
    }

    @Override
    protected void onActivityResult(int requestCode, int resultCode, Intent data) {
        recreate();
    }

    private void initialiseItemList(){
        new AsyncTask<Void, Void, ArrayList<CatalogueItem>>(){
            ProgressDialog progress;
            @Override
            protected void onPreExecute() {
                progress = ProgressDialog.show(DiscrepancyMonthlyActivity.this, "Loading", "Loading Items", true);
            }
            @Override
            protected ArrayList<CatalogueItem> doInBackground(Void... input){
                DiscrepancyHolder.initialiseMonthlyItems();
                return DiscrepancyHolder.getMonthlyItems();
            }

            @Override
            protected void onPostExecute(ArrayList<CatalogueItem> ciList){
                Collections.sort(ciList, new CatalogueItemBinComparator());
                SimpleAdapter adapter = new SimpleAdapter(getApplicationContext(), ciList, R.layout.monthly_discrepancy_row, new String[]{"itemCode", "description", "bin", "correctQty", "actualQty"}, new int[]{R.id.tvItemCode,R.id.tvItemName, R.id.tvBin, R.id.tvCorrect, R.id.tvActual});
                list.setAdapter(adapter);
                progress.dismiss();
            }
        }.execute();
    }
}
