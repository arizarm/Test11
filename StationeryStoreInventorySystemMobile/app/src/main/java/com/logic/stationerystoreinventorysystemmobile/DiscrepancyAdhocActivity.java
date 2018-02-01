package com.logic.stationerystoreinventorysystemmobile;

import android.app.Activity;
import android.app.ProgressDialog;
import android.content.Context;
import android.content.Intent;
import android.os.AsyncTask;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.view.Gravity;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.inputmethod.InputMethodManager;
import android.widget.AdapterView;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.SimpleAdapter;
import android.widget.Toast;

import java.util.ArrayList;

public class DiscrepancyAdhocActivity extends AppCompatActivity implements AdapterView.OnItemClickListener {
    ListView list;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_discrepancy_adhoc);

        list = findViewById(R.id.listItemsAdhoc);
        list.setOnItemClickListener(this);
        displayAll();
    }

    protected void searchClick(View v){
        list = findViewById(R.id.listItemsAdhoc);
        EditText txtSearch = findViewById(R.id.txtSearch);
        String searchString = txtSearch.getText().toString();
        new AsyncTask<String, Void, ArrayList<CatalogueItem>>(){
            ProgressDialog progress;
            String noSearchReturn;
            @Override
            protected void onPreExecute() {
                progress = ProgressDialog.show(DiscrepancyAdhocActivity.this, "Search", "Searching through items", true);
            }
            @Override
            protected ArrayList<CatalogueItem> doInBackground(String... search){
                noSearchReturn = "No items containing " + "'" + search[0] + "' found";
                return CatalogueItem.searchItems(search[0]);
            }

            @Override
            protected void onPostExecute(ArrayList<CatalogueItem> iList){
//                SimpleAdapter adapter = new SimpleAdapter(getApplicationContext(), iList, R.layout.adhoc_discrepancy_row, new String[]{"itemCode", "description", "balanceQty"}, new int[]{R.id.tvItemCode,R.id.tvItemName,R.id.tvBalanceQty});
                SimpleAdapter adapter = new SimpleAdapter(getApplicationContext(), iList, R.layout.adhoc_discrepancy_row, new String[]{"itemCode", "description"}, new int[]{R.id.tvItemCode,R.id.tvItemName});
                list.setAdapter(adapter);
                if(iList.isEmpty()){
                    Toast t = Toast.makeText(getApplicationContext(), noSearchReturn, Toast.LENGTH_LONG);
                    t.setGravity(Gravity.CENTER, 0, 0);
                    t.show();
                }
                progress.dismiss();
            }
        }.execute(searchString);
        hideKeyboard();
    }

    @Override
    public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
        CatalogueItem ci = (CatalogueItem) parent.getAdapter().getItem(position);
        Intent i = new Intent(this, DiscrepancyAdhocItemDetailsActivity.class);
        i.putExtra("itemCode", ci.get("itemCode"));
        startActivity(i);
    }

    protected void finaliseClick(View v){
        Intent i = new Intent(this, DiscrepancySummaryActivity.class);
        startActivity(i);
    }

    protected void displayAllClick(View v){
        displayAll();
        hideKeyboard();
    }

    private void displayAll(){
        list = findViewById(R.id.listItemsAdhoc);
        new AsyncTask<Void, Void, ArrayList<CatalogueItem>>(){
            ProgressDialog progress;
            @Override
            protected void onPreExecute() {
                progress = ProgressDialog.show(DiscrepancyAdhocActivity.this, "Loading", "Loading Items", true);
            }
            @Override
            protected ArrayList<CatalogueItem> doInBackground(Void... input){
                return CatalogueItem.getAllItems();
            }

            @Override
            protected void onPostExecute(ArrayList<CatalogueItem> iList){
//                SimpleAdapter adapter = new SimpleAdapter(getApplicationContext(), iList, R.layout.adhoc_discrepancy_row, new String[]{"itemCode", "description", "balanceQty"}, new int[]{R.id.tvItemCode,R.id.tvItemName,R.id.tvBalanceQty});
                SimpleAdapter adapter = new SimpleAdapter(getApplicationContext(), iList, R.layout.adhoc_discrepancy_row, new String[]{"itemCode", "description"}, new int[]{R.id.tvItemCode,R.id.tvItemName});
                list.setAdapter(adapter);
                progress.dismiss();
            }
        }.execute();
    }

    private void hideKeyboard(){
        InputMethodManager inputManager = (InputMethodManager) getSystemService(Context.INPUT_METHOD_SERVICE);
        inputManager.hideSoftInputFromWindow((null == getCurrentFocus()) ? null : getCurrentFocus().getWindowToken(), InputMethodManager.HIDE_NOT_ALWAYS);
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
