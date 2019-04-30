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
import java.util.Map;

public class IncomeWalkthrough extends AppCompatActivity implements PopupDialog.PopupDialogListener{

    private ExpandableListView expLV;

    private ExpandableListAdapter expLA;
    private List<List<String>> headerList;
    private HashMap<String, List<List<String>>> headerChildHash;

    private String key;
    private int key_posInHeaderList;
    private int key_posInList;

    private String addNewSubCatStr;

    AppCategory incomeCat;
    List<AppCategory> subCatList;

    private static final String TAG = "IncomeWalkthrough";

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_income_walkthrough);

        getIncomeCategories();

        initExpandableLV();
    }

    public void getIncomeCategories(){

        List<String> parentsList = new ArrayList<>();
        parentsList.add(null);

        List<AppBudgetType> typesList = new ArrayList<>();
        typesList.add(AppBudgetType.INCOME);

        //there is just one parent category of type Income
        incomeCat = Home.app.filterCategories(parentsList, typesList).get(0);

        parentsList.clear();
        parentsList.add(incomeCat.getName());

        subCatList = Home.app.filterCategories(parentsList, typesList);

    }

    public void initExpandableLV(){

        Log.i(TAG, "initExpandableLV");

        expLV = findViewById(R.id.incomeWalkthroughExpLV);

        headerList = new ArrayList<>();
        headerChildHash = new HashMap<>();

        initHeaderELV();
        initChildELV();

        updateExpListAdapter();

        expLV.setOnChildClickListener(onChildClickListener);
    }

    public void initHeaderELV(){

        Log.i(TAG, "initHeaderELV");

        List<String> itemList = catToItemList(incomeCat);
        headerList.add(itemList);

        //Category name is the key
        key = headerList.get(0).get(1);
        key_posInHeaderList = 0;
        key_posInList = 1;


    }

    public void initChildELV(){

        Log.i(TAG, "initChildELV");
        addNewSubCatStr = "add new sub category";

        List<List<String>> list = new ArrayList<>();

        for(AppCategory subcat: subCatList){
            List<String> itemList = catToItemList(subcat);
            list.add(itemList);
        }

        List<String> itemList = new ArrayList<>();
        itemList.add(addNewSubCatStr);
        list.add(itemList);

        headerChildHash.put(key, list);
    }

    public  List<String> catToItemList(AppCategory cat){
        List<String> itemList = new ArrayList<>();
        itemList.add(cat.getIcon()+"");
        itemList.add(cat.getName());
        itemList.add(cat.getDefBudget()+"€");
        return itemList;
    }

    public  List<String> strToItemList(String name, Double amount){
        List<String> itemList = new ArrayList<>();
        itemList.add(R.drawable.ic_subdirectory_arrow_right_black_24dp+"");
        itemList.add(name);
        itemList.add(amount+"€");
        return itemList;
    }

    private void updateExpListAdapter(){
        expLA = new ExpandableListAdapter(this, headerList, headerChildHash, key_posInList);
        expLV.setAdapter(expLA);
    }

    private ExpandableListView.OnChildClickListener onChildClickListener = new ExpandableListView.OnChildClickListener() {
        @Override
        public boolean onChildClick(ExpandableListView parent, View v, int groupPosition, int childPosition, long id) {

            List<String> childList =  headerChildHash.get(headerList.get(groupPosition).get(key_posInList)).get(childPosition);
            Log.i(TAG, "onChildClick -> " +childList+"");

            if(childList.size()==1 && childList.get(0).equals(addNewSubCatStr)){
                Log.i(TAG, addNewSubCatStr+" clicked");
                openDialog();
            }

            return false;
        }
    };

    public void openDialog() {
        PopupDialog exampleDialog = new PopupDialog();
        exampleDialog.show(getSupportFragmentManager(), TAG+"-> openDialog-> Popup Dialog");
    }

    @Override
    public void applyTexts(String name, Double amount) {

        Log.i(TAG, "name="+name+" amount="+amount);

        List<List<String>> list = headerChildHash.get(key);

        List<String> itemList = strToItemList(name, amount);

        list.add(0, itemList);

        headerChildHash.put(key, list);

        expLA.notifyDataSetChanged();

        Home.app.addCategory(incomeCat.getName(), name, R.drawable.ic_subdirectory_arrow_right_black_24dp, amount, 1, AppBudgetType.INCOME);
    }

}
