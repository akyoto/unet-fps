using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using MessageType = System.Byte;

public class NetworkTarget {
	public int connectionId;

	// Constructor
	public NetworkTarget(int connectionId) {
		this.connectionId = connectionId;
	}

	// Send
	public void Send(ClientMessage messageType, params object[] arguments) {
		NetworkSender.Send(connectionId, (MessageType) messageType, arguments);
	}

	// Send
	public void Send(ServerMessage messageType, params object[] arguments) {
		NetworkSender.Send(connectionId, (MessageType) messageType, arguments);
	}

	// SendUnreliable
	public void SendUnreliable(ClientMessage messageType, params object[] arguments) {
		NetworkSender.SendUnreliable(connectionId, (MessageType) messageType, arguments);
	}

	// SendUnreliable
	public void SendUnreliable(ServerMessage messageType, params object[] arguments) {
		NetworkSender.SendUnreliable(connectionId, (MessageType) messageType, arguments);
	}
}
