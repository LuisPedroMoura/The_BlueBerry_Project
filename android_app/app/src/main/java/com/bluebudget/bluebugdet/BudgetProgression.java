package com.bluebudget.bluebugdet;

public class BudgetProgression {

    private int categoryIcon;
    private String description;
    private String spentAmount;
    private String leftAmount;
    private int progressBar;
    //private int moreIconIV;

    public BudgetProgression(int categoryIcon, String description, String spentAmount, String leftAmount, int progressBar) {
        this.categoryIcon = categoryIcon;
        this.description = description;
        this.spentAmount = spentAmount;
        this.leftAmount = leftAmount;
        this.progressBar = progressBar;
    }

    public int getCategoryIcon() {
        return categoryIcon;
    }

    public void setCategoryIcon(int categoryIcon) {
        this.categoryIcon = categoryIcon;
    }

    public String getDescription() {
        return description;
    }

    public void setDescription(String description) {
        this.description = description;
    }

    public String getSpentAmount() {
        return spentAmount;
    }

    public void setSpentAmount(String spentAmount) {
        this.spentAmount = spentAmount;
    }

    public String getLeftAmount() {
        return leftAmount;
    }

    public void setLeftAmount(String leftAmount) {
        this.leftAmount = leftAmount;
    }

    public int getProgressBar() {
        return progressBar;
    }

    public void setProgressBar(int progressBar) {
        this.progressBar = progressBar;
    }


    /*
    public int getMoreIconIV() {
        return moreIconIV;
    }

    public void setMoreIconIV(int moreIconIV) {
        this.moreIconIV = moreIconIV;
    }
 */
}
