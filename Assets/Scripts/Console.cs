using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Console : SingletonMonoBehaviour<Console> {
	public int maxMessages;
	public GameObject messagePrefab;
	public Transform messageParent;
	public ScrollRect scrollRect;
	
	private GameObject[] messages;
	private int currentIndex;

	void Start() {
		currentIndex = 0;
		messages = new GameObject[maxMessages];

		for(int i = 0; i < maxMessages; i++) {
			messages[i] = NewMessage("", "");
			messages[i].SetActive(false);
		}
	}

	// Log
	public void Log(string message) {
		Log("[System]", message);
	}

	// Log
	public void Log(string channel, string message) {
		GameObject clone;

		if(currentIndex == maxMessages) {
			// Take first child
			clone = messageParent.GetChild(0).gameObject;

			// Move it to the last index
			clone.transform.SetAsLastSibling();
		} else {
			clone = messages[currentIndex];
			clone.SetActive(true);

			currentIndex++;
		}

		// Set message text
		var texts = clone.GetComponentsInChildren<Text>();
		texts[0].text = channel;
		texts[1].text = message;
	}

	// NewMessage
	GameObject NewMessage(string channel, string message) {
		var clone = GameObject.Instantiate(messagePrefab, messageParent);

		// Add message
		var texts = clone.GetComponentsInChildren<Text>();
		texts[0].text = channel;
		texts[1].text = message;

		return clone;
	}

	// Update
	void Update() {
		if(!CursorLock.instance.IsLocked())
			return;
		
		// Scroll to bottom
		scrollRect.normalizedPosition = Vector2.zero;
	}
}
