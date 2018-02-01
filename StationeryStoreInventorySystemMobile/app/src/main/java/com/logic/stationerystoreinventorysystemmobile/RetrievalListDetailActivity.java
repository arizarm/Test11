package com.logic.stationerystoreinventorysystemmobile;

import android.app.ProgressDialog;
import android.content.Intent;
import android.os.AsyncTask;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.SimpleAdapter;
import android.widget.TextView;
import android.widget.Toast;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.StringTokenizer;

//implements AdapterView.OnItemClickListener, View.OnClickListener,
public class RetrievalListDetailActivity extends AppCompatActivity {

    //static HashMap<String, String> itemCodeAndQty = new HashMap<String, String>();
    String retrievalID;
    ListView lv;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_retrieval_list_detail);

        lv = (ListView) findViewById(R.id.listView);
        retrievalID = getIntent().getExtras().getString("RetrievalIDKey");

        new AsyncTask<String, Void, List<Retrieval_ItemDetail>>() {

            ProgressDialog progress;
            @Override
            protected void onPreExecute() {
                progress = ProgressDialog.show(RetrievalListDetailActivity.this, "Loading", "Getting Retrieval List", true);
            }

            @Override
            protected List<Retrieval_ItemDetail> doInBackground(String... params) {
                return Retrieval_ItemDetail.getRetrieval_ItemDetail(params[0]);
            }
            @Override
            protected void onPostExecute(List<Retrieval_ItemDetail> result) {

                RetrievalListDetailAdapter adapter = new RetrievalListDetailAdapter(RetrievalListDetailActivity.this, R.layout.retrieval_list_detail_row, result);
                lv.setAdapter(adapter);

                progress.dismiss();
            }
        }.execute(retrievalID);
    }



    protected void BtnSaveClick(View v) {
        EditText qty;
        TextView itemCode;
        HashMap<String, String> itemCodeAndQty = new HashMap<String, String>();
        boolean RetrievedQtyIsHigherThanTotalRequestedQty = false;

        v.requestFocus();

        for (int i = 0; i < lv.getCount(); i++) {

            Retrieval_ItemDetail r = (Retrieval_ItemDetail) lv.getAdapter().getItem(i);
            String iCode = r.get("ItemCode");
            String retrievedQty = r.get("RetrievedQty");
            itemCodeAndQty.put(iCode, retrievedQty);

            // range validator
            String totalRequestedQty = r.get("TotalRequestedQty");
            if (Integer.parseInt(retrievedQty) > Integer.parseInt(totalRequestedQty)) {
                RetrievedQtyIsHigherThanTotalRequestedQty = true;
                Util.redsToast("Retrieved Quantity can not be higher than Total Requested Quantity !!",this);
                //Toast.makeText(RetrievalListDetailActivity.this, "Retrieved Quantity can not be higher than Total Requested Quantity !!", Toast.LENGTH_LONG).show();
            }
        }

        if (RetrievedQtyIsHigherThanTotalRequestedQty == false) {
            for (Map.Entry<String, String> entry : itemCodeAndQty.entrySet()) {
                new AsyncTask<String, Void, Void>() {
                    @Override
                    protected Void doInBackground(String... params) {
                        RetrievalListDetailUpdate.updateRetrieval(params[0], params[1], params[2]);
                        return null;
                    }

                    @Override
                    protected void onPostExecute(Void result) {
                        finish();
                    }
                }.execute(retrievalID, entry.getKey(), entry.getValue());
            }
        }

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