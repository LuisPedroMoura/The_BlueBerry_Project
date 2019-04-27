package com.bluebudget.bluebugdet;

import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.widget.ExpandableListView;
import android.widget.GridLayout;
import android.widget.TextView;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

public class IncomeWalkthrough extends AppCompatActivity {

    private ExpandableListView expLV;
    private ExpandableListAdapter expLA;
    private List<List<String>> headerList;
    private HashMap<String, List<List<String>>> headerChildHash;

    private static final String TAG = "IncomeWalkthrough";

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_income_walkthrough);


        initExpandableLV();


    }

    public void initExpandableLV(){

        Log.i(TAG, "initExpandableLV");

        expLV = findViewById(R.id.incomeWalkthroughExpLV);

        initHeaderELV();
        initChildELV();

        expLA = new ExpandableListAdapter(this, headerList, headerChildHash, 1);
        expLV.setAdapter(expLA);

    }

    public void initHeaderELV(){

        Log.i(TAG, "initHeaderELV");

        headerList = new ArrayList<>();
        headerChildHash = new HashMap<>();

        List<String> hL = new ArrayList<>();
        hL.add(R.drawable.ic_trending_up_black_24dp+"");
        hL.add("Income");
        hL.add("100.0€");

        headerList.add(hL);


    }

    public void initChildELV(){

        Log.i(TAG, "initChildELV");


        List<List<String>> list = new ArrayList<>();

        List<String> itemList0 = new ArrayList<>();
        itemList0.add("Gift");
        itemList0.add("100.0€");

        List<String> itemList1 = new ArrayList<>();
        itemList1.add("add new sub category");

        list.add(itemList1);
        list.add(itemList0);


        headerChildHash.put(headerList.get(0).get(1), list);


    }


}
