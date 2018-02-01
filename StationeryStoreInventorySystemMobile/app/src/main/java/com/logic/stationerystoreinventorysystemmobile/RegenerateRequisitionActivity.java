package com.logic.stationerystoreinventorysystemmobile;

import android.app.ProgressDialog;
import android.content.Intent;
import android.os.AsyncTask;
import android.os.StrictMode;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.ListView;
import android.widget.SimpleAdapter;
import android.widget.TextView;
import android.widget.Toast;

import java.util.ArrayList;
import java.util.List;

public class RegenerateRequisitionActivity extends AppCompatActivity {

    private static List<RegenerateRequisition> regenReqList = new ArrayList<RegenerateRequisition>();

    ArrayList<RegenerateRequisition> regenerateList = new ArrayList<RegenerateRequisition>();

    private static RegenerateRequisition regenReq;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_regenerate_requisition);

        Bundle b = getIntent().getExtras();

        TextView txtReqDate = (TextView) findViewById(R.id.regenReqDate);
        String reqDate = regenReq.get("ReqDate").toString();
        txtReqDate.setText(reqDate);

        TextView txtDepName = (TextView) findViewById(R.id.depName);
        String depName = regenReq.get("DepName").toString();
        txtDepName.setText(depName);

        TextView txtReqBy = (TextView) findViewById(R.id.reqBy);
        String reqBy = regenReq.get("ReqBy").toString();
        txtReqBy.setText(reqBy);

        final ListView lv = (ListView) findViewById(R.id.lstRegenReq);
        lv.setAdapter(new SimpleAdapter
                (this, regenReqList, R.layout.regenerate_requisition_row,
                        new String[]{"Code", "Description", "ShortfallQty"},
                        new int[]{R.id.itemCodeHidden, R.id.itemDesc, R.id.shortfallQty}));

        Button button = findViewById(R.id.btnGenerate);
        button.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {

                for (int i = 0; i < lv.getCount(); i++) {
                    v = lv.getChildAt(i);
                    CheckBox checkBox = v.findViewById(R.id.checkBox);
                    if(checkBox.isChecked())
                    {
                        RegenerateRequisition reqItem = (RegenerateRequisition) lv.getAdapter().getItem(i);
                        regenerateList.add(reqItem);
                    }
                }

                String message;
                if(regenerateList.size()!=0)
                {
                    new AsyncTask<Void, Void, Void>() {
                        ProgressDialog progress;
                        @Override
                        protected void onPreExecute() {
                            progress = ProgressDialog.show(getApplicationContext(), "Loading", "Generating Requisition", true);
                        }
                        @Override
                        protected Void doInBackground(Void... params) {
                            RegenerateRequisition.RegenerateRequisition(regenerateList);
                            return null;
                        }

                        @Override
                        protected void onPostExecute(Void params) {

                            Toast toast = Toast.makeText(getApplicationContext(), "Requisition Generation Successful",
                                    Toast.LENGTH_LONG);
                            toast.show();
                            Intent intent = new Intent(getApplicationContext(), DisbursementActivity.class);
                            startActivity(intent);
                            progress.dismiss();
                        }
                    }.execute();
                }
                else
                {
                    message = "Please select item to request!";
                    Util.customToast(message,getApplicationContext());
                }
            }
        });
    }

    public static void setRegenReqList(List<RegenerateRequisition> r)
    {
        regenReqList = r;
    }

    public void btnCancelClick(View v)
    {
        Intent intent = new Intent(getApplicationContext(), DisbursementActivity.class);
        startActivity(intent);
    }

    public static void setRegenerateRequisition(RegenerateRequisition r)
    {
        regenReq = r;
    }

    //to write code for ondestroy or onclose!!!
}
