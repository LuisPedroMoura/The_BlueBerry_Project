package com.bluebudget.bluebugdet;

import android.content.Context;
import android.graphics.Color;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.ImageView;
import android.widget.TextView;

import java.util.ArrayList;

public class TransactionsHistoryListAdapter extends ArrayAdapter<TransactionsHistory> {

    private Context context;
    private int resource;

    private static final String TAG = "TransHisListAdapter";

    public TransactionsHistoryListAdapter(Context context, int resource, ArrayList<TransactionsHistory> objects) {
        super(context, resource, objects);
        this.context = context;
        this.resource = resource;
        Log.d(TAG, "constructor");
    }

    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        Log.d(TAG, "get view");

        //get the history info
        String date = getItem(position).getDate();
        int icon = getItem(position).getIcon();
        String description = getItem(position).getDescription();
        String amount = getItem(position).getAmount();

        //create history object
        //TransactionsHistory transactionsHistory = new TransactionsHistory(date, icon, category, amount);

        LayoutInflater inflater = LayoutInflater.from(this.context);
        convertView = inflater.inflate(this.resource, parent, false);


        //update info
        TextView dateTV = convertView.findViewById(R.id.dateTextView);
        ImageView iconIV = convertView.findViewById(R.id.categoryIconImageView);
        TextView descriptionTV = convertView.findViewById(R.id.descriptionTextView);
        TextView amountTV = convertView.findViewById(R.id.amountTextView);

        dateTV.setText(date);
        iconIV.setImageResource(icon);
        descriptionTV.setText(description);
        amountTV.setText(amount);

        Double value = Double.parseDouble(amount);
        if(value<0){
            //Log.i(TAG, value+"");
            amountTV.setTextColor(amountTV.getContext().getResources().getColor(R.color.colorRed));
        }
        else{
            amountTV.setTextColor(amountTV.getContext().getResources().getColor(R.color.colorGreen));
        }

        return convertView;
    }
}
