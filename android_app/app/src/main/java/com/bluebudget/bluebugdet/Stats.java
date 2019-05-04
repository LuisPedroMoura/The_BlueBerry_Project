package com.bluebudget.bluebugdet;

import android.content.Intent;
import android.support.annotation.NonNull;
import android.support.design.widget.BottomNavigationView;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.support.v7.widget.Toolbar;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.widget.ListView;

import com.github.mikephil.charting.charts.PieChart;
import com.github.mikephil.charting.components.Description;
import com.github.mikephil.charting.data.PieData;
import com.github.mikephil.charting.data.PieDataSet;
import com.github.mikephil.charting.data.PieEntry;
import com.github.mikephil.charting.utils.ColorTemplate;

import java.util.ArrayList;
import java.util.List;

public class Stats extends AppCompatActivity {

    private static final String TAG = "Stats";
    private Toolbar toolbar;
    private PieChart pie;
    private ListView statsLV;
    private BottomNavigationView navigation;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_stats);

        toolbar = findViewById(R.id.bluebudgetToolbar);
        setSupportActionBar(toolbar); //to personalize

        pie = findViewById(R.id.piechart);
        initPieChart();

        initStatsCategoriesListView();

        //get the icon selected and go to the respective activity
        navigation = findViewById(R.id.navigation);
        navigation.setOnNavigationItemSelectedListener(mOnNavigationItemSelectedListener);

        //highlight the selected icon
        Menu menu = navigation.getMenu();
        MenuItem menuItem = menu.getItem(3);
        menuItem.setChecked(true);




    }

    private BottomNavigationView.OnNavigationItemSelectedListener mOnNavigationItemSelectedListener
            = new BottomNavigationView.OnNavigationItemSelectedListener() {

        @Override
        public boolean onNavigationItemSelected(@NonNull MenuItem item) {
            switch (item.getItemId()) {
                case R.id.navigation_home:
                    Log.d("STATS", "home clicked");
                    Intent home = new Intent(Stats.this, Home.class);
                    startActivity(home);
                    return true;
                case R.id.navigation_transactions:
                    Log.d("STATS", "transactions clicked");
                    Intent transactions = new Intent(Stats.this, Transactions.class);
                    startActivity(transactions);
                    return true;
                case R.id.navigation_budget:
                    Log.d("STATS", "budget clicked");
                    Intent budget = new Intent(Stats.this, Budget.class);
                    startActivity(budget);
                    return true;
                case R.id.navigation_stats:
                    Log.d("STATS", "stats clicked");
                    Intent stats = new Intent(Stats.this, Stats.class);
                    startActivity(stats);
                    return true;
            }
            return false;
        }
    };

    //personalize toolbar
    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        getMenuInflater().inflate(R.menu.menu_filter_toolbar, menu);
        return true;
    }

    //select toolbar item
    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        Log.i(TAG, item.toString()+" selected");
        return super.onOptionsItemSelected(item);
    }

    public void initPieChart(){
        List<PieEntry> pieEntries = new ArrayList<>();

        pieEntries.add(new PieEntry(1000));
        pieEntries.add(new PieEntry(500, Integer.toString(2000)));
        pieEntries.add(new PieEntry(6000, Integer.toString(1000)));

        pie.animateX(1000);
        pie.animateY(1000);

        PieDataSet pieDataSet = new PieDataSet(pieEntries, "cenas");
        pieDataSet.setColors(ColorTemplate.COLORFUL_COLORS);

        PieData pieData = new PieData(pieDataSet);
        pie.setData(pieData);


        Description description = new Description();
        description.setText("description");
        pie.setDescription(description);
        pie.invalidate();

    }


    private void initStatsCategoriesListView(){
        Log.d(TAG, "budget progression initiated");
        statsLV = findViewById(R.id.statsListView);

        Intent incomingIntent = getIntent();
        String fromBudget = incomingIntent.getStringExtra("fromBudget");


        ArrayList<BudgetProgression> budgetProgressionList = new ArrayList<>();


        Log.i(TAG, (fromBudget==null)+"");
        //from budget
        if(fromBudget!=null){
            //show cat and subcat

            String parentCatName = incomingIntent.getStringExtra("parentCat");
            AppCategory parent = Home.app.getCategory(parentCatName);

            BudgetProgression bp = getBudgetProgression(parent);
            budgetProgressionList.add(bp);

            List<AppCategory> subCatList = Home.app.filterCategories(parentCatName,AppBudgetType.EXPENSE);//Home.app.allCatTypeOrdered(AppBudgetType.EXPENSE);

            for(AppCategory c : subCatList){
                BudgetProgression bP = getBudgetProgression(c);
                budgetProgressionList.add(bP);
            }
        }
        //from navbar
        else{
            //show parent categories
            List<AppCategory> budgetTypeExpenses = Home.app.filterCategories(null,AppBudgetType.EXPENSE);//Home.app.allCatTypeOrdered(AppBudgetType.EXPENSE);

            for(AppCategory c : budgetTypeExpenses){
                BudgetProgression bp = getBudgetProgression(c);
                budgetProgressionList.add(bp);
            }
        }

        BudgetStatsListAdapter adapter = new BudgetStatsListAdapter(this, R.layout.layout_budget_stats, budgetProgressionList);
        statsLV.setAdapter(adapter);
    }


    public BudgetProgression getBudgetProgression(AppCategory c){
        String name = c.getName();
        Log.i(TAG, "category name " + name);

        List<String> catList = new ArrayList<>();
        catList.add(name);
        List<AppTransaction> allTransactions = Home.app.getTransactions(null, null, catList, null, null , null);

        int icon = c.getIcon();
        String description = name;
        Double spentAmount = Home.app.calculateBalance(allTransactions);
        Double leftAmount  = Home.app.calculateBalance(allTransactions);
        int progressBar = (int)((spentAmount*100)/(spentAmount+leftAmount));

        return new BudgetProgression(icon, description, spentAmount+"", leftAmount+"", progressBar);
    }

}
