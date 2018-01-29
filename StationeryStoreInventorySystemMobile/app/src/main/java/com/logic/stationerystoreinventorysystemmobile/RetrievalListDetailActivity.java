package com.logic.stationerystoreinventorysystemmobile;

import android.content.Intent;
import android.os.AsyncTask;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.SimpleAdapter;
import android.widget.TextView;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

//implements AdapterView.OnItemClickListener, View.OnClickListener,
public class RetrievalListDetailActivity extends AppCompatActivity  {

    //static HashMap<String, String> itemCodeAndQty = new HashMap<String, String>();
    String retrievalID;
    ListView lv;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_retrieval_list_detail);

        lv = (ListView) findViewById(R.id.listView);
        retrievalID = getIntent().getExtras().getString("RetrievalIDKey");

            new AsyncTask<String, Void, List<Retrieval_ItemDetail>>() {
                @Override
                protected List<Retrieval_ItemDetail> doInBackground(String... params) {
                    return Retrieval_ItemDetail.getRetrieval_ItemDetail(params[0]);
                }
                @Override
                protected void onPostExecute(List<Retrieval_ItemDetail> result) {

                    lv.setAdapter(new SimpleAdapter
                            (RetrievalListDetailActivity.this, result, R.layout.retrieval_list_detail_row,
                                    new String[]{"ItemCode", "Bin", "Description", "TotalRequestedQty","RetrievedQty"},
                                    new int[]{R.id.iCodeHidden, R.id.text1, R.id.text2, R.id.text3,R.id.EditText1}));
                }
            }.execute(retrievalID);
    }

    protected void BtnSaveClick(View v) {
        EditText qty;
        TextView itemCode;
        HashMap<String, String> itemCodeAndQty = new HashMap<String, String>();

        for (int i = 0; i < lv.getCount(); i++) {
            v = lv.getChildAt(i);
            qty = (EditText) v.findViewById(R.id.EditText1);
            itemCode = (TextView) v.findViewById(R.id.iCodeHidden);
            itemCodeAndQty.put(itemCode.getText().toString(),qty.getText().toString());
        }

        for(Map.Entry<String, String> entry : itemCodeAndQty.entrySet()) {
            new AsyncTask<String, Void, Void>() {
                @Override
                protected Void doInBackground(String... params) {
                    RetrievalListDetailUpdate.updateRetrieval(params[0],params[1],params[2]);
                    return null;
                }
                @Override
                protected void onPostExecute(Void result) {
                    finish();
                }
            }.execute(retrievalID,entry.getKey(),entry.getValue());
        }
    }


//    protected void BtnRestoreClick(View v) {
//        EditText et;
//        ArrayList<Integer> txtRetrievedList  = retrievalItemQty.get(retrievalID);
//
//        if(txtRetrievedList!=null){
//            if (txtRetrievedList.size() != 0) {
//                for (int i = 0; i < txtRetrievedList.size(); i++) {
//                    v = lv.getChildAt(i);
//                    et = (EditText) v.findViewById(R.id.EditText1);
//                    et.setText(txtRetrievedList.get(i).toString());
//                }
//            }
//        }else{
//            Toast.makeText(this,"There's no value to restore",Toast.LENGTH_LONG).show();
//        }
//    }

//    protected void BtnFinalizeDisbursmentListClick(View v) {
//        EditText et;
//        ArrayList<Integer> txtRetrievedList = new ArrayList<Integer>();
//        for (int i = 0; i < lv.getCount(); i++) {
//            v = lv.getChildAt(i);
//            et = (EditText) v.findViewById(R.id.EditText1);
//            txtRetrievedList.add(Integer.parseInt(et.getText().toString()));
//        }
//        retrievalItemQty.put(retrievalID,txtRetrievedList);
//    }
}