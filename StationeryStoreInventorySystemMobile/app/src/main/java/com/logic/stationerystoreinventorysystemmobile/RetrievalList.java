package com.logic.stationerystoreinventorysystemmobile;

import android.os.AsyncTask;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.widget.ListView;
import android.widget.SimpleAdapter;

import java.util.List;

public class RetrievalList extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_retrieval_list);

        final ListView lv = (ListView) findViewById(R.id.listView);
        lv.setOnItemClickListener(this);

        //list all Book before click searchBtn
        new AsyncTask<Void, Void, List<Retrieval>>() {
            @Override
            protected List<Retrieval> doInBackground(Void... params) {
                return Retrieval.list();
            }
            @Override
            protected void onPostExecute(List<Retrieval> result) {
                //customize row layout(can have many rows),but customization can not use with predefine feature
                lv.setAdapter(new SimpleAdapter
                        (MainActivity.this, result, R.layout.rowRetrievalList,
                                new String[]{"RetrievalNo","Date"},
                                new int[]{R.id.text1, R.id.text2}));
            }
        }.execute();
    }
}
