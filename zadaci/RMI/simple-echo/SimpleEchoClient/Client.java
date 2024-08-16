package SimpleEchoClient;

import java.net.MalformedURLException;
import java.rmi.Naming;
import java.rmi.NotBoundException;
import java.rmi.RemoteException;
import java.util.Scanner;

import SimpleEchoServer.IEchoService;

public class Client {
    public static void main(String[] args) {
        Scanner userInputReader = new Scanner(System.in);
        
        try {
            IEchoService echoService = (IEchoService) Naming.lookup("rmi://localhost:1099/echoService");

            String userMessage = "";

            while (!userMessage.equals("END"))
            {
                System.out.print("Enter the message you wish to echo: ");
                userMessage = userInputReader.nextLine();
                echoService.Echo(userMessage);
            }

        } catch (MalformedURLException | RemoteException | NotBoundException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        }
        finally {
            userInputReader.close();
        }
    }    
}