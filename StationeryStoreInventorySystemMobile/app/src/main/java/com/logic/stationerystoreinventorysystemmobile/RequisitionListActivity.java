package com.logic.stationerystoreinventorysystemmobile;

import android.content.Intent;
import android.content.SharedPreferences;
import android.os.AsyncTask;
import android.preference.PreferenceManager;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ListView;
import android.widget.SimpleAdapter;

import java.util.List;

public class RequisitionListActivity extends AppCompatActivity implements AdapterView.OnItemClickListener {

    String deptCode;
    SharedPreferences pref;
    SimpleAdapter sa;

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.mainmenu, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        switch (item.getItemId()) {
            case R.id.item1:
                Intent i1 = new Intent(this, LoginActivity.class);
                startActivity(i1);
                return true;
            case R.id.item2:
                Intent i2 = new Intent(this, RequisitionListActivity.class);
                startActivity(i2);
                return true;
            case R.id.item3:
                SharedPreferences.Editor editor = pref.edit();
                editor.clear();
                editor.commit();
                Intent i3 = new Intent(this, LoginActivity.class);
                startActivity(i3);
                return true;
            default:
                return super.onOptionsItemSelected(item);
        }
    }



    @Override
    public void onSaveInstanceState(Bundle outState) {
        super.onSaveInstanceState(outState);
        outState.putString("deptCode", deptCode);
    }

    void restoreInstance(Bundle state) {
        if (state != null) {
            deptCode = state.getString("deptCode");
        }
    }
    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        pref = PreferenceManager.getDefaultSharedPreferences(getApplicationContext());
        deptCode=pref.getString("deptCode","ENGL");

        setContentView(R.layout.activity_requisition_list);
        final ListView lv = (ListView) findViewById(R.id.requisitionListView);
        lv.setOnItemClickListener(this);
        new AsyncTask<String, Void, List<RequisitionListItem>>() {
            @Override
            protected List<RequisitionListItem> doInBackground(String... params) {
                return RequisitionListItem.list(params[0]);
            }

            @Override
            protected void onPostExecute(List<RequisitionListItem> result) {
                lv.setAdapter(sa = new SimpleAdapter
                        (RequisitionListActivity.this, result, R.layout.row, new String[]{"Date", "EmployeeName","RequisitionNo"},
                                new int[]{R.id.dateTV, R.id.empTV,R.id.reqTV}));
            }
        }.execute(deptCode);
    }

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


}

