package com.logic.stationerystoreinventorysystemmobile;


import android.os.AsyncTask;
import android.os.Bundle;
import android.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ListView;
import android.widget.SimpleAdapter;
import android.widget.TextView;

import java.util.HashMap;
import java.util.List;


/**
 * A simple {@link Fragment} subclass.
 */
public class DisbursementDetailFragment extends Fragment {

    private static HashMap<String,String> d;

    SimpleAdapter sa;

    public DisbursementDetailFragment() {
        // Required empty public constructor
    }


    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {

//        if(savedInstanceState != null)
//        {
//            disbId = savedInstanceState.getString("disbId");
//        }

        return inflater.inflate(R.layout.fragment_disbursement_detail, container, false);
    }

    @Override
    public void onStart() {
        super.onStart();
        View view = getView();
        if(view != null)
        {
            TextView disDate = view.findViewById(R.id.disbDate);
            TextView disbTime = view.findViewById(R.id.disbTime);
            TextView depName = view.findViewById(R.id.depName);
            TextView disColPoint = view.findViewById(R.id.disColPoint);

            disDate.setText(d.get("CollectionDate").toString());
            disbTime.setText(d.get("CollectionTime").toString());
            depName.setText(d.get("DepName").toString());
            disColPoint.setText(d.get("CollectionPoint").toString());

            final String disbId = d.get("DisbId").toString();

            final ListView lv = (ListView) view.findViewById((R.id.lstDisbDetail));
            //lv.setOnItemClickListener(this);
            new AsyncTask<Void, Void, List<DisbursementDetailListItems>>() {
                @Override
                protected List<DisbursementDetailListItems> doInBackground(Void... params) {
                    return DisbursementDetailListItems.getDisbursementDetailListItems(disbId);
                }

                @Override
                protected void onPostExecute(List<DisbursementDetailListItems> result) {

                    lv.setAdapter(sa = new SimpleAdapter(getActivity(), result,
                            R.layout.disbursement_detail_list_row,
                            new String[]{"ItemCode","ItemDesc", "ReqQty","ActualQty","Remarks"},
                            new int[]{R.id.itemCodeHidden, R.id.txtIDesc, R.id.txtReqQty, R.id.editTxtActQty, R.id.editTxtRemark}));
                }
            }.execute();
        }
    }

        public  void  setDisbursementListItems(HashMap<String,String> d)
        {
            this.d = d;
        }

}
