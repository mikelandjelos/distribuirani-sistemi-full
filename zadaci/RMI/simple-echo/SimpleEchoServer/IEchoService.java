package SimpleEchoServer;

import java.rmi.Remote;
import java.rmi.RemoteException;

public interface IEchoService extends Remote {
    public String Echo(String message) throws RemoteException;
}