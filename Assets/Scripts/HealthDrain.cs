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
		percent = World.Instance.WorldHealth;

		if (percent <= 0) EndGame();
		else if (percent < 25) img.color = Color.red;
		else if (percent < 50) img.color = Color.yellow;

		img.GetComponent<RectTransform>().sizeDelta = new Vector2(healthBarStartSize.x * World.Instance.WorldHealth / 100, healthBarStartSize.y);
	}

	private void EndGame() {
		GameObject menu = GameObject.Find("Main Camera");
		MenuOpen MO = menu.GetComponent<MenuOpen>();
		MO.EndGame();
	}
}
