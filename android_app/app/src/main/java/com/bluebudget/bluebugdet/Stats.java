package com.bluebudget.bluebugdet;

import android.content.Intent;
import android.graphics.Canvas;
import android.support.annotation.NonNull;
import android.support.design.widget.BottomNavigationView;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;

import com.github.mikephil.charting.charts.PieChart;
import com.github.mikephil.charting.components.Description;
import com.github.mikephil.charting.data.PieData;
import com.github.mikephil.charting.data.PieDataSet;
import com.github.mikephil.charting.data.PieEntry;
import com.github.mikephil.charting.utils.ColorTemplate;

import java.util.ArrayList;
import java.util.List;

public class Stats extends AppCompatActivity {

    private PieChart pie;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_stats);

        //get the icon selected and go to the respective activity
        BottomNavigationView navigation = findViewById(R.id.navigation);
        navigation.setOnNavigationItemSelectedListener(mOnNavigationItemSelectedListener);

        //highlight the selected icon
        Menu menu = navigation.getMenu();
        MenuItem menuItem = menu.getItem(3);
        menuItem.setChecked(true);

        pie = findViewById(R.id.piechart);
        initPieChart();


    }

    private BottomNavigationView.OnNavigationItemSelectedListener mOnNavigationItemSelectedListener
            = new BottomNavigationView.OnNavigationItemSelectedListener() {

        @Override
        public boolean onNavigationItemSelected(@NonNull MenuItem item) {
            switch (item.getItemId()) {
                case R.id.navigation_home:
                    Log.d("STATS", "home clicked");
                    Intent home = new Intent(Stats.this, Home.class);
                    startActivity(home);
                    return true;
                case R.id.navigation_transactions:
                    Log.d("STATS", "transactions clicked");
                    Intent transactions = new Intent(Stats.this, Transactions.class);
                    startActivity(transactions);
                    return true;
                case R.id.navigation_budget:
                    Log.d("STATS", "budget clicked");
                    Intent budget = new Intent(Stats.this, Budget.class);
                    startActivity(budget);
                    return true;
                case R.id.navigation_stats:
                    Log.d("STATS", "stats clicked");
                    return true;
            }
            return false;
        }
    };


    public void initPieChart(){
        List<PieEntry> pieEntries = new ArrayList<>();

        pieEntries.add(new PieEntry(1000));
        pieEntries.add(new PieEntry(500, Integer.toString(2000)));
        pieEntries.add(new PieEntry(6000, Integer.toString(1000)));

        pie.animateX(500);
        pie.animateY(500);

        PieDataSet pieDataSet = new PieDataSet(pieEntries, "cenas");
        pieDataSet.setColors(ColorTemplate.COLORFUL_COLORS);

        PieData pieData = new PieData(pieDataSet);
        pie.setData(pieData);


        Description description = new Description();
        description.setText("description");
        pie.setDescription(description);
        pie.invalidate();

    }
}
