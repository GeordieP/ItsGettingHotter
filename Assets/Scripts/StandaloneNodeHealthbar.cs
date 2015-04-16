using UnityEngine;
using System.Collections;

public class StandaloneNodeHealthbar : MonoBehaviour {

	public GameObject BarFill;		// The colored, "positive" part of the health bar
	private Vector2 healthBarStartSize;
	Node target;

	void Start () {
		//healthBarStartSize = BarFill.GetComponent<RectTransform>().sizeDelta;
		//print("startsize: " + healthBarStartSize);
	}

	public void Disable() {
		this.gameObject.SetActive(false);
	}
	public void Enable() {
		this.gameObject.SetActive(true);
	}

	public void SetTarget(Node _target) {
		target = _target;
		this.transform.position = Camera.main.WorldToScreenPoint(target.GUISpawnLocation.position);
		healthBarStartSize = BarFill.GetComponent<RectTransform>().sizeDelta;
	}

	void Update () {
		this.transform.position = Camera.main.WorldToScreenPoint(target.GUISpawnLocation.position);	
	}

	public void UpdateHealthBar(float _percent) {
		//print("percent: " + _percent);
		BarFill.GetComponent<RectTransform>().sizeDelta = new Vector2(healthBarStartSize.x * _percent, healthBarStartSize.y);
	}
}
