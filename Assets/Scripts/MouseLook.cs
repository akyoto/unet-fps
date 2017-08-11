using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour {
	public float sensitivity;

	private float rotationX;
	private float rotationY;
	
	// Update
	void Update() {
		if(!CursorLock.instance.IsLocked())
			return;
		
		rotationX += Input.GetAxisRaw("Mouse X") * sensitivity;
		rotationY += Input.GetAxisRaw("Mouse Y") * sensitivity;

		rotationY = Mathf.Clamp(rotationY, -90, 90);

		transform.rotation = Quaternion.AngleAxis(rotationX, Vector3.up) * Quaternion.AngleAxis(-rotationY, Vector3.right);
	}
}
