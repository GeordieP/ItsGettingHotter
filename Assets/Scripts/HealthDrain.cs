using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthDrain : MonoBehaviour {

	public float speed;
	Image img;

	private Vector2 healthBarStartSize;
	private float percent = 100f;
	// Use this for initialization
	void Start () {
		img = this.GetComponent<Image>();
		healthBarStartSize = img.GetComponent<RectTransform>().sizeDelta;
	}
	
	// Update is called once per frame
	void Update () {
		if (speed < 40f) speed *= 1.0004f;
		percent -= speed * Time.deltaTime;

		if (percent <= 0) EndGame();
		else if (percent < 25) img.color = Color.red;
		else if (percent < 50) img.color = Color.yellow;

		img.GetComponent<RectTransform>().sizeDelta = new Vector2(healthBarStartSize.x * percent / 100, healthBarStartSize.y);
	}

	public void UpdatePercentage(float _percent) {
		if (_percent > 0f && _percent < 100f) percent = _percent;
	}

	public void AddHealthPercentage(float _amtToAdd) {
		percent += _amtToAdd;

		if (percent > 100f) percent = 100f;
	}

	private void EndGame() {
		GameObject menu = GameObject.Find("Main Camera");
		MenuOpen MO = menu.GetComponent<MenuOpen>();
		MO.EndGame();
	}
}
