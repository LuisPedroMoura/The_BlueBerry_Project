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

public class EditIncome extends AppCompatActivity {

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

    private int idIntent;
    private String catIntent;
    private double amountIntent;
    private String dateIntent;
    private String locationIntent;
    private String notesIntent;
    private String fromWalletIntent;

    private static final String TAG = "EditIncome";

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_edit_income);

        Intent intent = getIntent();
        idIntent = intent.getIntExtra("id", -1);
        catIntent = intent.getStringExtra("cat");
        amountIntent = intent.getDoubleExtra("amount", 0);
        dateIntent = intent.getStringExtra("date");
        locationIntent = intent.getStringExtra("location");
        notesIntent = intent.getStringExtra("notes");
        fromWalletIntent = intent.getStringExtra("fromWallet");

        toolbar = findViewById(R.id.toolbar);
        setSupportActionBar(toolbar);


        fab = findViewById(R.id.editIncomefab);
        fab.setOnClickListener(checkFabOnClick);

        amountET = findViewById(R.id.amountEditIncomeET);
        amountET.setText(amountIntent+"");

        locationET = findViewById(R.id.locationEditIncomeET);
        locationET.setText(locationIntent);

        notesET = findViewById(R.id.notesEditIncomeET);
        notesET.setText(notesIntent);

        //Category Spinner
        categorySpinner = findViewById(R.id.categoryEditIncomeSpinner);
        initCategoryList();

        //wallet Spinner
        walletSpinner = findViewById(R.id.walletEditIncomeSpinner);
        initWalletList();


        //Date
        dateTV = findViewById(R.id.dateEditIncomeTV);
        dateTV.setOnClickListener(dateListener);
        dateTV.setText(dateIntent);

    }

    //////////////////////
    ///Category Spinner///
    //////////////////////
    private void initCategoryList(){

        categoryItemList = new ArrayList<>();

        AppCategory cat = Home.app.getCategory(catIntent);
        categoryItemList.add(new SpinnerItem(cat.getName(), cat.getIcon()));

        List<AppCategory> categoriesList = Home.app.allCatTypeOrdered(AppBudgetType.INCOME);
        categoriesList.remove(cat);

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

        walletItemList = new ArrayList<>();
        AppWallet w = Home.app.getWallet(fromWalletIntent);
        walletItemList.add(new SpinnerItem(w.getName(), w.getIcon()));

        List<AppWallet> walletsList = Home.app.getWalletsList();
        walletsList.remove(w);

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
            Log.i(TAG, "category " + clickedName+ " selected");
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
            Intent transactions = new Intent(EditIncome.this, Transactions.class);

            transactions.putExtra("transactionType", "EditIncome");
            transactions.putExtra("id", idIntent);
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

    public void editIncomeDeleteBtnClicked(View view) {
        CharSequence text = "To be implemented";
        int duration = Toast.LENGTH_LONG;

        Toast toast = Toast.makeText(this, text, duration);
        toast.show();
    }
}
