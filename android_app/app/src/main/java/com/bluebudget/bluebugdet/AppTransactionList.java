package com.bluebudget.bluebugdet;

import android.util.Log;

import java.util.ArrayList;
import java.util.Calendar;
import java.util.GregorianCalendar;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

public class AppTransactionList {

    private Map<Integer, AppTransaction> transactions = new HashMap<>();

    private static final String TAG = "APPTRANSACTIONLIST";


    public AppTransactionList() {}


    public void addIncome(double value, Calendar date, AppCategory category, String notes , String location,
                          String wallet) {
        AppTransaction t = new AppTransaction(value, date, category, notes, location, wallet,
                null, AppTransactionType.INCOME);
        transactions.put(t.getId(), t);
    }

    public void addExpense(double value, Calendar date, AppCategory category, String notes , String location,
                          String wallet) {
        AppTransaction t = new AppTransaction(-value, date, category, notes, location, wallet,
                null, AppTransactionType.EXPENSE);
        transactions.put(t.getId(), t);
    }

    public void addTransfer(double value, Calendar date, String notes , String location,
                          String wallet, String recipientWallet) {
        AppTransaction t = new AppTransaction(-value, date, null, notes, location, wallet,
                recipientWallet, AppTransactionType.TRANSFER);
        transactions.put(t.getId(), t);
    }

    public void removeTransaction(int id){
        transactions.remove(id);
    }

    public void removeTransaction(AppTransaction transaction){
        transactions.remove(transaction.getId());
    }

    public List<AppTransaction> filterTransactions(Calendar minDate, Calendar maxDate,
                                                   List<String> categories,
                                                   List<String> locations,
                                                   List<String> wallets,
                                                   AppTransactionType type) {

        List<AppTransaction> res = new ArrayList<>();

        if (minDate == null){
            minDate = new GregorianCalendar(0,0,0, 0, 0, 0);

        }
        if (maxDate == null){
            maxDate =  new GregorianCalendar(30000,12,31, 0, 0, 0);
        }

        for (Integer id : this.transactions.keySet()){

            AppTransaction tr = this.transactions.get(id);

            /*Log.i(TAG, "minDate.getClass().getSimpleName() " + minDate.getClass().getSimpleName());
            Log.i(TAG,"maxDate.getClass().getSimpleName() " + maxDate.getClass().getSimpleName());
            Log.i(TAG, "tr.getDate().getClass().getSimpleName() " + tr.getDate().getClass().getSimpleName());
            Log.i(TAG, "minDate.getTime() " + minDate.getTime().toString());
            Log.i(TAG, "maxDate.getTime() " + maxDate.getTime().toString());
            Log.i(TAG, "tr.getDate().getTime() " + tr.getDate().getTime().toString());
            Log.i(TAG, "tr.getDate().compareTo(minDate) >= 0 -> " + (tr.getDate().compareTo(minDate) >= 0));
            Log.i(TAG, "tr.getDate().compareTo(maxDate) <= 0 -> " + (tr.getDate().compareTo(maxDate) <= 0));
            Log.i(TAG, "tr.getDate().getTime().compareTo(minDate.getTime()) >= 0 " + (tr.getDate().getTime().compareTo(minDate.getTime()) >= 0));
            Log.i(TAG, "tr.getDate().getTime().compareTo(maxDate.getTime()) <= 0 " + (tr.getDate().getTime().compareTo(maxDate.getTime()) <= 0));
            */

            if (tr.getDate().compareTo(minDate) >= 0 && tr.getDate().compareTo(maxDate) <= 0){
                Log.i(TAG, "(categories == null) "+(categories == null));
                if (categories == null || categories.contains(tr.getCategory().getName()) || categories.contains(tr.getCategory().getParent())){
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
