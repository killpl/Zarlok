/**
 * 
 */
package pl.vetinari.zarlok;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;

/**
 * @author Adam
 *
 */
public class ServerAPI {

    private ServerConnection connection;
    private String APIKey;

    public ServerAPI(String serverURL){
        connection = new ServerConnection(serverURL);
    }

    public boolean login(String login, String password){
        String resp = connection.request("/login/"+login+"/"+password+"/");
        if(resp.contentEquals("Error"))
            return false;

        APIKey = resp;
        return true;
    }

    public ArrayList<Food> getFood(){
        ArrayList<Food> food = new ArrayList<Food>();

        String resp = connection.request("/food/"+APIKey+"/");
        if(resp.contentEquals("Error"))
            return null;

        try {
            JSONObject json = new JSONObject(resp);
            JSONArray categoriesArray = json.getJSONArray("food");

            for(int i=0; i<categoriesArray.length(); i++){
                JSONObject obj = categoriesArray.getJSONObject(i);

                Food f = new Food();
                f.id = obj.getInt("id");
                f.name = obj.getString("name");
                f.categoryId = obj.getInt("category");

                food.add(f);
            }
        } catch (JSONException ex){
            return null;
        }
        return food;
    }

    public Map<Integer, String> getCategories(){
        Map<Integer, String> categories = new HashMap<Integer, String>();

        String resp = connection.request("/categories/"+APIKey+"/");

        if(resp.contentEquals("Error"))
            return null;

        try {
            JSONObject json = new JSONObject(resp);
            JSONArray categoriesArray = json.getJSONArray("categories");

            for(int i=0; i<categoriesArray.length(); i++){
                JSONObject obj = categoriesArray.getJSONObject(i);

                categories.put(obj.getInt("id"), obj.getString("name"));
            }
        } catch (JSONException ex){
            return null;
        }
        return categories;
    }

    public boolean save(Integer foodId){
        String resp = connection.request("/eat/"+APIKey+"/"+foodId.toString()+"/");

        if(resp.contentEquals("Error"))
            return false;

        return true;
    }

}
