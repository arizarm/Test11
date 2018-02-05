package com.logic.stationerystoreinventorysystemmobile;

import android.content.Intent;
import android.os.AsyncTask;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.AdapterView;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Spinner;
import android.widget.TextView;

import org.w3c.dom.Text;

//AUTHOR : KHIN MYO MYO SHWE
public class DeptDetailActivity extends AppCompatActivity{

    final static int[] view = { R.id.txtDeptName, R.id.txtContactName, R.id.txtPhone, R.id.txtFax, R.id.txtHead,R.id.txtDeptRep,R.id.txtCollectPoint};
    final static String[] key = { "departmentName", "contactName", "phone", "fax","dHead","deptRep","collectId"};
     static String bid="";
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_dept_detail);
        Intent i = getIntent();
         bid = i.getStringExtra("dCode");
        new AsyncTask<String, Void, Department>() {
            @Override
            protected Department doInBackground(String... params) {
                return Department.getDept(params[0]);
                //return BookItem.getTitle(params[0]);
            }

            @Override
            protected void onPostExecute(Department result) {
                show(result);
            }
        }.execute(bid);
    }

    void show(Department dept) {
        int[] ids = { R.id.txtDeptName, R.id.txtContactName, R.id.txtPhone, R.id.txtFax, R.id.txtHead,R.id.txtDeptRep,R.id.txtCollectPoint};
        String[] keys = {"departmentName", "contactName", "phone", "fax","dHead","deptRep","collectId"};
        for (int i = 0; i < keys.length; i++) {
            TextView e = (TextView) findViewById(ids[i]);
            e.setText(dept.get(keys[i]));
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