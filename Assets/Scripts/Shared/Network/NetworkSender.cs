using UnityEngine.Networking;
using MessageType = System.Byte;

public class NetworkSender {
	// SendBytes
	static void SendBytes(byte[] buffer, int connectionId) {
		if(Server.instance != null) {
			Server.instance.Send(buffer, connectionId);
		} else {
			Client.instance.Send(buffer, connectionId);
		}
	}

	// SendBytesUnreliable
	static void SendBytesUnreliable(byte[] buffer, int connectionId) {
		if(Server.instance != null) {
			Server.instance.SendUnreliable(buffer, connectionId);
		} else {
			Client.instance.SendUnreliable(buffer, connectionId);
		}
	}

	// Send
	public static void Send(int connectionId, MessageType messageType, params object[] arguments) {
		var writer = new NetworkWriter();
		writer.Write((MessageType) messageType);
		
		for(var i = 0; i < arguments.Length; i++) {
			Serialization.Write(writer, arguments[i]);
		}

		SendBytes(writer.ToArray(), connectionId);
	}

	// SendUnreliable
	public static void SendUnreliable(int connectionId, MessageType messageType, params object[] arguments) {
		var writer = new NetworkWriter();
		writer.Write((MessageType) messageType);
		
		for(var i = 0; i < arguments.Length; i++) {
			Serialization.Write(writer, arguments[i]);
		}

		SendBytesUnreliable(writer.ToArray(), connectionId);
	}
}