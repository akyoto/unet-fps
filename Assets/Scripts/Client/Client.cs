using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Client : SingletonMonoBehaviour<Client> {
	public int maxConnections;
	public string address;
	public int port;
	public ConnectionConfig config;

	private int hostId;
	private int connectionId;
	private int reliableChannel;
	private int unreliableChannel;
	private byte error;

	// Start
	void Start() {
		Console.instance.Log("Connecting to server...");

		NetworkTransport.Init();

		reliableChannel = config.AddChannel(QosType.Reliable);
		unreliableChannel = config.AddChannel(QosType.Unreliable);

		var hostTopology = new HostTopology(config, maxConnections);

		hostId = NetworkTransport.AddHost(hostTopology, 0);
		connectionId = NetworkTransport.Connect(hostId, address, port, 0, out error);

		if((NetworkError)error != NetworkError.Ok) {
			Console.instance.Log("Connection failed: " + (NetworkError)error);
		}
		
		Console.instance.Log("Connected to server.");
	}
}
