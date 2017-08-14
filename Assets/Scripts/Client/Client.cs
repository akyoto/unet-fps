using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using MessageType = System.Byte;

public class Client : SingletonMonoBehaviour<Client> {
	public int maxConnections;
	public string address;
	public int port;
	public ConnectionConfig config;

	// Connect event
	public delegate void ConnectHandler();
	public event ConnectHandler onConnect;

	// NetworkTransport
	private int hostId;
	private int connectionId;
	private int reliableChannel;
	private int unreliableChannel;

	private const int bufferSize = 1024;
	private byte[] receiveBuffer;
	private byte error;

	private bool isStarted;

	[NonSerialized]
	public bool isConnected;

	// Start
	void Start() {
		Console.instance.Log("Connecting to server...");

		NetworkTransport.Init();
		RPCManager.instance.Register(this);

		reliableChannel = config.AddChannel(QosType.Reliable);
		unreliableChannel = config.AddChannel(QosType.Unreliable);
		receiveBuffer = new byte[bufferSize];

		var hostTopology = new HostTopology(config, maxConnections);

		hostId = NetworkTransport.AddHost(hostTopology, 0);
		connectionId = NetworkTransport.Connect(hostId, address, port, 0, out error);

		if((NetworkError)error != NetworkError.Ok) {
			Console.instance.Log("Connection failed: " + (NetworkError)error);
		}

		onConnect += () => {
			isConnected = true;
			Console.instance.Log("Connected to server.");
		};

		isStarted = true;
	}

	// Update
	void Update() {
		if(!isStarted)
			return;
		
		int receivingHostId;
		int connectionId;
		int channelId;
		int dataSize;

		NetworkEventType recData = NetworkTransport.Receive(out receivingHostId, out connectionId, out channelId, receiveBuffer, bufferSize, out dataSize, out error);

		if(error != 0) {
			Debug.Log("Network error: " + (NetworkError)error);
		}

		switch(recData) {
			case NetworkEventType.DataEvent:
				var buffer = receiveBuffer.Take(dataSize).ToArray();
				RPCManager.instance.HandleMessage(buffer, connectionId);
				break;
		}
	}

	// SendToServer
	public void SendToServer(byte[] buffer) {
		NetworkTransport.Send(hostId, connectionId, reliableChannel, buffer, buffer.Length, out error);
	}

	// Connected
	[NetworkCall(ServerMessageType.Connected)]
	public void Connected() {
		onConnect();
	}
}
