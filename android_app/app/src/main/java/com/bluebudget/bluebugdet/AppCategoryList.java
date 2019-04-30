package com.bluebudget.bluebugdet;

import android.util.Log;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.Set;

import static android.support.constraint.Constraints.TAG;

public class AppCategoryList {

    private Map<String, AppCategory> categories     = new HashMap<>();

    public AppCategoryList(){}


    public AppCategory getCategory(String name){
        return categories.get(name);
    }

    public void addCategory(String parent, String name, int icon, double def_budget, int def_recurrence, AppBudgetType type) {
        AppCategory newCat = new AppCategory(parent, name, icon, def_budget, def_recurrence, type);
        categories.put(name, newCat);
    }

    public void updateCategory(String name, double defBudget, int defRecurrence) {
        categories.get(name).update(defBudget, defRecurrence);
    }

    public void removeCategory(String name){
        categories.remove(name);
    }

    public List<AppCategory> getCategories(){
        List<AppCategory> res = new ArrayList<>();
        for (String name : categories.keySet()){
            AppCategory cat = categories.get(name);
            if (cat.getParent() == null){
                res.add(cat);
            }
        }
        return res;
    }

    public Map<AppCategory,List<AppCategory>> getCategoriesAndSubCategories(){
        Map<AppCategory,List<AppCategory>> res = new HashMap<>();

        List<AppCategory> cats = getCategories();
        for (AppCategory cat : cats){
            List<AppCategory> newList = new ArrayList<>();
            res.put(cat, newList);
        }

        for (String name : categories.keySet()){
            AppCategory cat = categories.get(name);
            if (cat.getParent() != null){
                AppCategory parent = getCategory(cat.getParent());
                res.get(parent).add(cat);
            }
        }
        return res;
    }

    public List<AppCategory> filterCategories( List<String> parentsList,
                                               List<AppBudgetType> typesList) {

        List<AppCategory> res = new ArrayList<>();

        for(String key : categories.keySet() ) {
            AppCategory cat = categories.get(key);

            if(parentsList == null || parentsList.contains(cat.getParent())) {
                if(typesList == null || typesList.contains(cat.getType())){
                    res.add(cat);
                }
            }
        }

        return res;
    }

    public List<AppCategory> allCatTypeOrdered(AppBudgetType type){
        List<AppCategory> res = new ArrayList<>();

        List<String> parentsList = new ArrayList<>();
        parentsList.add(null);
        List<AppBudgetType> typesList = new ArrayList<>();
        typesList.add(type);

        List<AppCategory> parentCatList =  Home.app.filterCategories(parentsList, typesList);
        Map<AppCategory,List<AppCategory>> catSubMap = Home.app.getCategoriesAndSubCategoriesDict();

        for(AppCategory category : parentCatList){
            res.add(category);
            List<AppCategory> subcatList = catSubMap.get(category);
            for( AppCategory subcat : subcatList ){
                res.add(subcat);
            }
        }

        return res;
    }

}
