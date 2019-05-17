package com.bluebudget.bluebugdet;

import android.content.Context;
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

        String date;
        int icon;
        String description;
        String amount;

        //get the history info
        if(getItem(position)==null){
            date = " ";
            icon = R.drawable.empty_background;
            description = " ";
            amount = " ";
        }
        else{
            date = getItem(position).getDate();
            icon = getItem(position).getIcon();
            description = getItem(position).getDescription();
            amount = getItem(position).getAmount();
        }



        LayoutInflater inflater = LayoutInflater.from(this.context);
        convertView = inflater.inflate(this.resource, parent, false);
        
        TextView dateTV = convertView.findViewById(R.id.dateTextView);
        ImageView iconIV = convertView.findViewById(R.id.categoryIconImageView);
        TextView descriptionTV = convertView.findViewById(R.id.descriptionTextView);
        TextView amountTV = convertView.findViewById(R.id.amountTextView);

        //update info
        dateTV.setText(date);
        iconIV.setImageResource(icon);
        descriptionTV.setText(description);
        amountTV.setText(amount);

        if(!amount.equals(" ")){
            Double value = Double.parseDouble(amount);
            if(icon == R.drawable.ic_compare_arrows_black_24dp){
                //is transfer
                amountTV.setText(-value+"");
                amountTV.setTextColor(amountTV.getContext().getResources().getColor(R.color.colorDarkBlue));
            }else{
                if(value<0){
                    //Log.i(TAG, value+"");
                    amountTV.setTextColor(amountTV.getContext().getResources().getColor(R.color.colorRed));
                }
                else{
                    amountTV.setTextColor(amountTV.getContext().getResources().getColor(R.color.colorGreen));
                }
            }
        }
        return convertView;
    }

}
