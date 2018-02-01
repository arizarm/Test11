package com.logic.stationerystoreinventorysystemmobile;

import android.annotation.SuppressLint;
import android.content.Intent;
import android.os.AsyncTask;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ListView;
import android.widget.SimpleAdapter;

import java.util.List;

public class DeptActivity extends AppCompatActivity implements AdapterView.OnItemClickListener {
    @SuppressLint("StaticFieldLeak")
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_dept);

        final ListView lv = (ListView) findViewById(R.id.deptListView);
        lv.setOnItemClickListener(this);

        new AsyncTask<Void,Void,List<Department>>(){
            protected  List<Department> doInBackground(Void...params){
                return  Department.listDepartment();
            }
            protected void onPostExecute(List<Department> result){
            SimpleAdapter sa=new SimpleAdapter(DeptActivity.this,result,
                    android.R.layout.simple_list_item_2,new String[]{"dCode","departmentName"},new int[]{android.R.id.text1,android.R.id.text2});
            lv.setAdapter(sa);
            }

        }.execute();

    }

    @Override
    public void onItemClick(AdapterView<?> av, View view, int position, long id) {
        try {
            Department d=(Department)av.getAdapter().getItem(position);
        Intent intent=new Intent(this, DeptDetailActivity.class);
        intent.putExtra("dCode",d.get("dCode"));
        startActivity(intent);
        }catch (Exception e){
            Log.e("tag tag",e.toString() );
        }
    }
}
