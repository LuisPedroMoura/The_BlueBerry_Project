package com.bluebudget.bluebugdet;

import android.content.Intent;
import android.nfc.Tag;
import android.os.Bundle;
import android.support.annotation.NonNull;
import android.support.design.widget.BottomNavigationView;
import android.support.design.widget.FloatingActionButton;
import android.support.v7.app.AppCompatActivity;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.TextView;

import java.util.Calendar;
import java.util.Date;
import java.util.GregorianCalendar;
import java.util.List;

public class Home extends AppCompatActivity {

    public static App app = new App();
    private FloatingActionButton expenseFab;

    private static final String TAG = "Home";

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_home);

        //get the icon selected and go to the respective activity
        BottomNavigationView navigation = findViewById(R.id.navigation);
        navigation.setOnNavigationItemSelectedListener(mOnNavigationItemSelectedListener);

        //highlight the selected icon
        Menu menu = navigation.getMenu();
        MenuItem menuItem = menu.getItem(0);
        menuItem.setChecked(true);

        initFabMenu();


    }

    private BottomNavigationView.OnNavigationItemSelectedListener mOnNavigationItemSelectedListener
            = new BottomNavigationView.OnNavigationItemSelectedListener() {

        @Override
        public boolean onNavigationItemSelected(@NonNull MenuItem item) {

            switch (item.getItemId()) {
                case R.id.navigation_home:
                    Log.i(TAG, "home clicked");
                    Intent newTransactions = new Intent(Home.this, WalkthroughIncome.class);
                    startActivity(newTransactions);
                    return true;
                case R.id.navigation_transactions:
                    Log.i(TAG, "transactions clicked");
                    Intent transactions = new Intent(Home.this, Transactions.class);
                    startActivity(transactions);
                    return true;
                case R.id.navigation_budget:
                    Log.d(TAG, "budget clicked");
                    Intent budget = new Intent(Home.this, Budget.class);
                    startActivity(budget);
                    return true;
                case R.id.navigation_stats:
                    Log.i(TAG, "stats clicked");
                    Intent stats = new Intent(Home.this, Stats.class);
                    startActivity(stats);
                    return true;
            }
            return false;
        }
    };

    private void initFabMenu() {
        expenseFab = findViewById(R.id.expenseHomeFab);

        expenseFab.setOnClickListener(expenseFabOnClick);
    }

    View.OnClickListener expenseFabOnClick = new View.OnClickListener() {
        public void onClick(View view) {
            Log.i(TAG, "onClick: expense fab");

            Intent newExpense = new Intent(Home.this, NewExpense.class);
            startActivity(newExpense);

        }
    };

}
