package com.bluebudget.bluebugdet;

import java.util.ArrayList;
import java.util.List;

public class AppWallet {
    
    private String name;
    private Double initialBalance;
    private List<AppTransaction> transactions;
    private double balance;

    
    public AppWallet(String name, double initialBalance) {
        this.name = name;
        this.initialBalance = initialBalance;
    }

    public String getName() {
        return name;
    }
    public void setName(String name) {
        this.name = name;
    }

    public Double getInitialBalance() {
        return initialBalance;
    }
    public void setInitialBalance(Double initialBalance) {
        this.initialBalance = initialBalance;
    }

    public List<AppTransaction> getTransactions(AppTransactionList transactionList){
        calcTransactions(transactionList);
        return this.transactions;
    }

    public double getBalance(AppTransactionList transactionList){
        calcTransactions(transactionList);
        calcBalance();
        return this.balance;
    }

    private void calcTransactions(AppTransactionList transactionList){
        List<String> wallet = new ArrayList<>();
        wallet.add(this.name);
        this.transactions = transactionList.filterTransactions(null, null, null, null, wallet);
    }

    private void calcBalance(){
        balance = this.initialBalance;
        for (AppTransaction tr : this.transactions){
            balance += tr.getValue();
        }
        this.balance = balance;
    }
}
