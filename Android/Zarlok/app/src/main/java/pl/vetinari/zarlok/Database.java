package pl.vetinari.zarlok;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;
import java.util.Map.Entry;

/**
 * Created by Adam on 2014-05-11.
 */
public final class Database {

    private Map<Integer, String> categories = new HashMap<Integer, String>();
    private ArrayList<Food> food;
    private ArrayList<History> history = new ArrayList<History>();

    private ServerAPI api;


    private static volatile Database inst = null;

    private Database(){}

    private Food getFoodById(int id){
        Food f = null;

        for(Food item : food){
            if(item.id==id)
                f = item;
        }

        return f;
    }

    public static Database instance(){
        if (inst == null) {
            synchronized (Database.class) {
                if (inst == null) {
                    inst = new Database();
                }
            }
        }
        return inst;
    }

    public void setServer(String address){
        api = new ServerAPI(address);
    }

    public boolean refresh(){
        categories = api.getCategories();
        food = api.getFood();

        history.clear();

        ArrayList<History> hist =api.getHistory();
        if(hist!=null) {
            history.addAll(hist);
        }

        if(categories==null || food==null || history==null)
            return false;

        return true;
    }

    public History getHistoryItem(int position){
        History item = history.get(position);

        if(item!=null) {
            item.food = getFoodById(item.foodId);
            item.category = categories.get(item.food.categoryId);
        }
        return item;
    }

    public int getHistoryCount(){
        if(history==null) return 0;
        return history.size();
    }

    public ArrayList<String> getCategories(){
        ArrayList<String> list = new ArrayList<String>();
        list.addAll(categories.values());
        return list;
    }

    public Boolean login(String login, String password){
        return api.login(login, password);
    }

    public ArrayList<String> getFoodByCategory(String category){
        int id = -1;
        for(Entry<Integer, String> entry : categories.entrySet()){
            if(entry.getValue().equals(category))
                id = entry.getKey();
        }

        ArrayList<String> foodArray = new ArrayList<String>();
        for(Food f : food) {
            if (f.categoryId == id)
                foodArray.add(f.name);
        }

        return foodArray;
    }

    public Boolean save(String category, String foodName){

        for(Food f : food) {
            if (f.name.equals(foodName )&& categories.get(f.categoryId).equals(category)) {
                boolean result = api.save(f.id);

                history.clear();
                history.addAll(api.getHistory());

                return result;
            }
        }

        return false;
    }
}
