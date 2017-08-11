using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {
	public RawImage healthBar;
	public int maxHealth;
	private int health;

	public delegate void DeathHandler();
	public event DeathHandler onDeath;

	// Start
	void Start() {
		health = maxHealth;

		onDeath += () => {
			Console.instance.Log(this.gameObject.name + " died.");
			Destroy(this.gameObject);
		};
	}

	// TakeDamage
	public void TakeDamage(int damage) {
		// Can't take any more damage when dead
		if(health <= 0)
			return;

		// Apply damage
		health -= damage;

		// Dead?
		if(health <= 0) {
			health = 0;
			onDeath();
		}
	}
	
	// Update
	void Update() {
		healthBar.rectTransform.anchorMax = new Vector2(health / (float)maxHealth, 1f);
	}
}
