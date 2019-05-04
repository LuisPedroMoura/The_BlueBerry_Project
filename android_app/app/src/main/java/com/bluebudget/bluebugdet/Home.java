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

import com.github.mikephil.charting.charts.PieChart;
import com.github.mikephil.charting.components.Description;
import com.github.mikephil.charting.data.PieData;
import com.github.mikephil.charting.data.PieDataSet;
import com.github.mikephil.charting.data.PieEntry;
import com.github.mikephil.charting.utils.ColorTemplate;

import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;
import java.util.GregorianCalendar;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

public class Home extends AppCompatActivity {

    public static App app = new App();


    private TextView walletsBalanceTV;
    private TextView incomesOverviewTV;
    private TextView expensesOverviewTV;
    private TextView transfersOverviewTV;
    private PieChart pie;
    private List<AppCategory> budgetTypeExpenses;
    private FloatingActionButton expenseFab;

    private static final String TAG = "Home";

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_home);

        pie = findViewById(R.id.homePieChart);
        walletsBalanceTV = findViewById(R.id.walletsBalanceTV);
        incomesOverviewTV = findViewById(R.id.incomesOverviewTV);
        expensesOverviewTV = findViewById(R.id.expensesOverviewTV);
        transfersOverviewTV = findViewById(R.id.transfersOverviewTV);

        initOverview();
        initPieChart();

        initFabMenu();

        //get the icon selected and go to the respective activity
        BottomNavigationView navigation = findViewById(R.id.navigation);
        navigation.setOnNavigationItemSelectedListener(mOnNavigationItemSelectedListener);

        //highlight the selected icon
        Menu menu = navigation.getMenu();
        MenuItem menuItem = menu.getItem(0);
        menuItem.setChecked(true);




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


    public void initOverview(){
        updateIncomesTV();
        updateExpensesTV();
        updateTransfersTV();
    }

    public void updateIncomesTV(){
        List<AppTransaction> incomes = Home.app.getTransactions(null, null, null, null,
                null, AppTransactionType.INCOME);

        double incomesAmount = Home.app.calculateBalance(incomes);
        incomesOverviewTV.setText(Double.toString(incomesAmount)+"€");
    }

    public void updateExpensesTV(){
        List<AppTransaction> expenses = Home.app.getTransactions(null, null, null, null,
                null, AppTransactionType.EXPENSE);

        double expensesAmount = Home.app.calculateBalance(expenses);
        expensesOverviewTV.setText(Double.toString(expensesAmount)+"€");
    }

    public void updateTransfersTV(){
        List<AppTransaction> transfers = Home.app.getTransactions(null, null, null, null,
                null, AppTransactionType.TRANSFER);;

        double transfersAmount = Home.app.calculateBalance(transfers);
        transfersOverviewTV.setText(Double.toString(transfersAmount)+"€");
    }


    public void initPieChart(){
        budgetTypeExpenses = Home.app.filterCategories(null,AppBudgetType.EXPENSE);
        Map<String, Float> spents = new HashMap<>();
        boolean hasData = false;
        for(AppCategory c : budgetTypeExpenses){
            String catName = c.getName();
            float spentAmount = (float)getSpentAmount(catName);
            Log.i(TAG, spentAmount+"!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            if(spentAmount!= (float)0.0){
                spents.put(catName, spentAmount);
                hasData=true;
            }
        }

        if(hasData) {
            drawPieChart(spents);
        }
    }

    public double getSpentAmount(String catName){
        List<String> catList = new ArrayList<>();
        catList.add(catName);
        List<AppTransaction> totalExpenses = Home.app.getTransactions(null, null, catList, null, null , AppTransactionType.EXPENSE);
        float spentAmount = (float) Home.app.calculateBalance(totalExpenses);
        spentAmount = spentAmount==-0? 0: -spentAmount;
        return spentAmount;
    }

    public void drawPieChart(Map<String, Float> spending){

        List<PieEntry> pieEntries = new ArrayList<>();

        for(String cat : spending.keySet()){
            pieEntries.add(new PieEntry(spending.get(cat), cat));
        }


        pie.animateX(1000);
        pie.animateY(1000);

        PieDataSet pieDataSet = new PieDataSet(pieEntries, "");
        pieDataSet.setColors(ColorTemplate.COLORFUL_COLORS);

        PieData pieData = new PieData(pieDataSet);
        pie.setData(pieData);


        Description description = new Description();
        description.setText("");
        pie.setDescription(description);
        pie.invalidate();
    }



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
