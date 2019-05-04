package com.bluebudget.bluebugdet;

import android.content.Context;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.ImageView;
import android.widget.ProgressBar;
import android.widget.TextView;

import java.util.ArrayList;

public class BudgetStatsListAdapter extends ArrayAdapter<BudgetProgression> {

    private Context context;
    private int resource;

    private static final String TAG = "BudgetProgressionLA";

    public BudgetStatsListAdapter(Context context, int resource, ArrayList<BudgetProgression> objects) {
        super(context, resource, objects);
        this.context = context;
        this.resource = resource;
        Log.d(TAG, "constructor");
    }

    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        Log.d(TAG, "get view");

        //get the progression info
        int catIcon = getItem(position).getCategoryIcon();
        String description = getItem(position).getDescription();
        String spentAmount = getItem(position).getSpentAmount();
        String leftAmount = getItem(position).getLeftAmount();
        int budgetProgress = getItem(position).getProgressBar();


        LayoutInflater inflater = LayoutInflater.from(this.context);
        convertView = inflater.inflate(this.resource, parent, false);


        //update info
        ImageView catIconIV = convertView.findViewById(R.id.categoryIconBudgetImageView);
        TextView descriptionTV = convertView.findViewById(R.id.descriptionBudgetTextView);
        TextView spentAmountTV = convertView.findViewById(R.id.spentAmountBudgetTextView);
        TextView leftAmountTV = convertView.findViewById(R.id.leftAmountBudgetTextView);
        ProgressBar budgetProgressBar = convertView.findViewById(R.id.budgetProgressBar);

        if(catIconIV!=null && descriptionTV!=null && spentAmountTV!=null && leftAmountTV!=null && budgetProgressBar!=null ){
            catIconIV.setImageResource(catIcon);
            descriptionTV.setText(description);
            spentAmountTV.setText(spentAmount);
            leftAmountTV.setText(leftAmount);
            budgetProgressBar.setProgress(budgetProgress);

        }




        return convertView;
    }

}
