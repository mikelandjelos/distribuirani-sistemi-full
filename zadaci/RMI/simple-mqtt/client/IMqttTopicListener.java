package client;


import java.rmi.Remote;
import java.rmi.RemoteException;

/**
 * IMqttTopicListener - Callback interface, implemented on client-side.
 */
public interface IMqttTopicListener extends Remote {
    public void notify(String message) throws RemoteException;
}