using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Shooter))]
public class ShooterController : MonoBehaviour {
	private Shooter shooter;

	// Start
	void Start() {
		shooter = GetComponent<Shooter>();
	}
	
	// Update
	void Update() {
		if(!CursorLock.instance.IsLocked())
			return;

		if(Input.GetMouseButton(0))
			shooter.Shoot();
	}
}
