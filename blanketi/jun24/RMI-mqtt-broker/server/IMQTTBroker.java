package server;

import java.rmi.Remote;
import java.rmi.RemoteException;

public interface IMQTTBroker extends Remote {
    void subscribe(String topic, IOnPublishCallback callback) throws RemoteException;

    void unsubscribe(String topic, IOnPublishCallback callback) throws RemoteException;

    void publish(String message, String topic) throws RemoteException;
}
