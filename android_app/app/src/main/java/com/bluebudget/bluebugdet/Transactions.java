package com.bluebudget.bluebugdet;

import android.content.Intent;
import android.support.annotation.NonNull;
import android.support.design.widget.BottomNavigationView;
import android.support.design.widget.CollapsingToolbarLayout;
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

import java.text.DateFormatSymbols;
import java.text.Format;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;
import java.util.List;

public class Transactions extends AppCompatActivity {

    FloatingActionButton addFab, incomeFab, expenseFab, transferFab;
    Float translationY = 100f;
    OvershootInterpolator interpolator = new OvershootInterpolator();
    Boolean isMenuOpen = false;

    ListView transactionsHistoryLV;

    private static final String TAG = "TRANSACTIONS";



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

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_transactions);

        //get the icon selected and go to the respective activity
        BottomNavigationView navigation = findViewById(R.id.navigation);
        navigation.setOnNavigationItemSelectedListener(mOnNavigationItemSelectedListener);

        //highlight the selected icon
        Menu menu = navigation.getMenu();
        MenuItem menuItem = menu.getItem(1);
        menuItem.setChecked(true);

        //add transactions
        //Home.app.addExpense(20.0, new Date(1990, 9, 21), "Home", "nota", "aveiro", "Current");

        initHistoryListView();

        initFabMenu();

    }

    private void initHistoryListView(){
        Log.d(TAG, "history initiated");


        transactionsHistoryLV = findViewById(R.id.transactionsHistoryListView);

        ArrayList<TransactionsHistory> transactionsHistoryList = new ArrayList<>();
        /*List<String> wallets = new ArrayList<>();
        wallets.add("Current");
        List<AppTransaction> allTransactions = Home.app.getTransactions(null, null, null, null, wallets , null);

        for(AppTransaction t : allTransactions){

            Format formatter = new SimpleDateFormat("dd-MM-yyyy");
            String date = formatter.format(t.getDate());

            int icon = R.drawable.ic_trending_down_black_24dp;
            if(t.getType() == AppTransactionType.TRANSFER){
                icon = R.drawable.ic_compare_arrows_black_24dp;
            }
            else if (t.getType() == AppTransactionType.INCOME){
                icon = R.drawable.ic_trending_up_black_24dp;
            }

            TransactionsHistory th = new TransactionsHistory(date, icon, t.getCategory(), Double.toString(t.getValue()));
            transactionsHistoryList.add(th);

            Log.d(TAG, "entered for");
        }

        TransactionsHistoryListAdapter adapter = new TransactionsHistoryListAdapter(this, R.layout.transactions_history_layout, transactionsHistoryList);
        transactionsHistoryLV.setAdapter(adapter);
*/

        TransactionsHistory th = new TransactionsHistory("21-9-1998", R.drawable.ic_compare_arrows_black_24dp, "Home", "20.0"+"€");
        transactionsHistoryList.add(th);
        TransactionsHistoryListAdapter adapter = new TransactionsHistoryListAdapter(this, R.layout.transactions_history_layout, transactionsHistoryList);
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
}
