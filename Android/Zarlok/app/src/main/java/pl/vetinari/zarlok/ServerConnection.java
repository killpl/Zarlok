/**
 * 
 */
package pl.vetinari.zarlok;

import org.apache.http.client.HttpClient;
import org.apache.http.client.ResponseHandler;
import org.apache.http.client.methods.HttpGet;
import org.apache.http.impl.client.BasicResponseHandler;
import org.apache.http.impl.client.DefaultHttpClient;

/**
 * @author Adam
 *
 */
public class ServerConnection {

    private String address;

    public ServerConnection(String serverAddress){
        address = serverAddress;
    }

    public String request(String url){
        HttpClient Client = new DefaultHttpClient();

        String response = "Error";
        try
        {
            HttpGet httpget = new HttpGet(address+url);
            ResponseHandler<String> responseHandler = new BasicResponseHandler();
            response = Client.execute(httpget, responseHandler);
        }
        catch(Exception ex)
        {
            ex.printStackTrace();
        }
        return response;
    }


}
