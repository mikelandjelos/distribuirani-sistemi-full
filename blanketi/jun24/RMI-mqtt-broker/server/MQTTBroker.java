package server;

import java.rmi.RemoteException;
import java.rmi.server.UnicastRemoteObject;
import java.util.ArrayList;
import java.util.HashMap;

public class MQTTBroker extends UnicastRemoteObject implements IMQTTBroker {
    private HashMap<String, ArrayList<IOnPublishCallback>> _topics = new HashMap<>();
    
    public MQTTBroker() throws RemoteException {
        super();
    }

    @Override
    synchronized public void subscribe(String topic, IOnPublishCallback callback) throws RemoteException {
        createTopic(topic);
        _topics.get(topic).add(callback);
    }

    @Override
    synchronized public void unsubscribe(String topic, IOnPublishCallback callback) throws RemoteException {
        if (!_topics.containsKey(topic))
            return;

        _topics.get(topic).remove(callback);
    }

    @Override
    synchronized public void publish(String message, String topic) throws RemoteException {
        if (!_topics.containsKey(topic))
            return;

        for (var callback : _topics.get(topic))
            callback.onPublish(message);
    }

    private void createTopic(String topic)
    {
        if (_topics.containsKey(topic))
            return;
        
        _topics.put(topic, new ArrayList<>());
    }
}
