package com.logic.stationerystoreinventorysystemmobile;

import android.content.Intent;
import android.os.AsyncTask;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ListView;
import android.widget.SimpleAdapter;

import java.util.List;

public class RetrievalListActivity extends AppCompatActivity implements AdapterView.OnItemClickListener {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_retrieval_list);

        final ListView lv = (ListView) findViewById(R.id.listView);
        lv.setOnItemClickListener(this);

        new AsyncTask<Void, Void, List<Retrieval>>() {
            @Override
            protected List<Retrieval> doInBackground(Void... params) {
                return Retrieval.list();
            }
            @Override
            protected void onPostExecute(List<Retrieval> result) {

                      lv.setAdapter(new SimpleAdapter
                        (RetrievalListActivity.this, result, R.layout.retrieval_list_row,
                                new String[]{"RetrievedDate","RetrievalID","RetrievedBy","RetrievalStatus"},
                                new int[]{R.id.text1, R.id.text2,R.id.text3,R.id.text4}));
            }
        }.execute();
    }

    @Override
    public void onItemClick(AdapterView<?> av, View view, int position, long id) {
        Retrieval s = (Retrieval) av.getAdapter().getItem(position);
        Intent intent = new Intent(this, RetrievalListDetailActivity.class);
        intent.putExtra("RetrievalIDKey", s.get("RetrievalID"));
        startActivityForResult(intent,1);
    }

    @Override
    protected void onActivityResult(int requestCode, int resultCode, Intent data) {
      recreate();
    }
}
