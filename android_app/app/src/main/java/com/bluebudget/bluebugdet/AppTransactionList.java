package com.bluebudget.bluebugdet;

import java.util.ArrayList;
import java.util.Date;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

public class AppTransactionList {

    private Map<Integer, AppTransaction> transactions = new HashMap<>();


    public AppTransactionList() {}


    public void addIncome(double value, Date date, String category, String notes , String location,
                          String wallet) {
        AppTransaction t = new AppTransaction(value, date, category, notes, location, wallet,
                null, AppTransactionType.INCOME);
        transactions.put(t.getId(), t);
    }

    public void addExpense(double value, Date date, String category, String notes , String location,
                          String wallet) {
        AppTransaction t = new AppTransaction(-value, date, category, notes, location, wallet,
                null, AppTransactionType.EXPENSE);
        transactions.put(t.getId(), t);
    }

    public void addTransfer(double value, Date date, String category, String notes , String location,
                          String wallet, String recipientWallet) {
        AppTransaction t = new AppTransaction(-value, date, category, notes, location, wallet,
                recipientWallet, AppTransactionType.TRANSFER);
        transactions.put(t.getId(), t);
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
                                                   List<String> wallets,
                                                   AppTransactionType type) {

        List<AppTransaction> res = new ArrayList<>();

        for (Integer id : this.transactions.keySet()){

            AppTransaction tr = this.transactions.get(id);

            if (minDate == null){
                minDate = new Date(0,0,0);
            }
            if (maxDate == null){
                maxDate = new Date();
            }

            if (tr.getDate().compareTo(minDate) >= 0 && tr.getDate().compareTo(maxDate) <= 0){
                if (categories == null || categories.contains(tr.getCategory())){
                    if (locations == null || locations.contains(tr.getLocation())){
                        if (type == null || tr.getType() == type) {
                            if (wallets == null || wallets.contains(tr.getWallet())) {
                                res.add(tr);
                            } else if ( type == AppTransactionType.TRANSFER && wallets.contains(tr.getRecipientWallet())) {
                                tr.setValue(-tr.getValue());
                                res.add(tr);
                            }
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
