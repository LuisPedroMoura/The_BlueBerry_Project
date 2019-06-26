package com.bluebudget.bluebugdet;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

public class AppWalletList {

    private List<AppWallet> wallets = new ArrayList<>();

    public AppWalletList() {}


    public AppWallet getWallet(String name){
        for(AppWallet w : wallets){
            if(w.getName().equals(name)){
                return w;//new AppWallet(w.getName(), w.getIcon(), w.getInitialBalance());
            }
        }
        return null;
    }

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

    public void updateWallet(String walletName) {    }

    public List<AppWallet> getWalletsList(){
        List<AppWallet> wL = new ArrayList<>();
        wL.addAll(this.wallets);

        return wL;
    }
}
