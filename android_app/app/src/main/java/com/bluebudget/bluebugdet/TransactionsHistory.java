package com.bluebudget.bluebugdet;

public class TransactionsHistory {
    private int id;
    private String date;
    private int icon;
    private String description;
    private String amount;
    private String location;
    private String notes;
    private String fromWallet;
    private String recipientWallet;

    public TransactionsHistory(int id,String date, int icon, String description, String amount,
                               String location, String notes, String fromWallet, String recipientWallet) {
        this.id = id;
        this.date = date;
        this.icon = icon;
        this.description = description;
        this.amount = amount;
        this.location=location;
        this.notes=notes;
        this.fromWallet = fromWallet;
        this.recipientWallet = recipientWallet;
    }

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
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

    public String getLocation() {
        return location;
    }

    public void setLocation(String location) {
        this.location = location;
    }

    public String getNotes() {
        return notes;
    }

    public void setNotes(String notes) {
        this.notes = notes;
    }

    public String getFromWallet() {
        return fromWallet;
    }

    public void setFromWallet(String fromWallet) {
        this.fromWallet = fromWallet;
    }

    public String getRecipientWallet() {
        return recipientWallet;
    }

    public void setRecipientWallet(String recipientWallet) {
        this.recipientWallet = recipientWallet;
    }
}
