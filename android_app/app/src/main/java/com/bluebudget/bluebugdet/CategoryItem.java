package com.bluebudget.bluebugdet;

public class CategoryItem {

    private String category;
    private int icon;

    public CategoryItem(String category, int icon){
        this.category = category;
        this.icon = icon;
    }

    public String getCategory(){
        return category;
    }

    public int getIcon(){
        return icon;
    }
}
