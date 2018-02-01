package com.logic.stationerystoreinventorysystemmobile;


import android.app.Fragment;
import android.app.ProgressDialog;
import android.content.Intent;
import android.graphics.Color;
import android.os.AsyncTask;
import android.os.Bundle;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.SimpleAdapter;
import android.widget.TextView;
import android.widget.Toast;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;


/**
 * A simple {@link Fragment} subclass.
 */
public class DisbursementDetailFragment extends Fragment {

    private static HashMap<String, String> d;

    SimpleAdapter sa;
    ListView lv;
    View view;
    String accessCode, remark, status, itemCode, itemDesc;
    int retrievedQty, actQty, reqQty, shortfallQty, discrepancyQty;
    boolean actualQtyValidate = true;
    boolean redToast;

    //create list for shortfall item to regenerate requisition
    List<RegenerateRequisition> regenReqList = new ArrayList<RegenerateRequisition>();

    public DisbursementDetailFragment() {
        // Required empty public constructor
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        return inflater.inflate(R.layout.fragment_disbursement_detail, container, false);
    }

    @Override
    public void onStart() {
        super.onStart();
        view = getView();
        if (view != null) {

            TextView disDate = view.findViewById(R.id.disbDate);
            TextView disbTime = view.findViewById(R.id.disbTime);
            TextView depName = view.findViewById(R.id.depName);
            TextView disColPoint = view.findViewById(R.id.disColPoint);

            disDate.setText(d.get("CollectionDate").toString());
            disbTime.setText(d.get("CollectionTime").toString());
            depName.setText(d.get("DepName").toString());
            disColPoint.setText(d.get("CollectionPoint").toString());

            final String disbId = d.get("DisbId").toString();

            lv = (ListView) view.findViewById((R.id.lstDisbDetail));

            new AsyncTask<Void, Void, List<DisbursementDetailListItems>>() {
                ProgressDialog progress;

                @Override
                protected void onPreExecute() {
                    progress = ProgressDialog.show(getActivity(), "Loading", "Getting disbursement Detail", true);
                }

                @Override
                protected List<DisbursementDetailListItems> doInBackground(Void... params) {
                    //get disbursement detail from wcf using disbursement id
                    return DisbursementDetailListItems.getDisbursementDetailListItems(disbId);
                }

                @Override
                protected void onPostExecute(List<DisbursementDetailListItems> result) {
                    //set result data to listview
                    DisbursementAdapter adapter = new DisbursementAdapter(getActivity(), R.layout.disbursement_detail_list_row, result);
                    lv.setAdapter(adapter);
                    progress.dismiss();

                }
            }.execute();

            try {

                Button button = view.findViewById(R.id.btnAck);
                button.setOnClickListener(new View.OnClickListener() {
                    @Override
                    public void onClick(View v) {

                        v.requestFocus();

                        new AsyncTask<Void, Void, String>() {
                            ProgressDialog progress;

                            @Override
                            protected void onPreExecute() {
                                progress = ProgressDialog.show(getActivity(), "Loading", "Processing disbursement...", true);
                            }

                            @Override
                            protected String doInBackground(Void... params) {
                                actualQtyValidate = true;

                                //crate array list to hold items to be updated
                                final ArrayList<DisbursementDetailListItems> toBeUpdated = new ArrayList<DisbursementDetailListItems>();
                                //get data from list view
                                for (int i = 0; i < lv.getCount(); i++) {

                                    //get disbursement object from list view
                                    DisbursementDetailListItems disb = (DisbursementDetailListItems) lv.getAdapter().getItem(i);

                                    try {
                                        actQty = Integer.parseInt(disb.get("ActualQty").toString());
                                    } catch (Exception e) {
                                        status = "Actual Quantity cannot be empty!";
                                        redToast = true;
                                    }

                                    reqQty = Integer.parseInt(disb.get("ReqQty").toString());
                                    retrievedQty = Integer.parseInt(disb.get("RetrievedQty").toString());
                                    remark = disb.get("Remarks").toString();

                                    //check for shortfall item
                                    if (actQty < reqQty) {

                                        Log.i("ShortfallCheck", "Shortfall item exists");

                                        itemDesc = disb.get("ItemDesc");
                                        itemCode = disb.get("ItemCode");
                                        shortfallQty = reqQty - actQty;
                                        RegenerateRequisition reqItem = new RegenerateRequisition(itemCode, itemDesc, String.valueOf(shortfallQty), disbId);
                                        regenReqList.add(reqItem);

                                        Log.i("ShortfallItems", itemCode + itemDesc + String.valueOf(shortfallQty));

                                        //check for discrepancy
                                        if (actQty < retrievedQty) {
                                            discrepancyQty = actQty - retrievedQty;
                                            DiscrepancyHolder.addDiscrepancy(itemCode, discrepancyQty);

                                            Log.i("Discrepancy Item", itemCode + String.valueOf(discrepancyQty));
                                        }
                                    } else if (actQty > retrievedQty) {
                                        //display error for invalid quantity
                                        actualQtyValidate = false;
                                    }

                                    //add to disbursement object to be updated in database
                                    DisbursementDetailListItems detailListItems = new DisbursementDetailListItems(disbId, String.valueOf(actQty), remark);
                                    toBeUpdated.add(detailListItems);
                                }
                                if (actualQtyValidate == false) {
                                    status = "Actual Quantity cannot be more than requested quantity";
                                    redToast = true;
                                } else {
                                    //get textbox values
                                    EditText edtAccessCode = view.findViewById(R.id.accessCode);
                                    accessCode = edtAccessCode.getText().toString();

                                    //create object to check access code
                                    final AccessCodeCheck codeCheckObj = new AccessCodeCheck(disbId, accessCode);

                                    //check access code
                                    boolean chkR = AccessCodeCheck.checkAccessCode(codeCheckObj);

                                    //check if access code is correct
                                    if (chkR) {

                                        //save current data to database if access code is correct
                                        DisbursementDetailListItems.UpdateDisbursement(toBeUpdated);
                                        status = "Acknowledgement successful!";
                                        redToast = false;
                                        actualQtyValidate = true;
                                    } else {
                                        //return error if wrong access code
                                        status = "Wrong Access Code!";
                                        redToast = true;
                                        actualQtyValidate = false;
                                    }
                                }

                                //if access code ok
                                if (actualQtyValidate == true) {
                                    if (regenReqList.size() != 0) {
                                        RegenerateRequisitionActivity.setRegenReqList(regenReqList);
                                        RegenerateRequisition r = RegenerateRequisition.GetRegenrateInfo(disbId);
                                        RegenerateRequisitionActivity.setRegenerateRequisition(r);
                                        Intent intent = new Intent(getActivity(), RegenerateRequisitionActivity.class);
                                        startActivity(intent);
                                    } else {
                                        Intent intent = new Intent(getActivity(), DisbursementActivity.class);
                                        startActivity(intent);
                                    }
                                }

                                return status;
                            }

                            @Override
                            protected void onPostExecute(String result) {

                                Log.i("Result", result);
                                //display toast message at the end of transaction
                                if(redToast)
                                {
                                    Util.redsToast(result,getActivity());
                                }
                                else
                                {
                                    Util.greenToast(result,getActivity());
                                }

                                progress.dismiss();

                            }
                        }.execute();
                    }
                });
            } catch (Exception e) {
                Log.e("Error", e.getMessage());
            }
        }
    }

    //to get disbursement object from other class
    public void setDisbursementListItems(HashMap<String, String> d) {
        this.d = d;
    }
}
