using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using MessageType = System.Byte;

public class NetworkPlayer {
	public int connectionId;

	// Constructor
	public NetworkPlayer(int connectionId) {
		this.connectionId = connectionId;
	}

	// Send
	void Send(byte[] message) {
		Server.instance.Send(message, connectionId);
	}

	// Send
	public void Send(ClientMessageType messageType, params object[] arguments) {
		var writer = new NetworkWriter();
		writer.Write((MessageType) messageType);
		
		for(var i = 0; i < arguments.Length; i++) {
			RPCManager.Write(writer, arguments[i]);
		}

		Send(writer.ToArray());
	}

	// Send
	public void Send(ServerMessageType messageType, params object[] arguments) {
		var writer = new NetworkWriter();
		writer.Write((MessageType) messageType);
		
		for(var i = 0; i < arguments.Length; i++) {
			RPCManager.Write(writer, arguments[i]);
		}

		Send(writer.ToArray());
	}
}
