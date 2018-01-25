package com.logic.stationerystoreinventorysystemmobile;

import android.app.Activity;
import android.os.AsyncTask;
import android.os.Bundle;
import android.view.View;
import android.widget.ListView;
import android.widget.SimpleAdapter;

import java.util.ArrayList;

public class DiscrepancyAdhoc extends Activity {
    ListView list;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_discrepancy_adhoc);

//        list = findViewById(R.id.listItemsAdhoc);
//        new AsyncTask<Void, Void, ArrayList<CatalogueItem>>(){
//
//            @Override
//            protected ArrayList<CatalogueItem> doInBackground(Void... input){
//                return CatalogueItem.getAllBooks();
//            }
//
//            @Override
//            protected void onPostExecute(ArrayList<CatalogueItem> iList){
//                SimpleAdapter adapter = new SimpleAdapter(getApplicationContext(), iList, R.layout.adhoc_discrepancy_row, new String[]{"itemCode", "description"}, new int[]{R.id.tvItemCode,R.id.tvItemName});
//                list.setAdapter(adapter);
//            }
//        }.execute();
//        ArrayList<CatalogueItem> iList = new ArrayList<>();
//        iList.add(new CatalogueItem("C001", "book", "each", 43));
//        iList.add(new CatalogueItem("C002", "paper", "boxes", 34));
//        iList.add(new CatalogueItem("C003", "apple", "each", 56));
//        iList.add(new CatalogueItem("C004", "pear", "cup", 41));
//        SimpleAdapter adapter = new SimpleAdapter(getApplicationContext(), iList, R.layout.adhoc_discrepancy_row, new String[]{"itemCode", "description"}, new int[]{R.id.tvItemCode,R.id.tvItemName});
//                list.setAdapter(adapter);
    }

    protected void searchClick(View v){

    }

    protected void finaliseClick(View v){

    }
}
