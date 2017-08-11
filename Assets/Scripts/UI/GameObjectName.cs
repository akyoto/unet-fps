using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class GameObjectName : MonoBehaviour {
	public GameObject target;
	private Text text;

	// Start
	void Start() {
		text = GetComponent<Text>();
	}
	
	// Update
	void Update() {
		if(text.text != target.name) {
			text.text = target.name;
		}
	}
}
