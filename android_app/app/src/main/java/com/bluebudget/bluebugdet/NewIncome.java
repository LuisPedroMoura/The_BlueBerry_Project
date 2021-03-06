package com.bluebudget.bluebugdet;

import android.app.DatePickerDialog;
import android.content.Intent;
import android.os.Bundle;
import android.support.design.widget.FloatingActionButton;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.Toolbar;
import android.util.Log;
import android.view.View;
import android.widget.AdapterView;
import android.widget.DatePicker;
import android.widget.EditText;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.Toast;

import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.List;

public class NewIncome extends AppCompatActivity {

    private Toolbar toolbar;

    private FloatingActionButton fab;
    private EditText amountET;
    private TextView dateTV;
    private Spinner categorySpinner;
    private EditText locationET;
    private EditText notesET;
    private Spinner walletSpinner;

    private ArrayList<SpinnerItem> categoryItemList;
    private ArrayList<SpinnerItem> walletItemList;

    private static final String TAG = "NewIncome";

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_new_income);

        toolbar = findViewById(R.id.toolbar);
        setSupportActionBar(toolbar);


        fab = findViewById(R.id.newIncomefab);
        fab.setOnClickListener(checkFabOnClick);

        amountET = findViewById(R.id.amountNewIncomeET);

        locationET = findViewById(R.id.locationNewIncomeET);

        notesET = findViewById(R.id.notesNewIncomeET);

        //Category Spinner
        categorySpinner = findViewById(R.id.categoryNewIncomeSpinner);
        initCategoryList();

        //wallet Spinner
        walletSpinner = findViewById(R.id.walletNewIncomeSpinner);
        initWalletList();


        //Date
        dateTV = findViewById(R.id.dateNewIncomeTV);
        dateTV.setOnClickListener(dateListener);
        SimpleDateFormat sdf = new SimpleDateFormat("dd-MM-yyyy");
        dateTV.setText(sdf.format(Calendar.getInstance().getTime()));

    }

    //////////////////////
    ///Category Spinner///
    //////////////////////
    private void initCategoryList(){

        categoryItemList = new ArrayList<>();

        List<AppCategory> categoriesList = Home.app.allCatTypeOrdered(AppBudgetType.INCOME);

        for(AppCategory category : categoriesList){
            categoryItemList.add(new SpinnerItem(category.getName(), category.getIcon()));
        }
        categoryItemList.add(new SpinnerItem("add new category", R.drawable.empty));
        categoryItemList.add(new SpinnerItem("add new sub-category", R.drawable.empty));


        SpinnerAdapter categoryAdapter = new SpinnerAdapter(this, categoryItemList);
        categorySpinner.setAdapter(categoryAdapter);
        categorySpinner.setOnItemSelectedListener(av);
    }

    //////////////////////
    ////Wallet Spinner////
    //////////////////////
    private void initWalletList(){
        List<AppWallet> walletsList = Home.app.getWalletsList();
        walletItemList = new ArrayList<>();

        for(AppWallet wallet : walletsList){
            walletItemList.add(new SpinnerItem(wallet.getName(), wallet.getIcon()));
        }
        walletItemList.add(new SpinnerItem("add new wallet", R.drawable.empty));


        SpinnerAdapter walletAdapter = new SpinnerAdapter(this, walletItemList);
        walletSpinner.setAdapter(walletAdapter);
        walletSpinner.setOnItemSelectedListener(av);
    }

    AdapterView.OnItemSelectedListener av = new AdapterView.OnItemSelectedListener() {
        @Override
        public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
            SpinnerItem clickedItem = (SpinnerItem) parent.getItemAtPosition(position);
            String clickedName = clickedItem.getName();
            showSpinnerToast(view, clickedName);
            Log.i(TAG, "category " + clickedName+ " selected !!!!!!!!!!!!!!!!!!!!!!!!!!");
        }

        @Override
        public void onNothingSelected(AdapterView<?> parent) {

        }
    };

    private void showSpinnerToast(View view, String clickedName){
        switch (clickedName){
            case "add new category":
            case "add new sub-category":
            case "add new wallet":
                CharSequence text = "To be implemented";
                int duration = Toast.LENGTH_SHORT;

                Toast toast = Toast.makeText(view.getContext(), text, duration);
                toast.show();
        }
    }

    //////////////////////
    /////////Date/////////
    //////////////////////
    View.OnClickListener dateListener = new View.OnClickListener() {
        @Override
        public void onClick(View v) {
            showDatePickerDialog();
        }
    };

    public void showDatePickerDialog(){
        DatePickerDialog datePickerDialog = new DatePickerDialog(
                this,
                onDateSetListener,
                Calendar.getInstance().get(Calendar.YEAR),
                Calendar.getInstance().get(Calendar.MONTH),
                Calendar.getInstance().get(Calendar.DAY_OF_MONTH));
        datePickerDialog.show();
    }


    DatePickerDialog.OnDateSetListener onDateSetListener = new DatePickerDialog.OnDateSetListener(){
        @Override
        public void onDateSet(DatePicker view, int year, int month, int dayOfMonth) {
            String date = dayOfMonth + "-" + (month+1) + "-" + year;
            dateTV.setText(date);
        }
    };


    View.OnClickListener checkFabOnClick = new View.OnClickListener() {
        @Override
        public void onClick(View view) {
            Log.d(TAG, "check clicked");
            Intent transactions = new Intent(NewIncome.this, Transactions.class);

            transactions.putExtra("transactionType", "NewIncome");
            String amount = amountET.getText().toString();
            if(amount.equals("")){
                amount = 0.0+"";
            }
            transactions.putExtra("amount", Double.parseDouble(amount));
            transactions.putExtra("date", dateTV.getText().toString());

            SpinnerItem csi = (SpinnerItem) categorySpinner.getSelectedItem();
            transactions.putExtra("category",csi.getName());
            transactions.putExtra("location", locationET.getText().toString());
            transactions.putExtra("notes", notesET.getText().toString());
            SpinnerItem wsi = (SpinnerItem) walletSpinner.getSelectedItem();
            transactions.putExtra("wallet", wsi.getName());

            startActivity(transactions);
        }
    };
}
