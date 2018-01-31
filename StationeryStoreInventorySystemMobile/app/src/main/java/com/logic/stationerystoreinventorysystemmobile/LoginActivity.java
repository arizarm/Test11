package com.logic.stationerystoreinventorysystemmobile;

import android.app.Activity;
import android.content.Intent;
import android.content.SharedPreferences;
import android.graphics.Color;
import android.os.AsyncTask;
import android.os.StrictMode;
import android.preference.PreferenceManager;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;
import android.content.Intent;

public class LoginActivity extends Activity {

    EditText email,password;
    Button loginBtn;

    boolean loginCheck = false;
    String message;

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

                if(email.getText().toString().matches("") || password.getText().toString().matches(""))
                {
                    Toast.makeText(getApplicationContext(),
                            "Please fill both User ID and Password", Toast.LENGTH_SHORT).show();
                }
                else {
                    Employee emp = Employee.VerifyEmployee(email.getText().toString(), password.getText().toString());
                    if (emp != null) {

                        SharedPreferences pref = PreferenceManager.getDefaultSharedPreferences(getApplicationContext());
                        SharedPreferences.Editor editor = pref.edit();

                        editor.putString("eid", emp.get("eid"));
                        editor.putString("deptCode", emp.get("deptCode"));
                        editor.putString("ename", emp.get("ename"));
                        editor.putString("role", emp.get("role"));
                        editor.putString("password", emp.get("password"));
                        editor.putString("email", emp.get("email"));
                        editor.putString("isTemphead", emp.get("isTemphead"));
                        editor.putString("startDate", emp.get("startDate"));
                        editor.putString("endDate", emp.get("endDate"));
                        editor.commit();

                        if (emp.get("role").equals("DepartmentHead")) {
                            loginCheck = true;
                            message = "Department Head Access Granted";
                        }
                        else if(emp.get("role").equals("Store Clerk") || emp.get("role").equals("Store Supervisor") || emp.get("role").equals("Store Manager"))
                        {
                            loginCheck = true;
                            message = "Store Employee Access Granted";
                        }
                        else if (emp.get("isTemphead").equals("Y"))
                        {
                            new AsyncTask<String, Void, Boolean>() {
                                @Override
                                protected Boolean doInBackground(String... params) {
                                    return Employee.CheckIsTempHead(params[0]);
                                }

                                @Override
                                protected void onPostExecute(Boolean result) {
                                    if (result)
                                    {
                                        loginCheck = true;
                                        message = "Acting Dept Head Access Granted";
                                    }//
                                }
                            }.execute(emp.get("eid"));
                        }
                        else
                        {
                            message = "Access Denied";
                        }
                    }
                    else
                    {
                        message = "Wrong Credentials";
                    }

                    Toast.makeText(getApplicationContext(),message, Toast.LENGTH_SHORT).show();

                    if(loginCheck)
                    {
                        Intent i = new Intent(getApplicationContext(), MainActivity.class);
                        startActivity(i);
                    }
                }
            }
        });
    }
}
