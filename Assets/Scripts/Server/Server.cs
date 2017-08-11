using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class Server : SingletonMonoBehaviour<Server> {
	public int maxConnections;
	public int port;
	public ConnectionConfig config;

	// Connect event
	public delegate void ConnectHandler(int connectionId);
	public event ConnectHandler onConnect;

	// Disconnect event
	public delegate void DisconnectHandler(int connectionId);
	public event DisconnectHandler onDisconnect;

	private int hostId;
	private int reliableChannel;
	private int unreliableChannel;

	private bool isStarted;

	private const int bufferSize = 1024;
	private byte[] receiveBuffer;
	private byte error;

	// Start
	void Start() {
		NetworkTransport.Init();

		reliableChannel = config.AddChannel(QosType.Reliable);
		unreliableChannel = config.AddChannel(QosType.Unreliable);

		var hostTopology = new HostTopology(config, maxConnections);

		hostId = NetworkTransport.AddHost(hostTopology, port);

		receiveBuffer = new byte[bufferSize];

		onConnect += (connectionId) => {
			Debug.Log("Player " + connectionId + " connected.");
		};

		onDisconnect += (connectionId) => {
			Debug.Log("Player " + connectionId + " disconnected.");
		};

		isStarted = true;
	}

	// Update
	void Update() {
		if(!isStarted)
			return;
		
		int recHostId;
		int connectionId;
		int channelId;
		int dataSize;

		NetworkEventType recData = NetworkTransport.Receive(out recHostId, out connectionId, out channelId, receiveBuffer, bufferSize, out dataSize, out error);

		switch(recData) {
			case NetworkEventType.Nothing:
				break;

			case NetworkEventType.ConnectEvent:
				onConnect(connectionId);
				break;

			case NetworkEventType.DataEvent:
				string msg = Encoding.Unicode.GetString(receiveBuffer, 0, dataSize);
				Debug.Log(msg);
				break;

			case NetworkEventType.DisconnectEvent:
				onDisconnect(connectionId);
				break;
		}
	}
}
