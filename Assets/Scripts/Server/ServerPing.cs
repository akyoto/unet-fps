using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerPing : MonoBehaviour {
	// Start
	void Start() {
		RPCManager.instance.Register(this);
	}

	// Ping
	[NetworkCall(ClientMessage.Ping)]
	public void Ping(NetworkPlayer player, long timeStamp) {
		var ping = Utils.GetTimeStamp() - timeStamp;

		// Send ping back to client
		player.Send(ServerMessage.Ping, ping);
	}
}
