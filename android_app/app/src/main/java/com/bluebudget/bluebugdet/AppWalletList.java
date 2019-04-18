package com.bluebudget.bluebugdet;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

public class AppWalletList {

    private List<AppWallet> wallets = new ArrayList<>();

    public AppWalletList() {}


    public void addWallet(String name, int icon, Double initialBalance){
        AppWallet wallet = new AppWallet(name, icon, initialBalance);
        wallets.add(wallet);
    }

    public void removeWallet(String name) {
        for (AppWallet wallet : wallets){
            if (wallet.getName().equals(name)){
                wallets.remove(wallet);
                break;
            }
        }
    }

    public void updateWallet(String walletName) {

    }

    public List<AppWallet> getWalletsList(){
        return this.wallets;
    }
}
