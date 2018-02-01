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

public class RetrievalListDetailAdapter extends ArrayAdapter<Retrieval_ItemDetail> {

    private List<Retrieval_ItemDetail> items;
    int resource;

    public RetrievalListDetailAdapter(Context context, int resource, List<Retrieval_ItemDetail> items) {
        super(context, resource, items);
        this.resource = resource;
        this.items = items;
    }

    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        LayoutInflater inflater = (LayoutInflater) getContext().getSystemService(Activity.LAYOUT_INFLATER_SERVICE);

        View v =   inflater.inflate(resource, null);
        final Retrieval_ItemDetail d = items.get(position);
        if (d != null) {

            TextView itemCodeHidden = v.findViewById(R.id.iCodeHidden);
            itemCodeHidden.setText(d.get("ItemCode"));

            TextView Bin = v.findViewById(R.id.text1);
            Bin.setText(d.get("Bin"));

            TextView Description = v.findViewById(R.id.text2);
            Description.setText(d.get("Description"));

            TextView TotalRequestedQty = v.findViewById(R.id.text3);
            TotalRequestedQty.setText(d.get("TotalRequestedQty"));

            final EditText editTxtRetrievedQty = v.findViewById(R.id.EditText1);
            editTxtRetrievedQty.setText(d.get("RetrievedQty"));

            if (d.get("RetrievedQty") != null) {
                editTxtRetrievedQty.setText(d.get("RetrievedQty"));
            }
            editTxtRetrievedQty.setOnFocusChangeListener(new View.OnFocusChangeListener() {
                @Override
                public void onFocusChange(View v2, boolean hasFocus) {
                    if(!hasFocus){
                        String retrievedQty = editTxtRetrievedQty.getText().toString();
                        d.saveRetrievedQty(retrievedQty);
                    }
                }
            });
        }
        return v;
    }
}
