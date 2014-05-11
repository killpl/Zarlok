package pl.vetinari.zarlok;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;

/**
 * Created by Adam on 2014-05-11.
 */
public final class Database {

    private Map<Integer, String> categories = new HashMap<Integer, String>();
    private ArrayList<Food> food;
    private ServerAPI api;

    private static volatile Database inst = null;

    private Database(){}

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

        if(categories==null || food==null)
            return false;

        return true;
    }

    public ArrayList<String> getCategories(){
        ArrayList<String> list = new ArrayList<String>();
        list.addAll(categories.values());
        return list;
    }

    public Boolean login(String login, String password){
        return api.login(login, password);
    }
}
