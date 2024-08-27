package client;

import java.net.MalformedURLException;
import java.rmi.Naming;
import java.rmi.NotBoundException;
import java.rmi.RemoteException;
import java.rmi.server.UnicastRemoteObject;
import java.util.HashMap;
import java.util.Scanner;
import java.util.function.Consumer;

import server.IMQTTBroker;
import server.IOnPublishCallback;

public class Client {
    private static HashMap<String, IOnPublishCallback> _subscriptions = new HashMap<>();

    public static void main(String[] args) {
        Scanner stdin = new Scanner(System.in);

        try {
            IMQTTBroker mqttBroker = (IMQTTBroker) Naming.lookup("rmi://localhost:1099/mqttBroker");

            boolean running = true;

            while (running) {
                System.out.println("Choose an option:\n" +
                        "\t[1] - Subscribe to topic.\n" +
                        "\t[2] - Publish to topic.\n" +
                        "\t[3] - Unsubscribe from topic.\n" +
                        "\t[4] - Exit.");

                int option = Integer.parseInt(stdin.nextLine());

                switch (option) {
                    case 1:
                        System.out.print("Enter a topic: ");
                        String topic = stdin.nextLine();

                        if (_subscriptions.containsKey(topic)) {
                            System.out.println("Already subscribed!");
                            break;
                        }

                        IOnPublishCallback callback = new OnPublishCallback((String message) -> {
                            System.out.println("Message received [`topic:`" + topic + "`] - \"" + message + "\"");
                        });

                        _subscriptions.put(topic, callback);

                        mqttBroker.subscribe(topic, callback);
                        break;
                    case 2:
                        System.out.print("Enter a message: ");
                        String message = stdin.nextLine();

                        System.out.print("Enter a topic: ");
                        topic = stdin.nextLine();

                        mqttBroker.publish(message, topic);
                        break;
                    case 3:
                        System.out.print("Enter a topic: ");
                        topic = stdin.nextLine();

                        if (!_subscriptions.containsKey(topic)) {
                            System.out.println("You're not subscribed!");
                            break;
                        }

                        callback = _subscriptions.get(topic);
                        mqttBroker.unsubscribe(topic, callback);
                        break;
                    case 4:
                        System.out.println("Exiting...");
                        running = false;
                        for (var kvTopicCallback : _subscriptions.entrySet())
                            mqttBroker.unsubscribe(kvTopicCallback.getKey(), kvTopicCallback.getValue());
                        break;
                    default:
                        System.out.println("Unknown option! Try again.");
                        break;
                }
            }
        } catch (MalformedURLException | RemoteException | NotBoundException e) {
            e.printStackTrace();
        }
    }

    public static class OnPublishCallback extends UnicastRemoteObject implements IOnPublishCallback {
        Consumer<String> callbackFunction;

        public OnPublishCallback(Consumer<String> callback) throws RemoteException {
            super();
            callbackFunction = callback;
        }

        @Override
        public void onPublish(String message) throws RemoteException {
            callbackFunction.accept(message);
        }
    }
}
