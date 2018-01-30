package com.logic.stationerystoreinventorysystemmobile;

import android.app.DatePickerDialog;
import android.app.Dialog;
import android.app.ProgressDialog;
import android.graphics.Color;
import android.icu.text.SimpleDateFormat;
import android.os.AsyncTask;
import android.os.Build;
import android.support.annotation.RequiresApi;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.text.Editable;
import android.view.View;
import android.widget.AdapterView;
import android.widget.Button;
import android.widget.DatePicker;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.Toast;

import java.text.ParseException;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;
import java.util.LinkedHashMap;
import java.util.List;
import java.util.Map;

public class UpdateActingDeptHeadActivity extends AppCompatActivity implements AdapterView.OnItemSelectedListener {

    static final int DATE_DIALOG_SID = 1;
    static final int DATE_DIALOG_EID = 2;

    Spinner spinner;
    TextView txtcid;
    TextView name;
    TextView txtSDate;
    TextView txtEDate;
    Button btnSDate;
    Button btnEDate;
    private int year;
    private int month;
    private int day;
    String bid = "BDTD";
    Map.Entry<String, String> items;
    private LinkedHashMapAdapter<String, String> adapter;

    @RequiresApi(api = Build.VERSION_CODES.N)
    @Override
    protected void onCreate(Bundle savedInstanceState) {

        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_update_acting_dept_head);
        spinner = (Spinner) findViewById(R.id.spinnerCollectPoint);
        txtcid = (TextView) findViewById(R.id.txt1);
        name = (TextView) findViewById(R.id.txtError);
        txtSDate = (TextView) findViewById(R.id.txtSDate);
        txtEDate = (TextView) findViewById(R.id.txtEDate);




        new AsyncTask<String, Void, LinkedHashMap<String, String>>() {
            ProgressDialog progress;

            @Override
            protected LinkedHashMap<String, String> doInBackground(String... params) {
                return Employee.listActingHead(params[0]);
            }

            @Override
            protected void onPostExecute(LinkedHashMap<String, String> result) {
                List<LinkedHashMap<String, String>> fillMaps = new ArrayList<LinkedHashMap<String, String>>();

                fillMaps.add(result);
                adapter = new LinkedHashMapAdapter<String, String>(UpdateActingDeptHeadActivity.this, android.R.layout.simple_spinner_item, result);

                adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);

                spinner.setAdapter(adapter);

                spinner.setOnItemSelectedListener(UpdateActingDeptHeadActivity.this);

            }
        }.execute(bid);

        new AsyncTask<String, Void, Employee>() {
            @Override
            protected Employee doInBackground(String... params) {
                return Employee.getActingDeptHeadID(params[0]);
            }

            @Override
            protected void onPostExecute(Employee result) {

                 txtcid.setText(result.get("ename"));
                 String ahead=txtcid.getText().toString();
                if(result==null){
                   spinner.setSelection(0);


                }else {
                    int a = getIndex(spinner, result.get("ename"));

                    spinner.setSelection(a);
                    txtSDate.setText(result.get("startDate"));
                    txtEDate.setText(result.get("endDate"));
//                    btnSDate.setClickable(true);
//                    btnEDate.setClickable(true);
                }
            }
        }.execute(bid);

        setCurrentDateOnView();
        addListenerOnButton();
        txtSDate.addTextChangedListener(this);

        Button btnUpdate = (Button) findViewById(R.id.btnUpdateAHead);
        btnUpdate.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {

                final Employee emp = new Employee();
                boolean isSame = false;


                String oselected = txtcid.getText().toString();
                String cselected = items.getValue();

                String cid = items.getKey();
                //String k = String.valueOf(cid);
                if (oselected.equalsIgnoreCase(cselected)) {

                    String errormsg = "Original Department Acting Head";


                    TextView error = (TextView) spinner.getSelectedView();
                    error.setError("Error");
                    error.setTextColor(Color.RED);
                    error.setText(errormsg);

                    /*((TextView) spinner.getSelectedView()).setError("Your Error Text");
                    */
                    Toast.makeText(getApplicationContext(), "Please choose another Employee", Toast.LENGTH_LONG).show();





                    isSame=true;
                }
//                if (txtSDate.getText()=="" && items.getKey()!="0"){
//                    txtSDate.setError("Please Choose Date");
//
//                    Toast.makeText(getApplicationContext(), "Please choose Date", Toast.LENGTH_LONG).show();
//                }
//                if (txtEDate.getText()=="" && items.getKey()!="0"){
//                    txtEDate.setError("Please Choose Date");
//
//                    Toast.makeText(getApplicationContext(), "Please choose Date", Toast.LENGTH_LONG).show();
//                }
                if(oselected=="null" && items.getKey()=="0"){
                    String errormsg = "No Department Acting Head";
                    TextView error = (TextView) spinner.getSelectedView();
                    error.setError("Error");
                    error.setTextColor(Color.RED);
                    error.setText(errormsg);
                    Toast.makeText(getApplicationContext(), "Please choose another Employee"+oselected, Toast.LENGTH_LONG).show();
                    isSame = true;
                }
//                if (!isSame) {
//                    // name.setVisibility(View.GONE);
//                    emp.put("deptcode", bid);
//                    emp.put("eid", cid);
//                    emp.put("role","Representative");
//
//                    new AsyncTask<Employee, Void, Void>() {
//                        @Override
//                        protected Void doInBackground(Employee... params) {
//                            Employee.updateActingDHead(params[0]);
//                            return null;
//                        }
//
//                        @Override
//                        protected void onPostExecute(Void result) {
//
//
//                        }
//                    }.execute(emp);
//
//                    new AsyncTask<String, Void, String>() {
//                        @Override
//                        protected String doInBackground(String... params) {
//                            return Employee.getActingDeptHeadID(params[0]);
//                        }
//
//                        @Override
//                        protected void onPostExecute(String result) {
//
//                            txtcid.setText(result);
//                            int a = getIndex(spinner, result);
//
//                            spinner.setSelection(a);
//
//                        }
//                    }.execute(bid);
//
//                    Toast.makeText(UpdateActingDeptHeadActivity.this, "Update Success!", Toast.LENGTH_LONG).show();
//
//                }
            }
        });
    }


    public void setCurrentDateOnView() {
        //Date d=ConvertStringToDate(txtSDate.getText().toString());

        final Calendar cal = Calendar.getInstance();
        year = cal.get(Calendar.YEAR);
        month = cal.get(Calendar.MONTH);
        day = cal.get(Calendar.DAY_OF_MONTH);

        // set current date into textview
        txtSDate.setText(new StringBuilder().append(day).append("/").append(month + 1)
                .append("/").append(year)
                .append(" "));

        // set current date into datepicker
        //dpSDate.init(year, month, day, null);

        // set current date into textview
        txtEDate.setText(new StringBuilder().append(day).append("/").append(month + 1)
                .append("/").append(year)
                .append(" "));


    }

    public void addListenerOnButton() {

        btnSDate=(Button)findViewById(R.id.btnForSDate);
        btnSDate.setOnClickListener(new View.OnClickListener() {

            @Override
            public void onClick(View v) {

                showDialog(DATE_DIALOG_SID);

            }
        });

        btnEDate=(Button)findViewById(R.id.btnForEDate);
        btnEDate.setOnClickListener(new View.OnClickListener() {

            @Override
            public void onClick(View v) {

                showDialog(DATE_DIALOG_EID);

            }
        });
    }

    @Override
    protected Dialog onCreateDialog(int id) {
        switch (id) {
            case DATE_DIALOG_SID:

                // set date picker as current date
                return new DatePickerDialog(this, datePickerListenerS,
                        year, month, day);
            case DATE_DIALOG_EID:

                // set date picker as current date
                return new DatePickerDialog(this, datePickerListenerE,
                        year, month, day);

            default: break;
        }
        return null;
    }

    private DatePickerDialog.OnDateSetListener datePickerListenerS
            = new DatePickerDialog.OnDateSetListener() {

        // when dialog box is closed, below method will be called.
        public void onDateSet(DatePicker view, int selectedYear,
                              int selectedMonth, int selectedDay) {
            year = selectedYear;
            month = selectedMonth;
            day = selectedDay;


            // set selected date into textview
            txtSDate.setText(new StringBuilder().append(day).append("/").append(month + 1)
                    .append("/").append(year)
                    .append(" "));

            // set selected date into datepicker also

        }
    };

    private DatePickerDialog.OnDateSetListener datePickerListenerE
            = new DatePickerDialog.OnDateSetListener() {

        // when dialog box is closed, below method will be called.
        public void onDateSet(DatePicker view, int selectedYear,
                              int selectedMonth, int selectedDay) {
            year = selectedYear;
            month = selectedMonth;
            day = selectedDay;
                txtEDate.setText(new StringBuilder().append(day).append("/").append(month + 1)
                        .append("/").append(year)
                        .append(" "));

            }


    } ;


    public void afterTextChanged(Editable s) {
        // validation code goes here
        if(txtSDate.getText()=="")
        {
            txtSDate.setError("hi");
        }
    }


//@RequiresApi(api = Build.VERSION_CODES.N)
//public  Date ConvertStringToDate(String date){
//
//
//    SimpleDateFormat dateFormat = new SimpleDateFormat("dd/MM/yyyy");
//    Date convertedDate = new Date();
//    try {
//        convertedDate = dateFormat.parse(date);
//    } catch (ParseException e) {
//        e.printStackTrace();
//    }
//    return convertedDate;
//}


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


        public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {

            spinner.setSelection(position);
            //long itemid=parent.getItemIdAtPosition(position);
            items = (Map.Entry<String, String>) spinner.getSelectedItem();
            if(items.getKey()=="0"){
                btnSDate.setEnabled(false);
                btnEDate.setEnabled(false);
                txtSDate.setText(null);
                txtEDate.setText(null);
            }
            else {
                btnSDate.setEnabled(true);
                btnEDate.setEnabled(true);
            }

            Toast.makeText(parent.getContext(), "Selected: " + items.getValue() + "," + items.getKey(), Toast.LENGTH_LONG).show();
        }

        public void onNothingSelected(AdapterView<?> arg0) {
            // TODO Auto-generated method stub
        }
    }


