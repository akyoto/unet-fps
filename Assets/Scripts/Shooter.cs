using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour {
	public Camera cam;
	public GameObject lineEffect;
	public Transform gunShotStart;
	public float fireRate;
	private float lastShot;
	private RaycastHit hit;
	
	// Shoot
	public void Shoot() {
		if(Time.time - lastShot < fireRate)
			return;
		
		lastShot = Time.time;

		// Bullet trajectory
		var lineObject = GameObject.Instantiate(lineEffect, Vector3.zero, Quaternion.identity);
		var lineRenderer = lineObject.GetComponent<LineRenderer>();

		lineRenderer.SetPosition(0, gunShotStart.position);

		if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit)) {
			Console.instance.Log("Hit " + hit.collider.gameObject.name);

			lineRenderer.SetPosition(1, hit.point);

			var health = hit.collider.gameObject.GetComponent<Health>();

			if(health == null)
				return;
			
			health.TakeDamage(10);
		} else {
			Console.instance.Log("Hit nothing");

			lineRenderer.SetPosition(1, cam.transform.position + cam.transform.forward * 1000f);
		}
	}
}
