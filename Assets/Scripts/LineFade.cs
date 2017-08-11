using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LineFade : MonoBehaviour {
	public float duration;
	public Color startColor;
	public Color endColor;

	private float start;
	private float width;
	private LineRenderer lineRenderer;

	// Start
	void Start() {
		lineRenderer = GetComponent<LineRenderer>();
		lineRenderer.startColor = startColor;
		lineRenderer.endColor = endColor;
		start = Time.time;
		width = lineRenderer.widthMultiplier;
	}
	
	// Update
	void Update() {
		var timePassed = Time.time - start;
		var progress = timePassed / duration;
		var multiplier = 1f - progress;

		lineRenderer.startColor = new Color(startColor.r, startColor.g, startColor.b, startColor.a * multiplier);
		lineRenderer.endColor = new Color(endColor.r, endColor.g, endColor.b, endColor.a * multiplier);
		lineRenderer.widthMultiplier = width * multiplier;

		if(timePassed >= duration) {
			Destroy(this.gameObject);
		}
	}
}
