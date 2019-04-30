package com.bluebudget.bluebugdet;

import android.content.Context;
import android.graphics.Typeface;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseExpandableListAdapter;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.TextView;

import java.util.HashMap;
import java.util.List;

public class ExpandableListAdapter extends BaseExpandableListAdapter {
    private Context context;
    private List<List<String>> headerList;
    private HashMap<String, List<List<String>>> headerChildMap;
    private int key;

    private static final String TAG = "ExpandableListAdapter";

    public ExpandableListAdapter(Context context, List<List<String>> headerList, HashMap<String, List<List<String>>> headerChildMap, int key) {
        this.context = context;
        this.headerList = headerList;
        this.headerChildMap = headerChildMap;
        this.key = key;
    }

    @Override
    public int getGroupCount() {
        return headerList.size();
    }

    @Override
    public int getChildrenCount(int groupPosition) {
       return headerChildMap.get(headerList.get(groupPosition).get(key)).size();
    }

    @Override
    public Object getGroup(int groupPosition) {

        Log.i(TAG, "getGroup -> groupPosition = "+groupPosition);

        return headerList.get(groupPosition);
    }

    @Override
    public Object getChild(int groupPosition, int childPosition) {
         return headerChildMap.get(headerList.get(groupPosition).get(key)).get(childPosition);
    }

    @Override
    public long getGroupId(int groupPosition) {
        return groupPosition;
    }

    @Override
    public long getChildId(int groupPosition, int childPosition) {
        return childPosition;
    }

    @Override
    public boolean hasStableIds() {
        return false;
    }

    @Override
    public View getGroupView(int groupPosition, boolean isExpanded, View convertView, ViewGroup parent) {

        List<String> headerList = (List<String>) getGroup(groupPosition);

        int icon = Integer.parseInt(headerList.get(0));
        String description = headerList.get(1);
        String amount = headerList.get(2);


        if(convertView==null){
            LayoutInflater inflater = (LayoutInflater) this.context.getSystemService(Context.LAYOUT_INFLATER_SERVICE);

            convertView = inflater.inflate(R.layout.layout_expandablelistview_header, null);

        }


        ImageView iconIV = convertView.findViewById(R.id.headerIconImageView);
        iconIV.setImageResource(icon);

        TextView descriptionTV = convertView.findViewById(R.id.headerDescriptionTextView);
        descriptionTV.setTypeface(null, Typeface.BOLD);
        descriptionTV.setText(description);

        TextView amountTV = convertView.findViewById(R.id.headerAmountTextView);
        amountTV.setTypeface(null, Typeface.BOLD);
        amountTV.setText(amount);

        return convertView;
    }

    @Override
    public View getChildView(int groupPosition, int childPosition, boolean isLastChild, View convertView, ViewGroup parent) {

        Log.i(TAG, "-------------------getChildView");

        List<String> childList = (List<String>)  getChild(groupPosition, childPosition);


        LayoutInflater inflater = (LayoutInflater) this.context.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
        if(childList.size()==2){
            Log.i(TAG, "Inflate -> childList.size()==2");
            convertView = inflater.inflate(R.layout.layout_expandablelistview_child, null);
        }
        else{
            Log.i(TAG, "Inflate -> else");
            convertView = inflater.inflate(R.layout.layout_expandablelistview_textview, null);
        }




        TextView addSubCatTV = convertView.findViewById(R.id.textviewTextView);
        TextView descriptionTV = convertView.findViewById(R.id.childDescriptionTextView);
        TextView amountTV = convertView.findViewById(R.id.childAmountTextView);

        if(childList.size()==2){

            Log.i(TAG, "childList.size()==2");

            String description = childList.get(0);
            Log.i(TAG, "description " + description);
            descriptionTV.setText(description);
            String amount = childList.get(1);
            amountTV.setText(amount);

            Log.i(TAG, "description = " + description);
            Log.i(TAG, "amount = " + amount);
        }
        else if(childList.size()==1){ //add sub category
            Log.i(TAG, "childList.size()==1");

            String text = childList.get(0);
            addSubCatTV.setText(text);
            addSubCatTV.setPadding(100,0,0,0);

            Log.i(TAG, "text = " + text);

        }

        Log.i(TAG, "-------------------getChildView----------------------------");

        return convertView;
    }

    @Override
    public boolean isChildSelectable(int groupPosition, int childPosition) {
        return true;
    }
}
