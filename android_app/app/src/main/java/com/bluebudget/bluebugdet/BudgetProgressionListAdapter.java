package com.bluebudget.bluebugdet;

import android.content.Context;
import android.support.v7.app.AppCompatActivity;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.ImageView;
import android.widget.ProgressBar;
import android.widget.TextView;

import java.util.ArrayList;

public class BudgetProgressionListAdapter extends ArrayAdapter<BudgetProgression>
                                            implements PopupDialogEdit.PopupDialogListener{

    private Context context;
    private int resource;

    private static final String TAG = "BudgetProgressionLA";

    public BudgetProgressionListAdapter(Context context, int resource, ArrayList<BudgetProgression> objects) {
        super(context, resource, objects);
        this.context = context;
        this.resource = resource;
        Log.d(TAG, "constructor");
    }

    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        Log.d(TAG, "get view");

        int catIcon;
        String description;
        String spentAmount;
        String leftAmount;
        int budgetProgress;

        //get the progression info

        //Log.i(TAG, "position " + position);
        //Log.i(TAG, "getItem(position)==null " + (getItem(position)==null));
        if(getItem(position) == null){
            catIcon = R.drawable.empty_background;
            description = " ";
            spentAmount = " ";
            leftAmount = " ";
            budgetProgress = -1;
        }
        else{
            catIcon = getItem(position).getCategoryIcon();
            description = getItem(position).getDescription();
            spentAmount = getItem(position).getSpentAmount();
            leftAmount = getItem(position).getLeftAmount();
            budgetProgress = getItem(position).getProgressBar();
            //int moreIconIV;
        }


        LayoutInflater inflater = LayoutInflater.from(this.context);
        convertView = inflater.inflate(this.resource, parent, false);


        //update info
        ImageView catIconIV = convertView.findViewById(R.id.categoryIconBudgetImageView);
        TextView descriptionTV = convertView.findViewById(R.id.descriptionBudgetTextView);
        TextView spentAmountTV = convertView.findViewById(R.id.spentAmountBudgetTextView);
        TextView leftAmountTV = convertView.findViewById(R.id.leftAmountBudgetTextView);
        ProgressBar budgetProgressBar = convertView.findViewById(R.id.budgetProgressBar);
        ImageView moreIconIV = convertView.findViewById(R.id.moreIconImageView);

        if(catIconIV!=null && descriptionTV!=null && spentAmountTV!=null && leftAmountTV!=null && budgetProgressBar!=null ){
            catIconIV.setImageResource(catIcon);
            descriptionTV.setText(description);
            spentAmountTV.setText(spentAmount);
            leftAmountTV.setText(leftAmount);
            Log.i(TAG, "getItem(position)!=null " + (getItem(position)!=null));
            if(getItem(position)!=null){
                Log.i(TAG, "set progress bar progress");
                budgetProgressBar.setProgress(budgetProgress);
                moreIconIV.setOnClickListener(moreIconListener);
            }
            else{
                Log.i(TAG, "set INVISIBLE");
                TextView spentTV = convertView.findViewById(R.id.spent);
                TextView leftTV = convertView.findViewById(R.id.left);
                spentTV.setVisibility(View.GONE);
                leftTV.setVisibility(View.GONE);
                budgetProgressBar.setVisibility(View.GONE);
                moreIconIV.setVisibility(View.GONE);
            }
        }


        return convertView;
    }

    View.OnClickListener moreIconListener = new View.OnClickListener() {
        @Override
        public void onClick(View v) {

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

    @Override
    public void applyTexts(String name, Double amount) {
        
    }
}
