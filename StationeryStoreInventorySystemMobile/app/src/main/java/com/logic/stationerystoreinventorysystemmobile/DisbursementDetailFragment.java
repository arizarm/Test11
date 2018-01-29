package com.logic.stationerystoreinventorysystemmobile;


import android.app.Fragment;
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
                @Override
                protected List<DisbursementDetailListItems> doInBackground(Void... params) {
                    return DisbursementDetailListItems.getDisbursementDetailListItems(disbId);
                }

                @Override
                protected void onPostExecute(List<DisbursementDetailListItems> result) {

                    lv.setAdapter(sa = new SimpleAdapter(getActivity(), result,
                            R.layout.disbursement_detail_list_row,
                            new String[]{"ItemCode", "ItemDesc", "ReqQty", "ActualQty", "ActualQty", "Remarks"},
                            new int[]{R.id.itemCodeHidden, R.id.txtIDesc, R.id.txtReqQty, R.id.editTxtActQty, R.id.retrievedQtyHidden, R.id.editTxtRemark}));
                }
            }.execute();

            try {

                Button button = view.findViewById(R.id.btnAck);
                button.setOnClickListener(new View.OnClickListener() {
                    @Override
                    public void onClick(View v) {

                        new AsyncTask<Void, Void, String>() {
                            @Override
                            protected String doInBackground(Void... params) {

                                //create list for shortfall item to regenerate requisition
                                List<RequisitionListItem> regenReqList = new ArrayList<RequisitionListItem>();
                                //crate array list to hold items to be updated
                                final ArrayList<DisbursementDetailListItems> toBeUpdated = new ArrayList<DisbursementDetailListItems>();
                                //boolean to validate quantity
                                boolean actualQtyValidate = true;
                                //get data from list view
                                for (int i = 0; i < lv.getCount(); i++) {
                                    //get view from list view to access controls inside list view
                                    View vv = lv.getChildAt(i);

                                    //get textbox values
                                    EditText edtActQty = vv.findViewById(R.id.editTxtActQty);
                                    TextView txtReqQty = vv.findViewById(R.id.txtReqQty);
                                    TextView txtRetrievedQty = view.findViewById(R.id.retrievedQtyHidden);
                                    EditText edtRemark = vv.findViewById(R.id.editTxtRemark);

                                    //assign textbox values to local variable
                                    actQty = Integer.parseInt(edtActQty.getText().toString());
                                    reqQty = Integer.parseInt(txtReqQty.getText().toString());
                                    retrievedQty = Integer.parseInt(txtRetrievedQty.getText().toString());
                                    remark = edtRemark.getText().toString();

                                    //check for shortfall item
                                    if (actQty < reqQty) {

                                        Log.i("ShortfallCheck", "Shortfall item exists");

                                        TextView txtDesc = vv.findViewById(R.id.txtIDesc);
                                        TextView txtItemCode = view.findViewById(R.id.itemCodeHidden);
                                        itemDesc = txtDesc.getText().toString();
                                        itemCode = txtItemCode.getText().toString();
                                        shortfallQty = reqQty - actQty;
                                        RequisitionListItem reqItem = new RequisitionListItem(itemCode, itemDesc, String.valueOf(shortfallQty));
                                        regenReqList.add(reqItem);

                                        Log.i("ShortfallItems", itemCode + itemDesc + String.valueOf(shortfallQty) );

                                        //check for discrepancy
                                        if (actQty < retrievedQty) {
                                            discrepancyQty = actQty - retrievedQty;
                                            DiscrepancyHolder.addDiscrepancy(itemCode, discrepancyQty);

                                            Log.i("Discrepancy Item", itemCode + String.valueOf(discrepancyQty));
                                        }
                                    }
                                    else if (actQty > retrievedQty) {
                                        //display error for invalid quantity
                                        actualQtyValidate = false;
                                    }

                                    //add to disbursement object to be updated in database
                                    DisbursementDetailListItems detailListItems = new DisbursementDetailListItems(disbId, String.valueOf(actQty), remark);
                                    toBeUpdated.add(detailListItems);
                                }
                                if (actualQtyValidate == false) {
                                    status = "Actual Quantity cannot be more than requested quantity";
                                    actualQtyValidate = true;
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
                                    } else {
                                        //return error if wrong access code
                                        status = "Wrong Access Code!";
                                    }
                                }
                                return status;
                            }

                            @Override
                            protected void onPostExecute(String result) {
                                Log.i("Result", result);
                                //display toast message at the end of transaction
                                customToast(result);
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

    //custom toast message box
    public void customToast(String message) {

        Toast toast = Toast.makeText(getActivity(), message,
                Toast.LENGTH_LONG);

        TextView toastMessage = (TextView) toast.getView().findViewById(android.R.id.message);
        toastMessage.setTextColor(Color.RED);
        toast.show();
    }
}
