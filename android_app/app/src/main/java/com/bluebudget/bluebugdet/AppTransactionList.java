package com.bluebudget.bluebugdet;

import java.util.ArrayList;
import java.util.Date;
import java.util.List;

public class AppTransactionList {

    private List<AppTransaction> transactions = new ArrayList<>();


    public AppTransactionList() {}


    public void addIncome(double value, Date date, String category, String notes , String location,
                          String wallet, String recipientWallet) {
        AppTransaction t = new AppTransaction(value, date, category, notes, location, wallet,
                recipientWallet, AppTransactionType.INCOME);
        transactions.add(t.getId(), t);
    }

    public void addExpense(double value, Date date, String category, String notes , String location,
                          String wallet, String recipientWallet) {
        AppTransaction t = new AppTransaction(-value, date, category, notes, location, wallet,
                recipientWallet, AppTransactionType.EXPENSE);
        transactions.add(t.getId(), t);
    }

    public void addTransfer(double value, Date date, String category, String notes , String location,
                          String wallet, String recipientWallet) {
        AppTransaction t = new AppTransaction(-value, date, category, notes, location, wallet,
                recipientWallet, AppTransactionType.TRANSFER);
        transactions.add(t.getId(), t);
    }

    public void removeTransaction(int id){
        transactions.remove(id);
    }

    public void removeTransaction(AppTransaction transaction){
        transactions.remove(transaction.getId());
    }

    public List<AppTransaction> filterTransactions(Date minDate, Date maxDate,
                                                   List<Integer> categories,
                                                   List<String> locations,
                                                   List<String> wallets) {

        List<AppTransaction> res = new ArrayList<>();

        for (AppTransaction tr : this.transactions){

            if (minDate == null){
                minDate = new Date(0,0,0);
            }
            if (maxDate == null){
                maxDate = new Date();
            }

            if (tr.getDate().compareTo(minDate) >= 0 && tr.getDate().compareTo(maxDate) <= 0){
                if (categories == null || categories.contains(tr.getCategory())){
                    if (locations == null || locations.contains(tr.getLocation())){
                        if (wallets == null || wallets.contains(tr.getWallet())){
                            res.add(tr);
                        }
                        else if (wallets.contains(tr.getRecipientWallet())){
                            tr.setValue(-tr.getValue());
                            res.add(tr);
                        }
                    }
                }
            }
        }
        return res;
    }

    static public double calculateBalance(List<AppTransaction> transactions){
        double balance = 0.0;
        for (AppTransaction tr : transactions){
            balance += tr.getValue();
        }
        return balance;
    }


}
