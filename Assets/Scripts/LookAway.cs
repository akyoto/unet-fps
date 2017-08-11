using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAway : MonoBehaviour {
	public Transform target;
	
	// Update
	void Update() {
		transform.rotation = Quaternion.LookRotation(transform.position - target.position);
	}
}
