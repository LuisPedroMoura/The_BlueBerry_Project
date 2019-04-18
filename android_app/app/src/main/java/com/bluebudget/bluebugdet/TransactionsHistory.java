package com.bluebudget.bluebugdet;

public class TransactionsHistory {
    private String date;
    private int icon;
    private String category;
    private String amount;

    public TransactionsHistory(String date, int icon, String category, String amount) {
        this.date = date;
        this.icon = icon;
        this.category = category;
        this.amount = amount;
    }

    public String getDate() {
        return date;
    }

    public void setDate(String date) {
        this.date = date;
    }

    public int getIcon() {
        return icon;
    }

    public void setIcon(int icon) {
        this.icon = icon;
    }

    public String getCategory() {
        return category;
    }

    public void setCategory(String category) {
        this.category = category;
    }

    public String getAmount() {
        return amount;
    }

    public void setAmount(String amount) {
        this.amount = amount;
    }
}
