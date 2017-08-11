using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorLock : SingletonMonoBehaviour<CursorLock> {
	// Start
	void Start() {
		LockCursor();
	}

	// OnApplicationFocus
	void OnApplicationFocus(bool hasFocus) {
		if(hasFocus) {
			LockCursor();
		}
    }

	// LockCursor
	void LockCursor() {
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}
	
	// UnlockCursor
	void UnlockCursor() {
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}

	// IsLocked
	public bool IsLocked() {
		return Cursor.lockState != CursorLockMode.None;
	}

	// ToggleCursorLock
	void ToggleCursorLock() {
		if(IsLocked()) {
			UnlockCursor();
		} else {
			LockCursor();
		}
	}

	// Update
	void Update() {
		if(Input.GetKeyDown(KeyCode.LeftAlt)) {
			ToggleCursorLock();
		}
	}
}
