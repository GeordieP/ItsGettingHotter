using UnityEngine;
using System.Collections;

// Temp
using UnityEngine.UI;

// I really hate having this inherit from MonoBehaviour, but we need the update function, so keep it for now
public class World {
	private static World instance;
	public static World Instance {
		get {
			if (instance == null) instance = new World();
			return instance;
		}
	}

	private float worldHealth = Balance.WorldStartHealth;
	private float worldCoInfluence = Balance.WorldStartCoInfluence;
	private float worldChInfluence = Balance.WorldStartChInfluence;
	private float worldCO2 = Balance.WorldStartCo2;
	private float worldCH4 = Balance.WorldStartCh4;
	private float pollutionFactor;

	private Text debugText;

	public float WorldHealth {
		get { return worldHealth; }
		set {
			if (value > 0) worldHealth = value;
			else worldHealth = 0;
		}
	}

	public float WorldCH4 {
		get { return worldCH4; }
		set {
			if (value > 0) worldCH4 = value;
			else worldCH4 = 0;
		}
	}

	public float WorldCO2 {
		get { return worldCO2; }
		set {
			if (value > 0) worldCO2 = value;
			else worldCO2 = 0;
		}
	}

	private World() {
		// Initially update the pollution value
		UpdatePollution();
		debugText = GameObject.Find("DebugText").GetComponent<Text>();
	}

	public void Update() {
		UpdatePollution();
		UpdateHealth();
		DebugText(string.Format("worldhealth {0} | co2 {1} | ch4 {2}", Mathf.Round(worldHealth * 10.0f) / 10.0f, Mathf.Round(worldCO2 * 10.0f) / 10.0f, Mathf.Round(worldCH4 * 10.0f) / 10.0f));
	}

	private void DebugText(string output) {
		debugText.text = output;
	}

	public void UpdatePollution() {
		pollutionFactor = (worldCH4 * worldChInfluence) + (worldCO2 * worldCoInfluence);
	}

	public void UpdateHealth() {
		// TODO: Rename or change variable used to TimeLeftInMinutes?
		worldHealth -= pollutionFactor * Time.deltaTime;
	}	
}
