package com.bluebudget.bluebugdet;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.ImageView;
import android.widget.TextView;

import java.util.ArrayList;

public class SpinnerAdapter extends ArrayAdapter<SpinnerItem> {

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
        if(convertView==null){
            convertView= LayoutInflater.from(getContext()).inflate(
                    R.layout.spinner_row_layout, parent, false
            );
        }

        ImageView categoryIcon = convertView.findViewById(R.id.spinnerIconImageView);
        TextView categoryName = convertView.findViewById(R.id.spinnerNameTextView);

        SpinnerItem currentItem = getItem(position);

        if(currentItem!=null) {
            categoryIcon.setImageResource(currentItem.getIcon());
            categoryName.setText(currentItem.getName());
        }

        return convertView;

    }
}
