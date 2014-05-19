package pl.vetinari.zarlok;

import android.app.Fragment;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ListView;


/**
 * A simple {@link Fragment} subclass.
 *
 */
public class HistoryFragment extends android.support.v4.app.Fragment {

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        View view =inflater.inflate(R.layout.fragment_history, container, false);

        ListView list = (ListView) view.findViewById(R.id.history_list);
        list.setAdapter(new HistoryAdapter(getActivity()));

        return view;
    }

}
