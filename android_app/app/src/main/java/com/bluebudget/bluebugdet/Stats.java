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
import android.view.View;
import android.widget.AdapterView;
import android.widget.ListView;

import com.github.mikephil.charting.charts.PieChart;
import com.github.mikephil.charting.components.Description;
import com.github.mikephil.charting.data.PieData;
import com.github.mikephil.charting.data.PieDataSet;
import com.github.mikephil.charting.data.PieEntry;
import com.github.mikephil.charting.utils.ColorTemplate;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.Set;

public class Stats extends AppCompatActivity {

    private static final String TAG = "Stats";
    private Toolbar toolbar;
    private PieChart pie;
    private ListView statsLV;
    private ArrayList<BudgetProgression> budgetProgressionList;
    private BottomNavigationView navigation;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_stats);

        toolbar = findViewById(R.id.bluebudgetToolbar);
        setSupportActionBar(toolbar); //to personalize

        pie = findViewById(R.id.piechart);

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

    public void initPieChart(Map<String, Float> spents){

        List<PieEntry> pieEntries = new ArrayList<>();

        for(String cat : spents.keySet()){
            pieEntries.add(new PieEntry(spents.get(cat), cat));
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


    private void initStatsCategoriesListView(){
        Log.d(TAG, "budget progression initiated");
        statsLV = findViewById(R.id.statsListView);
        statsLV.setOnItemClickListener(statsLVListener);

        Intent incomingIntent = getIntent();
        String fromBudget = incomingIntent.getStringExtra("fromBudget");


        budgetProgressionList = new ArrayList<>();


        Log.i(TAG, (fromBudget==null)+"");
        //from budget
        if(fromBudget!=null){
            //show cat and subcat
            String parentCatName = incomingIntent.getStringExtra("parentCat");
            AppCategory parent = Home.app.getCategory(parentCatName);

            List<AppCategory> subCatList = Home.app.filterCategories(parentCatName,AppBudgetType.EXPENSE);//Home.app.allCatTypeOrdered(AppBudgetType.EXPENSE);

            List<AppCategory> all = new ArrayList<>();
            all.add(parent);
            all.addAll(subCatList);
            getCategoriesToBudgetProgressionListList(all);
        }
        //from navbar
        else{
            //show parent categories
            List<AppCategory> budgetTypeExpenses = Home.app.filterCategories(null,AppBudgetType.EXPENSE);//Home.app.allCatTypeOrdered(AppBudgetType.EXPENSE);
            getCategoriesToBudgetProgressionListList(budgetTypeExpenses);
        }

        BudgetStatsListAdapter adapter = new BudgetStatsListAdapter(this, R.layout.layout_budget_stats, budgetProgressionList);
        statsLV.setAdapter(adapter);
    }


    AdapterView.OnItemClickListener statsLVListener = new AdapterView.OnItemClickListener() {
        @Override
        public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
            Intent stats = new Intent(Stats.this, Stats.class);
            stats.putExtra("fromBudget", "true");
            stats.putExtra("parentCat", budgetProgressionList.get(position).getDescription());

            startActivity(stats);
        }
    };

    public void getCategoriesToBudgetProgressionListList(List<AppCategory> categoriesList){
        Map<String, Float> spents = new HashMap<>();
        boolean hasData = false;
        for(AppCategory c : categoriesList){
            BudgetProgression bp = getBudgetProgression(c);
            budgetProgressionList.add(bp);

            String catName = c.getName();
            float spentAmount = (float)getSpentAmount(catName);
            Log.i(TAG, spentAmount+"!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            if(spentAmount!= (float)0.0){
                spents.put(catName, spentAmount);
                hasData=true;
            }
        }

        if(hasData) {
            initPieChart(spents);
        }
    }

    public BudgetProgression getBudgetProgression(AppCategory c){
        String name = c.getName();
        Log.i(TAG, "category name " + name);

        List<String> catList = new ArrayList<>();
        catList.add(name);
        List<AppTransaction> totalExpenses = Home.app.getTransactions(null, null, catList, null, null , AppTransactionType.EXPENSE);

        int icon = c.getIcon();
        String description = name;
        double spentAmount = getSpentAmount(name);
        double budgetAmount = c.getDefBudget();
        double leftAmount  = budgetAmount - spentAmount;
        int progressBar = (int)((spentAmount*100)/budgetAmount);

        return new BudgetProgression(icon, description, spentAmount+"", leftAmount+"", progressBar);
    }

    public double getSpentAmount(String catName){
        List<String> catList = new ArrayList<>();
        catList.add(catName);
        List<AppTransaction> totalExpenses = Home.app.getTransactions(null, null, catList, null, null , AppTransactionType.EXPENSE);
        float spentAmount = (float) Home.app.calculateBalance(totalExpenses);
        spentAmount = spentAmount==-0? 0: -spentAmount;
        return spentAmount;
    }

}
