package com.logic.stationerystoreinventorysystemmobile;

import android.app.Activity;
import android.app.ProgressDialog;
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

    boolean redToast = false;

    Employee emp;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);

        email = (EditText)findViewById(R.id.editText2);
        password = (EditText)findViewById(R.id.editText3);
        loginBtn = (Button) findViewById(R.id.button2);

        loginBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {

                if(email.getText().toString().matches("") || password.getText().toString().matches(""))
                {
                    message = "Please enter both User ID and Password";
                    Util.redsToast(message,LoginActivity.this);
                }
                else {
                    new AsyncTask<Void, Void, Employee>() {
                        ProgressDialog progress;

                        @Override
                        protected void onPreExecute() {
                            progress = ProgressDialog.show(LoginActivity.this, "Loading", "Verifying User", true);
                        }

                        @Override
                        protected Employee doInBackground(Void... params) {
                            try
                            {
                                return Employee.VerifyEmployee(email.getText().toString(), password.getText().toString());
                            }
                            catch (Exception e)
                            {
                                return null;
                            }
                        }

                        @Override
                        protected void onPostExecute(Employee result) {
                            if (result != null) {

                                emp = result;

                                SharedPreferences pref = PreferenceManager.getDefaultSharedPreferences(getApplicationContext());
                                SharedPreferences.Editor editor = pref.edit();

                                editor.putString("eid", emp.get("eid"));
                                editor.putString("deptCode", emp.get("deptCode"));
                                editor.putString("ename", emp.get("ename"));
                                editor.putString("role", emp.get("role"));
                                //editor.putString("password", emp.get("password"));
                                editor.putString("email", emp.get("email"));
                                editor.putString("isTemphead", emp.get("isTemphead"));
                                editor.putString("startDate", emp.get("startDate"));
                                editor.putString("endDate", emp.get("endDate"));
                                editor.commit();

                                if (emp.get("role").equals("DepartmentHead")) {
                                    loginCheck = true;
                                    message = "Department Head Access Granted";
                                }else if(emp.get("role").equals("Representative")){

                                    message = "Department Rep Access Granted";
                                    Intent i = new Intent(getApplicationContext(),UpdateCollectionPointActivity.class);
                                    startActivity(i);
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
                                            }else
                                            {
                                                message = "Access Denied";
                                                redToast = true;
                                            }
                                        }
                                    }.execute(emp.get("eid"));
                                }
                                else
                                {
                                    message = "Access Denied";
                                    redToast=true;
                                }
                            }
                            else
                            {
                                message = "Wrong Credentials";
                                redToast=true;
                            }

                            if(redToast)
                            {
                                Util.redsToast(message,LoginActivity.this);
                            }
                            else
                            {
                                Util.greenToast(message,LoginActivity.this);
                            }

                            progress.dismiss();

                            if(loginCheck)
                            {
                                Intent i = new Intent(getApplicationContext(), MainActivity.class);
                                startActivity(i);
                            }
                        }
                    }.execute();
                }
            }
        });
    }

    @Override
    public void onBackPressed() {
        Intent i = new Intent(getApplicationContext(), LoginActivity.class);
        startActivity(i);
    }
}
