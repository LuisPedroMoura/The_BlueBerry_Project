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

public class PopupDialog extends AppCompatDialogFragment {

    private EditText nameET;
    private EditText amountET;
    private PopupDialogListener listener;

    @Override
    public Dialog onCreateDialog(Bundle savedInstanceState) {
        AlertDialog.Builder builder = new AlertDialog.Builder(getActivity());

        LayoutInflater inflater = getActivity().getLayoutInflater();
        View view = inflater.inflate(R.layout.layout_popup_new_income_sub_cat, null);

        builder.setView(view)
                .setTitle("New sub-category")
                .setNegativeButton("cancel", new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface dialogInterface, int i) {

                    }
                })
                .setPositiveButton("ok", new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface dialogInterface, int i) {
                        String username = nameET.getText().toString();
                        String password = amountET.getText().toString();
                        listener.applyTexts(username, password);
                    }
                });

        nameET = view.findViewById(R.id.newSubCatNameEditText);
        amountET = view.findViewById(R.id.newSubCatAmountEditText);

        return builder.create();
    }

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
        void applyTexts(String username, String password);
    }
}
