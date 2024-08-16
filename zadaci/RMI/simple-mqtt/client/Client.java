package client;

import java.net.MalformedURLException;
import java.rmi.Naming;
import java.rmi.NotBoundException;
import java.rmi.RemoteException;
import java.rmi.server.UnicastRemoteObject;
import java.util.Scanner;

import server.IMqttBroker;

public class Client {
    public static void main(String[] args) {
        Scanner console = new Scanner(System.in);

        try {
            var mqttBroker = (IMqttBroker) Naming.lookup("rmi://localhost:1099/mqttBroker");
            
            while (true) {
                System.out.println(
                    "Welcome! What do you want to do?\n" + 
                    "\t[1] - Subscribe to a topic.\n" + 
                    "\t[2] - Publish a message.");
                String choice = console.nextLine();
                String topic;
                switch (choice) {
                    case "1":
                        System.out.print("Enter desired topic: ");
                        topic = console.nextLine();
                        IMqttTopicListener listener = new MqttTopicListener();
                        mqttBroker.subscribe(topic, listener);        
                        System.out.println("Successful subscription to topic: `" + topic + "`!");
                        break;
                    case "2":
                        System.out.print("Enter topic to publish to: ");
                        topic = console.nextLine();
                        System.out.print("Enter a message: ");
                        String message = console.nextLine();
                        mqttBroker.publish(message, topic);
                        break;
                    default:
                        System.out.println("Unknown option! Try again.");
                        break;
                }
            }
        } catch (MalformedURLException | RemoteException | NotBoundException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        }
        
        console.close();
    }    

    public static class MqttTopicListener extends UnicastRemoteObject implements IMqttTopicListener {
        public MqttTopicListener() throws RemoteException {
            super();
        }

        @Override
        public void notify(String message) throws RemoteException {
            System.out.println("Received message: `" + message + "`");
        }
    }
}