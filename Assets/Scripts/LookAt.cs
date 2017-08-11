using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour {
	public Transform target;

	// Start
	void Start() {
		
	}
	
	// Update
	void Update() {
		transform.LookAt(target);
	}
}
