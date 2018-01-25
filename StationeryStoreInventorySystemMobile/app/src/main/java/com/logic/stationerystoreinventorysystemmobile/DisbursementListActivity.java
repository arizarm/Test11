package com.logic.stationerystoreinventorysystemmobile;

import android.content.Intent;
import android.os.AsyncTask;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ListView;
import android.widget.SimpleAdapter;

import java.util.List;

public class DisbursementListActivity extends AppCompatActivity implements AdapterView.OnItemClickListener {

    SimpleAdapter sa;
    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        setContentView(R.layout.activity_disbursement_list);
        final ListView lv = (ListView) findViewById(R.id.disbursementListView);
        lv.setOnItemClickListener(this);
        new AsyncTask<Void, Void, List<DisbursementListItems>>() {
            @Override
            protected List<DisbursementListItems> doInBackground(Void... params) {
                return DisbursementListItems.getDisbursementListItems();
            }

            @Override
            protected void onPostExecute(List<DisbursementListItems> result) {

                lv.setAdapter(sa = new SimpleAdapter(getApplicationContext(), result,
                        R.layout.disbursement_list_row,
                        new String[]{"DisbId","DepName", "CollectionPoint","CollectionDate", "CollectionTime"},
                        new int[]{R.id.disbIDHidden, R.id.depName, R.id.disColPoint, R.id.disDate, R.id.disTime}));
            }
        }.execute();
    }

    @Override
    public void onItemClick(AdapterView<?> av, View view, int position, long id) {
        try {
            DisbursementListItems b = (DisbursementListItems) av.getAdapter().getItem(position);
            Intent intent = new Intent(this, DisbursementDetailListItemsActivity.class);
            intent.putExtra("DisbId", b.get("DisbId").toString());
            startActivity(intent);
        }catch (Exception e){
            Log.e("Disbtems onclick",e.toString() );
        }
    }
}