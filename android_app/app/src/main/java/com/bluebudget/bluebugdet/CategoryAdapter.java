package com.bluebudget.bluebugdet;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.ImageView;
import android.widget.TextView;

import java.util.ArrayList;

public class CategoryAdapter extends ArrayAdapter<CategoryItem> {

    public CategoryAdapter(Context context, ArrayList<CategoryItem> categoryList){
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
                    R.layout.category_spinner_row, parent, false
            );
        }

        ImageView categoryIcon = convertView.findViewById(R.id.categoryIconImageView);
        TextView categoryName = convertView.findViewById(R.id.categoryNameTextView);

        CategoryItem currentItem = getItem(position);

        if(currentItem!=null) {
            categoryIcon.setImageResource(currentItem.getIcon());
            categoryName.setText(currentItem.getCategory());
        }

        return convertView;

    }
}
