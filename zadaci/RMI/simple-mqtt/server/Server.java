package server;
import java.net.MalformedURLException;
import java.rmi.Naming;
import java.rmi.RemoteException;
import java.rmi.registry.LocateRegistry;
import java.util.logging.Logger;

public class Server {
    public static void main(String[] args) {
        try {
            LocateRegistry.createRegistry(1099);
            IMqttBroker mqttBroker = new MqttBroker();
            Naming.rebind("rmi://localhost:1099/mqttBroker", mqttBroker);
        } catch (RemoteException | MalformedURLException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        }
        Logger.getGlobal().info("Mqtt broker listening on port 1099");
    }
}