package com.bluebudget.bluebugdet;

import android.content.Intent;
import android.os.Bundle;
import android.support.design.widget.FloatingActionButton;
import android.support.design.widget.Snackbar;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.Toolbar;
import android.util.Log;
import android.view.View;
import android.widget.ListView;

import java.text.Format;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.GregorianCalendar;
import java.util.List;

public class testScrollingActivity extends AppCompatActivity {

    private ListView transactionsHistoryLV;
    private static final String TAG = "testScrollingActivity";

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_test_scrolling);
        Toolbar toolbar = findViewById(R.id.toolbar);
        setSupportActionBar(toolbar);


        getNewTransactionInfo();

        initHistoryListView();
    }


    private void getNewTransactionInfo(){
        //Log.i(TAG, "!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        Log.i(TAG, "getNewTransactionInfo");

        Intent incomingIntent = getIntent();

        String transactionType = incomingIntent.getStringExtra("transactionType");

        if (transactionType != null) {

            Double amount = incomingIntent.getDoubleExtra("amount", 0);
            String date = incomingIntent.getStringExtra("date");
            String [] a = date.split("-");
            Calendar calendar = new GregorianCalendar(Integer.parseInt(a[2]),Integer.parseInt(a[1]),Integer.parseInt(a[0]));
            String category = incomingIntent.getStringExtra("category");
            String location = incomingIntent.getStringExtra("location");
            String notes = incomingIntent.getStringExtra("notes");
            String wallet = incomingIntent.getStringExtra("wallet");
            String recipientWallet = incomingIntent.getStringExtra("recipientWallet");

            Log.i(TAG, transactionType + " " + amount + " " + date + " " + category + " " + notes + " " + location + " " + wallet + " " + recipientWallet);

            //add transactions
            if(amount>0) {
                if(transactionType.equals("NewExpense")){
                    Home.app.addExpense(amount, calendar, Home.app.getCategory(category), notes, location, wallet);
                }
                else if(transactionType.equals("NewIncome")){
                    Home.app.addIncome(amount, calendar, Home.app.getCategory(category), notes, location, wallet);
                }
                else if(transactionType.equals("NewTransfer")){
                    Home.app.addTransfer(amount, calendar, notes, location, wallet, recipientWallet);
                    Log.i(TAG, "added new transfer");
                    List<AppTransaction> allTransactions = Home.app.getTransactions(null, null, null, null, null , null);
                    Log.i(TAG, allTransactions.size()+"");
                }
            }
        }
    }

    private void initHistoryListView(){
        Log.d(TAG, "history initiated");

        transactionsHistoryLV = findViewById(R.id.transactionsHistoryListView);

        ArrayList<TransactionsHistory> transactionsHistoryList = new ArrayList<>();
        List<AppTransaction> allTransactions = Home.app.getTransactions(null, null, null, null, null , null);

        for(AppTransaction t : allTransactions){

            Format formatter = new SimpleDateFormat("dd-MM-yyyy");
            String date = formatter.format(t.getDate().getTime());

            //Log.i(TAG, t.getValue()+"");
            //Log.i(TAG, t.getType()+"");


            String description;
            int icon;
            if(t.getType() == AppTransactionType.TRANSFER){
                description = "from " + t.getWallet() + "\nto " + t.getRecipientWallet();
                icon = R.drawable.ic_compare_arrows_black_24dp;
            }else{
                description = t.getCategory().getName();
                icon = Home.app.getCategory(description).getIcon();
            }


            TransactionsHistory th = new TransactionsHistory(date, icon, description, Double.toString(t.getValue()));
            transactionsHistoryList.add(th);
        }

        TransactionsHistoryListAdapter adapter = new TransactionsHistoryListAdapter(this, R.layout.layout_transactions_history, transactionsHistoryList);
        transactionsHistoryLV.setAdapter(adapter);

        Log.d(TAG, "history done");
    }
}
