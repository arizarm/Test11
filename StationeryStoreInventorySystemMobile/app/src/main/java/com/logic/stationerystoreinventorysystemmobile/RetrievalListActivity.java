package com.logic.stationerystoreinventorysystemmobile;

import android.app.ProgressDialog;
import android.content.Intent;
import android.graphics.Color;
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

import java.util.List;

public class RetrievalListActivity extends AppCompatActivity implements AdapterView.OnItemClickListener {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_retrieval_list);

        final ListView lv = (ListView) findViewById(R.id.listView);
        lv.setOnItemClickListener(this);

        if (!Retrieval.list().isEmpty()) {
            new AsyncTask<Void, Void, List<Retrieval>>() {

                ProgressDialog progress;

                @Override
                protected void onPreExecute() {
                    progress = ProgressDialog.show(RetrievalListActivity.this, "Loading", "Getting Retrieval List", true);
                }

                @Override
                protected List<Retrieval> doInBackground(Void... params) {
                    return Retrieval.list();
                }

                @Override
                protected void onPostExecute(List<Retrieval> result) {

                    lv.setAdapter(new SimpleAdapter
                            (RetrievalListActivity.this, result, R.layout.retrieval_list_row,
                                    new String[]{"RetrievedDate", "RetrievalID", "RetrievedBy", "RetrievalStatus"},
                                    new int[]{R.id.text1, R.id.text2, R.id.text3, R.id.text4}));

                    progress.dismiss();
                }
            }.execute();
        } else {
            Util.redsToast("There is no Pending Retrieval !!",this);
            Intent i2 = new Intent(this, MainActivity.class);
            startActivity(i2);
        }
    }

    @Override
    public void onItemClick(AdapterView<?> av, View view, int position, long id) {
        Retrieval s = (Retrieval) av.getAdapter().getItem(position);
        Intent intent = new Intent(this, RetrievalListDetailActivity.class);
        intent.putExtra("RetrievalIDKey", s.get("RetrievalID"));
        startActivityForResult(intent, 1);
    }

    @Override
    protected void onActivityResult(int requestCode, int resultCode, Intent data) {
        recreate();
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
