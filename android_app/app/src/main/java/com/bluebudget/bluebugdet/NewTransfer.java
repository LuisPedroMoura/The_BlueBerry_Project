package com.bluebudget.bluebugdet;

import android.content.Intent;
import android.os.Bundle;
import android.support.design.widget.FloatingActionButton;
import android.support.design.widget.Snackbar;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.Toolbar;
import android.util.Log;
import android.view.View;
import android.widget.AdapterView;
import android.widget.Spinner;

import java.util.ArrayList;

public class NewTransfer extends AppCompatActivity {

    private static final String TAG = "NewTransaction";

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_new_transfer);

        Toolbar toolbar = findViewById(R.id.toolbar);
        setSupportActionBar(toolbar);


        FloatingActionButton fab = findViewById(R.id.fab);
        fab.setOnClickListener(checkFabOnClick);

        //Category Spinner
        Spinner categorySpinner = findViewById(R.id.categorySpinner);

        ArrayList<SpinnerItem> categoryItemList = initCategoryList();
        SpinnerAdapter categoryAdapter = new SpinnerAdapter(this, categoryItemList);
        categorySpinner.setAdapter(categoryAdapter);
        categorySpinner.setOnItemSelectedListener(av);

        //(from) wallet Spinner
        Spinner walletSpinner = findViewById(R.id.walletSpinner);

        ArrayList<SpinnerItem> walletItemList = initWalletList();
        SpinnerAdapter walletAdapter = new SpinnerAdapter(this, walletItemList);
        walletSpinner.setAdapter(walletAdapter);
        walletSpinner.setOnItemSelectedListener(av);

        //recipient wallet Spinner
        Spinner recipientWalletSpinner = findViewById(R.id.recipientWalletSpinner);
        ArrayList<SpinnerItem> recipientWalletItemList = initWalletList();
        SpinnerAdapter recipientWalletAdapter = new SpinnerAdapter(this, recipientWalletItemList);
        recipientWalletSpinner.setAdapter(recipientWalletAdapter);
        recipientWalletSpinner.setOnItemSelectedListener(av);

    }

    //////////////////////
    ///Category Spinner///
    //////////////////////
    private ArrayList<SpinnerItem> initCategoryList(){
        ArrayList<SpinnerItem> categoryItemList = new ArrayList<>();
        categoryItemList.add(new SpinnerItem("Food", R.drawable.ic_shopping_cart_black_24dp));
        categoryItemList.add(new SpinnerItem("Home", R.drawable.ic_home_black_24dp));
        categoryItemList.add(new SpinnerItem("Transports", R.drawable.ic_directions_car_black_24dp));

        return categoryItemList;
    }

    //////////////////////
    ////Wallet Spinner////
    //////////////////////
    private ArrayList<SpinnerItem> initWalletList(){
        ArrayList<SpinnerItem> categoryItemList = new ArrayList<>();
        categoryItemList.add(new SpinnerItem("Current", R.drawable.ic_account_balance_wallet_black_24dp));
        categoryItemList.add(new SpinnerItem("Savings", R.drawable.ic_account_balance_wallet_black_24dp));
        categoryItemList.add(new SpinnerItem("Future expenses", R.drawable.ic_account_balance_wallet_black_24dp));

        return categoryItemList;
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



    View.OnClickListener checkFabOnClick = new View.OnClickListener() {
        @Override
        public void onClick(View view) {
            Log.d(TAG, "check clicked");
            Intent transactions = new Intent(NewTransfer.this, Transactions.class);
            startActivity(transactions);
        }
    };
}
