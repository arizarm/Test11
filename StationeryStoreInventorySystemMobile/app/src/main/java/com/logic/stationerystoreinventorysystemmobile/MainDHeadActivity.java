package com.logic.stationerystoreinventorysystemmobile;

import android.app.ListActivity;
import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.ListView;
import android.widget.SimpleAdapter;

import java.util.List;

public class MainDHeadActivity extends ListActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main_dhead);
        List<Department> dept=Department.listDepartment();
        setListAdapter(new SimpleAdapter(this,dept,android.R.layout.simple_list_item_2,
                new String[]{"Department Code","Department Name"},new int[]{android.R.id.text1,android.R.id.text2}));

    }

    @Override
    protected void onListItemClick(ListView l, View v, int position, long id) {
        Department d=(Department)getListAdapter().getItem(position);
        Intent intent=new Intent(this,DeptDetailActivity.class);
        intent.putExtra("dcode",d.get("dCode"));
        startActivity(intent);
    }
}
