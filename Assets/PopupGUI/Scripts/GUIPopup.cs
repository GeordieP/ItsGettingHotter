using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIPopup : MonoBehaviour {

    public GameObject NodeNameText, HealthBar, WoodCountText, FoodCountText, OilCountText;
	private Node target;
	private Vector2 healthBarStartSize;

	void Start () {
		healthBarStartSize = HealthBar.GetComponent<RectTransform>().sizeDelta;
		Disable();
	}

	public void Disable() {
		this.gameObject.SetActive(false);
	}

	public void DetatchFromTarget() {
		target.DetachPopup();
	}

	public void Enable() {
		this.gameObject.SetActive(true);
	}

	public void SetTarget(Node _target) {
		// First, deatch from any node we're currently attached to
		if (target) target.DetachPopup();

		target = _target;
		this.transform.localScale = Vector3.one * 0.8f;

		_target.AttachPopup(this);

		Enable();
	}

	void Update() {
		if (target) {
			this.transform.position = Camera.main.WorldToScreenPoint(target.GUISpawnLocation.position);
		}
	}

	public void UpdateText(string _nodeName, int _woodCount, int _foodCount, int _oilCount) {
		NodeNameText.GetComponent<Text>().text = _nodeName.ToString();
		UpdateResourceText(_woodCount, _foodCount, _oilCount);
	}

	public void UpdateResourceText(int _woodCount, int _foodCount, int _oilCount) {
		WoodCountText.GetComponent<Text>().text = _woodCount.ToString();
		FoodCountText.GetComponent<Text>().text = _foodCount.ToString();
		OilCountText.GetComponent<Text>().text = _oilCount.ToString();
	}

	public void UpdateHealthBar(float _percent) {
		HealthBar.GetComponent<RectTransform>().sizeDelta = new Vector2(healthBarStartSize.x * _percent, healthBarStartSize.y);
	}
}
