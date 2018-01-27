package com.logic.stationerystoreinventorysystemmobile;

import android.os.AsyncTask;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.AdapterView;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.SimpleAdapter;
import android.widget.Toast;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

//implements AdapterView.OnItemClickListener, View.OnClickListener,
public class RetrievalListDetailActivity extends AppCompatActivity  {

    static HashMap<String, ArrayList<Integer>> retrievalItemQty = new HashMap<String, ArrayList<Integer>>();
    String retrievalID;
    ListView lv;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_retrieval_list_detail);

        lv = (ListView) findViewById(R.id.listView); //only in onCreate
     //   lv.setOnItemClickListener(this);

        retrievalID = getIntent().getExtras().getString("RetrievalIDKey");

//        Button Save = (Button) findViewById(R.id.BtnSave);
//        Save.setOnClickListener(this);
//
//        Button FinalizeDisbursmentList = (Button) findViewById(R.id.BtnFinalizeDisbursmentList);
//        Save.setOnClickListener(this);


        new AsyncTask<String, Void, List<Retrieval_ItemDetail>>() {
            @Override
            protected List<Retrieval_ItemDetail> doInBackground(String... params) {
                return Retrieval_ItemDetail.getRetrieval_ItemDetail(params[0]);
            }

            @Override
            protected void onPostExecute(List<Retrieval_ItemDetail> result) {

                lv.setAdapter(new SimpleAdapter
                        (RetrievalListDetailActivity.this, result, R.layout.retrieval_list_detail_row,
                                new String[]{"Bin", "Description", "TotalRequestedQty"},
                                new int[]{R.id.text1, R.id.text2, R.id.text3}));
            }
        }.execute(retrievalID);
    }


    protected void BtnRestoreClick(View v) {
        EditText et;
        ArrayList<Integer> txtRetrievedList  = retrievalItemQty.get(retrievalID);

        if(txtRetrievedList!=null){
            if (txtRetrievedList.size() != 0) {
                for (int i = 0; i < txtRetrievedList.size(); i++) {
                    v = lv.getChildAt(i);
                    et = (EditText) v.findViewById(R.id.EditText1);
                    et.setText(txtRetrievedList.get(i).toString());
                }
            }
        }else{
            Toast.makeText(this,"There's no value to restore",Toast.LENGTH_LONG);
        }
    }

    protected void BtnSaveClick(View v) {
        EditText et;
        ArrayList<Integer> txtRetrievedList = new ArrayList<Integer>();
        for (int i = 0; i < lv.getCount(); i++) {
            v = lv.getChildAt(i);
            et = (EditText) v.findViewById(R.id.EditText1);
            txtRetrievedList.add(Integer.parseInt(et.getText().toString()));
        }
        retrievalItemQty.put(retrievalID,txtRetrievedList);
    }

    protected void BtnFinalizeDisbursmentListClick(View v) {
        EditText et;
        ArrayList<Integer> txtRetrievedList = new ArrayList<Integer>();
        for (int i = 0; i < lv.getCount(); i++) {
            v = lv.getChildAt(i);
            et = (EditText) v.findViewById(R.id.EditText1);
            txtRetrievedList.add(Integer.parseInt(et.getText().toString()));
        }
        retrievalItemQty.put(retrievalID,txtRetrievedList);
    }

//    @Override
//    public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
//
//    }

//    @Override
//    public void onClick(View v) {
//        if (v.getId() == R.id.BtnSave) {
//            Save();
//        } else if (v.getId() == R.id.BtnFinalizeDisbursmentList) {
//
//        }
//    }


}