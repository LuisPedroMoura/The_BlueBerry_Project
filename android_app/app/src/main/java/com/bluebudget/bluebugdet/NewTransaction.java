package com.bluebudget.bluebugdet;

import android.content.Intent;
import android.os.Bundle;
import android.support.design.widget.FloatingActionButton;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.Toolbar;
import android.util.Log;
import android.view.View;
import android.widget.AdapterView;
import android.widget.Spinner;

import java.util.ArrayList;

public class NewTransaction extends AppCompatActivity {

    private static final String TAG = "NewTransaction";

    private ArrayList<CategoryItem> categoryItemList;
    private CategoryAdapter categoryAdapter;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_new_transaction);
        Toolbar toolbar = findViewById(R.id.toolbar);
        setSupportActionBar(toolbar);

        FloatingActionButton fab = findViewById(R.id.fab);
        fab.setOnClickListener(checkFabOnClick);


        initList();
        Spinner categorySpinner = findViewById(R.id.categorySpinner);
        categoryAdapter = new CategoryAdapter(this, categoryItemList);
        categorySpinner.setAdapter(categoryAdapter);

        categorySpinner.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                CategoryItem clickedItem = (CategoryItem) parent.getItemAtPosition(position);
                String clickedCategoryName = clickedItem.getCategory();

                Log.i(TAG, "category " + clickedCategoryName+ " selected");
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {

            }
        });

        //categoryDropDownImageView
    }

    private void initList(){
        categoryItemList = new ArrayList<>();
        categoryItemList.add(new CategoryItem("Food", R.drawable.ic_shopping_cart_black_24dp));
        categoryItemList.add(new CategoryItem("Home", R.drawable.ic_home_black_24dp));
        categoryItemList.add(new CategoryItem("Transports", R.drawable.ic_directions_car_black_24dp));
    }


    View.OnClickListener checkFabOnClick = new View.OnClickListener() {
        @Override
        public void onClick(View view) {
            Log.d(TAG, "check clicked");
            Intent transactions = new Intent(NewTransaction.this, Transactions.class);
            startActivity(transactions);
        }
    };

    View.OnClickListener dropDownOnClick = new View.OnClickListener() {
        @Override
        public void onClick(View view) {
            Log.d(TAG, "dropDown clicked");
            //Intent transactions = new Intent(NewTransaction.this, Transactions.class);
            //startActivity(transactions);
        }
    };

}