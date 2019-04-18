package com.bluebudget.bluebugdet;

import android.content.Intent;
import android.os.Bundle;
import android.support.design.widget.FloatingActionButton;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.Toolbar;
import android.util.Log;
import android.view.View;
import android.widget.AdapterView;
import android.widget.Spinner;
import android.widget.TextView;

import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.List;

public class NewTransfer extends AppCompatActivity {

    private static final String TAG = "NewTransaction";

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_new_transfer);

        Toolbar toolbar = findViewById(R.id.toolbar);
        setSupportActionBar(toolbar);


        FloatingActionButton fab = findViewById(R.id.newTransferfab);
        fab.setOnClickListener(checkFabOnClick);

        //Category Spinner
        Spinner categorySpinner = findViewById(R.id.categoryNewTransferSpinner);

        ArrayList<SpinnerItem> categoryItemList = initCategoryList();
        SpinnerAdapter categoryAdapter = new SpinnerAdapter(this, categoryItemList);
        categorySpinner.setAdapter(categoryAdapter);
        categorySpinner.setOnItemSelectedListener(av);

        //(from) wallet Spinner
        Spinner walletSpinner = findViewById(R.id.walletNewTransferSpinner);

        ArrayList<SpinnerItem> walletItemList = initWalletList();
        SpinnerAdapter walletAdapter = new SpinnerAdapter(this, walletItemList);
        walletSpinner.setAdapter(walletAdapter);
        walletSpinner.setOnItemSelectedListener(av);

        //recipient wallet Spinner
        Spinner recipientWalletSpinner = findViewById(R.id.recipientWalletNewTransferSpinner);
        ArrayList<SpinnerItem> recipientWalletItemList = initWalletList();
        SpinnerAdapter recipientWalletAdapter = new SpinnerAdapter(this, recipientWalletItemList);
        recipientWalletSpinner.setAdapter(recipientWalletAdapter);
        recipientWalletSpinner.setOnItemSelectedListener(av);


        //Date
        TextView dateTV = findViewById(R.id.dateNewTransferTV);
        dateTV.setOnClickListener(dateListener);
        Intent incomingIntent = getIntent();
        String dateContent = incomingIntent.getStringExtra("date");

        if(dateContent==null){
            Log.i(TAG, "dateContent == null");
            SimpleDateFormat sdf = new SimpleDateFormat("dd-MM-yyyy");
            dateTV.setText(sdf.format(Calendar.getInstance().getTime()));
        }
        else{
            Log.i(TAG, "dateContent != null");
            dateTV.setText(dateContent);
        }

    }

    //////////////////////
    ///Category Spinner///
    //////////////////////
    private ArrayList<SpinnerItem> initCategoryList(){

        List<AppCategory> categoriesList = Home.app.getCategoriesList();
        ArrayList<SpinnerItem> categoryItemList = new ArrayList<>();

        for(AppCategory category : categoriesList){
            categoryItemList.add(new SpinnerItem(category.getName(), category.getIcon()));
        }

        return categoryItemList;
    }

    //////////////////////
    ////Wallet Spinner////
    //////////////////////
    private ArrayList<SpinnerItem> initWalletList(){
        List<AppWallet> walletsList = Home.app.getWalletsList();
        ArrayList<SpinnerItem> walletItemList = new ArrayList<>();

        for(AppWallet wallet : walletsList){
            walletItemList.add(new SpinnerItem(wallet.getName(), wallet.getIcon()));
        }

        return walletItemList;
    }

    AdapterView.OnItemSelectedListener av = new AdapterView.OnItemSelectedListener() {
        @Override
        public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
            SpinnerItem clickedItem = (SpinnerItem) parent.getItemAtPosition(position);
            String clickedName = clickedItem.getName();

            Log.i(TAG, "category " + clickedName+ " selected");
        }

        @Override
        public void onNothingSelected(AdapterView<?> parent) {

        }
    };


    //////////////////////
    /////////Date/////////
    //////////////////////
    View.OnClickListener dateListener = new View.OnClickListener() {
        @Override
        public void onClick(View v) {
            Intent intent = new Intent(NewTransfer.this, CalendarPopUp.class);
            intent.putExtra("className", "NewTransfer");
            startActivity(intent);
        }
    };



    View.OnClickListener checkFabOnClick = new View.OnClickListener() {
        @Override
        public void onClick(View view) {
            Log.d(TAG, "check clicked");
            Intent transactions = new Intent(NewTransfer.this, Transactions.class);
            startActivity(transactions);
        }
    };
}
