package server;

import java.rmi.Remote;
import java.rmi.RemoteException;

public interface IOnPublishCallback extends Remote {
    void onPublish(String message) throws RemoteException;
}