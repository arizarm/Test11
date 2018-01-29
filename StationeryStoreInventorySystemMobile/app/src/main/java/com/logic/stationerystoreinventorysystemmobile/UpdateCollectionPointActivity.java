package com.logic.stationerystoreinventorysystemmobile;

import android.graphics.Color;
import android.os.AsyncTask;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.Toast;

import java.util.List;

public class UpdateCollectionPointActivity extends AppCompatActivity implements AdapterView.OnItemSelectedListener {

    Spinner spinner;
    String bid="BDTD";
    @Override
    protected void onCreate(Bundle savedInstanceState) {

        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_update_collectionpoint);
         final Spinner spinner = (Spinner) findViewById(R.id.spinnerCollectPoint);
        final TextView txtcid=(TextView)findViewById(R.id.txt1);
        final TextView name = (TextView)findViewById(R.id.txtError);

        new AsyncTask<Void,Void,List<String>>(){

            @Override
            protected List<String> doInBackground(Void... params) {
                return CollectionPoint.listCollectionPoint();
            }

            @Override
            protected void onPostExecute(List<String> result) {
                spinner.setAdapter(new ArrayAdapter<String>(getBaseContext(),

                        android.R.layout.simple_spinner_item, result));


            }
        }.execute();


        new AsyncTask<String,Void,String>(){
            @Override
            protected String doInBackground(String... params) {
                return Department.getCollectionID(params[0]);
            }

            @Override protected void onPostExecute(String result) {

                txtcid.setText(result);
                int a=getIndex(spinner,result);
                spinner.setSelection(a);

            }
        }.execute(bid);


        Button btnUpdate=(Button)findViewById(R.id.btnUpdateCollectPoint);
        btnUpdate.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                final Department d = new Department();
                boolean isSame = false;
                    String oselected=txtcid.getText().toString();
                    String cselected=spinner.getSelectedItem().toString();
                    long cid=spinner.getSelectedItemId()+1;
                    String k=String.valueOf(cid);
                    if (oselected.equalsIgnoreCase(cselected)){

                        String errormsg = "Original Collection Point";


                        TextView error = (TextView) spinner.getSelectedView();
                        error.setError("Error");
                        error.setTextColor(Color.RED);
                        error.setText(errormsg);
                        Toast.makeText(getApplicationContext(),"Please choose another collection Point" , Toast.LENGTH_LONG).show();
                        isSame=true;

                }
                if (!isSame) {
                    // name.setVisibility(View.GONE);
                    d.put("dCode",bid);
                    d.put("collectId",k);

                    new AsyncTask<Department, Void, Void>() {
                        @Override
                        protected Void doInBackground(Department... params){
                            Department.updateCollectionPoint(params[0]);
                            return null;
                        }
                        @Override
                        protected void onPostExecute(Void result) {


                        }
                    }.execute(d);

                    new AsyncTask<String,Void,String>(){
                        @Override
                        protected String doInBackground(String... params) {
                            return Department.getCollectionID(params[0]);
                        }

                        @Override protected void onPostExecute(String result) {

                            txtcid.setText(result);
                            int a=getIndex(spinner,result);
                            spinner.setSelection(a);

                        }
                    }.execute(bid);


                    Toast.makeText(UpdateCollectionPointActivity.this, "Update Success!", Toast.LENGTH_LONG).show();

                }
            }
       });

        spinner.setOnItemSelectedListener(this);
    }
    private int getIndex(Spinner spinner, String myString)
    {
        int index = 0;

        for (int i=0;i<spinner.getCount();i++){
            if (spinner.getItemAtPosition(i).toString().equalsIgnoreCase(myString)){
                index = i;
                break;
            }
        }
        return index;
    }




    public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
        // On selecting a spinner item
        String item = parent.getItemAtPosition(position).toString();
        long itemid = parent.getItemIdAtPosition(position + 1);

//        TextView name=(TextView)findViewById(R.id.txtError);
//       name.setVisibility(View.GONE);
        // Showing selected spinner item
        Toast.makeText(parent.getContext(), "Selected: " +item, Toast.LENGTH_LONG).show();
    }

    public void onNothingSelected(AdapterView<?> arg0) {
        // TODO Auto-generated method stub
    }

}