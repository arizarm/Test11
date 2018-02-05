package com.logic.stationerystoreinventorysystemmobile;

import android.annotation.SuppressLint;
import android.content.Intent;
import android.os.AsyncTask;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ListView;
import android.widget.SimpleAdapter;

import java.util.List;

//AUTHOR : KHIN MYO MYO SHWE
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
