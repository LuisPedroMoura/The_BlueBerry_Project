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
import android.widget.ListView;

import java.util.ArrayList;
import java.util.List;
import java.util.Map;

public class Budget extends AppCompatActivity {

    private Toolbar toolbar;
    private ListView budgetProgressionLV;
    private FloatingActionButton addFab;


    private static final String TAG = "Budget";

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_budget);

        toolbar = findViewById(R.id.budgetToolbar);
        setSupportActionBar(toolbar);

        initFabMenu();

        initBudgetProgressionListView();


        //get the icon selected and go to the respective activity
        BottomNavigationView navigation = findViewById(R.id.navigation);
        navigation.setOnNavigationItemSelectedListener(mOnNavigationItemSelectedListener);

        //highlight the selected icon
        Menu menu = navigation.getMenu();
        MenuItem menuItem = menu.getItem(2);
        menuItem.setChecked(true);



    }


    private BottomNavigationView.OnNavigationItemSelectedListener mOnNavigationItemSelectedListener
            = new BottomNavigationView.OnNavigationItemSelectedListener() {

        @Override
        public boolean onNavigationItemSelected(@NonNull MenuItem item) {
            switch (item.getItemId()) {
                case R.id.navigation_home:
                    Log.d("BUDGET", "home clicked");
                    Intent home = new Intent(Budget.this, Home.class);
                    startActivity(home);
                    return true;
                case R.id.navigation_transactions:
                    Log.d("BUDGET", "transactions clicked");
                    Intent transactions = new Intent(Budget.this, Transactions.class);
                    startActivity(transactions);
                    return true;
                case R.id.navigation_budget:
                    Log.d("BUDGET", "budget clicked");
                    return true;
                case R.id.navigation_stats:
                    Log.d("BUDGET", "stats clicked");
                    Intent stats = new Intent(Budget.this, Stats.class);
                    startActivity(stats);
                    return true;
            }
            return false;
        }
    };



    private void initBudgetProgressionListView(){
        Log.d(TAG, "budget progression initiated");

        budgetProgressionLV = findViewById(R.id.budgetProgressionListView);

        ArrayList<BudgetProgression> budgetProgressionList = new ArrayList<>();

        List<AppCategory> budgetTypeExpenses = Home.app.allCatTypeOrdered(AppBudgetType.EXPENSE);

        for(AppCategory c : budgetTypeExpenses){

            String catName = c.getName();
            Log.i(TAG, "category name " + catName);

            List<String> catList = new ArrayList<>();
            catList.add(catName);

            List<AppTransaction> allTransactions = Home.app.getTransactions(null, null, catList, null, null , null);

            for(AppTransaction ap : allTransactions ){
                Log.i(TAG, "AppTransaction -> category " + (ap.getCategory()!=null));
                if(ap.getCategory()!=null){
                    Log.i(TAG, "AppTransaction -> category name " + ap.getCategory().getName());
                }
                Log.i(TAG, "AppTransaction -> type " + ap.getType().name());

            }


            int categoryIcon = Home.app.getCategory(catName).getIcon();
            String description = catName;
            Double spentAmount = Home.app.calculateBalance(allTransactions);
            Double leftAmount  = Home.app.calculateBalance(allTransactions);
            int progressBar = (int)((spentAmount*100)/(spentAmount+leftAmount));

            BudgetProgression bp = new BudgetProgression(categoryIcon, description, spentAmount+"", leftAmount+"", progressBar);
            budgetProgressionList.add(bp);
        }

        BudgetProgressionListAdapter adapter = new BudgetProgressionListAdapter(this, R.layout.layout_budget_progression, budgetProgressionList);
        budgetProgressionLV.setAdapter(adapter);

        Log.d(TAG, "budget progression done");
    }


    private void initFabMenu() {
        addFab = findViewById(R.id.addBudgetFab);

        addFab.setOnClickListener(addFabOnClick);
    }

    View.OnClickListener addFabOnClick = new View.OnClickListener() {
        public void onClick(View view) {
            Log.i(TAG, "onClick: add fab");

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
