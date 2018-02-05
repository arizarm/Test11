package com.logic.stationerystoreinventorysystemmobile;

import android.app.Activity;
import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.EditText;
import android.widget.TextView;

import java.util.List;

/**
 * Created by Mo Mo on 30/1/2018.
 */

//AUTHOR : KHIN MO MO ZIN
public class DisbursementAdapter extends ArrayAdapter<DisbursementDetailListItems> {

    private List<DisbursementDetailListItems> items;
    int resource;

    public DisbursementAdapter(Context context, int resource, List<DisbursementDetailListItems> items) {
        super(context, resource, items);
        this.resource = resource;
        this.items = items;
    }

    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        LayoutInflater inflater = (LayoutInflater) getContext().getSystemService(Activity.LAYOUT_INFLATER_SERVICE);
        View v = inflater.inflate(resource, null);
        final DisbursementDetailListItems d = items.get(position);
        if (d != null) {

            TextView itemCodeHidden = v.findViewById(R.id.itemCodeHidden);
            itemCodeHidden.setText(d.get("ItemCode"));
            TextView txtIDesc = v.findViewById(R.id.txtIDesc);
            txtIDesc.setText(d.get("ItemDesc"));
            TextView txtReqQty = v.findViewById(R.id.txtReqQty);
            txtReqQty.setText(d.get("ReqQty"));
            final EditText editTxtActQty = v.findViewById(R.id.editTxtActQty);
            editTxtActQty.setText(d.get("ActualQty"));
            TextView retrievedQtyHidden = v.findViewById(R.id.retrievedQtyHidden);
            retrievedQtyHidden.setText(d.get("RetrievedQty"));
            final EditText editTxtRemark = v.findViewById(R.id.editTxtRemark);
            editTxtRemark.setText(d.get("Remarks"));

            if (d.get("ActualQty") != null) {
                editTxtActQty.setText(d.get("ActualQty"));
            }
            editTxtActQty.setOnFocusChangeListener(new View.OnFocusChangeListener() {
                @Override
                public void onFocusChange(View v2, boolean hasFocus) {
                    if(!hasFocus){
                        String actualQty = editTxtActQty.getText().toString();
                        d.saveActualQty(actualQty);
                    }
                }
            });

            if (d.get("Remarks") != null) {
                editTxtRemark.setText(d.get("Remarks"));
            }
            editTxtRemark.setOnFocusChangeListener(new View.OnFocusChangeListener() {
                @Override
                public void onFocusChange(View v2, boolean hasFocus) {
                    if(!hasFocus){
                        String remarks = editTxtRemark.getText().toString();
                        d.saveRemarks(remarks);
                    }
                }
            });
        }
        return v;
    }
}
