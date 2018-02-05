package com.logic.stationerystoreinventorysystemmobile;

import android.app.Activity;
import android.app.ProgressDialog;
import android.content.Intent;
import android.os.AsyncTask;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.EditText;
import android.widget.TextView;

import java.util.ArrayList;

//AUTHOR : EDWIN TAN
public class DiscrepancyMonthlyItemDetailsActivity extends AppCompatActivity {
    TextView tvItemCode;
    TextView tvItemName;
    TextView tvBalanceQty;
    TextView tvUom;
    TextView tvPendingAdj;
    EditText etActual;
    ArrayList<CatalogueItem> iList;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_discrepancy_monthly_item_details);
        iList = DiscrepancyHolder.getMonthlyItems();
        String itemCode = getIntent().getStringExtra("itemCode");
        new AsyncTask<String, Void, CatalogueItem>(){
            ProgressDialog progress;

            @Override
            protected void onPreExecute() {
                progress = ProgressDialog.show(DiscrepancyMonthlyItemDetailsActivity.this, "Search", "Searching through items", true);
            }
            @Override
            protected CatalogueItem doInBackground(String... itemCode){
                return CatalogueItem.getItem(itemCode[0]);
            }

            @Override
            protected void onPostExecute(CatalogueItem ci){
                tvItemCode = findViewById(R.id.tvItemCode);
                tvItemName = findViewById(R.id.tvItemName);
                tvBalanceQty = findViewById(R.id.tvBalance);
                tvUom = findViewById(R.id.tvUom);
                tvPendingAdj = findViewById(R.id.tvPendingAdj);

                tvItemCode.setText(ci.get("itemCode"));
                tvItemName.setText(ci.get("description"));
                tvBalanceQty.setText(ci.get("balanceQty"));
                tvUom.setText(ci.get("unitOfMeasure"));
                tvPendingAdj.setText(ci.get("adjustments"));
                progress.dismiss();
            }
        }.execute(itemCode);
    }

    protected void inputActualClick(View v){
        boolean complete = false;
        for(CatalogueItem ci : iList){
            if(ci.get("itemCode").equals(tvItemCode.getText().toString())){
                TextView tvError = findViewById(R.id.tvError);
                etActual = findViewById(R.id.etActual);
                String actualQty = etActual.getText().toString();
                if(Util.isInt(actualQty)){
                    if(Integer.parseInt(actualQty) >= 0){
                        if(actualQty.equals(tvBalanceQty.getText().toString())){
                            tvError.setText("Actual quantity is the same as current balance");
                            break;
                        }
                        else{
                            ci.monthlyActualInput(actualQty);
                            complete = true;
                            break;
                        }
                    }
                    else{
                        tvError.setText("Actual quantity cannot be negative");
                    }
                }
                else{
                    tvError.setText("Please input an integer value");
                    break;
                }
            }
        }

        if(complete){
//            Intent i = new Intent(this, DiscrepancyMonthlyActivity.class);
//            startActivity(i);
            finish();
        }
    }

    protected void markCorrectClick(View v){
        for(CatalogueItem ci : iList){
            if(ci.get("itemCode").equals(tvItemCode.getText().toString())){
                ci.monthlyCorrectInput();
                break;
            }
        }
        finish();
//        Intent i = new Intent(this, DiscrepancyMonthlyActivity.class);
//        startActivity(i);
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
