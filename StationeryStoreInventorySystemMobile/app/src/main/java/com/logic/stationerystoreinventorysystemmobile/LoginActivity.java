package com.logic.stationerystoreinventorysystemmobile;

import android.app.Activity;
import android.content.SharedPreferences;
import android.graphics.Color;
import android.os.StrictMode;
import android.preference.PreferenceManager;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

public class LoginActivity extends Activity {

    EditText email,password;
    Button loginBtn;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);

        email = (EditText)findViewById(R.id.editText2);
        password = (EditText)findViewById(R.id.editText3);
        loginBtn = (Button) findViewById(R.id.button2);

        StrictMode.setThreadPolicy(StrictMode.ThreadPolicy.LAX);
        loginBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Employee emp = Employee.VerifyEmployee(email.getText().toString(), password.getText().toString());
                if(emp !=null) {

                    SharedPreferences pref = PreferenceManager.getDefaultSharedPreferences(getApplicationContext());
                    SharedPreferences.Editor editor = pref.edit();

                    editor.putString("eid",emp.get("eid"));
                    editor.putString("deptCode",emp.get("deptCode"));
                    editor.putString("ename",emp.get("ename"));
                    editor.putString("role",emp.get("role"));
                    editor.putString("password",emp.get("password"));
                    editor.putString("email",emp.get("email"));
                    editor.putString("isTemphead",emp.get("isTemphead"));
                    editor.putString("startDate",emp.get("startDate"));
                    editor.putString("endDate",emp.get("endDate"));
                    editor.commit();

                    if(emp.get("role").equals("DepartmentHead"))
                    {
                        Intent i = new Intent(getApplicationContext(), RequisitionListActivity.class);
                        startActivity(i);
                        Toast.makeText(getApplicationContext(),
                                "Department Head Access Granted",Toast.LENGTH_SHORT).show();
                    }
                    else if(emp.get("isTemphead").equals("Y") )
                            /*startDate.before(yestDate) && endDate.before(tmrDate))*/
                    {
                        new AsyncTask<String,Void,Boolean>(){
                            @Override
                            protected Boolean doInBackground(String...params){
                                return Employee.CheckIsTempHead(params[0]);
                            }

                            @Override
                            protected void onPostExecute(Boolean result){
                                if(result){
                                    Intent i = new Intent(getApplicationContext(), RequisitionListActivity.class);
                                    startActivity(i);
                                    Toast.makeText(getApplicationContext(),
                                            "Acting Dept Head Access Granted", Toast.LENGTH_SHORT).show();
                                }else
                                {
                                    Toast.makeText(getApplicationContext(),
                                            "Access Denied",Toast.LENGTH_SHORT).show();
                                }
                            }
                        }.execute(emp.get("eid"));

                        /*if((today.equals(startCal) || today.after(startCal)) && (today.equals(endCal)||today.before(endCal))){
                            Intent i = new Intent(getApplicationContext(), RequisitionListActivity.class);
                            startActivity(i);
                            Toast.makeText(getApplicationContext(),
                                    "Acting Dept Head Access Granted", Toast.LENGTH_SHORT).show();
                        }*/
                    }
                    else
                    {
                        Toast.makeText(getApplicationContext(),
                                "Access Denied",Toast.LENGTH_SHORT).show();
                    }
                }else{
                    Toast.makeText(getApplicationContext(), "Wrong Credentials",Toast.LENGTH_SHORT).show();
                }
            }
        });
    }
}
