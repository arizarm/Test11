package com.logic.stationerystoreinventorysystemmobile;

import android.app.Activity;
import android.app.ProgressDialog;
import android.content.Intent;
import android.os.AsyncTask;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ListView;
import android.widget.SimpleAdapter;
import android.widget.TextView;

import java.util.ArrayList;
import java.util.Collections;

public class DiscrepancyMonthlyActivity extends AppCompatActivity implements AdapterView.OnItemClickListener  {
    ListView list;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_discrepancy_monthly);

        list = findViewById(R.id.lvItems);
        list.setOnItemClickListener(this);
        ArrayList<CatalogueItem> ciList = DiscrepancyHolder.getMonthlyItems();

        //If the static list of CatalogueItems in DiscrepancyHolder hasn't been initialised,
        //initialise it by querying the database, otherwise, get it and use it to populate the listview
        if(ciList != null){
            if(ciList.size() == 0){
                initialiseItemList();
            }
            else{
                Collections.sort(ciList, new CatalogueItemMonthlyComparator());
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
            if(ciList.size() > 0) {
                for (CatalogueItem ci : ciList) {
                    if (ci.get("correctQty").equals("N") && ci.get("actualQty") != null) {
                        int adjustment = Integer.parseInt(ci.get("actualQty")) - Integer.parseInt(ci.get("balanceQty"));
                        DiscrepancyHolder.addDiscrepancy(ci.get("itemCode"), adjustment);
                    }
                }
                Intent i = new Intent(this, DiscrepancySummaryActivity.class);
                startActivity(i);
            }
            else{
                Intent i = new Intent(this,MainActivity.class);
                Util.greenToast("Monthly check complete. No discrepancies found", this);
                startActivity(i);
            }
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
        //When exiting from DiscrepancyMonthlyItemDetailsActivity, the list will be refreshed to reflect the new input
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
                Collections.sort(ciList, new CatalogueItemMonthlyComparator());
                SimpleAdapter adapter = new SimpleAdapter(getApplicationContext(), ciList, R.layout.monthly_discrepancy_row, new String[]{"itemCode", "description", "bin", "correctQty", "actualQty"}, new int[]{R.id.tvItemCode,R.id.tvItemName, R.id.tvBin, R.id.tvCorrect, R.id.tvActual});
                list.setAdapter(adapter);
                progress.dismiss();
            }
        }.execute();
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
