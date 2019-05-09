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

public class SpinnerAdapter extends ArrayAdapter<SpinnerItem> {

    private static final String TAG = "SpinnerAdapter";

    public SpinnerAdapter(Context context, ArrayList<SpinnerItem> categoryList){
        super(context, 0, categoryList);
    }

    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        return initView(position, convertView, parent);
    }

    @Override
    public View getDropDownView(int position,  View convertView, ViewGroup parent) {
        return initView(position, convertView, parent);
    }

    private View initView(int position, View convertView, ViewGroup parent){

        Log.i(TAG, "(convertView==null) "+(convertView==null)+"***************************************");
        if(convertView==null){
            convertView= LayoutInflater.from(getContext()).inflate(R.layout.layout_spinner_row, parent, false);
        }

        ImageView categoryIcon = convertView.findViewById(R.id.spinnerIconImageView);
        TextView categoryName = convertView.findViewById(R.id.spinnerNameTextView);

        SpinnerItem currentItem = getItem(position);

        Log.i(TAG, "(currentItem!=null) "+(currentItem!=null)+"***************************************");
        if(currentItem!=null) {
            categoryIcon.setImageResource(currentItem.getIcon());
            categoryName.setText(currentItem.getName());
            Log.i(TAG, "currentItem.getIcon() "+ currentItem.getIcon());
            Log.i(TAG, "currentItem.getName() "+ currentItem.getName());
        }
        Log.i(TAG, "------------");
        return convertView;

    }
}
