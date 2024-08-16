package server;


import java.rmi.Remote;
import java.rmi.RemoteException;

import client.IMqttTopicListener;

public interface IMqttBroker extends Remote {
    public void subscribe(String topic, IMqttTopicListener listener) throws RemoteException;
    public void publish(String message, String topic) throws RemoteException;
}