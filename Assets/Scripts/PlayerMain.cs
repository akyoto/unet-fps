using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMain : MonoBehaviour {
	// Start
	void Start() {
		
	}
	
	// FixedUpdate
	void FixedUpdate() {
		if(!Client.instance.isConnected)
			return;
		
		Client.server.SendUnreliable(ClientMessage.Position, transform.position);
	}
}
