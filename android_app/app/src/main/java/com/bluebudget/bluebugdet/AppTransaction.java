package com.bluebudget.bluebugdet;
import java.util.Date;

public class AppTransaction {

    private static int COUNTER = 0;
    private int id;
    private double value;
    private AppTransactionType type;
    private Date date;
    private String category;
    private String notes;
    private String location;
    private String wallet;
    private String recipientWallet;

    public AppTransaction(double value, Date date, String category, String notes, String location,
                          String wallet, String recipientWallet, AppTransactionType type) {
        this.id = ++COUNTER;
        this.value = value;
        this.date = date;
        this.category = category;
        this.notes = notes;
        this.location = location;
        this.wallet = wallet;
        this.recipientWallet = recipientWallet;
    }


    public int getId() {
        return id;
    }
    public void setId(int id) {
        this.id = id;
    }

    public AppTransactionType getType() {
        return type;
    }
    public void setType(AppTransactionType type) {
        this.type = type;
    }

    public double getValue(){
        return this.value;
    }
    public void setValue(double value){
        this.value = value;
    }

    public Date getDate(){
        return this.date;
    }
    public void setDate(Date date){
        this.date = date;
    }

    public String getCategory(){
        return this.category;
    }
    public void setCategory(String categoryI){
        this.category = category;
    }

    public String getNotes(){
        return this.notes;
    }
    public void setNotes(String notes){
        this.notes = notes;
    }

    public String getLocation(){
        return this.location;
    }
    public void setLocation(String location){
        this.location = location;
    }

    public String getWallet() {
        return this.wallet;
    }
    public void setWallet(String walletName){
        this.wallet = walletName;
    }

    public String getRecipientWallet(){
        return this.recipientWallet;
    }
    public void setRecipientWallet(String walletName){
        this.recipientWallet = walletName;
    }
}
