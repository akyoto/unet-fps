using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	public float moveSpeed;
	public float dashForce;
	public float jumpForce;

	public Vector3 moveVector {
		get;
		set;
	}

	private Rigidbody rigidBody;

	// Start
	void Start() {
		rigidBody = GetComponent<Rigidbody>();
	}

	// Jump
	public void Jump() {
		rigidBody.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
	}

	// Dash
	public void Dash() {
		rigidBody.AddForce(moveVector * dashForce, ForceMode.Impulse);
	}
	
	// Update
	void FixedUpdate() {
		if(moveVector == Vector3.zero)
			return;

		rigidBody.MovePosition(transform.position + moveVector * moveSpeed * Time.fixedDeltaTime);
	}
}
