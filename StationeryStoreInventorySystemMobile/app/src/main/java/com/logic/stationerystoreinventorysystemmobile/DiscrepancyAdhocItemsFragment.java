package com.logic.stationerystoreinventorysystemmobile;

import android.app.Fragment;
import android.app.ListFragment;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ListView;
import android.widget.SimpleAdapter;

public class DiscrepancyAdhocItemsFragment extends Fragment {

    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        View v = inflater.inflate(R.layout.adhoc_discrepancy_item_list, container, false);
        ListView list = getActivity().findViewById(R.id.listItems);
        return v;
    }

}