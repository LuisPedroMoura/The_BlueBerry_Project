package com.bluebudget.bluebugdet;

import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.CalendarView;

import java.text.SimpleDateFormat;
import java.util.Date;

public class CalendarPopUp extends AppCompatActivity {

    private static final String TAG = "CALENDARPopup";

    private CalendarView calendarView;
    private String date;
    private String className;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_calendar_pop_up);

        Intent incomingIntent = getIntent();
        className = incomingIntent.getStringExtra("className");

        calendarView = findViewById(R.id.calendarView);

        //default date of calendarView
        SimpleDateFormat sdf = new SimpleDateFormat("dd-MM-yyyy");
        date = sdf.format(new Date(calendarView.getDate()));

        calendarView.setOnDateChangeListener(dateChangeListener);

    }

    CalendarView.OnDateChangeListener dateChangeListener = new CalendarView.OnDateChangeListener() {
        @Override
        public void onSelectedDayChange(CalendarView view, int year, int month, int dayOfMonth) {
            date = dayOfMonth + "-" + month + "-" + year;
            Log.i(TAG, date);
        }
    };

    public void okClicked(View view){
        Log.i(TAG, "ok clicked : " + date + " -> " + className);

        Intent intent = new Intent(this, Transactions.class);
        switch (className){
            case "NewExpense"   :   intent = new Intent(this, NewExpense.class);
                break;
            case "NewIncome"    :   intent = new Intent(this, NewIncome.class);
                break;
            case "NewTransfer"  :   intent = new Intent(this, NewTransfer.class);
                break;
        }
        intent.putExtra("date", date);
        startActivity(intent);
    }

    public void cancelClicked(View view){
        Log.i(TAG, "cancel clicked : " + className);

        Intent intent = new Intent(this, Transactions.class);
        switch (className){
            case "NewExpense"   :   intent = new Intent(this, NewExpense.class);
                                    break;
            case "NewIncome"    :   intent = new Intent(this, NewIncome.class);
                                    break;
            case "NewTransfer"  :   intent = new Intent(this, NewTransfer.class);
                                    break;
        }

        startActivity(intent);
    }


}
