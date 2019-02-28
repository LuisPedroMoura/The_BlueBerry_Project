package com.bluebudget.bluebugdet;

import android.os.Bundle;
import android.support.annotation.NonNull;
import android.support.design.widget.BottomNavigationView;
import android.support.v7.app.AppCompatActivity;
import android.util.Log;
import android.view.MenuItem;
import android.widget.TextView;

public class Home extends AppCompatActivity {

    private TextView mTextMessage;

    private BottomNavigationView.OnNavigationItemSelectedListener mOnNavigationItemSelectedListener
            = new BottomNavigationView.OnNavigationItemSelectedListener() {

        @Override
        public boolean onNavigationItemSelected(@NonNull MenuItem item) {
            switch (item.getItemId()) {
                case R.id.navigation_home:
                    Log.i("bottom bar", "home clicked");
                    //mTextMessage.setText(R.string.title_home);
                    return true;
                case R.id.navigation_transactions:
                    Log.i("bottom bar", "transactions clicked");
                    //mTextMessage.setText(R.string.title_transactions);
                    return true;
                case R.id.navigation_budget:
                    Log.i("bottom bar", "budget clicked");
                    //mTextMessage.setText(R.string.title_budget);
                    return true;
                case R.id.navigation_stats:
                    Log.i("bottom bar", "stats clicked");
                    //mTextMessage.setText(R.string.title_stats);
                    return true;
            }
            return false;
        }
    };

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_home);

        mTextMessage = (TextView) findViewById(R.id.message);
        BottomNavigationView navigation = (BottomNavigationView) findViewById(R.id.navigation);
        navigation.setOnNavigationItemSelectedListener(mOnNavigationItemSelectedListener);
    }

}
