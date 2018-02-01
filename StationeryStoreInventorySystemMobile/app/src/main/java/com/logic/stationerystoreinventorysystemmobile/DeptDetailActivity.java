package com.example.brandaling.myapplicationforad;

import android.content.Intent;
import android.os.AsyncTask;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.AdapterView;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Spinner;
import android.widget.TextView;

import org.w3c.dom.Text;

public class DeptDetailActivity extends AppCompatActivity{

    final static int[] view = { R.id.txtDeptName, R.id.txtContactName, R.id.txtPhone, R.id.txtFax, R.id.txtHead,R.id.txtDeptRep,R.id.txtCollectPoint};
    final static String[] key = { "departmentName", "contactName", "phone", "fax","dHead","deptRep","collectId"};
     static String bid="";
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        //Spinner spinner=findViewById(R.id.spinner2);
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

        //StrictMode.setThreadPolicy(StrictMode.ThreadPolicy.LAX);

        //BookItem book = BookItem.getId(bid);
        //show(book);

    }

    void show(Department dept) {
        int[] ids = { R.id.txtDeptName, R.id.txtContactName, R.id.txtPhone, R.id.txtFax, R.id.txtHead,R.id.txtDeptRep,R.id.txtCollectPoint};
        String[] keys = {"departmentName", "contactName", "phone", "fax","dHead","deptRep","collectId"};
        for (int i = 0; i < keys.length; i++) {
            TextView e = (TextView) findViewById(ids[i]);
            e.setText(dept.get(keys[i]));
        }



    }

}