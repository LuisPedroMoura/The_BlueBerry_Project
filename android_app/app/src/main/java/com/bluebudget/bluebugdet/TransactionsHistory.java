package com.bluebudget.bluebugdet;

public class TransactionsHistory {
    private String date;
    private int icon;
    private String description;
    private String amount;

    public TransactionsHistory(String date, int icon, String description, String amount) {
        this.date = date;
        this.icon = icon;
        this.description = description;
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

    public String getDescription() {
        return description;
    }

    public void setDescription(String description) {
        this.description = description;
    }

    public String getAmount() {
        return amount;
    }

    public void setAmount(String amount) {
        this.amount = amount;
    }
}
