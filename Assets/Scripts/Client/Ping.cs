using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using MessageType = System.Byte;

public class Ping : MonoBehaviour {
	public long interval;

	private float lastPacketTime = float.MinValue;

	// Start
	void Start() {
		RPCManager.instance.Register(this);
	}

	// FixedUpdate
	void FixedUpdate() {
		if(!Client.instance.isConnected)
			return;
		
		if(Time.time - lastPacketTime < interval)
			return;
		
		lastPacketTime = Time.time;
		
		var timeStamp = Utils.GetTimeStamp();

		var writer = new NetworkWriter();
		writer.Write((MessageType)ClientMessageType.Ping);
		writer.Write(timeStamp);

		Client.instance.SendToServer(writer.ToArray());
	}

	[NetworkCall(ServerMessageType.Ping)]
	public void ReceiveLatency(long latency) {
		Console.instance.Log("Ping: " + latency + " ms");
	}
}
