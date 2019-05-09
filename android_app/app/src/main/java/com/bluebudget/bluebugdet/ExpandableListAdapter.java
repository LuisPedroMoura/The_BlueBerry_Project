package com.bluebudget.bluebugdet;

import android.content.Context;
import android.graphics.Typeface;
import android.support.v7.app.AppCompatActivity;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseExpandableListAdapter;
import android.widget.ImageView;
import android.widget.TextView;

import java.util.HashMap;
import java.util.List;

public class ExpandableListAdapter extends BaseExpandableListAdapter {
    private Context context;
    private List<List<String>> headerList;
    private HashMap<String, List<List<String>>> headerChildMap;
    private int key;
    private int groupPos, childPos;

    private static final String TAG = "ExpandableListAdapter";

    public ExpandableListAdapter(Context context, List<List<String>> headerList, HashMap<String, List<List<String>>> headerChildMap, int key) {
        this.context = context;
        this.headerList = headerList;
        this.headerChildMap = headerChildMap;
        this.key = key;
        groupPos = -1;
        childPos = -1;
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

        LayoutInflater inflater = (LayoutInflater) this.context.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
        if(headerList.size()==3){
            //Log.i(TAG, "Inflate -> headerList.size()==3");
            convertView = inflater.inflate(R.layout.layout_expandablelistview_header, null);
        }
        else{
            //Log.i(TAG, "Inflate -> else");
            convertView = inflater.inflate(R.layout.layout_expandablelistview_textview, null);
        }


        ImageView iconIV = convertView.findViewById(R.id.headerIconImageView);
        TextView descriptionTV = convertView.findViewById(R.id.headerDescriptionTextView);
        TextView amountTV = convertView.findViewById(R.id.headerAmountTextView);
        ImageView moreIV = convertView.findViewById(R.id.headerMoreImageView);
        TextView addSubCatTV = convertView.findViewById(R.id.textviewTextView);
        groupPos=groupPosition;
        childPos=-1;
        if(headerList.size()==3){
            int icon = Integer.parseInt(headerList.get(0));
            String description = headerList.get(1);
            String amount = headerList.get(2);

            iconIV.setImageResource(icon);
            descriptionTV.setTypeface(null, Typeface.BOLD);
            descriptionTV.setText(description);
            amountTV.setTypeface(null, Typeface.BOLD);
            amountTV.setText(amount);
            moreIV.setOnClickListener(moreIVListener);

        }
        else{
            String text = headerList.get(0);
            addSubCatTV.setText(text);
            addSubCatTV.setPadding(100,0,0,0);

            //Log.i(TAG, "text = " + text);
        }

        return convertView;
    }

    @Override
    public View getChildView(int groupPosition, int childPosition, boolean isLastChild, View convertView, ViewGroup parent) {

        //Log.i(TAG, "getChildView");

        List<String> childList = (List<String>)  getChild(groupPosition, childPosition);


        LayoutInflater inflater = (LayoutInflater) this.context.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
        if(childList.size()==3){
            //Log.i(TAG, "Inflate -> childList.size()==2");
            convertView = inflater.inflate(R.layout.layout_expandablelistview_child, null);
        }
        else{
            //Log.i(TAG, "Inflate -> else");
            convertView = inflater.inflate(R.layout.layout_expandablelistview_textview, null);
        }




        TextView addSubCatTV = convertView.findViewById(R.id.textviewTextView);
        //ImageView iconIV = convertView.findViewById(R.id.childIconImageView);
        TextView descriptionTV = convertView.findViewById(R.id.childDescriptionTextView);
        TextView amountTV = convertView.findViewById(R.id.childAmountTextView);
        ImageView moreIV = convertView.findViewById(R.id.childMoreImageView);
        groupPos=groupPosition;
        childPos=childPosition;

        if(childList.size()==3){

            //Log.i(TAG, "childList.size()==3");
            //int icon = Integer.parseInt(childList.get(0));
            //iconIV.setImageResource(icon);
            String description = childList.get(1);
            descriptionTV.setText(description);
            String amount = childList.get(2);
            amountTV.setText(amount);
            moreIV.setOnClickListener(moreIVListener);

            //Log.i(TAG, "description = " + description);
            //Log.i(TAG, "amount = " + amount);
        }
        else if(childList.size()==1){ //add sub category
            //Log.i(TAG, "childList.size()==1");

            String text = childList.get(0);
            addSubCatTV.setText(text);
            addSubCatTV.setPadding(100,0,0,0);

            //Log.i(TAG, "text = " + text);

        }

        //Log.i(TAG, "-------------------getChildView----------------------------");

        return convertView;
    }

    @Override
    public boolean isChildSelectable(int groupPosition, int childPosition) {
        return true;
    }

    View.OnClickListener moreIVListener = new View.OnClickListener() {
        @Override
        public void onClick(View v) {


            Log.i(TAG, "!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!groupPos="+groupPos+" childPos="+childPos);

            openDialog("Edit", " ");
            Log.i(TAG, "more icon clicked");
        }
    };

    public void openDialog( String title, String tip) {
        PopupDialogEdit dialog = new PopupDialogEdit();
        dialog.setTitle(title);
        //dialog.setTipTV(tip);

        dialog.show(((AppCompatActivity) context).getSupportFragmentManager(), TAG+"-> openDialog-> Popup Dialog");
    }
}
