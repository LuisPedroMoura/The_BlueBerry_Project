package com.bluebudget.bluebugdet;

import android.app.AlertDialog;
import android.app.Dialog;
import android.content.Context;
import android.content.DialogInterface;
import android.os.Bundle;
import android.support.v7.app.AppCompatDialogFragment;
import android.view.LayoutInflater;
import android.view.View;
import android.widget.EditText;
import android.widget.TextView;

public class PopupDialogEdit extends AppCompatDialogFragment {

    private EditText nameET;
    private EditText amountET;
    private TextView tipTV;
    private PopupDialogListener listener;
    private String title = "";
    private static final String TAG = "PopupDialogEdit";

    @Override
    public Dialog onCreateDialog(Bundle savedInstanceState) {
        AlertDialog.Builder builder = new AlertDialog.Builder(getActivity());

        LayoutInflater inflater = getActivity().getLayoutInflater();
        View view = inflater.inflate(R.layout.layout_popup_edit, null);

        builder.setView(view)
                .setTitle(title)
                .setNeutralButton("Cancel", new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface dialogInterface, int i) {

                    }
                })
                .setNegativeButton("Delete", new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface dialogInterface, int i) {

                    }
                })
                .setPositiveButton("save", new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface dialogInterface, int i) {

                        try {
                            String name = nameET.getText().toString();
                            Double amount = Double.parseDouble(amountET.getText().toString());
                            listener.applyTexts(name, amount);
                        } catch (NumberFormatException e) {
                            e.printStackTrace();
                        }


                    }
                });

        nameET = view.findViewById(R.id.newSubCatNameEditText);
        amountET = view.findViewById(R.id.newSubCatAmountEditText);
        tipTV = view.findViewById(R.id.tipTextView);
        return builder.create();
    }

    public void setTitle(String title) {
        this.title = title;
    }

    public void setTipTV(String text){this.tipTV.setText(text);}

    @Override
    public void onAttach(Context context) {
        super.onAttach(context);

        try {
            listener = (PopupDialogListener) context;
        } catch (ClassCastException e) {
            throw new ClassCastException(context.toString() +
                    "must implement PopupDialogListener");
        }
    }

    public interface PopupDialogListener {
        void applyTexts(String name, Double amount);
    }
}
