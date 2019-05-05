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
import android.widget.AdapterView;
import android.widget.ListView;

import java.util.ArrayList;
import java.util.List;

public class Budget extends AppCompatActivity
                        implements PopupDialogEdit.PopupDialogListener{

    private Toolbar toolbar;
    private ListView budgetProgressionLV;
    private  ArrayList<BudgetProgression> budgetProgressionList;

    private FloatingActionButton addFab, incomeFab, expenseFab;
    private Float translationY = 100f;
    private OvershootInterpolator interpolator = new OvershootInterpolator();
    private Boolean isMenuOpen = false;


    private static final String TAG = "Budget";

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_budget);

        toolbar = findViewById(R.id.bluebudgetToolbar);
        setSupportActionBar(toolbar);//to personalize

        initBudgetProgressionListView();

        initFabMenu();

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

    private void initBudgetProgressionListView(){
        Log.d(TAG, "budget progression initiated");

        budgetProgressionLV = findViewById(R.id.budgetProgressionListView);

        budgetProgressionList = new ArrayList<>();

        List<AppCategory> budgetTypeExpenses = Home.app.filterCategories(null, AppBudgetType.EXPENSE);//Home.app.allCatTypeOrdered(AppBudgetType.EXPENSE);

        for(AppCategory c : budgetTypeExpenses){

            String catName = c.getName();
            //Log.i(TAG, "category name " + catName);

            List<String> catList = new ArrayList<>();
            catList.add(catName);

            List<AppTransaction> totalExpenses = Home.app.getTransactions(null, null, catList, null, null , AppTransactionType.EXPENSE);
            int categoryIcon = c.getIcon();
            String description = catName;
            Double spentAmount = Home.app.calculateBalance(totalExpenses);
            spentAmount = spentAmount==-0? 0: -spentAmount;
            Double budgetAmount = c.getDefBudget();
            Double leftAmount  = budgetAmount - spentAmount;
            int progressBar = (int)((spentAmount*100)/budgetAmount);

            Log.i(TAG, description+"");
            Log.i(TAG, "spentAmount "+spentAmount);
            Log.i(TAG, "budgetAmount "+budgetAmount);
            Log.i(TAG, "leftAmount "+leftAmount);
            Log.i(TAG, "progressBar "+progressBar);

            BudgetProgression bp = new BudgetProgression(categoryIcon, description, spentAmount+"", leftAmount+"", progressBar);
            budgetProgressionList.add(bp);
        }

        BudgetProgressionListAdapter adapter = new BudgetProgressionListAdapter(this, R.layout.layout_budget_progression, budgetProgressionList);
        budgetProgressionLV.setAdapter(adapter);

        budgetProgressionLV.setOnItemClickListener(budgetProgressionListener);

        Log.d(TAG, "budget progression done");
    }

    AdapterView.OnItemClickListener budgetProgressionListener = new AdapterView.OnItemClickListener() {
        @Override
        public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
            Log.i(TAG, "pos="+position+"; "+budgetProgressionList.get(position).getDescription());

            Intent stats = new Intent(Budget.this, Stats.class);
            stats.putExtra("fromBudget", "true");
            stats.putExtra("parentCat", budgetProgressionList.get(position).getDescription());

            startActivity(stats);

        }
    };

    private void initFabMenu() {
        addFab = findViewById(R.id.addBudgetFab);
        incomeFab = findViewById(R.id.incomeBudgetFab);
        expenseFab = findViewById(R.id.expenseBudgetFab);

        //set transparency and visibility
        hideFabAndLabel(incomeFab, findViewById(R.id.incomeTextView));
        hideFabAndLabel(expenseFab, findViewById(R.id.expenseTextView));

        incomeFab.setTranslationY(translationY);
        expenseFab.setTranslationY(translationY);

        addFab.setOnClickListener(addBudgetFabOnClick);
        incomeFab.setOnClickListener(incomeBudgetFabOnClick);
        expenseFab.setOnClickListener(expenseBudgetFabOnClick);
    }

    private void openMenu() {
        isMenuOpen = !isMenuOpen;
        //show sub fabs
        showFabAndLabel(incomeFab, findViewById(R.id.incomeTextView));
        showFabAndLabel(expenseFab, findViewById(R.id.expenseTextView));
    }

    private void closeMenu() {
        isMenuOpen = !isMenuOpen;

        //hide sub fabs
        hideFabAndLabel(incomeFab, findViewById(R.id.incomeTextView));
        hideFabAndLabel(expenseFab, findViewById(R.id.expenseTextView));
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

    View.OnClickListener addBudgetFabOnClick = new View.OnClickListener() {
        public void onClick(View view) {
            Log.i(TAG, "onClick: add fab");
            if (isMenuOpen) {
                closeMenu();
            } else {
                openMenu();
            }
        }
    };


    View.OnClickListener incomeBudgetFabOnClick = new View.OnClickListener() {
        public void onClick(View view) {
            Log.i(TAG, "onClick: income fab");
            if (isMenuOpen) {
                Intent newIncome = new Intent(Budget.this, AddIncomeBudget.class);
                startActivity(newIncome);
            }
        }
    };

    View.OnClickListener expenseBudgetFabOnClick = new View.OnClickListener() {
        public void onClick(View view) {
            Log.i(TAG, "onClick: expense fab");
            if (isMenuOpen) {
                Intent newExpense = new Intent(Budget.this, AddExpenseBudget.class);
                startActivity(newExpense);
            }
        }
    };

    @Override
    public void applyTexts(String name, Double amount) {
        
    }
}
