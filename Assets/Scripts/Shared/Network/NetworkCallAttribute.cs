using System;
using MessageType = System.Byte;

[AttributeUsage(AttributeTargets.All)]
public class NetworkCallAttribute : System.Attribute {
	public MessageType msgType;

	// Constructor
	public NetworkCallAttribute(ClientMessage msgType) {
		this.msgType = (MessageType) msgType;
	}

	// Constructor
	public NetworkCallAttribute(ServerMessage msgType) {
		this.msgType = (MessageType) msgType;
	}
}