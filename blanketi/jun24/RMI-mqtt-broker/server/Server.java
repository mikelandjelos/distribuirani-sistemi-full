package server;

import java.net.MalformedURLException;
import java.rmi.Naming;
import java.rmi.RemoteException;
import java.rmi.registry.LocateRegistry;

public class Server {
    public static void main(String[] args) {
        try {
            LocateRegistry.createRegistry(1099);
            IMQTTBroker mqttBroker = new MQTTBroker();
            Naming.rebind("rmi://localhost:1099/mqttBroker", mqttBroker);
            System.out.println("Server started running on port 1099...");
        } catch (RemoteException | MalformedURLException e) {
            e.printStackTrace();
        }
    }
}
