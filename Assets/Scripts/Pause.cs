using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour {
	public bool isPaused;
	public Canvas Menu;
	// Use this for initialization
	void Start () {
		Time.timeScale = 1.0f;
		Menu.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("escape") && !isPaused)
		{ 
			print("Paused"); 
			Time.timeScale = 0.0f; 
			isPaused = true;
			Menu.enabled = true;
		} 
		else if(Input.GetKeyDown("escape") && isPaused) 
		{ 
			print("Unpaused"); 
			Time.timeScale = 1.0f; 
			isPaused = false; 
			Menu.enabled = false;
		}
	}
}
