package com.bluebudget.bluebugdet;

import android.content.Intent;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.util.Log;
import android.view.View;
import android.widget.ExpandableListView;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

public class AddExpenseBudget extends AppCompatActivity
                                implements PopupDialogAdd.PopupDialogListener,
                                            PopupDialogEdit.PopupDialogListener {

    private ExpandableListView expLV;
    private ExpandableListAdapter expLA;
    private List<List<String>> headerList;
    private HashMap<String, List<List<String>>> headerChildHash;

    private int groupPositionClicked;
    private int childPositionClicked;
    private boolean addCatBtnClicked;

    private int posInList;

    private String addNewSubCatStr;
    private String addNewCatStr;

    List<AppCategory> budgetCatList;
    Map<AppCategory,List<AppCategory>> subCatMap;

    private static final String TAG = "BudgetWalkthrough";

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_add_expense_budget);


        groupPositionClicked = -1;
        childPositionClicked = -1;
        addCatBtnClicked = false;

        getBudgetCategories();

        initExpandableLV();
    }

    public void getBudgetCategories(){
        Log.i(TAG, "getIncomeCategories");

        budgetCatList = Home.app.filterCategories(null, AppBudgetType.EXPENSE);
        Log.i(TAG, "budgetCatList.size = " + budgetCatList.size());

        Map<AppCategory, List<AppCategory>> temp = Home.app.getCategoriesAndSubCategoriesDict();
        subCatMap = new HashMap<>();
        //filter
        for(AppCategory cat : budgetCatList){
            if(temp.containsKey(cat)){
                subCatMap.put(cat, temp.get(cat));
            }
        }
    }

    public void initExpandableLV(){

        Log.i(TAG, "initExpandableLV");

        expLV = findViewById(R.id.addExpenseBudgetExpLV);

        headerList = new ArrayList<>();
        headerChildHash = new HashMap<>();

        initHeaderELV();
        initChildELV();

        updateExpListAdapter();

        expLV.expandGroup(0);
        expLV.setOnChildClickListener(onChildClickListener);
    }

    public void initHeaderELV(){

        Log.i(TAG, "initHeaderELV");

        for(AppCategory cat : budgetCatList){
            List<String> catItemList = catToItemList(cat);
            headerList.add(catItemList);
        }

        //Category name is the pos in each headerList list
        posInList = 1;
    }

    public void initChildELV(){

        Log.i(TAG, "initChildELV");


        for(AppCategory cat : budgetCatList){
            List<AppCategory> subCatList = subCatMap.get(cat);

            List<List<String>> list = new ArrayList<>();

            for(AppCategory subcat: subCatList){
                List<String> itemList = catToItemList(subcat);
                list.add(itemList);
            }

            addNewSubCatStr = "add new sub category";
            List<String> itemList = strToItemList(addNewSubCatStr);
            list.add(itemList);

            headerChildHash.put(cat.getName(), list);
        }
    }

    public List<String> strToItemList(String s){
        List<String> itemList = new ArrayList<>();
        itemList.add(s);
        return itemList;
    }

    public List<String> catToItemList(AppCategory cat){
        List<String> itemList = new ArrayList<>();
        itemList.add(cat.getIcon()+"");
        itemList.add(Home.app.getCategoryFirstName(cat.getName()));
        itemList.add(cat.getDefBudget()+"€");
        return itemList;
    }

    public  List<String> argsToItemList(String name, Double amount){
        List<String> itemList = new ArrayList<>();
        itemList.add(R.drawable.ic_subdirectory_arrow_right_black_24dp+"");
        itemList.add(Home.app.getCategoryFirstName(name));
        itemList.add(amount+"€");
        return itemList;
    }

    private void updateExpListAdapter(){
        expLA = new ExpandableListAdapter(this, headerList, headerChildHash, posInList);
        expLV.setAdapter(expLA);
    }

    private ExpandableListView.OnChildClickListener onChildClickListener = new ExpandableListView.OnChildClickListener() {
        @Override
        public boolean onChildClick(ExpandableListView parent, View v, int groupPosition, int childPosition, long id) {

            List<String> childList =  headerChildHash.get(headerList.get(groupPosition).get(posInList)).get(childPosition);
            Log.i(TAG, "onChildClick -> " +childList+"");

            if(childList.size()==1 && childList.get(0).equals(addNewSubCatStr)){
                Log.i(TAG, addNewSubCatStr+" clicked");

                groupPositionClicked = groupPosition;
                childPositionClicked = childPosition;
                openDialog("New sub-category");
            }

            return false;
        }
    };

    public void openDialog(String title) {
        PopupDialogAdd dialog = new PopupDialogAdd();
        dialog.setTitle(title);
        dialog.show(getSupportFragmentManager(), TAG+"-> openDialog-> Popup Dialog");
    }

    @Override
    public void applyTexts(String name, Double amount) {

        Log.i(TAG, "name="+name+" amount="+amount);
        Log.i(TAG, groupPositionClicked+"");
        Log.i(TAG, childPositionClicked+"");

        if(addCatBtnClicked){
            addCatBtnClicked = false;

            Home.app.addCategory(null, name, R.drawable.ic_euro_symbol_black_24dp, amount, 1, AppBudgetType.EXPENSE);
            AppCategory cat = Home.app.getCategory(name);
            List<String> headerItemList = catToItemList(cat);
            headerList.add(headerItemList);


            List<List<String>> list = new ArrayList<>();
            List<String> childItemList = strToItemList(addNewSubCatStr);
            list.add(childItemList);

            headerChildHash.put(name, list);

            expLA.notifyDataSetChanged();
        }
        else{

            Log.i(TAG,"!!!!!!!!!!!!!!!applyTexts -> else");
            expLA.notifyDataSetChanged();

            List<String> itemList = argsToItemList(name, amount);

            String key = getKeyInPosition(groupPositionClicked);
            Log.i(TAG, "key "+key);
            List<List<String>> list = headerChildHash.get(key);
            list.add(0, itemList);

            headerChildHash.put(key, list);

            expLA.notifyDataSetChanged();

            AppCategory parent = Home.app.getCategory(key);
            String parentName = parent.getName();
            String subcatName = Home.app.newSubCategoryFullName(parentName, name);

            Home.app.addCategory(parentName, subcatName, parent.getIcon(), amount, 1, AppBudgetType.EXPENSE);
        }


        expLA.notifyDataSetChanged();

        //refresh
        Intent intent = getIntent();
        finish();
        startActivity(intent);


    }

    public String getKeyInPosition(int pos){
        Object[] array = headerChildHash.keySet().toArray();
        return  (String)array[pos];
    }

    public void addExpenseBudgetOkBtnClicked(View view){
        Log.i(TAG, "finish btn clicked");
        Intent finish = new Intent(AddExpenseBudget.this, Budget.class);
        startActivity(finish);
    }


    public void addExpenseBudgetAddBtnClicked(View view){
        Log.i(TAG, "add cat btn clicked");
        addCatBtnClicked=true;
        openDialog("New category");
    }

}
