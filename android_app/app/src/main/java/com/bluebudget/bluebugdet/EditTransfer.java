package com.bluebudget.bluebugdet;

import android.app.DatePickerDialog;
import android.content.Intent;
import android.os.Bundle;
import android.support.design.widget.FloatingActionButton;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.Toolbar;
import android.util.Log;
import android.view.View;
import android.widget.AdapterView;
import android.widget.DatePicker;
import android.widget.EditText;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.Toast;

import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.List;

public class EditTransfer extends AppCompatActivity {

    private Toolbar toolbar;

    private FloatingActionButton fab;
    private EditText amountET;
    private TextView dateTV;
    private EditText locationET;
    private EditText notesET;
    private Spinner walletSpinner;
    private Spinner recipientWalletSpinner;

    private int idIntent;
    private String catIntent;
    private double amountIntent;
    private String dateIntent;
    private String locationIntent;
    private String notesIntent;
    private String fromWalletIntent;
    private String recipientWalletIntent;

    private static final String TAG = "EditTransfer";

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_edit_transfer);

        Intent intent = getIntent();
        idIntent = intent.getIntExtra("id", -1);
        catIntent = intent.getStringExtra("cat");
        amountIntent = intent.getDoubleExtra("amount", 0);
        dateIntent = intent.getStringExtra("date");
        locationIntent = intent.getStringExtra("location");
        notesIntent = intent.getStringExtra("notes");
        fromWalletIntent = intent.getStringExtra("fromWallet");
        recipientWalletIntent = intent.getStringExtra("recipientWallet");


        toolbar = findViewById(R.id.toolbar);
        setSupportActionBar(toolbar);


        fab = findViewById(R.id.editTransferfab);
        fab.setOnClickListener(checkFabOnClick);

        amountET = findViewById(R.id.amountEditTransferET);
        amountET.setText(-amountIntent+"");

        locationET = findViewById(R.id.locationEditTransferET);
        locationET.setText(locationIntent);

        notesET = findViewById(R.id.notesEditTransferET);
        notesET.setText(notesIntent);

        //(from) wallet Spinner
        walletSpinner = findViewById(R.id.walletEditTransferSpinner);

        ArrayList<SpinnerItem> walletItemList = initWalletList(true);
        SpinnerAdapter walletAdapter = new SpinnerAdapter(this, walletItemList);
        walletSpinner.setAdapter(walletAdapter);
        walletSpinner.setOnItemSelectedListener(av);

        //recipient wallet Spinner
        recipientWalletSpinner = findViewById(R.id.recipientWalletEditTransferSpinner);
        ArrayList<SpinnerItem> recipientWalletItemList = initWalletList(false);
        SpinnerAdapter recipientWalletAdapter = new SpinnerAdapter(this, recipientWalletItemList);
        recipientWalletSpinner.setAdapter(recipientWalletAdapter);
        recipientWalletSpinner.setOnItemSelectedListener(av);


        //Date
        dateTV = findViewById(R.id.dateEditTransferTV);
        dateTV.setOnClickListener(dateListener);
        dateTV.setText(dateIntent);

    }

    //////////////////////
    ////Wallet Spinner////
    //////////////////////
    private ArrayList<SpinnerItem> initWalletList(boolean fromWallet){

        AppWallet w;
        Log.i(TAG, "fromWallet "+fromWallet+ "!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        if(fromWallet){
            w = Home.app.getWallet(fromWalletIntent);
        }
        else{
            w = Home.app.getWallet(recipientWalletIntent);
        }

        ArrayList<SpinnerItem> walletItemList = new ArrayList<>();
        walletItemList.add(new SpinnerItem(w.getName(), w.getIcon()));

        List<AppWallet> walletsList = Home.app.getWalletsList();
        boolean removed = walletsList.remove(w);
        Log.i(TAG, "w.getName() "+w.getName()+" removed "+removed+ "!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");

        for(AppWallet wallet : walletsList){
            walletItemList.add(new SpinnerItem(wallet.getName(), wallet.getIcon()));
        }
        walletItemList.add(new SpinnerItem("add new wallet", R.drawable.empty));

        return walletItemList;

    }

    AdapterView.OnItemSelectedListener av = new AdapterView.OnItemSelectedListener() {
        @Override
        public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
            SpinnerItem clickedItem = (SpinnerItem) parent.getItemAtPosition(position);
            String clickedName = clickedItem.getName();
            showSpinnerToast(view, clickedName);
            Log.i(TAG, "wallet " + clickedName+ " selected");
        }

        @Override
        public void onNothingSelected(AdapterView<?> parent) {

        }
    };

    private void showSpinnerToast(View view, String clickedName){
        switch (clickedName){
            case "add new category":
            case "add new sub-category":
            case "add new wallet":
                CharSequence text = "To be implemented";
                int duration = Toast.LENGTH_SHORT;

                Toast toast = Toast.makeText(view.getContext(), text, duration);
                toast.show();
        }
    }


    //////////////////////
    /////////Date/////////
    //////////////////////
    View.OnClickListener dateListener = new View.OnClickListener() {
        @Override
        public void onClick(View v) {
            showDatePickerDialog();
        }
    };

    public void showDatePickerDialog(){
        DatePickerDialog datePickerDialog = new DatePickerDialog(
                this,
                onDateSetListener,
                Calendar.getInstance().get(Calendar.YEAR),
                Calendar.getInstance().get(Calendar.MONTH),
                Calendar.getInstance().get(Calendar.DAY_OF_MONTH));
        datePickerDialog.show();
    }


    DatePickerDialog.OnDateSetListener onDateSetListener = new DatePickerDialog.OnDateSetListener(){
        @Override
        public void onDateSet(DatePicker view, int year, int month, int dayOfMonth) {
            String date = dayOfMonth + "-" + (month+1) + "-" + year;
            dateTV.setText(date);
        }
    };




    View.OnClickListener checkFabOnClick = new View.OnClickListener() {
        @Override
        public void onClick(View view) {
            Log.d(TAG, "check clicked");
            Intent transactions = new Intent(EditTransfer.this, Transactions.class);

            transactions.putExtra("transactionType", "EditTransfer");
            transactions.putExtra("id", idIntent);
            String amount = amountET.getText().toString();
            if(amount.equals("")){
                amount = 0.0+"";
            }

            transactions.putExtra("amount", Double.parseDouble(amount));
            transactions.putExtra("date", dateTV.getText().toString());

            transactions.putExtra("location", locationET.getText().toString());
            transactions.putExtra("notes", notesET.getText().toString());
            SpinnerItem wsi = (SpinnerItem) walletSpinner.getSelectedItem();
            transactions.putExtra("wallet", wsi.getName());
            SpinnerItem rwsi = (SpinnerItem) recipientWalletSpinner.getSelectedItem();
            transactions.putExtra("recipientWallet", rwsi.getName());

            //Log.i(TAG, "send edited- amount " + amount + "!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            Log.i(TAG, "send edited - from wallet " + wsi.getName() + "!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            Log.i(TAG, "send edited - recipient wallet " + rwsi.getName() + "!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");

            startActivity(transactions);
        }
    };

    public void editTransferDeleteBtnClicked(View view) {
        CharSequence text = "To be implemented";
        int duration = Toast.LENGTH_LONG;

        Toast toast = Toast.makeText(this, text, duration);
        toast.show();
    }
}
