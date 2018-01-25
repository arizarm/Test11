package com.logic.stationerystoreinventorysystemmobile;

import android.os.AsyncTask;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.widget.Button;
import android.widget.EditText;

public class RetrievalListDetail extends AppCompatActivity {

    final static int[] view = {R.id.EditText1, R.id.EditText2, R.id.EditText3, R.id.EditText4};//, R.id.editText};
    final static String[] key = {"Bin", "Description","ItemCode", "TotalRequestedQty"};//, "ActualQty"};


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_retrieval_list_detail);

        String RetrievalID = getIntent().getExtras().getString("RetrievalIDKey");

//        Button Search = (Button) findViewById(R.id.bt);
//        Search.setOnClickListener(this);



        new AsyncTask<String, Void, Retrieval>() {
            @Override
            protected Retrieval doInBackground(String... params) {
                return Retrieval.getRetrievalDetail(params[0]);
            }

            @Override
            protected void onPostExecute(Retrieval result) {
                for (int i = 0; i < view.length; i++) {
                    EditText t = (EditText) findViewById(view[i]);
                    t.setText(result.get(key[i]));
                }
            }
        }.execute(RetrievalID);
    }
}