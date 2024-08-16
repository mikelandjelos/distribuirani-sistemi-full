
package SimpleEchoServer;

import java.net.MalformedURLException;
import java.rmi.Naming;
import java.rmi.RemoteException;
import java.rmi.registry.LocateRegistry;

public class Server {

    public static void main(String[] args) {
        try {
            LocateRegistry.createRegistry(1099);
            IEchoService echoService = new EchoService();
            Naming.rebind("rmi://localhost:1099/echoService", echoService);
        } catch (RemoteException e) {
            e.printStackTrace();
        } catch (MalformedURLException e) {
            e.printStackTrace();
        }

        System.out.println("Server running on port 1099");
    }
}