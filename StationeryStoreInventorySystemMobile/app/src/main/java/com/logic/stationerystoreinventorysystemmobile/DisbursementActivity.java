package com.logic.stationerystoreinventorysystemmobile;

import android.app.Activity;
import android.app.FragmentTransaction;
import android.os.Bundle;

public class DisbursementActivity extends Activity
        implements DisbursementListFragment.DisbursementListListener{

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_disbursement);

    }

    @Override
    public void itemClicked(DisbursementListItems d)
    {
        DisbursementDetailFragment details = new DisbursementDetailFragment();
        FragmentTransaction ft = getFragmentManager().beginTransaction();
        details.setDisbursementListItems(d);
        ft.replace(R.id.fragment_container, details);
        ft.addToBackStack(null);
        ft.setTransition(FragmentTransaction.TRANSIT_FRAGMENT_FADE);
        ft.commit();
    }
}
