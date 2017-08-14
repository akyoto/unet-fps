using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using MessageType = System.Byte;

public class RPCManager : SingletonMonoBehaviour<RPCManager> {
	private Dictionary<MessageType, NetworkCall> functions = new Dictionary<MessageType, NetworkCall>();

	// HandleMessage
	public void HandleMessage(byte[] buffer, int connectionId) {
		var reader = new NetworkReader(buffer);

		// Message type
		MessageType msgType = reader.ReadByte();

		NetworkCall call;
		bool exists = functions.TryGetValue(msgType, out call);

		if(!exists) {
			Debug.LogError("Unknown message type: " + msgType);
			return;
		}

		var methodParams = call.method.GetParameters();
		var invokeParams = new object[methodParams.Length];

		for(var i = 0; i < methodParams.Length; i++) {
			var param = methodParams[i];
			var paramType = param.ParameterType;

			if(paramType == typeof(NetworkTarget)) {
				invokeParams[i] = NetworkPlayers.Find(connectionId);
				continue;
			}

			invokeParams[i] = Serialization.Read(reader, paramType);
		}

		// Call the method
		call.method.Invoke(call.obj, invokeParams);
	}

	// Register
	public void Register<T>(T obj) {
		var methods = obj.GetType().GetMethods();

		foreach(var method in methods) {
			var attributes = method.GetCustomAttributes(false);

			foreach(var attr in attributes) {
				if(attr.ToString() == "NetworkCallAttribute") {
					var netCallAttr = attr as NetworkCallAttribute;

					functions.Add(netCallAttr.msgType, new NetworkCall {
						obj = obj,
						method = method
					});
				}
			}
		}
	}
}