package com.logic.stationerystoreinventorysystemmobile;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.os.AsyncTask;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.view.Gravity;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.inputmethod.InputMethodManager;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

public class DiscrepancyAdhocItemDetailsActivity extends AppCompatActivity {
    TextView tvItemCode;
    TextView tvItemName;
    TextView tvBalanceQty;
    TextView tvUom;
    TextView tvPendingAdj;
    EditText etCurrentAdj;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_discrepancy_adhoc_item_details);

        String itemCode = getIntent().getStringExtra("itemCode");

        new AsyncTask<String, Void, CatalogueItem>(){

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
            }
        }.execute(itemCode);
    }

    protected void addItemClick(View v){
        etCurrentAdj = findViewById(R.id.etCurrentAdj);
        tvItemCode = findViewById(R.id.tvItemCode);
        TextView tvError = findViewById(R.id.tvError);
        int balanceQty = Integer.parseInt(tvBalanceQty.getText().toString());
        String adjustmentStr = etCurrentAdj.getText().toString();
        String itemCode = tvItemCode.getText().toString();
        if(Util.isInt(adjustmentStr)){
            tvError.setText("");
            int adjustment = Integer.parseInt(adjustmentStr);
            if(adjustment != 0){
                if(balanceQty + adjustment >= 0){
                    DiscrepancyHolder.addDiscrepancy(itemCode, adjustment);   //Adding a discrepancy to a static hashmap held in DiscrepancyHolder class
                    Toast t = Toast.makeText(this, "Item added", Toast.LENGTH_LONG);
                    Context c = getApplicationContext();
                    int offset = Math.round(150 * c.getResources().getDisplayMetrics().density);
                    //Setting the toast at a point below the center point, so that it doesn't overlap with the loading dialog in the
                    t.setGravity(Gravity.CENTER|Gravity.CENTER_HORIZONTAL, 0, offset);
                    t.show();
                    hideKeyboard();
                    finish();
                }
                else{
                    tvError.setText("Adjustment cannot reduce stock to below 0");
                }
            }
            else{
                tvError.setText("Please input a non-zero integer quantity");
            }
        }
        else{
            tvError.setText("Please input an integer quantity");
        }
    }

    private void hideKeyboard(){
        InputMethodManager inputManager = (InputMethodManager) getSystemService(Context.INPUT_METHOD_SERVICE);
        inputManager.hideSoftInputFromWindow((null == getCurrentFocus()) ? null : getCurrentFocus().getWindowToken(), InputMethodManager.HIDE_NOT_ALWAYS);
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
