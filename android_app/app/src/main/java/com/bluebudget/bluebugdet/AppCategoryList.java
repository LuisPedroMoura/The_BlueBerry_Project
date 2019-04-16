package com.bluebudget.bluebugdet;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.Set;

public class AppCategoryList {

    private Map<String, AppCategory> categories     = new HashMap<>();

    public AppCategoryList(){}


    public AppCategory getCategory(String name){
        return categories.get(name);
    }

    public void addCategory(String parent, String name, int icon, double def_budget, double def_recurrence) {
        AppCategory newCat = new AppCategory(parent, name, icon, def_budget, def_recurrence);
        categories.put(name, newCat);
    }

    public void updateCategory(String name, double defBudget, double defRecurrence) {
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
                res.get(cat.getParent()).add(cat);
            }
        }
        return res;
    }

}
