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
import android.widget.TextView;


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

    private TextView currentBalanceTV;
    private TextView monthlyBalanceTV;

    private Toolbar toolbar;
    private BottomNavigationView navigation;
    private ListView transactionsHistoryLV;
    private ArrayList<TransactionsHistory> transactionsHistoryList;

    private static final String TAG = "Transactions";


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_transactions);

        toolbar = findViewById(R.id.transactionToolbar);
        setSupportActionBar(toolbar);

        currentBalanceTV = findViewById(R.id.currentBalanceTransactions);
        monthlyBalanceTV = findViewById(R.id.monthlyBalanceTransactions);

        //get the icon selected and go to the respective activity
        navigation = findViewById(R.id.navigation);
        navigation.setOnNavigationItemSelectedListener(mOnNavigationItemSelectedListener);

        //highlight the selected icon
        Menu menu = navigation.getMenu();
        MenuItem menuItem = menu.getItem(1);
        menuItem.setChecked(true);

        getNewTransactionInfo();

        initHistoryListView();

        initBlances();

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

            int id = incomingIntent.getIntExtra("id", -1);
            Double amount = incomingIntent.getDoubleExtra("amount", 0);
            String date = incomingIntent.getStringExtra("date");
            String [] a = date.split("-");
            Calendar calendar = new GregorianCalendar(Integer.parseInt(a[2]),Integer.parseInt(a[1]),Integer.parseInt(a[0]));
            String category = incomingIntent.getStringExtra("category");
            String location = incomingIntent.getStringExtra("location");
            String notes = incomingIntent.getStringExtra("notes");
            String wallet = incomingIntent.getStringExtra("wallet");
            String recipientWallet = incomingIntent.getStringExtra("recipientWallet");

            //Log.i(TAG, "!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            //Log.i(TAG, transactionType + " " + amount + " " + date + " " + category + " " + notes + " " + location + " " + wallet + " " + recipientWallet);

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
                }
                else if(transactionType.equals("EditExpense")){
                    Home.app.updateExpense(id, amount, calendar, Home.app.getCategory(category), notes, location, wallet);
                }
                else if(transactionType.equals("EditIncome")){
                    Home.app.updateIncome(id, amount, calendar, Home.app.getCategory(category), notes, location, wallet);
                }
                else if(transactionType.equals("EditTransfer")){
                    Home.app.updateTransfer(id,amount, calendar, notes, location, wallet, recipientWallet);
                }
            }
        }
    }

    private void initHistoryListView(){
        Log.d(TAG, "history initiated");

        transactionsHistoryLV = findViewById(R.id.transactionsHistoryListView);

        transactionsHistoryList = new ArrayList<>();
        List<AppTransaction> allTransactions = Home.app.getTransactions(null, null, null, null, null , null);

        for(AppTransaction t : allTransactions){

            Format formatter = new SimpleDateFormat("dd-MM-yyyy");
            String date = formatter.format(t.getDate().getTime());


            String description;
            int icon;
            String recipientWallet = null;
            if(t.getType() == AppTransactionType.TRANSFER){
                description = "from " + t.getWallet() + "\nto " + t.getRecipientWallet();
                icon = R.drawable.ic_compare_arrows_black_24dp;
                recipientWallet = t.getRecipientWallet();
            }else{
                description = t.getCategory().getName();
                icon = Home.app.getCategory(description).getIcon();
            }

            int id = t.getId();
            String location = t.getLocation();
            String notes = t.getNotes();
            String fromWallet = t.getWallet();

            TransactionsHistory th = new TransactionsHistory(id, date, icon, description,
                                                                Double.toString(t.getValue()),
                                                                location, notes, fromWallet, recipientWallet);
            transactionsHistoryList.add(th);
        }
        transactionsHistoryList.add(null);


        TransactionsHistoryListAdapter adapter = new TransactionsHistoryListAdapter(this, R.layout.layout_transactions_history, transactionsHistoryList);
        transactionsHistoryLV.setAdapter(adapter);
        transactionsHistoryLV.setOnItemClickListener(transactionHistoryListener);

        Log.d(TAG, "history done");
    }


    AdapterView.OnItemClickListener transactionHistoryListener = new AdapterView.OnItemClickListener() {
        @Override
        public void onItemClick(AdapterView<?> parent, View view, int position, long id) {

            Log.i(TAG, "budgetProgressionList.get(position)==null: "+(transactionsHistoryList.get(position)==null));
            if(transactionsHistoryList.get(position)!=null){
                Log.i(TAG, "pos="+position+"; "+transactionsHistoryList.get(position).getDescription());

                TransactionsHistory transactionsHistory = transactionsHistoryList.get(position);

                Intent transactions;

                int icon = transactionsHistory.getIcon();
                int trans_id = transactionsHistory.getId();
                String cat = transactionsHistory.getDescription();
                String parentCat = cat.split(":")[0]; //parent cat
                double amount = Double.parseDouble(transactionsHistory.getAmount());
                String date = transactionsHistory.getDate();
                String location = transactionsHistory.getLocation();
                String notes = transactionsHistory.getNotes();
                String fromWallet = transactionsHistory.getFromWallet();
                String recipientWallet = transactionsHistory.getRecipientWallet();

                //is transfer
                if (icon == R.drawable.ic_compare_arrows_black_24dp){
                    transactions = new Intent(Transactions.this, EditTransfer.class);
                }
                //is income
                else if(parentCat.equals("Income")){
                    transactions = new Intent(Transactions.this, EditIncome.class);
                }
                //is expense
                else{
                    transactions = new Intent(Transactions.this, EditExpense.class);
                }

                transactions.putExtra("id", trans_id);
                transactions.putExtra("cat", cat);
                transactions.putExtra("amount", amount);
                transactions.putExtra("date", date);
                transactions.putExtra("location", location);
                transactions.putExtra("notes", notes);
                transactions.putExtra("fromWallet", fromWallet);
                transactions.putExtra("recipientWallet", recipientWallet);

                startActivity(transactions);

                Log.i(TAG, "item clicked");
            }
        }
    };





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
        int id = item.getItemId();

        if(id == R.id.filterIcon){
            Log.i(TAG, "filter clicked");
            Intent filter = new Intent(Transactions.this, Filter.class);
            filter.putExtra("className", "Transactions");
            startActivity(filter);
        }
        return super.onOptionsItemSelected(item);
    }


    public void initBlances(){

        //incomes
        List<AppTransaction> incomes = Home.app.getTransactions(null, null, null, null,
                null, AppTransactionType.INCOME);
        double incomeBalance = Home.app.calculateBalance(incomes);


        //expenses
        List<AppTransaction> expenses = Home.app.getTransactions(null, null, null, null,
                null, AppTransactionType.EXPENSE);
        double expensesBalance = Home.app.calculateBalance(expenses);


        currentBalanceTV.setText((incomeBalance+expensesBalance)+"€"); //expenses are a negative value
        monthlyBalanceTV.setText((incomeBalance+expensesBalance)+"€"); //expenses are a negative value

    }
}
