using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using MessageType = System.Byte;

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

	// NetworkTransport
	private int hostId;
	private int reliableChannel;
	private int unreliableChannel;

	private const int bufferSize = 1024;
	private byte[] receiveBuffer;
	private byte error;

	private bool isStarted;

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

			var player = new NetworkPlayer(connectionId);
			NetworkPlayers.Add(player);

			// Send connected message
			player.Send(ServerMessage.Connected);
		};

		onDisconnect += (connectionId) => {
			Debug.Log("Player " + connectionId + " disconnected.");
			NetworkPlayers.Remove(NetworkPlayers.Find(connectionId));
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

		if(error != 0) {
			Debug.Log("Network error: " + (NetworkError)error);
		}

		switch(recData) {
			case NetworkEventType.DataEvent:
				var buffer = receiveBuffer.Take(dataSize).ToArray();
				RPCManager.instance.HandleMessage(buffer, connectionId);
				break;
				
			case NetworkEventType.ConnectEvent:
				onConnect(connectionId);
				break;

			case NetworkEventType.DisconnectEvent:
				onDisconnect(connectionId);
				break;
		}
	}

	// Send
	public void Send(byte[] buffer, int connectionId, int channelId) {
		NetworkTransport.Send(hostId, connectionId, channelId, buffer, buffer.Length, out error);
	}

	// Send
	public void Send(byte[] buffer, int connectionId) {
		Send(buffer, connectionId, reliableChannel);
	}

	// SendUnreliable
	public void SendUnreliable(byte[] buffer, int connectionId) {
		Send(buffer, connectionId, unreliableChannel);
	}
}
