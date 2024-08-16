
package SimpleEchoServer;

import java.rmi.RemoteException;
import java.rmi.server.UnicastRemoteObject;
import java.util.logging.Logger;

public class EchoService extends UnicastRemoteObject implements IEchoService {    
    public EchoService() throws RemoteException
    {
        super();
    }
    
    @Override
    public String Echo(String message) throws RemoteException {
        Logger.getGlobal().info("Echoing message: `" + message + "`");
        return message;
    }
}