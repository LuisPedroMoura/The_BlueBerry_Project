package com.bluebudget.bluebugdet;

import android.app.DatePickerDialog;
import android.content.Intent;
import android.os.Bundle;
import android.support.design.widget.FloatingActionButton;
import android.support.design.widget.Snackbar;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.Toolbar;
import android.util.Log;
import android.view.View;
import android.widget.AdapterView;
import android.widget.DatePicker;
import android.widget.Spinner;
import android.widget.TextView;

import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.List;

public class Filter extends AppCompatActivity {

    private Toolbar toolbar;
    private FloatingActionButton fab;
    private Spinner categorySpinner;
    private Spinner walletSpinner;
    private TextView startDateTV;
    private TextView endDateTV;
    //private View.OnClickListener fabListener;
    //private AdapterView.OnItemSelectedListener av;
    //private View.OnClickListener startDateListener ;

    private static final String TAG = "Filter";

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_filter);

        toolbar = findViewById(R.id.toolbar);
        setSupportActionBar(toolbar);

        fab = findViewById(R.id.filterFab);
        fab.setOnClickListener(fabListener);


        //Category Spinner
        categorySpinner = findViewById(R.id.categorySpinnerFilter);

        ArrayList<SpinnerItem> categoryItemList = initCategoryList();
        SpinnerAdapter categoryAdapter = new SpinnerAdapter(this, categoryItemList);
        categorySpinner.setAdapter(categoryAdapter);
        categorySpinner.setOnItemSelectedListener(av);

        //wallet Spinner
        walletSpinner = findViewById(R.id.walletSpinnerFilter);

        ArrayList<SpinnerItem> walletItemList = initWalletList();
        SpinnerAdapter walletAdapter = new SpinnerAdapter(this, walletItemList);
        walletSpinner.setAdapter(walletAdapter);
        walletSpinner.setOnItemSelectedListener(av);


        //Date
        SimpleDateFormat sdf = new SimpleDateFormat("dd-MM-yyyy");

        startDateTV = findViewById(R.id.startDateFilterTV);
        startDateTV.setOnClickListener(startDateListener);
        startDateTV.setText(sdf.format(Calendar.getInstance().getTime()));

        endDateTV = findViewById(R.id.endDateFilterTV);
        endDateTV.setOnClickListener(endDateListener);
        endDateTV.setText(sdf.format(Calendar.getInstance().getTime()));

    }

    //////////////////////
    ///Category Spinner///
    //////////////////////
    private ArrayList<SpinnerItem> initCategoryList(){

        ArrayList<SpinnerItem> categoryItemList = new ArrayList<>();

        List<AppCategory> categoriesList = Home.app.getCategoriesList();

        for(AppCategory category : categoriesList){
            categoryItemList.add(new SpinnerItem(category.getName(), category.getIcon()));
        }

        return categoryItemList;
    }

    //////////////////////
    ////Wallet Spinner////
    //////////////////////
    private ArrayList<SpinnerItem> initWalletList(){
        List<AppWallet> walletsList = Home.app.getWalletsList();
        ArrayList<SpinnerItem> walletItemList = new ArrayList<>();

        for(AppWallet wallet : walletsList){
            walletItemList.add(new SpinnerItem(wallet.getName(), wallet.getIcon()));
        }

        return walletItemList;
    }

    private AdapterView.OnItemSelectedListener av = new AdapterView.OnItemSelectedListener() {
        @Override
        public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
            SpinnerItem clickedItem = (SpinnerItem) parent.getItemAtPosition(position);
            String clickedName = clickedItem.getName();

            Log.i(TAG, "category " + clickedName+ " selected");
        }

        @Override
        public void onNothingSelected(AdapterView<?> parent) {

        }
    };

    //////////////////////
    /////////Date/////////
    //////////////////////
    private View.OnClickListener startDateListener = new View.OnClickListener() {
        @Override
        public void onClick(View v) {
            showStartDatePickerDialog();
        }
    };

    public void showStartDatePickerDialog(){
        DatePickerDialog datePickerDialog = new DatePickerDialog(
                this,
                onStartDateSetListener,
                Calendar.getInstance().get(Calendar.YEAR),
                Calendar.getInstance().get(Calendar.MONTH),
                Calendar.getInstance().get(Calendar.DAY_OF_MONTH));
        datePickerDialog.show();
    }


    DatePickerDialog.OnDateSetListener onStartDateSetListener = new DatePickerDialog.OnDateSetListener(){
        @Override
        public void onDateSet(DatePicker view, int year, int month, int dayOfMonth) {
            String date = dayOfMonth + "-" + (month+1) + "-" + year;
            startDateTV.setText(date);
        }
    };


    private View.OnClickListener endDateListener = new View.OnClickListener() {
        @Override
        public void onClick(View v) {
            showEndDatePickerDialog();
        }
    };

    public void showEndDatePickerDialog(){
        DatePickerDialog datePickerDialog = new DatePickerDialog(
                this,
                onEndDateSetListener,
                Calendar.getInstance().get(Calendar.YEAR),
                Calendar.getInstance().get(Calendar.MONTH),
                Calendar.getInstance().get(Calendar.DAY_OF_MONTH));
        datePickerDialog.show();
    }


    DatePickerDialog.OnDateSetListener onEndDateSetListener = new DatePickerDialog.OnDateSetListener(){
        @Override
        public void onDateSet(DatePicker view, int year, int month, int dayOfMonth) {
            String date = dayOfMonth + "-" + (month+1) + "-" + year;
            endDateTV.setText(date);
        }
    };


    private View.OnClickListener fabListener = new View.OnClickListener() {
        @Override
        public void onClick(View view) {
            Intent incomingIntent = getIntent();
            String className = incomingIntent.getStringExtra("className");

            if(className.equals("Transactions")){
                Intent filter = new Intent(Filter.this, Transactions.class);
                startActivity(filter);
            }
            else if(className.equals("Budget")){
                Intent filter = new Intent(Filter.this, Budget.class);
                startActivity(filter);
            }
            else if(className.equals("Stats")){
                Intent filter = new Intent(Filter.this, Stats.class);
                startActivity(filter);
            }


        }
    };
}
