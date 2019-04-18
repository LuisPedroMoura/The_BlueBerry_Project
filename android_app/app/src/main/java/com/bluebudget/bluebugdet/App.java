package com.bluebudget.bluebugdet;

import java.util.Date;
import java.util.List;
import java.util.Map;

public class App {


    boolean firstUse = true;

    private AppCategoryList categories;
    private AppWalletList wallets;
    private AppTransactionList transactions;



    public App(){
        this.transactions = new AppTransactionList();
        this.wallets = new AppWalletList();
        this.categories = new AppCategoryList();

        addWallet("Current", R.drawable.ic_account_balance_wallet_black_24dp, -1.0);
        addWallet("Future Expenses", R.drawable.ic_account_balance_wallet_black_24dp, -1.0);
        addWallet("Savings",R.drawable.ic_account_balance_wallet_black_24dp, -1.0);

        addCategory(null, "Home", R.drawable.ic_home_black_24dp, 0.0, 1);
        addCategory(null, "Food", R.drawable.ic_shopping_cart_black_24dp, 0.0, 1);
        addCategory(null, "Transports", R.drawable.ic_directions_car_black_24dp, 0.0, 1);
    }


    // ---------------------------------------------------------------------------------------------
    // CATEGORIES ----------------------------------------------------------------------------------
    // ---------------------------------------------------------------------------------------------
    public AppCategory getCategory(String name){
        return categories.getCategory(name);
    }

    public void addCategory(String parent, String name, int icon, double defBudget, int defRecurrence) {
        categories.addCategory(parent, name, icon, defBudget, defRecurrence);
    }

    public void updateCategory(String name, double defBudget, int defRecurrence) {
        categories.updateCategory(name, defBudget, defRecurrence);
    }

    public void removeCategory(String name){
        categories.removeCategory(name);
    }

    public List<AppCategory> getCategoriesList(){
        return categories.getCategories();
    }

    public Map<AppCategory,List<AppCategory>> getCategoriesAndSubCategoriesDict(){
        return categories.getCategoriesAndSubCategories();
    }


    // ---------------------------------------------------------------------------------------------
    // WALLETS -------------------------------------------------------------------------------------
    // ---------------------------------------------------------------------------------------------

    public void addWallet(String name, int icon, Double initialBalance){
        wallets.addWallet(name, icon, initialBalance);
    }

    public void removeWallet(String name) {
        wallets.removeWallet(name);
    }

    // does nothing for the moment
    public void updateWallet(String walletName) {
        wallets.updateWallet(walletName);
    }

    public List<AppWallet> getWalletsList(){
        return wallets.getWalletsList();
    }


    // ---------------------------------------------------------------------------------------------
    // TRANSACTIONS --------------------------------------------------------------------------------
    // ---------------------------------------------------------------------------------------------

    public void addIncome(double value, Date date, String category, String notes , String location,
                          String wallet) {
        transactions.addIncome(value, date, category, notes , location, wallet);
    }

    public void addExpense(double value, Date date, String category, String notes , String location,
                          String wallet) {
        transactions.addExpense(value, date, category, notes , location, wallet);
    }

    public void addTransfer(double value, Date date, String category, String notes , String location,
                          String wallet, String recipientWallet) {
        transactions.addTransfer(value, date, category, notes , location, wallet, recipientWallet);
    }

    public List<AppTransaction> getTransactions(Date minDate, Date maxDate,
                                                   List<Integer> categoryIDs,
                                                   List<String> locations,
                                                   List<String> wallets,
                                                   AppTransactionType type) {

        return transactions.filterTransactions(minDate, maxDate, categoryIDs, locations, wallets, type);
    }


    public double calculateBalance(List<AppTransaction> transactions){
        return AppTransactionList.calculateBalance(transactions);
    }



}
