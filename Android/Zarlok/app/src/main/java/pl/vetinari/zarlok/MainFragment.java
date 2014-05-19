package pl.vetinari.zarlok;

import android.os.AsyncTask;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemSelectedListener;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.Spinner;
import android.widget.Toast;


/**
 * Activity for saving new eaten food
 *
 */
public class MainFragment extends android.support.v4.app.Fragment {

    Spinner foodSpinner;
    Spinner categoriesSpinner;

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {

        // Inflate the layout for this fragment
        View view = inflater.inflate(R.layout.fragment_main, container, false);

        categoriesSpinner = (Spinner)view.findViewById(R.id.spinner_categories);
        foodSpinner = (Spinner)view.findViewById(R.id.spinner_food);

        categoriesSpinner.setOnItemSelectedListener(new OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                String categoryName = (String)categoriesSpinner.getSelectedItem();

                // Set Food Spinner value
                ArrayAdapter<String> adapter = new ArrayAdapter<String>(view.getContext(), android.R.layout.simple_spinner_item,  Database.instance().getFoodByCategory(categoryName));
                adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
                foodSpinner.setAdapter(adapter);
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {
                foodSpinner.setAdapter(null);
            }
        });

        Button save = (Button)view.findViewById(R.id.button_save);
        save.setOnClickListener(new OnClickListener() {
            @Override
            public void onClick(View v) {
                new SaveTask().execute(v);
            }
        });

        return view;
    }

    public class SaveTask extends AsyncTask<View, Void, Boolean>{

        View view;

        @Override
        protected Boolean doInBackground(View... params) {
            view = params[0];
            return Database.instance().save(categoriesSpinner.getSelectedItem().toString(), foodSpinner.getSelectedItem().toString());
        }

        @Override
        protected void onPostExecute(final Boolean success) {
            String message;
            if(success){
                message = "Zapisano poprawnie :)";
            } else {
                message = "Błąd zapisu :(";
            }

            Toast.makeText(view.getContext(), message, Toast.LENGTH_LONG).show();
        }
    };


}
