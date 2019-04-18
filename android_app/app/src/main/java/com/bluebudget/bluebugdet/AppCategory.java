package com.bluebudget.bluebugdet;

public class AppCategory {

    private String parent;
    private String name;
    private int icon;
    private double defBudget;
    private int defRecurrence;
    private double monthlyBudget;
    private double yearlyBudget;


    public AppCategory(String parent, String name, int icon, double defBudget, int defRecurrence) {
        this.parent = parent;
        this.name = name;
        this.icon = icon;
        this.defBudget = defBudget;
        this.defRecurrence = defRecurrence;
        this.monthlyBudget = defBudget/defRecurrence;
        this.yearlyBudget = this.monthlyBudget*12;
    }

    public String getParent() {
        return parent;
    }
    public void setParent(String parent) {
        this.parent = parent;
    }

    public String getName() {
        return name;
    }
    public void setName(String name) {
        this.name = name;
    }

    public int getIcon() {
        return icon;
    }
    public void setIcon(int icon) {
        this.icon = icon;
    }

    public double getDefBudget() {
        return defBudget;
    }
    public void setDefBudget(double defBudget) {
        this.defBudget = defBudget;
    }

    public int getDefRecurrence() {
        return defRecurrence;
    }
    public void setDefRecurrence(int defRecurrence) {
        this.defRecurrence = defRecurrence;
    }

    public double getMonthlyBudget() {
        return monthlyBudget;
    }
    public void setMonthlyBudget(double monthlyBudget) {
        this.monthlyBudget = monthlyBudget;
    }

    public double getYearlyBudget() {
        return yearlyBudget;
    }
    public void setYearlyBudget(double yearlyBudget) {
        this.yearlyBudget = yearlyBudget;
    }

    public void update(double defBudget, int defRecurrence){
        this.defBudget = defBudget;
        this.defRecurrence = defRecurrence;
        this.monthlyBudget = defBudget/defRecurrence;
        this.yearlyBudget = this.monthlyBudget*12;
    }
}
