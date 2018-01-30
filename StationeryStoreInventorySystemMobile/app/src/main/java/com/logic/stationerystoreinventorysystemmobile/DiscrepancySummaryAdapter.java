package com.logic.stationerystoreinventorysystemmobile;

import android.app.Activity;
import android.content.Context;
import android.text.Editable;
import android.text.TextWatcher;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.EditText;
import android.widget.TextView;

import java.util.List;

/**
 * Created by edwon on 29/1/2018.
 */

public class DiscrepancySummaryAdapter extends ArrayAdapter<Discrepancy> {

    private List<Discrepancy> items;
    int resource;

    public DiscrepancySummaryAdapter(Context context, int resource, List<Discrepancy> items) {
        super(context, resource, items);
        this.resource = resource;
        this.items = items;
    }

    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        LayoutInflater inflater = (LayoutInflater) getContext().getSystemService(Activity.LAYOUT_INFLATER_SERVICE);
        View v = inflater.inflate(resource, null);
        final Discrepancy d = items.get(position);
        if (d != null) {
            TextView tvItemCode = v.findViewById(R.id.tvItemCode);
            tvItemCode.setText(d.get("itemCode"));
            TextView tvItemName = v.findViewById(R.id.tvItemName);
            tvItemName.setText(d.get("description"));
            TextView tvBalance = v.findViewById(R.id.tvBalance);
            tvBalance.setText(d.get("balanceQty"));
            TextView tvAdj = v.findViewById(R.id.tvAdj);
            tvAdj.setText(d.get("adjustmentQty"));
            final EditText etRemarks = v.findViewById(R.id.etRemarks);
            if (d.get("remarks") != null) {
                etRemarks.setText(d.get("remarks"));
            }
            etRemarks.setOnFocusChangeListener(new View.OnFocusChangeListener() {
                @Override
                public void onFocusChange(View v2, boolean hasFocus) {
                        String remarks = etRemarks.getText().toString();
                        d.saveRemarks(remarks);
                }
            });
        }
        return v;
    }
}