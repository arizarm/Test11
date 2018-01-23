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

public class RequisitionListActivity extends AppCompatActivity implements AdapterView.OnItemClickListener {

    SimpleAdapter sa;
    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        setContentView(R.layout.activity_requisition_list);
        final ListView lv = (ListView) findViewById(R.id.requisitionListView);
        lv.setOnItemClickListener(this);
        new AsyncTask<Void, Void, List<RequisitionListItem>>() {
            @Override
            protected List<RequisitionListItem> doInBackground(Void... params) {
                return RequisitionListItem.list();
            }

            @Override
            protected void onPostExecute(List<RequisitionListItem> result) {
                lv.setAdapter(sa = new SimpleAdapter
                        (RequisitionListActivity.this, result, R.layout.row, new String[]{"Date", "EmployeeName","RequisitionNo"},
                                new int[]{R.id.dateTV, R.id.empTV,R.id.reqTV}));
            }
        }.execute();
    }

/*    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        MenuInflater inflater = getMenuInflater();
        inflater.inflate(R.menu.menu_search, menu);
        MenuItem item = menu.findItem(R.id.menuSearch);
        SearchView searchView = (SearchView)item.getActionView();

        searchView.setOnQueryTextListener(new SearchView.OnQueryTextListener() {
            @Override
            public boolean onQueryTextSubmit(String query) {
                return false;
            }

            @Override
            public boolean onQueryTextChange(String newText) {
                sa.getFilter().filter(newText);
                return false;
            }
        });

        return super.onCreateOptionsMenu(menu);
    }*/

    @Override
    public void onItemClick(AdapterView<?> av, View view, int position, long id) {
        try {
            RequisitionListItem b = (RequisitionListItem) av.getAdapter().getItem(position);
            Intent intent = new Intent(this, RequisitionDetailActivity.class);
            intent.putExtra("RequisitionNo", b.get("RequisitionNo").toString());
            intent.putExtra("Date", b.get("Date").toString());
            intent.putExtra("Requestor", b.get("EmployeeName").toString());
            startActivity(intent);
        }catch (Exception e){
            Log.e("tag tag",e.toString() );
        }
    }
}

