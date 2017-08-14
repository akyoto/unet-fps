using System;
using MessageType = System.Byte;

[AttributeUsage(AttributeTargets.All)]
public class NetworkCallAttribute : System.Attribute {
	public MessageType msgType;

	// Constructor
	public NetworkCallAttribute(ClientMessageType msgType) {
		this.msgType = (MessageType) msgType;
	}

	// Constructor
	public NetworkCallAttribute(ServerMessageType msgType) {
		this.msgType = (MessageType) msgType;
	}
}