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

        if(convertView==null){
            LayoutInflater inflater = (LayoutInflater) this.context.getSystemService(Context.LAYOUT_INFLATER_SERVICE);

            convertView = inflater.inflate(R.layout.layout_expandablelistview_child, null);

        }

        List<String> childList = (List<String>)  getChild(groupPosition, childPosition);


        LinearLayout linearLayout = convertView.findViewById(R.id.childLinearLayout);
        TextView addTV = convertView.findViewById(R.id.childAddSubCatTextView);
        ImageView iconIV = convertView.findViewById(R.id.childIconImageView);
        TextView descriptionTV = convertView.findViewById(R.id.childDescriptionTextView);
        TextView amountTV = convertView.findViewById(R.id.childAmountTextView);
        ImageView moreIV = convertView.findViewById(R.id.childMoreImageView);

        if(childList.size()==2){

            Log.i(TAG, "childList.size()==2");

            /*   iconIV.setVisibility(View.VISIBLE);
            descriptionTV.setVisibility(View.VISIBLE);
            amountTV.setVisibility(View.VISIBLE);
            moreIV.setVisibility(View.VISIBLE);
            addTV.setVisibility(View.INVISIBLE);
            LinearLayout.LayoutParams params = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MATCH_PARENT, LinearLayout.LayoutParams.WRAP_CONTENT);
            params.setMargins(0,0,0,0);
            addTV.setLayoutParams(params);
            LinearLayout.LayoutParams lp = (LinearLayout.LayoutParams) linearLayout.getLayoutParams();
            lp.height = LinearLayout.LayoutParams.MATCH_PARENT;
*/

            String description = childList.get(0);
            String amount = childList.get(1);
            descriptionTV.setText(description);
            amountTV.setText(amount);

            Log.i(TAG, "description = " + description);
            Log.i(TAG, "amount = " + amount);
        }
        else if(childList.size()==1){ //add sub category
            Log.i(TAG, "childList.size()==1");

            iconIV.setVisibility(View.INVISIBLE);
            descriptionTV.setVisibility(View.INVISIBLE);
            amountTV.setVisibility(View.INVISIBLE);
            moreIV.setVisibility(View.INVISIBLE);
            addTV.setVisibility(View.VISIBLE);

            String text = childList.get(0);
            addTV.setText(text);
            addTV.setPadding(100,0,0,0);

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
