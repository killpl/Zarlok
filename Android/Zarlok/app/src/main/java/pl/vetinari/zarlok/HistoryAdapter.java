package pl.vetinari.zarlok;

import android.app.Activity;
import android.content.Context;
import android.graphics.Color;
import android.text.format.DateFormat;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.TextView;

/**
 * Created by Adam on 2014-05-12.
 */
public class HistoryAdapter extends BaseAdapter {

    private static LayoutInflater inflater=null;

    public HistoryAdapter(Activity activity) {
        inflater = (LayoutInflater)activity.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
    }
    @Override
    public int getCount() {
        return Database.instance().getHistoryCount();
    }

    @Override
    public Object getItem(int position) {
        return position;
    }

    @Override
    public long getItemId(int position) {
        return position;
    }

    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        View vi=convertView;
        if(convertView==null)
            vi = inflater.inflate(R.layout.list_row, null);

        TextView name = (TextView)vi.findViewById(R.id.name);
        TextView category = (TextView)vi.findViewById(R.id.category);
        TextView date = (TextView)vi.findViewById(R.id.date);

        History item = Database.instance().getHistoryItem(position);

        // Achievements
        if(item.foodId<0){
            name.setText("Zdobyłeś osiągnięcie: " + item.category);
            name.setTextSize(12);
            name.setTextColor(Color.BLUE);
            date.setText("");
        }
        else // History
        {
            name.setText(item.food.name);
            name.setTextColor(Color.BLACK);
            category.setText(item.category);
            date.setText(new DateFormat().format("hh:mm dd.MM.yyyy", item.date));
        }
        return vi;
    }

    public void update(){
        notifyDataSetChanged();
        notifyDataSetInvalidated();
    }
}
