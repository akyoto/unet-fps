using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerSetup : MonoBehaviour {
	// Start
	void Start() {
		Application.targetFrameRate = (int)(1f / Time.fixedDeltaTime);
		Debug.Log("FPS: " + Application.targetFrameRate);
	}
}
