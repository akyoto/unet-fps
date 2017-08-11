using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerController : MonoBehaviour {
	private Player player;
	private Vector3 inputVector;

	// Start
	void Start() {
		player = GetComponent<Player>();
	}
	
	// Update
	void Update() {
		if(!CursorLock.instance.IsLocked())
			return;
		
		inputVector.x = 0f;
		inputVector.z = 0f;

		if(Input.GetKey(KeyCode.W)) {
			inputVector.z += 1f;
		}

		if(Input.GetKey(KeyCode.S)) {
			inputVector.z -= 1f;
		}

		if(Input.GetKey(KeyCode.A)) {
			inputVector.x -= 1f;
		}

		if(Input.GetKey(KeyCode.D)) {
			inputVector.x += 1f;
		}

		if(Input.GetKey(KeyCode.Space)) {
			player.Jump();
		}

		player.moveVector = Quaternion.AngleAxis(Camera.main.transform.rotation.eulerAngles.y, Vector3.up) * inputVector;
		player.moveVector.Normalize();
	}
}
