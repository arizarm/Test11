package com.logic.stationerystoreinventorysystemmobile;

import android.app.ProgressDialog;
import android.graphics.Color;
import android.os.AsyncTask;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.view.View;
import android.widget.AdapterView;
import android.widget.Button;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.Toast;

import java.util.ArrayList;
import java.util.LinkedHashMap;
import java.util.List;
import java.util.Map;

public class UpdateDeptRepActivity extends AppCompatActivity implements AdapterView.OnItemSelectedListener {

    Spinner spinner;
    TextView txtcid;
    TextView name;
    String bid = "BDTD";
    Map.Entry<String, String> items ;
    private LinkedHashMapAdapter<String, String> adapter;

    @Override
    protected void onCreate(Bundle savedInstanceState) {

        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_update_deptrep);
        spinner = (Spinner) findViewById(R.id.spinnerCollectPoint);
        txtcid = (TextView) findViewById(R.id.txt1);
        name = (TextView) findViewById(R.id.txtError);

        new AsyncTask<String, Void, LinkedHashMap<String,String>>() {
            ProgressDialog progress;

            @Override
            protected  LinkedHashMap<String,String> doInBackground(String... params) {
                return Employee.listEmployee(params[0]);
            }

            @Override
            protected void onPostExecute(LinkedHashMap<String,String> result) {
                List<LinkedHashMap<String, String>> fillMaps = new ArrayList<LinkedHashMap<String, String>>();
                fillMaps.add(result);
                adapter = new LinkedHashMapAdapter<String, String>(UpdateDeptRepActivity.this, android.R.layout.simple_spinner_item, result);

                adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);

                spinner.setAdapter(adapter);

                spinner.setOnItemSelectedListener(UpdateDeptRepActivity.this);

            }
        }.execute(bid);

        new AsyncTask<String, Void, String>() {
            @Override
            protected String doInBackground(String... params) {
                return Employee.getDeptRepID(params[0]);
            }

            @Override
            protected void onPostExecute(String result) {

                txtcid.setText(result);
                int a = getIndex(spinner, result);

                spinner.setSelection(a);

            }
        }.execute(bid);




        Button btnUpdate = (Button) findViewById(R.id.btnUpdateDeptRep);
        btnUpdate.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {

                final Employee emp = new Employee();
                boolean isSame = false;



                String oselected = txtcid.getText().toString();
                String cselected =items.getValue();

                String cid = items.getKey();
                //String k = String.valueOf(cid);
                if (oselected.equalsIgnoreCase(cselected)) {

                    String errormsg = "Original Department Rep";


                    TextView error = (TextView) spinner.getSelectedView();
                    error.setError("Error");
                    error.setTextColor(Color.RED);
                    error.setText(errormsg);
                    /*((TextView) spinner.getSelectedView()).setError("Your Error Text");
                    */
                    Toast.makeText(getApplicationContext(), "Please choose another Employee", Toast.LENGTH_LONG).show();
                    isSame = true;

                }
                if (!isSame) {
                   // name.setVisibility(View.GONE);
                    emp.put("dCode", bid);
                    emp.put("eId", cid);
                    emp.put("role","Representative");

                    new AsyncTask<Employee, Void, Void>() {
                        @Override
                        protected Void doInBackground(Employee... params) {
                            Employee.updateDeptRep(params[0]);
                            return null;
                        }

                        @Override
                        protected void onPostExecute(Void result) {


                        }
                    }.execute(emp);

                    new AsyncTask<String, Void, String>() {
                        @Override
                        protected String doInBackground(String... params) {
                            return Employee.getDeptRepID(params[0]);
                        }

                        @Override
                        protected void onPostExecute(String result) {

                            txtcid.setText(result);
                            int a = getIndex(spinner, result);

                            spinner.setSelection(a);

                        }
                    }.execute(bid);

                    Toast.makeText(UpdateDeptRepActivity.this, "Update Success!", Toast.LENGTH_LONG).show();

                }
            }
        });


    }

    private int getIndex(Spinner spinner, String myString) {


        int index = 0;

        for (int i = 0; i < spinner.getCount(); i++) {
            spinner.setSelection(i);
             items = (Map.Entry<String, String>) spinner.getSelectedItem();
            if (items.getValue().equalsIgnoreCase(myString)) {
                index = i;
                break;
            }
        }
        return index;
    }



    @Override
    public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {

        spinner.setSelection(position);

        items = (Map.Entry<String, String>) spinner.getSelectedItem();

        Toast.makeText(parent.getContext(), "Selected: " + items.getValue(), Toast.LENGTH_LONG).show();
    }

    public void onNothingSelected(AdapterView<?> arg0) {
        // TODO Auto-generated method stub
    }
}

