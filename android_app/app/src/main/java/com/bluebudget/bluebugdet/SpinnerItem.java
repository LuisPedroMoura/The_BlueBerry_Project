package com.bluebudget.bluebugdet;

public class SpinnerItem {

    private String name;
    private int icon;

    public SpinnerItem(String name, int icon){
        this.name = name;
        this.icon = icon;
    }

    public String getName(){ return name; }

    public int getIcon(){
        return icon;
    }
}
