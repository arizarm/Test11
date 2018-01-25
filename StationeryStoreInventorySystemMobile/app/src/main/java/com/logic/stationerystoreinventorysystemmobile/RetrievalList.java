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

public class RetrievalList extends AppCompatActivity implements AdapterView.OnItemClickListener {

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
                        (RetrievalList.this, result, R.layout.retrieval_list_row,
                                new String[]{"RetrievedDate","RetrievalID","RetrievedBy"},
                                new int[]{R.id.text1, R.id.text2,R.id.text3}));
            }
        }.execute();
    }

    @Override
    public void onItemClick(AdapterView<?> av, View view, int position, long id) {
        Retrieval s = (Retrieval) av.getAdapter().getItem(position);
        Intent intent = new Intent(this, RetrievalListDetail.class);
        intent.putExtra("RetrievalIDKey", s.get("RetrievalID"));
        startActivity(intent);
    }
}
