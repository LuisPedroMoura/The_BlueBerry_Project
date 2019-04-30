package com.bluebudget.bluebugdet;

import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
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
    private int key;
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


        expLV.setOnChildClickListener(onChildClickListener);

        expLA = new ExpandableListAdapter(this, headerList, headerChildHash, key);
        expLV.setAdapter(expLA);

    }

    private ExpandableListView.OnChildClickListener onChildClickListener = new ExpandableListView.OnChildClickListener() {
        @Override
        public boolean onChildClick(ExpandableListView parent, View v, int groupPosition, int childPosition, long id) {
            Log.i(TAG, "onChildClick -> " + headerChildHash.get(headerList.get(groupPosition).get(key)).get(childPosition)+"");
            return false;
        }
    };

    public void initHeaderELV(){

        Log.i(TAG, "initHeaderELV");

        headerList = new ArrayList<>();

        List<String> hL1 = new ArrayList<>();
        hL1.add(R.drawable.ic_trending_up_black_24dp+"");
        hL1.add("Income");
        hL1.add("100.0€");

        headerList.add(hL1);

        key = 1; //Income is the key (pos 1)

    }

    public void initChildELV(){

        Log.i(TAG, "initChildELV");

        headerChildHash = new HashMap<>();

        List<List<String>> list = new ArrayList<>();

        /*List<String> itemList0 = new ArrayList<>();
        itemList0.add("Gift");
        itemList0.add("100.0€");*/

        List<String> itemList1 = new ArrayList<>();
        itemList1.add("add new sub category");


        /*list.add(itemList0);
        list.add(itemList0);
        list.add(itemList0);
        list.add(itemList0);*/

        list.add(itemList1);

        headerChildHash.put(headerList.get(0).get(key), list);

    }




}
