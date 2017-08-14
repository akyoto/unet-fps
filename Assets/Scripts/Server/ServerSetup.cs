using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerSetup : MonoBehaviour {
	// Start
	void Start() {
		// Limit FPS
		Application.targetFrameRate = (int)(1f / Time.fixedDeltaTime);

		// No audio
		AudioListener.pause = true;
	}
}
