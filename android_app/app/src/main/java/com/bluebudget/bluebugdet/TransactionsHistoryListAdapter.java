package com.bluebudget.bluebugdet;

import android.content.Context;
import android.graphics.Color;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.view.animation.Animation;
import android.view.animation.AnimationUtils;
import android.widget.ArrayAdapter;
import android.widget.ImageView;
import android.widget.TextView;

import java.util.ArrayList;

public class TransactionsHistoryListAdapter extends ArrayAdapter<TransactionsHistory> {

    private static class ViewHolder{
        TextView dateTV;
        ImageView iconIV;
        TextView descriptionTV;
        TextView amountTV;

    }



    private Context context;
    private int resource;
    private int lastPosition;

    private static final String TAG = "TransHisListAdapter";

    public TransactionsHistoryListAdapter(Context context, int resource, ArrayList<TransactionsHistory> objects) {
        super(context, resource, objects);
        this.context = context;
        this.resource = resource;
        this.lastPosition = -1;
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

        //create the view result for showing the animation
        final View result;

        ViewHolder holder;

        if(convertView==null){
            LayoutInflater inflater = LayoutInflater.from(this.context);
            convertView = inflater.inflate(this.resource, parent, false);

            holder = new ViewHolder();
            holder.dateTV = convertView.findViewById(R.id.dateTextView);
            holder.iconIV = convertView.findViewById(R.id.categoryIconImageView);
            holder.descriptionTV = convertView.findViewById(R.id.descriptionTextView);
            holder.amountTV = convertView.findViewById(R.id.amountTextView);

            result = convertView;
            convertView.setTag(holder);
        }
        else {
            holder = (ViewHolder) convertView.getTag();
            result = convertView;
        }



        Animation animation = AnimationUtils.loadAnimation(context,
                (position > lastPosition) ? R.anim.load_down_anim : R.anim.load_up_anim);
        result.startAnimation(animation);
        lastPosition = position;

        //update info
        holder.dateTV.setText(date);
        holder.iconIV.setImageResource(icon);
        holder.descriptionTV.setText(description);
        holder.amountTV.setText(amount);

       Double value = Double.parseDouble(amount);
        if(value<0){
            //Log.i(TAG, value+"");
            holder.amountTV.setTextColor(holder.amountTV.getContext().getResources().getColor(R.color.colorRed));
        }
        else{
            holder.amountTV.setTextColor(holder.amountTV.getContext().getResources().getColor(R.color.colorGreen));
        }

        return convertView;
    }

}
