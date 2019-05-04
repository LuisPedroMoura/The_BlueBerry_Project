package com.bluebudget.bluebugdet;

import android.content.Intent;
import android.support.annotation.NonNull;
import android.support.design.widget.BottomNavigationView;
import android.support.design.widget.FloatingActionButton;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.support.v7.widget.Toolbar;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.animation.OvershootInterpolator;
import android.widget.ListView;


import java.text.Format;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.GregorianCalendar;
import java.util.List;

public class Transactions extends AppCompatActivity {

    private FloatingActionButton addFab, incomeFab, expenseFab, transferFab;
    private Float translationY = 100f;
    private OvershootInterpolator interpolator = new OvershootInterpolator();
    private Boolean isMenuOpen = false;

    private Toolbar toolbar;
    private BottomNavigationView navigation;
    private ListView transactionsHistoryLV;

    private static final String TAG = "Transactions";


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_transactions);

        toolbar = findViewById(R.id.transactionToolbar);
        setSupportActionBar(toolbar);

        //get the icon selected and go to the respective activity
        navigation = findViewById(R.id.navigation);
        navigation.setOnNavigationItemSelectedListener(mOnNavigationItemSelectedListener);

        //highlight the selected icon
        Menu menu = navigation.getMenu();
        MenuItem menuItem = menu.getItem(1);
        menuItem.setChecked(true);

        Calendar calendar = new GregorianCalendar(1998,9,21);
        Home.app.addExpense(10.0, calendar, Home.app.getCategory("Income"), "", "", "Current");
        Home.app.addExpense(10.0, calendar, Home.app.getCategory("Income"), "", "", "Current");
        Home.app.addExpense(10.0, calendar, Home.app.getCategory("Income"), "", "", "Current");
        Home.app.addExpense(10.0, calendar, Home.app.getCategory("Income"), "", "", "Current");
        Home.app.addExpense(10.0, calendar, Home.app.getCategory("Income"), "", "", "Current");
        Home.app.addExpense(10.0, calendar, Home.app.getCategory("Income"), "", "", "Current");
        Home.app.addExpense(10.0, calendar, Home.app.getCategory("Income"), "", "", "Current");
        Home.app.addExpense(10.0, calendar, Home.app.getCategory("Income"), "", "", "Current");
        Home.app.addExpense(10.0, calendar, Home.app.getCategory("Income"), "", "", "Current");
        Home.app.addExpense(10.0, calendar, Home.app.getCategory("Income"), "", "", "Current");



        getNewTransactionInfo();

        initHistoryListView();

        initFabMenu();

    }


    private BottomNavigationView.OnNavigationItemSelectedListener mOnNavigationItemSelectedListener
            = new BottomNavigationView.OnNavigationItemSelectedListener() {

        @Override
        public boolean onNavigationItemSelected(@NonNull MenuItem item) {
            switch (item.getItemId()) {
                case R.id.navigation_home:
                    Log.d(TAG, "home clicked");
                    Intent home = new Intent(Transactions.this, Home.class);
                    startActivity(home);
                    return true;
                case R.id.navigation_transactions:
                    Log.d(TAG, "transactions clicked");
                    return true;
                case R.id.navigation_budget:
                    Log.d(TAG, "budget clicked");
                    Intent budget = new Intent(Transactions.this, Budget.class);
                    startActivity(budget);
                    return true;
                case R.id.navigation_stats:
                    Log.d(TAG, "stats clicked");
                    Intent stats = new Intent(Transactions.this, Stats.class);
                    startActivity(stats);
                    return true;
            }
            return false;
        }
    };


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


    private void initFabMenu() {
        addFab = findViewById(R.id.addFab);
        incomeFab = findViewById(R.id.incomeFab);
        expenseFab = findViewById(R.id.expenseFab);
        transferFab = findViewById(R.id.transferFab);

        //set transparency and visibility
        hideFabAndLabel(incomeFab, findViewById(R.id.incomeTextView));
        hideFabAndLabel(expenseFab, findViewById(R.id.expenseTextView));
        hideFabAndLabel(transferFab, findViewById(R.id.transferTextView));

        incomeFab.setTranslationY(translationY);
        expenseFab.setTranslationY(translationY);
        transferFab.setTranslationY(translationY);

        addFab.setOnClickListener(addFabOnClick);
        incomeFab.setOnClickListener(incomeFabOnClick);
        expenseFab.setOnClickListener(expenseFabOnClick);
        transferFab.setOnClickListener(transferFabOnClick);
    }

    private void openMenu() {
        isMenuOpen = !isMenuOpen;
        //show sub fabs
        showFabAndLabel(incomeFab, findViewById(R.id.incomeTextView));
        showFabAndLabel(expenseFab, findViewById(R.id.expenseTextView));
        showFabAndLabel(transferFab, findViewById(R.id.transferTextView));

    }

    private void closeMenu() {
        isMenuOpen = !isMenuOpen;

        //hide sub fabs
        hideFabAndLabel(incomeFab, findViewById(R.id.incomeTextView));
        hideFabAndLabel(expenseFab, findViewById(R.id.expenseTextView));
        hideFabAndLabel(transferFab, findViewById(R.id.transferTextView));
    }


    private void showFabAndLabel(FloatingActionButton fab, View txtView){
        fab.show();
        fab.animate().translationY(0f).alpha(1f).setInterpolator(interpolator).setDuration(300).start();
        txtView.setVisibility(View.VISIBLE);
    }

    private void hideFabAndLabel(FloatingActionButton fab, View txtView){
        fab.animate().translationY(translationY).alpha(0f).setInterpolator(interpolator).setDuration(300).start();
        fab.hide();
        txtView.setVisibility(View.INVISIBLE);
    }

    View.OnClickListener addFabOnClick = new View.OnClickListener() {
        public void onClick(View view) {
            Log.i(TAG, "onClick: add fab");
            if (isMenuOpen) {
                closeMenu();
            } else {
                openMenu();
            }
        }
    };


    View.OnClickListener incomeFabOnClick = new View.OnClickListener() {
        public void onClick(View view) {
            Log.i(TAG, "onClick: income fab");
            if (isMenuOpen) {
                Intent newIncome = new Intent(Transactions.this, NewIncome.class);
                startActivity(newIncome);
            }
        }
    };

    View.OnClickListener expenseFabOnClick = new View.OnClickListener() {
        public void onClick(View view) {
            Log.i(TAG, "onClick: expense fab");
            if (isMenuOpen) {
                Intent newExpense = new Intent(Transactions.this, NewExpense.class);
                startActivity(newExpense);
            }
        }
    };

    View.OnClickListener transferFabOnClick = new View.OnClickListener() {
        public void onClick(View view) {
            Log.i(TAG, "onClick: transfer fab");
            if (isMenuOpen) {
                Intent newTransfer = new Intent(Transactions.this, NewTransfer.class);
                startActivity(newTransfer);
            }
        }
    };


    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        getMenuInflater().inflate(R.menu.menu_filter_toolbar, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        Log.i(TAG, item.toString()+" selected");
        return super.onOptionsItemSelected(item);
    }
}
