package server;


import java.rmi.RemoteException;
import java.rmi.server.UnicastRemoteObject;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import client.IMqttTopicListener;

public class MqttBroker extends UnicastRemoteObject implements IMqttBroker {
    Map<String, List<IMqttTopicListener>> topics;

    public MqttBroker() throws RemoteException {
        super();
        topics = new HashMap<>();
    }
    
    @Override
    synchronized public void subscribe(String topic, IMqttTopicListener listener) throws RemoteException {
        var listeners = topics.getOrDefault(topic, null);
        if (listeners == null)
        {
            listeners = new ArrayList<>();
            topics.put(topic, listeners);
        }
        listeners.add(listener);        
    }
    
    @Override
    synchronized public void publish(String message, String topic) throws RemoteException {
        var listeners = topics.getOrDefault(topic, null);
        if (listeners == null)
            return;
        for (IMqttTopicListener listener : listeners) {
            listener.notify(message);
        }
    }
}