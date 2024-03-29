package com.logic.stationerystoreinventorysystemmobile;


import android.app.Activity;
import android.app.ListFragment;
import android.app.ProgressDialog;
import android.content.Intent;
import android.os.AsyncTask;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ListView;
import android.widget.SimpleAdapter;

import java.util.List;


/**
 * A simple {@link Fragment} subclass.
 */

//AUTHOR : KHIN MO MO ZIN
public class DisbursementListFragment extends ListFragment {

    //inner interface for List Listener that will implement in DisbursementActivity.java
    interface DisbursementListListener {
        void itemClicked(DisbursementListItems d);
    }

    private DisbursementListListener listener;

    public DisbursementListFragment() {
        // Required empty public constructor
    }


    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {

        final LayoutInflater inf = inflater;


        new AsyncTask<Void, Void, List<DisbursementListItems>>() {

            ProgressDialog progress;

            @Override
            protected void onPreExecute() {
                progress = ProgressDialog.show(getActivity(), "Loading", "Getting Disbursement List", true);
            }

            @Override
            protected List<DisbursementListItems> doInBackground(Void... params) {
                return DisbursementListItems.getDisbursementListItems();
            }

            @Override
            protected void onPostExecute(List<DisbursementListItems> result) {

                SimpleAdapter sa = new SimpleAdapter(inf.getContext(), result,
                        R.layout.disbursement_list_row,
                        new String[]{"DisbId", "DepName", "CollectionDate"},
                        new int[]{R.id.disbIDHidden, R.id.disDepName, R.id.disbDate});

                setListAdapter(sa);
                progress.dismiss();

                if (result.isEmpty()) {
                    Util.redsToast("There is no Pending Disbursement!! ", getActivity());
                    Intent i2 = new Intent(getActivity(), MainActivity.class);
                    startActivity(i2);
                }
            }
        }.execute();
        return super.onCreateView(inflater, container, savedInstanceState);
    }

    @Override
    public void onAttach(Activity activity) {
        super.onAttach(activity);
        this.listener = (DisbursementListListener) activity;
    }

    @Override
    public void onListItemClick(ListView l, View v, int position, long id) {
        if (listener != null) {
            DisbursementListItems d = (DisbursementListItems) getListAdapter().getItem((int) id);
            listener.itemClicked(d);
        }
    }
}
