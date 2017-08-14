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
	public void Send(ClientMessage messageType, params object[] arguments) {
		Send((MessageType) messageType, arguments);
	}

	// Send
	public void Send(ServerMessage messageType, params object[] arguments) {
		Send((MessageType) messageType, arguments);
	}

	// Send
	public void Send(MessageType messageType, params object[] arguments) {
		var writer = new NetworkWriter();
		writer.Write((MessageType) messageType);
		
		for(var i = 0; i < arguments.Length; i++) {
			Serialization.Write(writer, arguments[i]);
		}

		Send(writer.ToArray());
	}
}
