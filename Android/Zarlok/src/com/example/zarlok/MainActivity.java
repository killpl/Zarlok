package com.example.zarlok;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.view.Menu;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.Spinner;

public class MainActivity extends Activity {

	
	/**
	 * Funkcja onCreate
	 * Komentarz testowy do Doxygen
	 * Test, test, test.
	 */
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        
		Spinner spinner = (Spinner) findViewById(R.id.spinner_categories);
		ArrayAdapter<CharSequence> adapter = ArrayAdapter.createFromResource(this, R.array.categories_array, android.R.layout.simple_spinner_item);
		adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
		spinner.setAdapter(adapter);
		
		Button button = (Button) findViewById(R.id.history);
		button.setOnClickListener(new OnClickListener() {
			
			@Override
			public void onClick(View view) {

		        // Komentarz
		        Intent myIntent = new Intent(view.getContext(), HistoryActivity.class);
		        startActivityForResult(myIntent, 0);
			}
		});
    }

    /**
	 * Funkcja onCreateOptionsMenu
	 * Test, test, test.
	 */
    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.main, menu);
        return true;
    }
    
}
