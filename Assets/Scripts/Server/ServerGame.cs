using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerGame : MonoBehaviour {
	// Start
	void Start() {
		RPCManager.instance.Register(this);
	}
	
	// Update
	[NetworkCall(ClientMessage.Position)]
	public void Position(NetworkTarget player, Vector3 position) {
		Debug.Log("Player " + player.connectionId + " position " + position);
	}
}
