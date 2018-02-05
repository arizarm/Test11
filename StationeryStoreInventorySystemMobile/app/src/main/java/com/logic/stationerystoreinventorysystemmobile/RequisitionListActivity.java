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
import android.widget.TextView;
import android.widget.Toast;

import org.w3c.dom.Text;

import java.util.List;

//AUTHOR : APRIL SHAR
public class RequisitionListActivity extends AppCompatActivity implements AdapterView.OnItemClickListener {

    String deptCode;
    SharedPreferences pref;
    SimpleAdapter sa;
    int pendingCount=0;
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
                pendingCount=result.size();
                setTitle("Total Pending: "+Integer.toString(pendingCount));
                if(result.size()==0)
                {
                    TextView txt = (TextView) findViewById(R.id.textView9);
                    txt.setVisibility(View.VISIBLE);
                }
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

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.mainmenu, menu);
        return true;
    }

    public boolean onOptionsItemSelected(MenuItem item) {
        switch (item.getItemId()) {

            case R.id.item1:
                Intent i1 = new Intent(this, RequisitionListActivity.class);
                startActivity(i1);
                return true;
            case R.id.item2:
                Intent i2 = new Intent(this, UpdateDeptRepActivity.class);
                startActivity(i2);
                return true;
            case R.id.item3:
                Intent i3 = new Intent(this, UpdateCollectionPointActivity.class);
                startActivity(i3);
                return true;
            case R.id.item4:
                Util.LogOut(this);
                return true;
            default:
                return super.onOptionsItemSelected(item);
        }
    }

}

